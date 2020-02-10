using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dr.TrainsCli.Data
{
    public class RouteMessage
    {
        [JsonPropertyName("stops")]
        public List<RouteStops>? Stops { get; set; }


        public override string ToString()
        {
            if(Stops == null)
            {
                return "Route information not available";
            }

            return String.Join(", ", Stops);
        }


        public class RouteStops
        {
            [JsonPropertyName("station_name")]
            public string? StationName { get; set; }

            [JsonPropertyName("aimed_arrival_time")]
            public string? ScheduledArrivalTime { get; set; }

            [JsonPropertyName("expected_arrival_time")]
            public string? ExpectedArrivalTime { get; set; }

            [JsonPropertyName("status")]
            public string? Status { get; set; }


            public override string ToString()
            {
                return $"{StationName ?? "Unknown"} ({ExpectedArrivalTime ?? "##:##"})";
            }
        }
    }
}
