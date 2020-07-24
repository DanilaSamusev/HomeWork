using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringToIntParser
{
    public class StringToIntParser
    {
        public int? ParseString(string input)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
