using ECommerceDemo.API.Endpoints;
using ECommerceDemo.API.Extensions.ServiceRegistration;
using ECommerceDemo.API.Middleware;
using ECommerceDemo.API.Services;
using ECommerceDemo.Application;
using ECommerceDemo.Application.Abstractions.Services;
using ECommerceDemo.Application.Behaviors;
using ECommerceDemo.Infrastructure.Extensions.ServiceRegistration;
using ECommerceDemo.Infrastructure.Persistence;
using ECommerceDemo.Infrastructure.Persistence.Helpers;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwagger();
builder.Services.AddHttpContextAccessor(); // IHttpContextAccessor için
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<AuditFieldSetter>();
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddMediatR(typeof(ApplicationAssembly).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(ApplicationAssembly));
builder.Services.AddAutoMapper(typeof(ApplicationAssembly));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddRabbitMq(builder.Configuration);

var app = builder.Build();

// Start Migrations
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ECommerceDemoDbContext>();
    db.Database.Migrate();
}

app.UseSerilogRequestLogging();

app.UseExceptionHandler(opt => { });
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapAuthEndpoints();
app.MapOrderEndpoints();

app.Run();
