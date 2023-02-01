using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class StripComments
    {
        public static string StripComment(string text, string[] commentSymbols)
        {
            string[] result = new string[] { };
            string temp = string.Empty;
            string[] stringArray = text.Split("\n");
            foreach (var item in stringArray)
            {
                temp = item;
                for (int i = 0; i < commentSymbols.Length; i++)
                {
                    if (item.Contains(commentSymbols[i]))
                    {
                        var index = item.IndexOf(commentSymbols[i]);
                        temp = item.Substring(0, index).TrimEnd(' ');
                    }
                }
                result = result.Append(temp.TrimEnd()).ToArray();
            }
            return string.Join("\n", result);
        }
    }
}
