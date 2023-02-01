using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeWars.Kyu3
{
    internal class MyBedmasApprovedCalculator
    {
        public static double calculate(string s)
        {
            return calc(ToRPN(s));
        }

        public static string[] ToRPN(string s)
        {
            string pattern = @"(\-)|(\/)|(\*)|(\^)|(\+)|(\()|(\))";

            var sub = Regex.Split(s, pattern);

            Stack<string> stack = new Stack<string>();

            string[] result = { };

            foreach (var item in sub)
            {
                if (double.TryParse(item, NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var i))
                    result = result.Append(item).ToArray();

                else
                    switch (item)
                    {
                        case "-":
                            if (stack.FirstOrDefault() == "^" || stack.FirstOrDefault() == "/" || stack.FirstOrDefault() == "*")
                                result = result.Append(stack.Pop()).ToArray();
                            stack.Push(item);
                            break;

                        case "+":
                            if (stack.FirstOrDefault() == "^" || stack.FirstOrDefault() == "/" || stack.FirstOrDefault() == "*")
                                result = result.Append(stack.Pop()).ToArray();
                            stack.Push(item);
                            break;

                        case "*":
                            if (stack.FirstOrDefault() == "^" || stack.FirstOrDefault() == "/" || stack.FirstOrDefault() == "*")
                                result = result.Append(stack.Pop()).ToArray();
                            stack.Push(item);
                            break;

                        case "/":
                            if (stack.FirstOrDefault() == "^" || stack.FirstOrDefault() == "/" || stack.FirstOrDefault() == "*")
                                result = result.Append(stack.Pop()).ToArray();
                            stack.Push(item);
                            break;

                        case "^":
                            if (stack.FirstOrDefault() == "^")
                                result = result.Append(stack.Pop()).ToArray();
                            stack.Push(item);
                            break;

                        case "(":
                            stack.Push(item);
                            break;

                        case ")":
                            var temp = stack.ToArray();
                            foreach (var j in temp)
                            {
                                if (j != "(")
                                    result = result.Append(stack.Pop()).ToArray();
                                else
                                {
                                    stack.Pop();
                                    break;
                                }
                            }
                            break;
                    }
            }

            while (stack.Count > 0)
                result = result.Append(stack.Pop()).ToArray();

            return result;
        }

        public static double calc(string[] result)
        {
            Stack<double> temp = new Stack<double>();

            foreach (var item in result)
            {
                if (double.TryParse(item, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out var i))
                {
                    temp.Push(i);
                    continue;
                }

                var right = temp.Pop();
                var left = temp.Pop();

                switch (item)
                {
                    case "-":
                        temp.Push(left - right);
                        break;

                    case "+":
                        temp.Push(left + right);
                        break;

                    case "*":
                        temp.Push(left * right);
                        break;

                    case "/":
                        temp.Push(left / right);
                        break;

                    case "^":
                        temp.Push(Math.Pow(left, right));
                        break;
                }
            }

            return temp.Pop();
        }
    }
}
