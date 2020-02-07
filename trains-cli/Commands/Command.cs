using System;
using System.Threading.Tasks;


namespace trains_cli.Commands
{
    public abstract class Command
    {
        abstract public string Name { get; }

        abstract public string HelpMessage { get; }


        abstract public Task ExecuteAsync(App app, string[] args);
    }
}
