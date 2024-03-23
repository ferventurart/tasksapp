using System.Reflection;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Web.Api.Common.Abstractions.Behavior;
using Web.Api.Common.Persistence;
using Web.Api.Extensions;
using Web.Api.Host;

var appAssembly = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);
const string policyName = "SpaPolicy";
// Common
builder.Services.AddEfCore();

// Host
builder.Services.AddMediatR(configure =>
{
    configure.RegisterServicesFromAssemblyContaining<Program>();
    configure.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo
{
    Description = "With this api you could manage your daily tasks.",
    Title = "Task Api",
    Version = "v1",
    Contact = new OpenApiContact
    {
        Email = "ferventurart@gmail.com",
        Name = "Fernando Ventura",
        Url = new Uri("https://github.com/ferventurart")
    }
}));
builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
    options.AddPolicy(policyName,
        policy => policy
            .WithOrigins(builder.Configuration.GetValue<string>("ClientUrl")!)
    ));

builder.Services.ConfigureFeatures(builder.Configuration, appAssembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplySeeds();
}

app.UseHttpsRedirection();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseCors(policyName);

app.UseExceptionHandler();

app.RegisterEndpoints(appAssembly);

app.Run();