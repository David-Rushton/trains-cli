using System;


namespace Dr.TrainsCli.Extensions
{
    public static class ConsoleEx
    {
        public static Bookmark GetBookmark()
        {
            return new Bookmark
            {
                Top = Console.CursorTop,
                Left = Console.CursorLeft
            };
        }

        public static void GoToBookmark(Bookmark bookmark)
        {
            Console.CursorTop = bookmark.Top;
            Console.CursorLeft = bookmark.Left;
        }


        public struct Bookmark
        {
            public int Top { get; set; }

            public int Left { get; set; }
        }
    }
}
