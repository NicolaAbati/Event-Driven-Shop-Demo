using MassTransit;
using NotificationService.Application.Interfaces;
using NotificationService.Infrastructure;
using NotificationService.Presentation.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<INotificationRepository, InMemoryNotificationRepository>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h => { h.Username("guest"); h.Password("guest"); });
        cfg.ReceiveEndpoint("notification-service", e => e.ConfigureConsumer<OrderCreatedConsumer>(context));
    });
});

var app = builder.Build();

app.MapGet("/notifications", (INotificationRepository repo) =>
{
    return repo.GetLast(10);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();
