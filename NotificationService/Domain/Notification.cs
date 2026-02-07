namespace NotificationService.Domain;

/// <summary>
/// Represents a notification that is sent to a customer.
/// </summary>
/// <param name="Id">The unique identifier of the notification.</param>
/// <param name="Email">The email address of the customer who will receive the notification.</param>
/// <param name="Message">The message content of the notification.</param>
/// <param name="CreatedAt">The date and time when the notification was created.</param>
public record Notification(Guid Id, string Email, string Message, DateTime CreatedAt);
