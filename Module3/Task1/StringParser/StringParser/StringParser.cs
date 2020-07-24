using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringParser
{
    public class StringParser
    {
        private List<string> Inputs { get; set; } = new List<string>();
        private List<string> ParsedStrings { get; set; } = new List<string>();

        public void Run()
        {
            Console.WriteLine("Enter strings to parse. Enter 'exit' to finish entering!");
            ReadStrings();
            ParseStrings();
            ViewParsedStrings();
        }

        private void ReadStrings()
        {
            string input;

            do
            {
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Incorrect input! Enter not end word.");
                }
                else
                {
                    Inputs.Add(input);
                }

            } 
            while (!input.Equals("end"));

        }

        private void ParseStrings()
        {
            if (Inputs == null)
            {
                throw new NullReferenceException("Input strings can't be null!");
            }

            foreach (var input in Inputs)
            {
                string firstSymbol = input.First().ToString();
                ParsedStrings.Add(firstSymbol);
            }
        }

        private void ViewParsedStrings()
        {
            Console.WriteLine("Parsed strings:");

            if (ParsedStrings == null)
            {
                throw new NullReferenceException("Parsed strings can't be null!");
            }

            foreach (var parsedString in ParsedStrings)
            {
                Console.WriteLine(parsedString);
            }
        }

    }
}
