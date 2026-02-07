using MassTransit;
using Shared.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Create a new order and publish an OrderCreatedEvent
app.MapPost("/orders", async (IPublishEndpoint publishEndpoint) =>
{
    var orderId = Guid.NewGuid();

    await publishEndpoint.Publish(new OrderCreatedEvent(
        orderId,
        "customer@email.com"
    ));

    return Results.Ok(new { OrderId = orderId });
});

app.Run();
