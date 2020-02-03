using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace trains_cli
{
    public class DeparturesMessage
    {
        [JsonPropertyName("departures")]
        public Dictionary<string, List<DepartureDetails>> Departures { get; set; }

        public override string ToString()
        {
            return string.Join("\n", Departures["all"].Select(d => d.ToString()));
        }


        public class DepartureDetails
        {
            [JsonPropertyName("platform")]
            public string Platform { get; set; }

            [JsonPropertyName("aimed_departure_time")]
            public string ScheduledDepartureTime { get; set; }

            [JsonPropertyName("expected_departure_time")]
            public string ExpectedDepartureTime { get; set; }

            [JsonPropertyName("best_departure_estimate_mins")]
            public int ExpectedDepartureInMinutes { get; set; }

            [JsonPropertyName("origin_name")]
            public string DepartingFrom { get; set; }

            [JsonPropertyName("destination_name")]
            public string TerminatingAt { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; }

            public override string ToString()
            {
                return $"\tFrom: {DepartingFrom}\n\tTo: {TerminatingAt}\n\tStatus: {Status}\n\tPlatfrom: {Platform}\n\tScheduled Departure: {ScheduledDepartureTime}\n\tExpected Departure: {ExpectedDepartureTime}\n\tDeparting In (mins): {ExpectedDepartureInMinutes}\n";
            }
        }
    }
}
