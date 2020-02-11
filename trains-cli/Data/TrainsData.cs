using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Dr.TrainsCli.Configuration;


namespace Dr.TrainsCli.Data
{
    public class TrainsData
    {
        HttpClient _httpClient = new HttpClient();

        readonly Config _config;


        public TrainsData(Config config)
        {
            _config = config;
            _httpClient.BaseAddress = new System.Uri("http://transportapi.com/v3/uk/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add
            (
                new MediaTypeWithQualityHeaderValue("application/text")
            );
        }


        public async Task<StationMessage> FindStationAsync(string searchFor)
            => await GetRestRequest<StationMessage>
            (
                "places.json",
                new string[]
                {
                    $"query={searchFor}",
                    "type=train_station"
                }
            );

        public async Task<DeparturesMessage> GetDepartures(string fromStationCode, string toStationCode)
        {
            var departures = await GetRestRequest<DeparturesMessage>
            (
                $"train/station/{fromStationCode}/live.json",
                new string[]
                {
                    $"calling_at={toStationCode}"
                }
            );

            if(departures.Departures.ContainsKey("all"))
            {
                departures.Departures["all"]
                    .Where(d => d.RouteUrl!["id"] != null)
                    .Select(async d => d.Route = await GetRestRequest<RouteMessage>(d.RouteUrl!["id"]));
            }

            return departures;
        }


        private async Task<T> GetRestRequest<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(responseStream);
        }

        private async Task<T> GetRestRequest<T>(string apiPath, string[] searchTerms)
        {
            var url = $"{apiPath}?app_id={_config.AppId}&app_key={_config.AppKey}&{GetQueryString()}";
            return await GetRestRequest<T>(url);

            string GetQueryString() => string.Join('&', searchTerms);
        }
    }
}
