using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class Snail
    {
        public static int[] Сrawl(int[][] array)
        {
            List<int> result = new List<int>();
            var temp = array.ToList();
            while (temp.Count > 0)
            {
                //to the right
                result = result.Concat(temp.First()).ToList();
                temp.RemoveAt(0);
                if (temp.Count > 0)
                {
                    //down
                    for (int i = 0; i < temp.Count; i++)
                    {
                        result.Add(temp[i][^1]);
                        temp[i] = temp[i].Take(temp[i].Length - 1).ToArray();
                    }
                    //to the left
                    result = result.Concat(temp.Last().Reverse()).ToList();
                    temp.RemoveAt(temp.Count - 1);
                    //up
                    for (int i = temp.Count - 1; i > 0; i--)
                    {
                        result.Add(temp[i][0]);
                        temp[i] = temp[i].Skip(1).ToArray();
                    }
                }
            }

            return result.ToArray();
        }
    }
}
