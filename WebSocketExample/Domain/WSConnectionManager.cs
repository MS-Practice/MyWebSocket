using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace WebSocketExample.Domain
{
    public class WSConnectionManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> _sockets = new();

        public WebSocket GetSocketByClientId(string clientId) => _sockets[clientId];

        public void AddSocket(string clientId, WebSocket webSocket) => _sockets[clientId] = webSocket;

        public void RemoveSocket(string clientId) => _sockets.Remove(clientId, out _);
    }
}
