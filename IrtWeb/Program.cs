
using Asp.Versioning;
using Irt.Application;
using Irt.Application.Datasources;
using Irt.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Serilog;
using MassTransit.DependencyInjection;
using Irt.Application.Datasets;
using Irt.SharedKernel.ErrorHandling.MiddleWare;
using IrtWeb.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);


builder.Services.AddHttpContextAccessor();

IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EnableLowerCamelCase();
    odataBuilder.EntitySet<DatasourceDto>("Datasource").EntityType.HasKey(x => x.Id);
    odataBuilder.EntitySet<DatasetDto>("Datasets").EntityType.HasKey(x => x.Id);
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

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 5001;
});

Log.Logger = new LoggerConfiguration()
    .Enrich.WithThreadId()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

builder.Host.UseSerilog((context, configuration)
=> configuration.ReadFrom.Configuration(context.Configuration)
.Enrich.FromLogContext()
.WriteTo.Console());

// health checks
builder.Services.AddHealthChecks();

builder.Services.AddScoped(typeof(IScopedBusContextProvider<>), typeof(ScopedBusContextProvider<>));

var app = builder.Build();
//var kafkaConsumerService = app.Services.GetRequiredService<IHostedService>();
//await kafkaConsumerService.StartAsync(CancellationToken.None);
//await Task.Delay(Timeout.Infinite);
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandlerMiddleWare>();
app.UseMiddleware<ResultErrorHandlingMiddleWare>();

app.MapControllers();

app.UseSerilogRequestLogging(); // Logging incoming http requests

app.MapHealthChecks("/health");

app.Run();

public partial class Program {} // Marked partial to allow testing with WebApplicationFactory