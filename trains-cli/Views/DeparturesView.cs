using System;
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

            int resultsReturned = 0;
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

                resultsReturned++;
                if(resultsReturned >= firstResults)
                {
                    return;
                }
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
                    if(output == false && stop.StationCode.ToLower() == fromStationCode.ToLower())
                    {
                        output = true;
                    }

                    if(output == true)
                    {
                        text.Append(stop.StationName);
                        text.Append($" ({stop.ExpectedArrivalTime}) ");
                    }

                    if(output == true && stop.StationCode.ToLower() == toStationCode.ToLower())
                    {
                        return;
                    }
                }
            }
        }
    }
}
