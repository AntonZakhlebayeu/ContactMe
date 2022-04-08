using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;


namespace ContactMe.SocketsManager;

public class ConnectionManager
{
    private readonly ConcurrentDictionary<string, WebSocket> _connections = new ConcurrentDictionary<string, WebSocket>();

    public WebSocket GetSocketById(string id)
    {
        return _connections.FirstOrDefault(x => x.Key == id).Value;
    }

    public ConcurrentDictionary<string, WebSocket> GetAllConnections()
    {
        return _connections;
    }

    public string GetId(WebSocket webSocket)
    {
        return _connections.FirstOrDefault(x => x.Value == webSocket).Key;
    }

    public async Task RemoveSocketAsync(string id)
    {
        _connections.TryRemove(id, out var socket);
        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Socket connection closed.",
            CancellationToken.None);
    }

    public void AddSocket(WebSocket webSocket)
    {
        _connections.TryAdd(GetConnectionId(), webSocket);
    }

    private string GetConnectionId()
    {
        return Guid.NewGuid().ToString("N");
    }
}