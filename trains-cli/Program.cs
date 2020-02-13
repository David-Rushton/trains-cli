using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Dr.TrainsCli.Commands;
using Dr.TrainsCli.Configuration;
using Dr.TrainsCli.Data;
using Dr.TrainsCli.Extensions;
using Dr.TrainsCli.Views;


namespace Dr.TrainsCli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // try
            // {
                var app = await bootstrap();
                await app.ExecuteAsync(args);
            // }
            // catch (Exception e)
            // {
                // // TODO: Make pretty and colourful.
                // Console.WriteLine($"!!!\n{e.Message}\n!!!");
            // }
        }

        private static async Task<App> bootstrap()
        {
            var config = await ConfigFactory.GetConfigAsync();
            var trainsData = new TrainsData(config);
            var view = new BaseView();
            var app = new App(config, view, trainsData);

            app.RegisterCommand(new ConfigCommand(view));;
            app.RegisterCommand(new FindCommand(new StationView()));
            app.RegisterCommand(new LiveDeparturesCommand(new DeparturesView()));
            return app;
        }
    }
}
