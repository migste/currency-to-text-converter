var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

// Read port from configuration (appsettings.json or environment). Falls back to 32500.
var port = builder.Configuration.GetValue<int>("Port", 32500);
app.Run($"http://localhost:{port}");
