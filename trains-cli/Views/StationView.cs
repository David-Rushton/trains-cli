using System;
using System.Threading.Tasks;
using Dr.TrainsCli.Data;


namespace Dr.TrainsCli.Views
{
    public class StationView: BaseView
    {
        public async Task RenderAsync(StationMessage message)
        {
            await Task.Run(() => Console.WriteLine(message));
        }
    }
}
