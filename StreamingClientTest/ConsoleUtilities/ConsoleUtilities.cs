using System;
using System.Net;

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

        public static void ShowHttpStatusCode(HttpStatusCode statusCode)
        {
            Console.Write($"Status Code: ");

            if ((int)statusCode >= 200 && (int)statusCode <= 299)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            
            Console.WriteLine($"{(int)statusCode} {statusCode}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
