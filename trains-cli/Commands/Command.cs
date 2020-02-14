using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dr.TrainsCli.Views;


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
