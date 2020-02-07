using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Dr.TrainsCli.Extensions;


namespace Dr.TrainsCli.Commands
{
    public class ConfigCommand : Command
    {
        public override string Name => "config";

        public override string HelpMessage => throw new NotImplementedException();


        public override async Task ExecuteAsync(App app, string[] args)
        {
            if(args.ContainsOption("--edit", "-e"))
            {
                await Task.Run(() => EditConfigFie(app.Config?.ConfigFilePath ?? Configuration.ConfigFactory.ConfigFilePath));
                return;
            }

            // TODO: Show hel
            throw new Exception($"Config option(s) not supported: {string.Join(", ", args)}");
        }


        private void EditConfigFie(string configFilePath)
        {
            Console.WriteLine("\n\tOpening config file...\n");
            new Process
            {
                StartInfo = new ProcessStartInfo(configFilePath)
                {
                    UseShellExecute = true
                }
            }.Start();
        }
    }
}
