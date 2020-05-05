using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoLab
{
    public class DirectionsService
    {
        private string apikey = "XXXXXXXXXXXXXXXXXXXXXXX";
        private string endpoint = "https://maps.googleapis.com/maps/api/directions/json";
        private string labPlace = "12.3456,123.456";

        private static readonly HttpClient httpClient = new HttpClient();

        public async Task<int> GetDistance(double latitude, double longitud)
        {
            var response = await GetDirectionsAsync(latitude, longitud);
            return response.routes[0].legs[0].distance.value;
        }

        private async Task<dynamic> GetDirectionsAsync(double latitude, double longitud, string mode= "walking")
        {
            var origin = $"{latitude},{longitud}";
            var url = $"{endpoint}?key={apikey}&origin={origin}&destination={labPlace}&mode={mode}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            var response = await httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(json);
        }
    }
}
