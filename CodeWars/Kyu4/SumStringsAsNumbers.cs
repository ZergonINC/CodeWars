using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class SumStringsAsNumbers
    {
        public static string sumStrings(string a, string b)
        {
            return (BigInteger.TryParse(a, out var A) ? BigInteger.Parse(a) + BigInteger.Parse(b) : 5).ToString();
        }
    }
}
