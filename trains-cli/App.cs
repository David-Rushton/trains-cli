using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dr.TrainsCli.Commands;
using Dr.TrainsCli.Configuration;
using Dr.TrainsCli.Data;
using Dr.TrainsCli.Views;


namespace Dr.TrainsCli
{
    public class App
    {
        readonly Dictionary<string, Command> _commands = new Dictionary<string, Command>();


        internal App(Config config, BaseView view, TrainsData trainsData)
        {
            Config = config;
            View = view;
            TrainsData = trainsData;
        }


        public TrainsData TrainsData { get; internal set; }

        public Config Config { get; internal set; }

        public BaseView View { get; internal set; }


        public void RegisterCommand(Command command)
        {
            if(!(_commands.TryAdd(command.Name.ToLower(), command)))
            {
                throw new Exception($"Cannot add command.  Name already registered: {command.Name}");
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


        private void ExecuteCommand(Task command, string[] args)
        {
            while( ! command.IsCompleted )
            {
                Console.Write("...");
                Thread.Sleep(100);
            }
        }

        private void Initialise()
        {
            if(Config.AppId == null || Config.AppKey == null) {
                throw new Exception(@"
    A valid API key is required to use this service.
    Visit https://developer.transportapi.com to register.
    Update the config file to continue:
        trains-cli config --edit
");
            }
        }
    }
}
