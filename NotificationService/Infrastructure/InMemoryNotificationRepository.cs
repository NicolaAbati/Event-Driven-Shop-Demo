using NotificationService.Application.Interfaces;
using NotificationService.Domain;

namespace NotificationService.Infrastructure;

/// <summary>
/// In-memory implementation of the INotificationRepository interface.
/// </summary>
public class InMemoryNotificationRepository : INotificationRepository
{
    /// <summary>
    /// A list to store notifications in memory. 
    /// In a real application, this would be replaced with a database or other persistent storage.
    /// </summary>
    private readonly List<Notification> _notifications = [];

    /// <summary>
    /// Adds a new notification to the in-memory list.
    /// </summary>
    /// <param name="notification"></param>
    public void Add(Notification notification) => _notifications.Add(notification);

    /// <summary>
    /// Retrieves the most recent notifications, ordered by creation date in descending order.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public IEnumerable<Notification> GetLast(int count)
        => _notifications.OrderByDescending(n => n.CreatedAt).Take(count);
}
