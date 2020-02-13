using System;
using System.Text;
using System.Threading.Tasks;
using Dr.TrainsCli.Data;


namespace Dr.TrainsCli.Views
{
    public class DeparturesView: BaseView
    {
        public async Task RenderAsync(DeparturesMessage message, string fromStationCode, string toStationCode)
        {
            await Task.Run(() => Render(message, fromStationCode, toStationCode));
        }


        private void Render(DeparturesMessage message, string fromStationCode, string toStationCode)
        {
            if( ! message.Departures.ContainsKey("all") )
            {
                throw new Exception("Departure times not available");
            }

            foreach(var departure in message.Departures["all"])
            {
                var sb = new StringBuilder();
                sb.Append($"{departure.ExpectedDepartureTime}: ");
                sb.Append($"{departure.DepartingFrom} to ");
                sb.AppendLine(departure.TerminatingAt);

                sb.Append($"Departing in {departure.ExpectedDepartureInMinutes} mins ");
                sb.AppendLine($"from platform #{departure.Platform}");

                AddRoute(sb, departure.Route);
                sb.AppendLine();

                this.WriteLine(sb);
            }


            void AddRoute(StringBuilder text, RouteMessage? route)
            {
                if(route == null || route.Stops == null)
                {
                    text.AppendLine("Timetable not availble");
                }

                bool output = false;
                foreach(var stop in route!.Stops!)
                {
                    if(output == false && stop.StationCode == fromStationCode)
                    {
                        output = true;
                    }

                    text.Append(stop.StationName);
                    text.Append($" ({stop.ExpectedArrivalTime}) ");

                    if(output == true && stop.StationCode == toStationCode)
                    {
                        return;
                    }
                }
            }
        }
    }
}
