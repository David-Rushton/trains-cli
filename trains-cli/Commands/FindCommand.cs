using System;
using System.Threading.Tasks;


namespace Dr.TrainsCli.Commands
{
    public class FindCommand : Command
    {
        public override string Name => "find";

        public override string HelpMessage => "find a station code";


        public override async Task ExecuteAsync(App app, string[] args)
        {
            if(args.Length > 0)
            {
                foreach(var arg in args)
                {
                    var stations =  await app.TrainsData.FindStationAsync(arg);
                    Console.WriteLine(stations);
                }
            }

            throw new Exception("Expected 1 or more search terms");
        }
    }
}
