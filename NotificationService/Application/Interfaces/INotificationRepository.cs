using NotificationService.Domain;

namespace NotificationService.Application.Interfaces;

/// <summary>
/// Repository interface for managing notifications. 
/// This interface defines the contract for adding new notifications 
/// and retrieving the most recent notifications.
/// </summary>
public interface INotificationRepository
{
    void Add(Notification notification);
    IEnumerable<Notification> GetLast(int count);
}
