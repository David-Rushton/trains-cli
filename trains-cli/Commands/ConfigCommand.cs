using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Dr.TrainsCli.Extensions;


namespace Dr.TrainsCli.Commands
{
    public class ConfigCommand : Command
    {
        public override string Name => "config";

        public override string HelpMessage => "Config [options]\nOptions\n--edit | -e\tEdit the configuration file";


        public override async Task ExecuteAsync(App app, string[] args)
        {
            if(args.ContainsOption("--edit", "-e"))
            {
                await Task.Run
                (
                    () =>
                    EditConfigFie(app, app.Config.ConfigFilePath)
                );
                return;
            }

            // TODO: Show help
            throw new Exception($"Config option(s) not supported: {string.Join(", ", args)}");
        }

        private void EditConfigFie(App app, string configFilePath)
        {
            app.Views.BaseView.WriteLine("Opening config file...\n");
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
