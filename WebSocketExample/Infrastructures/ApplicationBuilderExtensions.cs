using System.Net.WebSockets;
using System.Text;

namespace WebSocketExample.Infrastructures
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMyWebSocketServer(this IApplicationBuilder app, MyWebSocketOption options)
        {
            app.UseWebSockets();

            app.Map(options.Endpoint, appBuilder =>
            {
                appBuilder.Use( MyWebSocketMiddleware);
            });

            return app;
        }

        private static async Task MyWebSocketMiddleware(HttpContext context, Func<Task> next)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private static async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);

                try
                {
                    receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
    }

    public class MyWebSocketOption
    {
        public string Endpoint { get; set; } = "/ws";
    }
}
