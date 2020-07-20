using System;

namespace Introducer
{
    public class Introducer
    {
        public string GetIntroductionMessage(string userName)
        {
            string date = GetDate();
            var introductionMessage = $"Hello, {userName}! Time is: {date}";

            return introductionMessage;
        }

        private string GetDate()
        {
            DateTime time = DateTime.Now;

            return time.ToString();
        }
    }
}
