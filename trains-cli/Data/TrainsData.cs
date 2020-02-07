using System;
using System.Collections.Generic;
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
        {
            var url = $"places.json?query={searchFor}&type=train_station&app_id={_config.AppId}&app_key={_config.AppKey}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            using var responseSteam = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<StationMessage>(responseSteam);
        }

        public async Task<DeparturesMessage> GetDepartures(string fromStationCode, string toStationCode)
        {
            // train/station/{ *** station_code *** }/live.json    *** FST ***
            // calling_at=SOE


            var searchTerms = new string[]
            {
                $"calling_at={toStationCode}"
            };
            return await GetRestRequest<DeparturesMessage>($"train/station/{fromStationCode}/live.json", searchTerms);
        }


        private async Task<T> GetRestRequest<T>(string xxx, string[] searchTerms)
        {
            var url = $"{xxx}?app_id={_config.AppId}&app_key={_config.AppKey}&{string.Join('&', searchTerms)}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            using var responseSteam = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(responseSteam);
        }
    }
}
