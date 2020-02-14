using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dr.TrainsCli.Commands;
using Dr.TrainsCli.Configuration;
using Dr.TrainsCli.Data;
using CliViews = Dr.TrainsCli.Views;


namespace Dr.TrainsCli
{
    public class App
    {
        readonly Dictionary<string, Command> _commands = new Dictionary<string, Command>();


        internal App(Config config, CliViews.Views views, TrainsData trainsData)
        {
            Config = config;
            Views = views;
            TrainsData = trainsData;

            ExitIfNoApiKey();
        }


        public TrainsData TrainsData { get; private set; }

        public Config Config { get; private set; }

        public CliViews.Views Views { get; private set; }


        public void RegisterCommand(Command command)
        {
            if(!(_commands.TryAdd(command.Name.ToLower(), command)))
            {
                Debug.Assert(false, "$Cannot add command.  Name already registered: {command.Name}");
            }
        }

        public async Task ExecuteAsync(string[] args)
        {
            if(args.Length == 0)
            {
                // show usage
                throw new NotImplementedException();
            }

            if(_commands.TryGetValue(args[0].ToLower(), out var command))
            {
                await command.ExecuteAsync(this, args.Skip(1).ToArray());
            }
            else
            {
                throw new Exception($"Command not recognised: {args[0]}");
            }
        }

        private void ExitIfNoApiKey()
        {
            if(Config.AppId == null || Config.AppKey == null)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An API key is required to use this service");
                sb.AppendLine("Visit https://developer.transportapi.com to register");
                sb.AppendLine("Then update the config file to continue:");
                sb.AppendLine("  trains-cli config --edit");

                Views.BaseView.WriteError(sb);
                Environment.Exit(1);
            }
        }
    }
}
