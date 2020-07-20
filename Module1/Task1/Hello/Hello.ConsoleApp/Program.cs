using System;

namespace Hello.ConsoleApp
{
    class Program
    {
        private const string ErrorMessage = "You didn't enter user name!";

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string userName = args[0];

                IntroduceUser(userName);
            }
            else
            {
                Console.WriteLine(ErrorMessage);
            }
        }

        public static void IntroduceUser(string userName)
        {
            var introducer = new Introducer.Introducer();
            string introductionMessage = introducer.GetIntroductionMessage(userName);

            Console.Write(introductionMessage);
        }
    }
}
