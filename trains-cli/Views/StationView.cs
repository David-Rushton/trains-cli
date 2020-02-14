using System;
using System.Linq;
using System.Threading.Tasks;
using Dr.TrainsCli.Data;


namespace Dr.TrainsCli.Views
{
    public class StationView: BaseView
    {
        public async Task RenderAsync(StationMessage message)
        {
            var output = "Station Search\n--------------\n\n";
            var stations = message.Stations.Count;
            var maxLen = message.Stations.Max(s => s?.StationName?.Length ?? 0);

            if(stations == 0)
            {
                output = "No stations found";
            }
            else
            {
                maxLen +=  10;
                output += $"+------+-----".PadRight(maxLen, '-') + "+\n";
                output += $"| Code | Name".PadRight(maxLen, ' ') + "|\n";
                output += $"+------+-----".PadRight(maxLen, '-') + "+\n";
                foreach(var station in message.Stations)
                {
                    output += $"| {station.StationCode}  | {station.StationName}".PadRight(maxLen, ' ') + "|\n";
                }
                output += $"+------+-----".PadRight(maxLen, '-') + "+\n";
            }

            await Task.Run(() => this.WriteLine(output));
        }
    }
}
