using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Dr.TrainsCli.Extensions;
using Dr.TrainsCli.Views;


namespace Dr.TrainsCli.Commands
{
    public class ConfigCommand : Command
    {
        private readonly BaseView _view;

        public ConfigCommand(BaseView view)
            => (_view) = (view);


        public override string Name => "config";

        public override string HelpMessage => throw new NotImplementedException();


        public override async Task ExecuteAsync(App app, string[] args)
        {
            if(args.ContainsOption("--edit", "-e"))
            {
                await Task.Run
                (
                    () =>
                    EditConfigFie(app.Config?.ConfigFilePath ?? Configuration.ConfigFactory.ConfigFilePath)
                );
                return;
            }

            // TODO: Show help
            throw new Exception($"Config option(s) not supported: {string.Join(", ", args)}");
        }


        private void EditConfigFie(string configFilePath)
        {
            _view.WriteLine("\n\tOpening config file...\n");
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
