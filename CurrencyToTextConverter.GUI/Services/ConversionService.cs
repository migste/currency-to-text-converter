using System.Net.Http;
using System.Text.Json;
using System.IO;

namespace CurrencyToTextConverter.GUI.Services
{
    public class ConversionService
    {
        private readonly string _baseUrl;

        public ConversionService(string? baseUrl = null)
        {
            if (!string.IsNullOrEmpty(baseUrl))
            {
                _baseUrl = baseUrl;
                return;
            }

            var port = 32500;

            try
            {
                var json = File.ReadAllText("appsettings.json");
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("Port", out var value))
                {
                    value.TryGetInt32(out port);
                }
            }
            catch
            {
                // ignore errors and use default port
            }

            _baseUrl = $"http://localhost:{port}";
        }

        public async Task<(bool Success, string? Text, string? ErrorMessage)> ConvertAsync(string amount, string lang)
        {
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                var urlAmount = Uri.EscapeDataString(amount);
                var url = $"{_baseUrl}/convert?amount={urlAmount}&lang={lang}";
                var response = await client.GetAsync(url).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return (false, null, content);
                }

                try
                {
                    using var doc = JsonDocument.Parse(content);
                    if (doc.RootElement.TryGetProperty("text", out var text))
                    {
                        return (true, text.GetString() ?? string.Empty, null);
                    }
                }
                catch (JsonException ex)
                {
                    return (false, null, "Error parsing Json response: " + ex.Message);
                }

                return (true, content, null);
            }
            catch (Exception ex)
            {
                return (false, null, "Error contacting server: " + ex.Message);
            }
        }
    }
}
