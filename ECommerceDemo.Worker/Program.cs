using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Application.Abstractions.Services;
using ECommerceDemo.Infrastructure.Persistence.Helpers;
using ECommerceDemo.Infrastructure.Persistence.Repositories;
using ECommerceDemo.Worker;
using ECommerceDemo.Worker.Extensions;
using ECommerceDemo.Worker.Services;
using StackExchange.Redis;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<AuditFieldSetter>();
builder.Services.AddScoped<ICurrentUserService, FakeCurrentUserService>();
builder.Services.AddScoped(typeof(ICacheRepository), typeof(CacheRepository));
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]));
builder.Services.AddRabbitMqConsumers(builder.Configuration);

var host = builder.Build();
host.Run();
