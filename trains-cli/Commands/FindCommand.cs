using System;
using System.Threading.Tasks;
using Dr.TrainsCli.Views;


namespace Dr.TrainsCli.Commands
{
    public class FindCommand : Command
    {
        internal readonly StationView _stationView;

        public FindCommand(StationView stationView)
            => (_stationView) = (stationView);


        public override string Name => "find";

        public override string HelpMessage => "find a station code";


        public override async Task ExecuteAsync(App app, string[] args)
        {
            if(args.Length > 0)
            {
                foreach(var arg in args)
                {
                    var stations =  await app.TrainsData.FindStationAsync(arg);
                    _stationView.WriteLine(stations.ToString());
                }

                return;
            }

            throw new Exception("Expected 1 or more search terms");
        }
    }
}
