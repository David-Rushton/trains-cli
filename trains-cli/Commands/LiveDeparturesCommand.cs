using System;
using System.Threading.Tasks;


namespace Dr.TrainsCli.Commands
{
    public class LiveDeparturesCommand: Command
    {
        public override string Name => "live";

        public override string HelpMessage => "throw new NotImplementedException();";


        public async override Task ExecuteAsync(App app, string[] args)
        {
            if(args.Length == 2)
            {
                var departures =  await app.TrainsData.GetDepartures(args[0], args[1]);
                // Console.WriteLine(departures);
                await app.Views.DeparturesView.RenderAsync(departures, args[0], args[1], 3);
                return;
            }

            throw new Exception("Usage: trains-cli live station1 station2");
        }
    }
}
