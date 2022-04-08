using System.Reflection;
using Microsoft.AspNetCore.WebSockets;

namespace ContactMe.SocketsManager;

public static class SocketExtensions
{
    public static IServiceCollection AddWebSocketManager(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ConnectionManager>();
        foreach (var type in Assembly.GetEntryAssembly()!.ExportedTypes)
        {
            if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
            {
                serviceCollection.AddSingleton(type);
            }
        }

        return serviceCollection;
    }

    public static IApplicationBuilder MapSockets(this IApplicationBuilder app, PathString appPathString, SocketHandler? socketHandler)
    {
        return app.Map(appPathString,(x) => x.UseMiddleware<SocketMiddleware>(socketHandler));
    }
}