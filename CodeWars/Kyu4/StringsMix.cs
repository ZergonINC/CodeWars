using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class StringsMix
    {
        public static Dictionary<string, int> Group(string s)
        {
            Regex reg = new Regex(@"[a-z]", RegexOptions.IgnoreCase);

            var result = reg.Matches(s)
                .Select(a => a.Value)
                .GroupBy(a => a)
                .Where(a => a.Count() > 1)
                .OrderByDescending(a => a.Count())
                .Select(a => new { a.Key, V = a.Count() })
                .ToDictionary(a => a.Key, a => a.V);

            return result;
        }


        //    public static string Mix(string s1, string s2)
        //    {
        //        var result = new List<string>();

        //        var result1 = Mixing.Group(s1);
        //        var result2 = Mixing.Group(s2);

        //        if (result1.Count == 0 && result2.Count == 0)
        //            return "";

        //        var temp = "";

        //        if (result2.Count == 0)
        //            foreach (var i in result1)
        //            {
        //                result.Add($"1:{temp.PadRight(i.Value, Convert.ToChar(i.Key))}");
        //                temp = "";
        //            }

        //        foreach (var i in result1)
        //        {
        //            foreach (var j in result2)
        //            {
        //                if (i.Key == j.Key)
        //                {
        //                    if (i.Value > j.Value)
        //                        result.Add($"1:{temp.PadRight(i.Value, Convert.ToChar(i.Key))}");

        //                    if (i.Value < j.Value)
        //                        result.Add($"2:{temp.PadRight(j.Value, Convert.ToChar(j.Key))}");

        //                    if (i.Value == j.Value)
        //                        result.Add($"3:{temp.PadRight(j.Value, Convert.ToChar(j.Key))}");
        //                }

        //                if (!result2.Select(a => a.Key).Contains(i.Key) && !result.Contains($"1:{temp.PadRight(i.Value, Convert.ToChar(i.Key))}"))
        //                    result.Add($"1:{temp.PadRight(i.Value, Convert.ToChar(i.Key))}");

        //                if (!result1.Select(a => a.Key).Contains(j.Key) && !result.Contains($"2:{temp.PadRight(j.Value, Convert.ToChar(j.Key))}"))
        //                    result.Add($"2:{temp.PadRight(j.Value, Convert.ToChar(j.Key))}");

        //                temp = "";
        //            }
        //        }

        //        result = result.OrderByDescending(a => a.Length).ThenBy(a => a).ToList();

        //        foreach (var item in result)
        //        {
        //            if (Regex.IsMatch(item, @"[a-z]"))
        //                temp += item[0] == '3' ? item.Replace(item[0], '=') + "/" : item + "/";
        //        }

        //        return temp[0..^1];
        //    }
    }
}
