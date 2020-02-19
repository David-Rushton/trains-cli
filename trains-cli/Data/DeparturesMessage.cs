using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Dr.TrainsCli.Data
{
    public class DeparturesMessage
    {
        #pragma warning disable CS8618
        [JsonPropertyName("departures")]
        public Dictionary<string, List<DepartureDetails>> Departures { get; set; }
        #pragma warning restore CS8618

        public override string ToString()
        {
            if(Departures.ContainsKey("all"))
            {
                return string.Join("\n", Departures["all"].Select(d => d.ToString()));
            }

            return "Departure information not availble";
        }


        public class DepartureDetails
        {
            [JsonPropertyName("platform")]
            public string? Platform { get; set; }

            [JsonPropertyName("aimed_departure_time")]
            public string? ScheduledDepartureTime { get; set; }

            [JsonPropertyName("expected_departure_time")]
            public string? ExpectedDepartureTime { get; set; }

            [JsonPropertyName("best_departure_estimate_mins")]
            public int? ExpectedDepartureInMinutes { get; set; }

            [JsonPropertyName("origin_name")]
            public string? DepartingFrom { get; set; }

            [JsonPropertyName("destination_name")]
            public string? TerminatingAt { get; set; }

            [JsonPropertyName("status")]
            public string? Status { get; set; }

            public override string ToString()
            {
                return @$"
    From: {DepartingFrom ?? "Unknown"}
    To: {TerminatingAt ?? "Unknown"}
    Platfrom: {Platform ?? "Unknown"}
    Scheduled Departure: {ScheduledDepartureTime ?? "Unknown"}
    Expected Departure: {ExpectedDepartureTime ?? "Unknown"}
    Departing In (mins): {ExpectedDepartureInMinutes?.ToString() ?? "Unknown"}
";
            }
        }
    }
}
