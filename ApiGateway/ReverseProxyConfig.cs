namespace ApiGateway;

public static class ReverseProxyConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void ConfigureReverseProxy(this WebApplicationBuilder builder)
    {
        builder.Services.AddReverseProxy()
            .LoadFromMemory(
                [
                    new Yarp.ReverseProxy.Configuration.RouteConfig()
                    {
                        RouteId = "order_route",
                        ClusterId = "order_cluster",
                        Match = new Yarp.ReverseProxy.Configuration.RouteMatch
                        {
                            Path = "/orders/{**catchall}"
                        }
                    },
                    new Yarp.ReverseProxy.Configuration.RouteConfig()
                    {
                        RouteId = "notification_route",
                        ClusterId = "notification_cluster",
                        Match = new Yarp.ReverseProxy.Configuration.RouteMatch
                        {
                            Path = "/notifications/{**catchall}"
                        }
                    }
                ],
                [
                    new Yarp.ReverseProxy.Configuration.ClusterConfig()
                    {
                        ClusterId = "order_cluster",
                        Destinations = new Dictionary<string, Yarp.ReverseProxy.Configuration.DestinationConfig>
                        {
                            { "order", new() { Address = "http://ordersservice:8080/" } }
                        }
                    },
                    new Yarp.ReverseProxy.Configuration.ClusterConfig()
                    {
                        ClusterId = "notification_cluster",
                        Destinations = new Dictionary<string, Yarp.ReverseProxy.Configuration.DestinationConfig>
                        {
                            { "notification", new() { Address = "http://notificationservice:8080/" } }
                        }
                    }
                ]
            );
    }
}
