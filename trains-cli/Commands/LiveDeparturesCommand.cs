using System;
using System.Threading.Tasks;


namespace trains_cli.Commands
{
    public class LiveDeparturesCommand: Command
    {
        public override string Name => "live";

        public override string HelpMessage => "throw new NotImplementedException();";


        public async override Task ExecuteAsync(App app, string[] args)
        {
            Console.WriteLine("Getting departures...");
            var departures =  await app.TrainsData.GetDepartures("FST", "SOE");
            Console.WriteLine(departures);
        }
    }
}
