using WebSocketExample.Infrastructures;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllers();

var app = builder.Build();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(5),
};

app.UseWebSockets(webSocketOptions);

app.UseDefaultFiles();
app.UseStaticFiles();
//app.MapControllers();

app.UseMyWebSocketServer(new MyWebSocketOption { Endpoint = "/ws" });

//app.MapGet("/", () => "Hello World!");

app.Run();
