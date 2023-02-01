using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class NextBiggerNumberWithTheSameDigits
    {
        static void swap(char[] ar, int i, int j)
        {
            char temp = ar[i];
            ar[i] = ar[j];
            ar[j] = temp;
        }

        public static long NextBiggerNumber(long n)
        {
            string result = string.Empty;
            var ar = n.ToString().ToCharArray();
            int i;
            for (i = ar.Length - 1; i > 0; i--)
            {
                if (ar[i] > ar[i - 1])
                {
                    break;
                }
            }

            if (i == 0)
                return -1;
            else
            {
                int x = ar[i - 1], min = i;
                for (int j = i + 1; j < ar.Length; j++)
                {
                    if (ar[j] > x && ar[j] < ar[min])
                    {
                        min = j;
                    }
                }
                swap(ar, i - 1, min);

                Array.Sort(ar, i, ar.Length - i);

                for (i = 0; i < ar.Length; i++)
                    result += ar[i];
                return Convert.ToInt64(result);
            }
        }
    }
}
