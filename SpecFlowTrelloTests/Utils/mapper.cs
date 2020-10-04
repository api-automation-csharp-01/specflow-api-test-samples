using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SpecFlowTrelloTests.Utils
{
    public sealed class Mapper
    {
        private Mapper()
        { 
        
        }

        public static string MapValue(string value, Dictionary<string, string> storeData)
        {
            var regex = @"\{[A-Z_-]+}";
            MatchCollection matches = Regex.Matches(value, regex);
            foreach (Match match in matches)
            {
                foreach (Capture capture in match.Captures)
                {
                    Regex regexKeys = new Regex("[{}]");
                    string key = regexKeys.Replace(capture.Value, "");
                    if (storeData.ContainsKey(key))
                    {
                        value = value.Replace(capture.Value, storeData[key]);
                    }
                }
            }
            return value;
        }
    }
}
