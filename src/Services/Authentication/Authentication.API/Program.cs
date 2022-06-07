using Authentication.API.EventBusConsumer;
using Authentication.API.Filters;
using Authentication.API.Middleware;
using Authentication.Application;
using Authentication.Infrastructure;
using Authentication.Infrastructure.Persistence;
using EventBus.Messages.Common;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {

    config.AddConsumer<UserRegistrationConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(configuration["EventBusSettings:HostAddress"]);        

        cfg.ReceiveEndpoint(EventBusConstants.AuthenticationQueue, c => {
            c.ConfigureConsumer<UserRegistrationConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddScoped<UserRegistrationConsumer>();

builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHealthChecks()
        .AddDbContextCheck<MembershipDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
