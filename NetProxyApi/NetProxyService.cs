using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetProxyApi
{
    public class NetProxyService
    {
        private string _apiKey;
        public NetProxyService(string apiKey)
        {
            _apiKey = apiKey;
        }

        private static HttpClient _client;
        public static HttpClient GetClient()
        {
            if (_client == null)
            {
                var handler = new HttpClientHandler();
                handler.AutomaticDecompression = ~System.Net.DecompressionMethods.None;
                _client = new HttpClient(handler);
            }
            return _client;
        }

        public async Task<RotationResult> GetCurrentIpAsync()
        {
            var url = $"https://api.netproxy.io/api/rotateProxy/getCurrentProxy?apiKey={_apiKey}";

            var client = GetClient();
            var res = await client.GetAsync(url);
            var text = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
            {
                return null;
            }
            var dyn = JsonConvert.DeserializeObject<dynamic>(text);
            var data = dyn.data?.ToString() as string;
            var rs = JsonConvert.DeserializeObject<RotationResult>(data);
            rs.RefreshAt = rs.RefreshAt.ToLocalTime();

            return rs;
        }

        public async Task<RotationResult> RotateProxyAsync(ProxyType type = ProxyType.All, string country = "all")
        {
            var url = $"https://api.netproxy.io/api/rotateProxy/getNewProxy?apiKey={_apiKey}&country={country}&type={type.ToString().ToLower()}";

            var client = GetClient();
            var res = await client.GetAsync(url);
            var text = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
            {
                return null;
            }
            var dyn = JsonConvert.DeserializeObject<dynamic>(text);
            var data = dyn.data?.ToString() as string;
            var rs = JsonConvert.DeserializeObject<RotationResult>(data);
            rs.RefreshAt = rs.RefreshAt.ToLocalTime();

            return rs;
        }

        public async Task<List<string>> GetAllCountriesAsync()
        {
            var url = $"https://api.netproxy.io/api/rotateProxy/location?apiKey={_apiKey}";

            var client = GetClient();
            var res = await client.GetAsync(url);
            var text = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
            {
                return null;
            }
            var dyn = JsonConvert.DeserializeObject<dynamic>(text);
            var data = dyn.data?.countries?.ToString() as string;
            var rs = JsonConvert.DeserializeObject<List<string>>(data);

            return rs;
        }
    }
}
