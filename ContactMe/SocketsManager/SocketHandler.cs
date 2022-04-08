using System.Net.WebSockets;
using System.Text;

namespace ContactMe.SocketsManager;

public abstract class SocketHandler
{
   public ConnectionManager Connections { get; set; }

   public SocketHandler(ConnectionManager connections)
   {
      Connections = connections;
   }

   public virtual async Task OnConnected(WebSocket webSocket)
   {
      await Task.Run(() => { Connections.AddSocket(webSocket); });
   }

   public virtual async Task OnDisconnected(WebSocket webSocket)
   {
      await Connections.RemoveSocketAsync(Connections.GetId(webSocket));
   }

   public async Task SendMessage(WebSocket webSocket, string message)
   {
      if (webSocket.State != WebSocketState.Open)
         return;

      await webSocket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message), 0, message.Length), WebSocketMessageType.Text,
         true, CancellationToken.None);
   }

   public async Task SendMessageToAll(string message)
   {
      foreach (var con in Connections.GetAllConnections())
      {
         await SendMessage(con.Value, message);
      }
   }

   public abstract Task Recieve(WebSocket webSocket, WebSocketReceiveResult result, byte[] buffer);
}