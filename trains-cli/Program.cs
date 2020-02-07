using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using trains_cli.Commands;
using trains_cli.Configuration;
using trains_cli.Data;
using trains_cli.Extensions;


namespace trains_cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var app = await bootstrap();
                await app.ExecuteAsync(args);
            }
            catch (Exception e)
            {
                // TODO: Make pretty and colourful.
                Console.WriteLine($"!!!\n{e.Message}\n!!!");
            }
        }

        private static async Task<App> bootstrap()
        {
            var configPath = ConfigFactory.ConfigFilePath;
            var config = await ConfigFactory.GetConfigAsync();
            var trainsData = new TrainsData(config);
            var app = new App(config, configPath, trainsData);

            app.RegisterCommand(new ConfigCommand());
            app.RegisterCommand(new FindCommand());
            app.RegisterCommand(new LiveDeparturesCommand());
            return app;
        }
    }
}
