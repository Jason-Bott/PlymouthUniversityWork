using System;
using System.Collections.Generic;

namespace Exercise
{
    /// <summary>
    /// In this exercise you are to work without using any looping structure but
    /// rely only on recusive methods to solve the problems
    /// </summary>
    public class ProgramA2
    {

        /// <summary>
        /// Multiply all values from the array using recursive calls.<br/>
        /// You are also not allowed to use object variables or loops to save the state.
        /// </summary>
        /// <param name="values">A number of integers to sum up</param>
        /// <param name="position">A value to check the position in the array. 
        /// Initially position will be zero.</param>
        /// <returns>the resulting product </returns>
        public int RecursiveMultiplicationFromFirst(int[] values, int position)
        {
            if (position < values.Length)
            {
                return values[position] * RecursiveMultiplicationFromFirst(values, position+1);
            }

            return 1;
        }

        /// <summary>
        /// Add all values from the array. <br/>
        /// You are also not allowed to use object variables or loops.
        /// </summary>
        /// <param name="values">A number of integers to sum up</param>
        /// <param name="position">You can use this value to check the position in the array. 
        /// Initially position will contain the last index you should be using.</param>
        /// <returns>Return the resulting value using recursion only.</returns>
        public int RecursiveAdditionFromLast(int [] values,int position)
        {
            if (position >= 0)
            {
                return values[position] + RecursiveAdditionFromLast(values, position - 1);
            }

            return 0;
        }



        /// <summary>
        /// Adds all values from the list and returns the total sum.
        /// 
        /// Add up all values from the list without using extra functionality
        /// </summary>
        /// <param name="values">a list of float values to add</param>
        /// <returns>the result of the summation with an accurary of 2 decimal places</returns>
        public double RecursiveSum(List<double> values)
        {
            if (values.Count > 0)
            {
                decimal sum = Convert.ToDecimal(values[0]);
                values.RemoveAt(0);
                return Convert.ToDouble(Decimal.Round(sum + Convert.ToDecimal(RecursiveSum(values)), 2));
            }

            return 0;
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Exercise 4A2!");
        }
    }
}
