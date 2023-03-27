using System;
using System.Collections.Generic;
using System.IO;

namespace Exercise
{

    public class ProgramB
    {

        /// <summary>
        ///Split the given text using the given character and
        ///shuffle the strings so that the elements are concatinated in reverse order
        ///using the separator to connect them.
        ///
        /// Now also make sure to keep the dot at the end of the sentence and that
        /// all words in the sentence are lower case except for the sentence start
        /// example: "Hello my friend.", ' ' turns into "Friend my hello."
        /// </summary>
        /// <param name="sentence">a sample sentence to read</param>
        /// <param name="mark">the separator used between the words</param>
        /// <returns></returns>
        public string ReorderSentence(string sentence, char mark)
        {
            string[] words = sentence.Split(mark);
            string[] moreWords = new string[words.Length];
            words[words.Length - 1] = words[words.Length - 1].Replace(".", string.Empty);
            words[0] = words[0].ToLower();

            for (int i = 0; i < words.Length; i++)
            {
                moreWords[i] = words[words.Length - i - 1];
            }

            moreWords[words.Length - 1] += ".";

            char[] chars = new char[moreWords[0].Length];

            for (int i = 0; i < moreWords[0].Length; i++)
            {
                chars[i] = moreWords[0][i];
            }

            chars[0] = char.ToUpper(chars[0]);
            string firstWord = new string(chars);

            moreWords[0] = firstWord;

            string newSentence = string.Join(mark, moreWords);
            return newSentence;
        }

        /// <summary>
        /// Adds a subset of integer numbers. Each number used must be smaller than boundary
        /// 
        /// NO advanced mibaries are allowed to solve this method
        /// 
        /// </summary>
        /// <param name="array">the input dataset</param>
        /// <param name="boundary">the threshold which defines the value above the maximum number that should considered for addition</param>
        /// <returns>the sum of all numbers that are below the threshold</returns>
        public int AddValuesBelowBoundary(int[] array, int boundary)
        {
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < boundary)
                {
                    sum += array[i];
                }
            }

            return sum;
        }

        /// <summary>
        /// Retrieve Substring between start and finish.
        /// Split the given string and create a new string that combines all split elements including the split character
        /// from the start and before the finish index ( cuts in the text) .
        /// </summary>
        /// <param name="original">The input text which contains a number of potential split characters</param>
        /// <param name="split">the character used to cut the original</param>
        /// <param name="start">the first part to take and add it to the return value</param>
        /// /// <param name="finish">the index beyond the values to take into account</param>
        /// <returns></returns>
        public string FindSpecificSubText(string original, char split, int start, int finish)
        {
            string[] sentence = original.Split(split);
            string[] subSentence = new string[finish - start];

            for (int i = start; i < finish; i++)
            {
                subSentence[i - start] = sentence[i];
            }

            string subString = string.Join(split, subSentence);

            return subString;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Exercise 5B!");
        }
    }
}
