namespace NotificationService.Presentation.Consumers.Orders;

using MassTransit;
using NotificationService.Application.Interfaces;
using NotificationService.Domain;
using Shared.Contracts;

/// <summary>
/// Consumer that listens for OrderCreatedEvent messages.
/// </summary>
/// <param name="repository">The repository used to store notifications.</param>
public class OrderCreatedConsumer(INotificationRepository repository) : IConsumer<OrderCreatedEvent>
{
    /// <summary>
    /// Handles the consumption of an OrderCreatedEvent message.
    /// </summary>
    /// <param name="context">The context of the consumed message.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var notification = new Notification(
            Guid.NewGuid(),
            context.Message.CustomerEmail,
            "Thanks for your order!",
            DateTime.UtcNow
        );
        repository.Add(notification);

        Console.WriteLine($"[NotificationService] Notification sent to {notification.Email}.");
        return Task.CompletedTask;
    }
}