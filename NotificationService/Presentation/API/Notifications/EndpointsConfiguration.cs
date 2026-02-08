namespace NotificationService.Presentation.API.Notifications;


public static class EndpointsConfiguration
{
    /// <summary>
    /// Configures notification-related HTTP endpoints for the specified web application.
    /// </summary>
    public static void MapNotificationsEndpoints(this WebApplication app)
    {
        app.MapGetNotificationsEndpoint();
    }
}
