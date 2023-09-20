namespace WebSocketExample.Domain
{
    public class RedisSessionManager
    {
        internal readonly struct WSConnectSession
        {
            public string ClientId1 { get; init; }
            public string ClientId2 { get; init; }
            public Guid SessionId { get; init; }
        }

        internal readonly struct ClientRequestPackage
        {
            public string ClientId { get; init; }
            public List<object> Contents { get; init; }
        }
    }
}
