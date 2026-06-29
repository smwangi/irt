
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
using Irt.SharedKernel.ErrorHandling.MiddleWare;
using IrtWeb.Configuration;
using IrtWeb.GraphQL;
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

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 5001;
});

 // RegisterDbContext<ApplicationDbContext>(DbContextKind.Resolver) is important — it tells HotChocolate to resolve a fresh ApplicationDbContext per resolver invocation (correct for the [Service] ApplicationDbContext db parameter and avoids the "DbContext is not thread-safe" pitfall when GraphQL executes resolvers in parallel).
builder.Services
    .AddGraphQLServer()
    .RegisterDbContextFactory<ApplicationDbContext>()
    .AddQueryType()
    .AddObjectTypeExtension<Query>(_ => { })
    .AddProjections()
    .AddFiltering()
    .AddSorting();

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
app.MapGraphQL("/graphql");

app.UseSerilogRequestLogging(); // Logging incoming http requests

app.MapHealthChecks("/health");

app.Run();
return;

public partial class Program {} // Marked partial to allow testing with WebApplicationFactory
