using System;
using System.Collections.Generic;


namespace Exercise
{
    public class Program1
    {
        /// <summary>
        /// From the given source string extract a specific letter. To find the letter cut  the string using "mark1" and select from the parts the "counterA"'s element.
        /// From this element return the "counterB"'s letter. If the letter is an empty char return the first letter after it.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="mark1"></param>
        /// <param name="counterA"></param>
        /// <param name="counterB"></param>
        /// <returns></returns>
        public char ExtractCharacter(string source, string mark1, int counterA, int counterB)
        {
            string[] splitText = source.Split(mark1);
            string element = splitText[counterA];
            return element[counterB];
        }

        /// <summary>
        /// Sums up all elements of the list and returns the final result
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public float CalculateSumOfElements(List<float> values)
        {
            float sum = 0;

            foreach (float value in values)
            {
                sum += value;
            }

            return sum;
        }

        /// <summary>
        /// return the amount of negative numbers contained in the strings
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public int CountNegativeNumbers(List<string> array)
        {
            int count = 0;

            foreach (string element in array)
            {
                int number = 0;
                bool isNumber = int.TryParse(element, out number);
                if (number < 0)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// return the outcome of adding all negative numbers between -2 and -8 (exclusiding those values) contained in the strings
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public int AddNegNumbersFromFixedInterval(List<string> array)
        {
            int sum = 0;

            foreach (string element in array)
            {
                int number = 0;
                bool isNumber = int.TryParse(element, out number);
                if (number < -2 && number > -8)
                {
                    sum += number;
                }
            }

            return sum;
        }

        /// <summary>
        /// Sum up the values of the list but only consider the elements 
        /// which are larger than the lower threshold 
        /// and smaller than the upper threshold.
        /// example [1,2,3,4,5], lower>2, upper=4, => result = 7
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public int SumValuesWithinInterval(List<int> numbers, int lower, int upper)
        {
            int sum = 0;

            foreach (int number in numbers)
            {
                if (number > lower && number <= upper)
                {
                    sum += number;
                }
            }

            return sum;
        }

        /// <summary>
        ///  extract the operations and values from within the string using the provided split token and return the result
        ///  of the formula
        ///  example "3 + 4 - 7" result: 0; example2: " 3 -  4 * 7 " result: -1
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public int PerformNaiveCalculation(string formula, string token)
        {
            string[] splitText = formula.Split(token);
            int sum = 0;
            bool isNumber = int.TryParse(splitText[0], out sum);

            for (int i = 1; i < splitText.Length; i+=2)
            {
                string operators = splitText[i].Trim();

                if (operators == "+")
                {
                    int number = 0;
                    bool isNumber2 = int.TryParse(splitText[i + 1], out number);
                    sum += number;
                }
                else if (operators == "-")
                {
                    int number = 0;
                    bool isNumber2 = int.TryParse(splitText[i + 1], out number);
                    sum -= number;
                }
            }

            return sum;

        }

        /// <summary>
        /// return the outcome of adding all values where the key contains either "tokenA" or "tokenB"
        /// do not use Select or Lamdas or anything besides loops and conditional checks, doing so will fail the entire exercise 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public double ExtractAndCalculateValues(Dictionary<string, double> dictionary, string tokenA, string tokenB)
        {
            double sum = 0;

            foreach (KeyValuePair<string, double> i in dictionary)
            {
                if (i.Key.Contains(tokenA) || i.Key.Contains(tokenB))
                {
                    sum += i.Value;
                }
            }

            return sum;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("This is the combined Test Set For Exercises 3 PartA!");



        }
    }
}
