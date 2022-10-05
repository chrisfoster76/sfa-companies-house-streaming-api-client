using System;

namespace StreamingClientTest
{
    public static class ConsoleUtilities
    {
        public static void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Companies House Api Stream Test Client");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
