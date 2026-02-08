using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Interfaces;

namespace NotificationService.Presentation.API.Notifications;

public static class GetNotificationsEndpoint
{
    /// <summary>
    /// Maps the GET /notifications endpoint to retrieve the most recent notifications from the repository.
    /// </summary>
    /// <param name="app">The web application instance to which the notifications endpoint will be added.</param>
    public static void MapGetNotificationsEndpoint(this WebApplication app)
    {
        app.MapGet("/notifications", (INotificationRepository repo, [FromQuery] int count) =>
        {
            return repo.GetLast(count);
        });
    }
}
