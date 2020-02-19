using System;
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

            if(stations == 0)
            {
                output = "No stations found";
            }
            else
            {
                foreach(var station in message.Stations)
                {
                    output += $"{station.StationName} ({station.StationCode})\n";
                }
            }

            await Task.Run(() => this.WriteLine(output));
        }
    }
}
