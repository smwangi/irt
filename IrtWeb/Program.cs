
using Asp.Versioning;
using Irt.Application;
using Irt.Application.Configuration.ApiResponse;
using Irt.Application.Datasources;
using Irt.Infrastructure;
using IrtWeb.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
//builder.Services.AddControllers();

builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);


builder.Services.AddHttpContextAccessor();

IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EnableLowerCamelCase();
    odataBuilder.EntitySet<DatasourceDto>("Datasources").EntityType.HasKey(x => x.Id);
    return odataBuilder.GetEdmModel();
}

builder.Services
.AddControllers()
.AddOData(opt => opt
    .Select()
    .Filter()
    .OrderBy()
    .Count()
    .SetMaxTop(100)
    .AddRouteComponents("irt/api/v{version:apiVersion}", GetEdmModel()));

builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.ApiVersionReader = ApiVersionReader.Combine(
        new HeaderApiVersionReader("api-version"),
        new QueryStringApiVersionReader("api-version"),
        new UrlSegmentApiVersionReader());
}).AddMvc()
.AddApiExplorer(opt =>
{
    opt.GroupNameFormat = "'v'VVV";
    opt.SubstituteApiVersionInUrl = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
});

Log.Logger = new LoggerConfiguration()
    .Enrich.WithThreadId()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
//builder.Host.UseSerilog();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        /*var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        return new BadRequestObjectResult(new { Errors = errors });*/
        var errors = context.ModelState
            .Where(ms => ms.Value.Errors.Count > 0)
            .ToDictionary(
                ms => ms.Key,
                ms => ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var response = ApiResponse<object>.Error(new List<ApiError> { new ApiError("Validation error") { Code = "400" } });

        return new BadRequestObjectResult(response);
    };
});

builder.Host.UseSerilog((context, configuration)
=> configuration.ReadFrom.Configuration(context.Configuration)
.Enrich.FromLogContext()
.WriteTo.Console());

// health checks
builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseMiddleware<ErrorHandlingMiddleware>();


app.UseSerilogRequestLogging(); // Logging incoming http requests

app.MapHealthChecks("/health");

app.Run();

public partial class Program {} // Marked partial to allow testing with WebApplicationFactory