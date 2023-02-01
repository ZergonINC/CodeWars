using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class TheObservedPin
    {
        public static List<string> GetPINs(string observed)
        {
            string[][] possibleCombinations = TheObservedPin.GetPossibleCombinations(observed);
            
            if (observed.Length == 1) return possibleCombinations[0].ToList();

            List<string> result = TheObservedPin.GetResult(possibleCombinations);

            return result;
        }

        public static string[][] GetPossibleCombinations(string observed)
        {
            List<string> temp = new();
            string[][] possibleCombinations = new string[observed.Length][];

            string[][] keypadLayout =
            {
                new string[] {"1","2","3"},
                new string[] {"4","5","6"},
                new string[] {"7","8","9"},
                new string[] {"","0",""}
            };

            for (byte i = 0; i < observed.Length; i++)
            {
                string currentNumber = observed[i].ToString();

                for (int j = 0; j < keypadLayout.Length; j++)
                {
                    if (Array.IndexOf(keypadLayout[j], currentNumber) >= 0)
                    {
                        int row = j;
                        int column = Array.IndexOf(keypadLayout[j], currentNumber);

                        for (int a = -1; a < 2; a++)
                        {
                            int tempColumn = column;
                            int tempRow = row + a;
                            if ((tempColumn >= 0 && tempColumn < 3) && (tempRow >= 0 && tempRow < 4))
                            {
                                string tempValue = keypadLayout[tempRow][tempColumn];
                                if (!tempValue.Equals(String.Empty) && !temp.Contains(tempValue)) temp.Add(tempValue);
                            }
                        }

                        for (int b = -1; b < 2; b++)
                        {
                            int tempColumn = column + b;
                            int tempRow = row;
                            if ((tempColumn >= 0 && tempColumn < 3) && (tempRow >= 0 && tempRow < 4))
                            {
                                string tempValue = keypadLayout[tempRow][tempColumn];
                                if (!tempValue.Equals(String.Empty) && !temp.Contains(tempValue)) temp.Add(tempValue);
                            }
                        }
                    }
                }

                possibleCombinations[i] = temp.ToArray();
                temp.Clear();
            }
            return possibleCombinations;
        }

        public static List<string> GetResult(string[][] possibleCombinations)
        {
            List<string> temp = new();
            string[] result = possibleCombinations[0];

            for (int j = 1; j < possibleCombinations.Length; j++)
            {
                string[] currentAddValues = possibleCombinations[j];
                for (int y = 0; y < result.Length; y++)
                    for (int t = 0; t < currentAddValues.Length; t++)
                        temp.Add(string.Concat(result[y], currentAddValues[t]));

                result = temp.ToArray();
                temp.Clear();
            }
            return result.ToList();
        }
    }
}
