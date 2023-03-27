using System;
using System.Collections.Generic;

namespace Exercise
{
    public class ProgramA
    {

        /// <summary>
        /// From the given 2 arrays create a dictiory that uses the element from alpha array as key and the corresponding element from the beta array as value.
        ///
        /// </summary>
        /// <param name="beta">values</param>
        /// <param name="alpha">keys</param>
        /// <returns></returns>     
        public Dictionary<int, string> BuildDictionary(int [] alpha, string [] beta)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string> (alpha.Length);
            for (int i = 0; i < alpha.Length; i++)
            {
                dictionary.Add (alpha[i], beta[i]);
            }
            return dictionary;
        }

        /// <summary>
        /// Fromt the given 2 arrays create a dictionarry that uses the element from beta array as key and the corresponding element from the alpha array as value.
        /// The data could be compromised so make use of a try catch block. 
        /// Do not remove the broken pair but for the broken keys use the position of the value in its array as key. 
        /// For broken values use "missing" 
        /// If a key has already been used increase its numeric value by one (assume an int value to start with).
        ///
        /// </summary>
        /// <param name="beta">keys</param>
        /// <param name="alpha">values</param>
        /// <returns></returns>
        public Dictionary<string, string> BuildDictionaryBeta( string[] alpha, string[] beta)
        {
            int dictionaryLength = 0;
            if (alpha.Length > beta.Length)
            {
                dictionaryLength = alpha.Length;
            }
            else
            {
                dictionaryLength = beta.Length;
            }

            Dictionary<string, string> dictionary = new Dictionary<string, string> (dictionaryLength);
            int keyNum = 0;
            string keyString = "";
           
            for (int i = 0; i < dictionaryLength; i++)
            {
                if (i < beta.Length)
                {
                    bool duplicate = false;
                    foreach (KeyValuePair<string, string> j in dictionary)
                    {
                        if (j.Key == beta[i])
                        {
                            duplicate = true;
                        }
                    }

                    if (duplicate == true)
                    {
                        keyNum = int.Parse(beta[i]) + 1;
                        keyString = keyNum.ToString();
                    }
                    else
                    {
                        keyString = beta[i];
                    }
                }
                else
                {
                    keyNum = i;
                    keyString = keyNum.ToString();
                }


                string value = "";
                if (i < alpha.Length)
                {
                    value = alpha[i];
                }
                else
                {
                    value = "missing";
                }
                

                dictionary.Add(keyString, value);
            }

            return dictionary;
        }

        /// <summary>
        ///  remove from the dictionary all pairs where the key contains clean
        ///  add a new pair to the dictionary with key = "sum" as key the summation of all remaining values
        ///  add a new pair to the dictionary with key = "mult" as key the product of all remaining values
        /// </summary>
        /// <param name="data"></param>
        /// <param name="clean"></param>
        public void ModifyDictionary(Dictionary<string, int> data, string clean)
        {
            //Do something with the dict here!
            int sum = 0;
            int mult = 0;
            foreach (KeyValuePair<string, int> i in data)
            {
                if (i.Key.Contains(clean))
                {
                    continue;
                }

                else
                {
                    mult = 1;
                }
            }

            foreach (KeyValuePair<string, int> i in data)
            {
                if (i.Key.Contains(clean))
                {
                    data.Remove(i.Key);
                }

                else
                {
                    sum += i.Value;
                    mult *= i.Value;
                }
            }

            data.Add("sum", sum );
            data.Add("mult", mult);
        }

        /// <summary>
        /// remove from the dictionary all pairs where the key contains the substring "marker"
        /// Create and return a new list which contains the remaining values. 
        /// At the last but one position in the new list save the sum of those values and and at the last position the divsion.
        /// If the division is not possible add NaN.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="marker">the marker for identifying elements</param>
        /// <returns></returns>
        public List<double> ModifyDictionaryGamma(Dictionary<string, double> data, string marker)
        {
            List<double> result = new List<double>();
            double sum = 0;
            double mult = 1;

            foreach (KeyValuePair<string, double> i in data)
            {
                if (i.Key.Contains(marker))
                {
                    data.Remove(i.Key);
                }
                else
                {
                    sum += i.Value;
                    mult *= i.Value;
                    result.Add(i.Value);
                }
            }

            result.Add(sum);
            result.Add(mult);
            return result;
        }

        /// <summary>
        /// remove from the dictionary all pairs where the values are positive
        /// add a new pair to the dictionary with key = "sum" as key the summation of all remaining values
        ///add a new pair to the dictionary with key = "mult" as key the product of all remaining values
        ///add a new pair to the dictionary with key = "div" as key the division of all remaining values
        ///round the appropriate values to 10 decimal places
        /// </summary>
        /// <param name="data"></param>
        /// <returns>the updated dictionary</returns>
        public Dictionary<string, double> ModifyDictionaryBeta(Dictionary<string, double> data)
        {
            double sum = 0;
            double mult = 1;
            double div = 0;
            bool NaN = false;

            foreach (KeyValuePair<string, double> i in data)
            {
                if (i.Value > 0)
                {
                    data.Remove(i.Key);
                }
                else if (div == 0)
                {
                    sum += i.Value;
                    mult *= i.Value;
                    div = i.Value;
                }
                else if (i.Value == 0)
                {
                    sum += i.Value;
                    mult *= i.Value;
                    NaN = true;
                }
                else
                {
                    sum += i.Value;
                    mult *= i.Value;
                    div /= i.Value;
                }

            }

            data.Add("sum", sum);
            data.Add("mult", mult);
            if (NaN == true)
            {
                div = double.NaN;
                data.Add("div", div);
            }
            else
            {
                data.Add("div", div);
            }
            return data;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Exercise 4A!");
        }


    }
}
