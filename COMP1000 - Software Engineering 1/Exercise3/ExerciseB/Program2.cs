using System;
using System.Collections.Generic;

namespace Exercise
{
    public class Program2
    {


        /// <summary>
        ///  extract the operations and values from within the string using the provided split mark and return the result
        ///  of the formula
        ///  example "3 + 4 - 7" result: 0; example2: " 3 -  4 * 7 + 25" result: 0
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public int PerformNonNaiveCalculation(string formula, string mark)
        {
            string[] splitText = formula.Split(mark);
            int sum = 0;
            bool isNumber = int.TryParse(splitText[0], out sum);

            for (int i = 1; i < splitText.Length; i += 2)
            {
                string operators = splitText[i].Trim();

                if (operators == "*")
                {
                    int number = 0;
                    bool isNumber2 = int.TryParse(splitText[i + 1], out number);
                    sum *= number;
                }

                else if (operators == "/")
                {
                    int number = 0;
                    bool isNumber2 = int.TryParse(splitText[i + 1], out number);
                    sum /= number;
                }

                else if (i < splitText.Length - 2)
                {
                    string nextOperator = splitText[i + 2].Trim();

                    if (nextOperator == "*")
                    {
                        int num1 = 0;
                        bool isNumber1 = int.TryParse(splitText[i + 1], out num1);
                        int num2 = 0;
                        bool isNumber2 = int.TryParse(splitText[i + 3], out num2);
                        int num3 = num1 * num2;

                        if (i < splitText.Length - 4)
                        {
                            string nextNextOperator = splitText[i + 4].Trim();

                            if (nextNextOperator == "*")
                            {
                                int num4 = 0;
                                bool isNumber4 = int.TryParse(splitText[i + 5], out num4);
                                int num5 = num3 * num4;

                                if (operators == "+")
                                {
                                    sum += num5;
                                }
                                else if (operators == "-")
                                {
                                    sum -= num5;
                                }

                                i += 4;
                            }

                            else if (nextNextOperator == "/")
                            {
                                int num4 = 0;
                                bool isNumber4 = int.TryParse(splitText[i + 5], out num4);
                                int num5 = num3 / num4;

                                if (operators == "+")
                                {
                                    sum += num5;
                                }
                                else if (operators == "-")
                                {
                                    sum -= num5;
                                }

                                i += 4;
                            }

                            else if (nextNextOperator == "+" || nextNextOperator == "-")
                            {
                                if (operators == "+")
                                {
                                    sum += num3;
                                }
                                else if (operators == "-")
                                {
                                    sum -= num3;
                                }

                                i += 2;
                            }
                        }

                        else if (i >= splitText.Length - 4)
                        {
                            if (operators == "+")
                            {
                                sum += num3;
                            }
                            else if (operators == "-")
                            {
                                sum -= num3;
                            }

                            i += 2;
                        }
                    }

                    else if (nextOperator == "/")
                    {
                        int num1 = 0;
                        bool isNumber1 = int.TryParse(splitText[i + 1], out num1);
                        int num2 = 0;
                        bool isNumber2 = int.TryParse(splitText[i + 3], out num2);
                        int num3 = num1 / num2;

                        if (i < splitText.Length - 4)
                        {
                            string nextNextOperator = splitText[i + 4].Trim();

                            if (nextNextOperator == "*")
                            {
                                int num4 = 0;
                                bool isNumber4 = int.TryParse(splitText[i + 5], out num4);
                                int num5 = num3 * num4;

                                if (operators == "+")
                                {
                                    sum += num5;
                                }
                                else if (operators == "-")
                                {
                                    sum -= num5;
                                }

                                i += 4;
                            }

                            else if (nextNextOperator == "/")
                            {
                                int num4 = 0;
                                bool isNumber4 = int.TryParse(splitText[i + 5], out num4);
                                int num5 = num3 / num4;

                                if (operators == "+")
                                {
                                    sum += num5;
                                }
                                else if (operators == "-")
                                {
                                    sum -= num5;
                                }

                                i += 4;
                            }

                            else if (nextNextOperator == "+" || nextNextOperator == "-")
                            {
                                if (operators == "+")
                                {
                                    sum += num3;
                                }
                                else if (operators == "-")
                                {
                                    sum -= num3;
                                }

                                i += 2;
                            }

                        }

                        else if (i >= splitText.Length - 4)
                        {
                            if (operators == "+")
                            {
                                sum += num3;
                            }
                            else if (operators == "-")
                            {
                                sum -= num3;
                            }

                            i += 2;
                        }
                    }

                    else if (operators == "+")
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

                else if (operators == "+")
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



        static void Main(string[] args)
        {
            Console.WriteLine("This is the combined Test Set For Exercises 3 PartB!");
        }
    }
}
