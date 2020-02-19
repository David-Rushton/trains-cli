using System;
using System.Text;
using System.Threading;


namespace Dr.TrainsCli.Views
{
    public class BaseView
    {
        internal BaseView()
        { }


        public void WriteLine(string message)
            => WriteToConsole($"{message}\n");

        public void WriteLine(StringBuilder message)
            => WriteLine(message.ToString());


        public void WriteError(string message)
            => WriteToConsole($"{message}\n", ConsoleColor.Yellow, ConsoleColor.DarkRed);

        public void WriteError(StringBuilder message)
            => WriteError(message.ToString());


        private void WriteToConsole
        (
            string message,
            ConsoleColor trace = ConsoleColor.DarkRed,
            ConsoleColor final = ConsoleColor.Green
        )
        {
            var originalForegroundColor = Console.ForegroundColor;

            foreach(char character in message)
            {
                if( Console.CursorLeft >= (Console.WindowWidth - 1))
                {
                    Console.WriteLine();
                }

                // var currentCursonPosition = ConsoleEx.GetBookmark();
                Console.ForegroundColor = trace;
                Console.Write(character);
                Thread.Sleep(20);

                if( ! (character == '\n' || character == '\r' || character == '\t') )
                {

                    // ConsoleEx.GoToBookmark(currentCursonPosition);
                    Console.ForegroundColor = final;
                    Console.Write($"\b{character}");
                }
            }

            Console.ForegroundColor = originalForegroundColor;
        }
    }
}
