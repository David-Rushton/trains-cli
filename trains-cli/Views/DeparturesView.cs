using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dr.TrainsCli.Data;


namespace Dr.TrainsCli.Views
{
    public class DeparturesView: BaseView
    {
        public async Task RenderAsync(DeparturesMessage message, string fromStationCode, string toStationCode, int firstResults)
        {
            await Task.Run(() => Render(message, fromStationCode, toStationCode, firstResults));
        }


        private void Render(DeparturesMessage message, string fromStationCode, string toStationCode, int firstResults)
        {
            if( ! message.Departures.ContainsKey("all") )
            {
                throw new Exception("Departure times not available");
            }

            this.WriteLine("Live Departures");
            this.WriteLine("---------------\n");

            var departures = message.Departures["all"].Take(firstResults);
            foreach(var departure in departures)
            {
                var sb = new StringBuilder();
                sb.Append($"The {departure.ExpectedDepartureTime} to ");
                sb.AppendLine(departure.TerminatingAt);
                sb.Append($"Departing in {departure.ExpectedDepartureInMinutes}");
                sb.Append(departure.ExpectedDepartureInMinutes == 1 ? " minute ": " minutes ");
                sb.AppendLine($"from platform #{departure.Platform}");
                AddRouteTimetable(sb, departure.Route, fromStationCode, toStationCode);
                sb.AppendLine();

                this.WriteLine(sb);
            }
        }

        private void AddRouteTimetable(StringBuilder text, RouteMessage? route, string fromStationCode, string toStationCode)
        {
            var startIndex = route?.Stops?.FindIndex(s => s.StationCode == fromStationCode.ToUpper()) ?? 0;
            var endIndex = route?.Stops?.FindIndex(s => s.StationCode == toStationCode.ToUpper()) ?? 0;
            var stops = endIndex - startIndex - 1; // Remove boarding\alighting station from count of stops

            // Null check here is added to help the compiler identify that
            // stops are not null
            if(startIndex > 0 && startIndex <= endIndex || route?.Stops == null)
            {
                text.Append("Cannot download timetable");
                return;
            }

            switch(stops)
            {
                case 0:
                    text.AppendLine("Direct");
                    break;

                case 1:
                    text.AppendLine("1 stop");
                    break;

                default:
                    text.AppendLine($"{stops} stops");
                    break;
            }

            var destinationStop = route.Stops[endIndex];
            text.AppendLine($"Arriving at {destinationStop.StationName} {route.Stops[endIndex].ExpectedArrivalTime}");
        }
    }
}
