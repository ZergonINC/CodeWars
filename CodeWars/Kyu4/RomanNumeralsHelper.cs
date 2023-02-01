using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class RomanNumeralsHelper
    {
        public static string ToRoman(int n)
        {
            string result = "";

            int temp = n / 1000;
            if (temp != 0)
            {
                result += result.PadRight(temp, 'M');
            }

            temp = (n / 100) % 10;
            if (temp != 0)
                result += RomanH.GetValueOrDefault(temp);

            temp = (n / 10) % 10;
            if (temp != 0)
                result += RomanD.GetValueOrDefault(temp);

            temp = n % 10;
            if (temp != 0)
                result += RomanE.GetValueOrDefault(temp);

            return result;
        }

        public static int FromRoman(string romanNumeral)
        {
            int result = 0;
            var temp = romanNumeral.ToCharArray();

            for (int i = 0; i < temp.Length; i++)
            {
                if (Roman.GetValueOrDefault(temp[i]) >= Roman.GetValueOrDefault(temp[i != temp.Length - 1 ? i + 1 : i]))
                {
                    result += Roman.GetValueOrDefault(temp[i]);
                    continue;
                }
                if (Roman.GetValueOrDefault(temp[i]) < Roman.GetValueOrDefault(temp[i + 1]))
                    result -= Roman.GetValueOrDefault(temp[i]);
            }

            return result;
        }

        public static Dictionary<char, int> Roman = new Dictionary<char, int>
        {
            { 'I', 1},
            { 'V', 5},
            { 'X', 10},
            { 'L', 50},
            { 'C', 100},
            { 'D', 500},
            { 'M', 1000}
        };

        public static Dictionary<int, string> RomanH = new Dictionary<int, string>
        {
            { 1, "C" },
            { 2, "CC" },
            { 3, "CCC"},
            { 4, "CD"},
            { 5, "D"},
            { 6, "DC"},
            { 7, "DCC"},
            { 8, "DCCC"},
            { 9, "CM"}
        };

        public static Dictionary<int, string> RomanD = new Dictionary<int, string>
        {
            { 1, "X" },
            { 2, "XX" },
            { 3, "XXX"},
            { 4, "XL"},
            { 5, "L"},
            { 6, "LX"},
            { 7, "LXX"},
            { 8, "LXXX"},
            { 9, "XC"}
        };

        public static Dictionary<int, string> RomanE = new Dictionary<int, string>
        {
            { 1, "I" },
            { 2, "II" },
            { 3, "III"},
            { 4, "IV"},
            { 5, "V"},
            { 6, "VI"},
            { 7, "VII"},
            { 8, "VIII"},
            { 9, "IX"}
        };
    }
}
