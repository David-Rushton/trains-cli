using System;
using System.Threading.Tasks;
using Dr.TrainsCli.Views;


namespace Dr.TrainsCli.Commands
{
    public class LiveDeparturesCommand: Command
    {
        internal readonly DeparturesView _departuresView;

        public LiveDeparturesCommand(DeparturesView departuresView)
            => (_departuresView) = (departuresView);


        public override string Name => "live";

        public override string HelpMessage => "throw new NotImplementedException();";


        public async override Task ExecuteAsync(App app, string[] args)
        {
            if(args.Length == 2)
            {
                var departures =  await app.TrainsData.GetDepartures(args[0], args[1]);
                await _departuresView.RenderAsync(departures, args[0], args[1]);
                return;
            }

            throw new Exception("Usage: trains-cli live station1 station2");
        }
    }
}
