var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<CurrencyToTextConverter.Server.Factories.CurrencyConverterFactory>();
builder.Services.AddSingleton<CurrencyToTextConverter.Server.Factories.CurrencyDescriptorFactory>();

var app = builder.Build();
app.MapControllers();

// read port from configuration (appsettings.json or environment)
// falls back to 32500 if not specified
var port = builder.Configuration.GetValue<int>("Port", 32500);
app.Run($"http://localhost:{port}");
