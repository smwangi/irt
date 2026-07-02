
using Asp.Versioning;
using Irt.Application;
using Irt.Infrastructure;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Serilog;
using MassTransit.DependencyInjection;
using Irt.Application.Datasets;
using Irt.Application.Datasource;
using Irt.Application.ReportingScopes;
using Irt.Infrastructure.Database.Postgres;
using IrtWeb.Configuration;
using IrtWeb.GraphQL;
using IrtWeb.GraphQL.IndicatorDefinitions;
using IrtWeb.GraphQL.ReportingScopes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddControllers()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count());
builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.UseProblemDetailsValidationResponse());

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddMvc();

builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);


builder.Services.AddHttpContextAccessor();

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
        options.HttpsPort = 5001;
    });
}

 // RegisterDbContext<ApplicationDbContext>(DbContextKind.Resolver) is important — it tells HotChocolate to resolve a fresh ApplicationDbContext per resolver invocation (correct for the [Service] ApplicationDbContext db parameter and avoids the "DbContext is not thread-safe" pitfall when GraphQL executes resolvers in parallel).
builder.Services
    .AddGraphQLServer()
    .RegisterDbContextFactory<ApplicationDbContext>()
    .AddQueryType()
    .AddMutationType()
    .AddTypeExtension<ReportingScopeMutations>()
    .AddTypeExtension<ReportingScopeQueries>()
    .AddTypeExtension<IndicatorDefinitionMutations>()
    .AddTypeExtension<IndicatorDefinitionQueries>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .ModifyRequestOptions(o => o.IncludeExceptionDetails = builder.Environment.IsDevelopment())
    .AddErrorFilter(error => GraphQlErrorFilter.OnError(error, builder.Environment.IsDevelopment()));

Log.Logger = new LoggerConfiguration()
    .Enrich.WithThreadId()
    .Enrich.WithMachineName()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog((context, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
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
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}
app.UseMiddleware<GlobalExceptionHandlerMiddleWare>();

app.UseSerilogRequestLogging(); // Must run before endpoint middleware so Path is logged

app.MapControllers();
app.MapGraphQL("/irt/v1/graphql");

app.MapHealthChecks("/health");

app.Run();
return;

public partial class Program {} // Marked partial to allow testing with WebApplicationFactory
