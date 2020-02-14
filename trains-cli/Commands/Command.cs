using System.Threading.Tasks;


namespace Dr.TrainsCli.Commands
{
    public abstract class Command
    {
        internal Command()
        { }


        abstract public string Name { get; }

        abstract public string HelpMessage { get; }


        abstract public Task ExecuteAsync(App app, string[] args);
    }
}
