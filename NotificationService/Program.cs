using MassTransit;
using NotificationService.Application.Interfaces;
using NotificationService.Infrastructure;
using NotificationService.Presentation.API.Notifications;
using NotificationService.Presentation.Consumers.Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

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

// CORS Configuration to allow requests from the API Gateway
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5000") // App Gateway
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors();
app.MapNotificationsEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
