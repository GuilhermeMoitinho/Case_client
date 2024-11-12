using CaseClient.Application.Commands;
using CaseClient.Application.Notifications;
using CaseClient.Application.Notifications.Abstractions;
using CaseClient.Application.Validations;
using CaseClient.Core.Data;
using CaseClient.Data;
using CaseClient.Data.CustomerContext;
using CaseClient.Data.Repositories;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Events.Handler;
using CaseClient.Domain.Events;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using CaseClient.Infrastructure.EventHandlers;
using MediatR;

namespace CaseClient.WebAPI.Configuration;

internal static class DependencyInjectionConfig
{
    internal static WebApplicationBuilder AddDependency(this WebApplicationBuilder builder)
    {
        #region Config AppDbContext (PostgreSQL)
        var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                               $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                               $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                               $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
                               $"Database={Environment.GetEnvironmentVariable("DB_NAME")}" ?? builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<PostgreSqlDbContext>(x => x.UseNpgsql(connectionString));
        #endregion

        #region Config MongoDbContext
        var mongoHost = Environment.GetEnvironmentVariable("MONGO_HOST");
        var mongoPort = Environment.GetEnvironmentVariable("MONGO_PORT");
        var mongoUser = Environment.GetEnvironmentVariable("MONGO_USER");
        var mongoPassword = Environment.GetEnvironmentVariable("MONGO_PASSWORD");
        var mongoDbName = Environment.GetEnvironmentVariable("MONGO_DB");

        var mongoConnectionString = $"mongodb://{mongoUser}:{mongoPassword}@{mongoHost}:{mongoPort}/{mongoDbName}?authSource=admin";

        builder.Services.AddSingleton<MongoDbContext>(sp =>
            new MongoDbContext(mongoConnectionString, mongoDbName));
        #endregion

        #region Config Dependency Injection
        builder.Services.AddScoped<INotifier, Notifier>();

        builder.Services.AddTransient<ICustomerReadRepository, CustomerReadRepository>();
        builder.Services.AddTransient<ICustomerWriteRepository, CustomerWriteRepository>();

        builder.Services.AddScoped<IUnitOfWork, UnityOfWork>();
        #endregion

        #region Events Dependency Injection
        builder.Services.AddScoped<INotificationHandler<CustomerCreatedEvent>, CustomerCreatedEventHandler>();
        builder.Services.AddScoped<INotificationHandler<CustomerDeletedEvent>, CustomerDeletedEventHandler>();
        builder.Services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerUpdatedEventHandler>();
        #endregion

        #region Config Fluent Validation
        builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
        builder.Services.AddFluentValidationAutoValidation();
        #endregion

        #region Config MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));
        #endregion

        #region Config Caching
        builder.Services.AddMemoryCache();
        #endregion

        #region Config Cors
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Production", op =>
            {
                op.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });
        #endregion

        #region Config Logging
        builder.Logging.ClearProviders();

        builder.Logging.AddConfiguration(builder.Configuration);

        builder.Logging.AddJsonConsole();
        #endregion

        #region Config Health Checks
        builder.Services.AddHealthChecks();
        #endregion


        return builder;
    }
}
