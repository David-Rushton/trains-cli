using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace trains_cli.Data
{
    public class StationMessage
    {
        #pragma warning disable CS8618
        [JsonPropertyName("member")]
        public List<Station> Stations { get ; set; }
        #pragma warning restore CS8618


        public override string ToString()
        {
            return String.Join("\n", Stations.Select(s => s.ToString()));
        }


        public class Station
        {
            [JsonPropertyName("type")]
            public string? Type { get; set; }

            [JsonPropertyName("name")]
            public string? StationName { get; set; }

            [JsonPropertyName("station_code")]
            public string? StationCode { get; set; }

            public override string ToString()
            {
                return @$"
    Type: {Type ?? "Unknown"}
    Station Name: {StationName ?? "Unknown"}
    Station Code: {StationCode ?? "Unknown"}
";
            }
        }

    }
}
