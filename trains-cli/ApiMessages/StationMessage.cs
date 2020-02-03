using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace trains_cli
{
    public class StationMessage
    {
        [JsonPropertyName("member")]
        public List<Station> Stations { get ; set; }


        public override string ToString()
        {
            return String.Join("\n", Stations.Select(s => $"\t{s.Type}: {s.Name} ({s.StationCode})"));
        }


        public class Station
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("station_code")]
            public string StationCode { get; set; }
        }
    }
}
