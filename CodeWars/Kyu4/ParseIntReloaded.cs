using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class ParseIntReloaded
    {
        public static Dictionary<string, int> dic = new Dictionary<string, int>
        {
            {"zero", 0 },
            {"one", 1 },
            {"two", 2 },
            {"three", 3 },
            {"four", 4 },
            {"five", 5 },
            {"six", 6 },
            {"seven", 7 },
            {"eight", 8 },
            {"nine", 9 },
            {"ten", 10 },
            {"eleven", 11 },
            {"twelve", 12 },
            {"thirteen", 13 },
            {"fourteen", 14 },
            {"fifteen", 15 },
            {"sixteen", 16 },
            {"seventeen", 17 },
            {"eighteen", 18 },
            {"nineteen", 19 },
            {"twenty", 20 },
            {"thirty", 30 },
            {"forty", 40 },
            {"fifty", 50 },
            {"sixty", 60 },
            {"seventy", 70 },
            {"eighty", 80 },
            {"ninety", 90 }
        };

        public static int ParseInt(string s)
        {
            char[] delimiterChars = { ' ', '-' };
            List<string> words = new List<string>();
            int result = 0;
            if (s.Contains("million"))
                return 1000000;
            if (s.Contains("thousand"))
            {
                int result1 = 0;
                int result2 = 0;
                words = s.Split("thousand").ToList();
                List<string> word1 = words[0].Split(delimiterChars).ToList();
                List<string> word2 = words[1].Split(delimiterChars).ToList();
                foreach (var i in word1)
                {
                    if (i == "hundred")
                        result1 *= 100;
                    if (i == "thousand")
                        result1 *= 1000;
                    result1 += dic.GetValueOrDefault(i);
                }
                foreach (var i in word2)
                {
                    if (i == "hundred")
                        result2 *= 100;
                    if (i == "thousand")
                        result2 *= 1000;
                    result2 += dic.GetValueOrDefault(i);
                }
                result = result1 * 1000 + result2;
            }
            else
            {
                words = s.Split(delimiterChars).ToList();
                foreach (var i in words)
                {
                    if (i == "hundred")
                        result *= 100;
                    if (i == "thousand")
                        result *= 1000;
                    result += dic.GetValueOrDefault(i);
                }
            }
            return result;

        }
    }
}
