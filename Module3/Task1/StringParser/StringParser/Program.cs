using System;

namespace StringParser
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task1
            var parser = new StringParser();
            parser.Run();

            // Task2
            var stringToIntParser = new StringToIntParser.StringToIntParser();
            Console.Write("Enter your number: ");
            string input = Console.ReadLine();
            int? parsedNumber = stringToIntParser.ParseString(input);

            if (parsedNumber == null)
            {
                Console.WriteLine("Incorrect input format!");
            }
            else
            {
                Console.WriteLine(parsedNumber);
            }
            
        }
    }
}
