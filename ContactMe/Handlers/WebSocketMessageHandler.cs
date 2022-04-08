using System.Net.WebSockets;
using System.Text;
using ContactMe.SocketsManager;

namespace ContactMe.Handlers;

public class WebSocketMessageHandler : SocketHandler
{
    public WebSocketMessageHandler(ConnectionManager connections) : base(connections)
    {
    }

    public override async Task OnConnected(WebSocket webSocket)
    {
        await base.OnConnected(webSocket);
        var socketId = Connections.GetId(webSocket);
        await SendMessageToAll($"{socketId} just joined to the party!");
    }

    public override async Task Recieve(WebSocket webSocket, WebSocketReceiveResult result, byte[] buffer)
    {
        var socketId = Connections.GetId(webSocket);
        var message = $"{socketId} said: {Encoding.UTF8.GetString(buffer,0,result.Count)}";
        await SendMessageToAll(message);
        
        
    }
}