using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ContactMe.SocketsManager;

public class SocketMiddleware
{
    private readonly RequestDelegate _next;
    private SocketHandler SocketHandler { get; set; }

    public SocketMiddleware(RequestDelegate next, SocketHandler socketHandler)
    {
        _next = next;
        SocketHandler = socketHandler;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.WebSockets.IsWebSocketRequest)
            return;

        var socket = await context.WebSockets.AcceptWebSocketAsync();
        await SocketHandler.OnConnected(socket);
        
        await Receive(socket, async (result, buffer) =>
        {
            if (result.MessageType == WebSocketMessageType.Text)
            {
                await SocketHandler.Recieve(socket, result, buffer);
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                await SocketHandler.OnDisconnected(socket);
                
            }
        });
    }
    
    public async Task Receive(WebSocket webSocket, Action<WebSocketReceiveResult, byte[]> messageHandler)
    {
        var buffer = new byte [1024 * 4];

        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            messageHandler(result, buffer);
        }
    }
}