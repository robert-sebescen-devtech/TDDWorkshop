using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD
{
    public class StringCalculator
    {
        public int Add(string commaSeparatedNumbers)
        {
            if (commaSeparatedNumbers.Equals(string.Empty))
            {
                return 0;
            }

            bool hasCustomDelimiter = HasCustomDelimiter(commaSeparatedNumbers);
            char customDelimiter = ';';
            if(hasCustomDelimiter)
            {
                customDelimiter = GetCustomDelimiter(commaSeparatedNumbers);
                commaSeparatedNumbers = commaSeparatedNumbers.Substring(4);
            }

            char[] separators = hasCustomDelimiter ? new char[] { customDelimiter } : new char[] {',', '\n'};

            int[] numbers = commaSeparatedNumbers.Split(separators).Select(int.Parse).ToArray();

            CheckForNegatives(numbers);

            return numbers.Sum();
        }

        private void CheckForNegatives(int[] numbers)
        {
            List<int> negatives = numbers.Where(number => number < 0).ToList();
            if (negatives.Count > 0)
            {
                throw new Exception(string.Join(",", negatives));
            }
        }

        private static char GetCustomDelimiter(string commaSeparatedNumbers)
        {
            return commaSeparatedNumbers[2];
        }

        private static bool HasCustomDelimiter(string commaSeparatedNumbers)
        {
            return commaSeparatedNumbers.StartsWith("//");
        }
    }
}
