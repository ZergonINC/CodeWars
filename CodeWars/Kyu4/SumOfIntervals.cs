using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class SumOfIntervals
    {
        public static int SumIntervals((int, int)[] intervals)
        {
            List<int> result = new List<int>();
            foreach (var item in intervals)
                result.AddRange(Enumerable.Range(item.Item1, item.Item2 - item.Item1));
            return result.Distinct().Count();
        }
    }
}
