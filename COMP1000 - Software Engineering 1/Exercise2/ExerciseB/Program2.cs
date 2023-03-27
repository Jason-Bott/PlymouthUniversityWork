using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise
{
    public class Program2
    {
        /// <summary>
        /// From the given inputs, this method pick the smallest numeric object and returns it.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <param name="obj3"></param>
        /// <param name="obj4"></param>
        /// <returns></returns>
        public float ReturnSmallestNumber(int obj1, int obj2, float obj3, string obj4)
        {
            if (obj1 < obj2 && obj1 < obj3)
            {
                return obj1;
            }

            else if (obj2 < obj3)
            {
                return obj2;
            }
            
            else
            {
                return obj3;
            }
        }

        /// <summary>
        /// The method cuts the given text using the given character and returns the first
        /// numeric integer value that is contained on either side of the cuts.
        /// 
        /// example: "11bhive",'b' returns 11
        /// </summary>
        /// <param name="text1">input text</param>
        /// <param name="character">split character</param>
        /// <returns></returns>
        public int RetrieveNumberFromSubText(string text1, char character)
        {
            string[] splitText = text1.Split(character);

            for (int i = 0; i < splitText.Length; i++)
            {
                bool isNumber = splitText[i].Any(char.IsDigit);
                bool isFloat = splitText[i].Contains(".");
                bool hasLetters = splitText[i].Any(char.IsLetter);

                if (isNumber == true && isFloat == false && hasLetters == false)
                {
                    int x = Int32.Parse(splitText[i]);
                    return x;
                }
            }

            return -1;
        }

        /// <summary>
        /// Split the given text using the given character and 
        /// shuffle the strings so that the elements are concatinated in reverse order
        /// using the mark again to join them.
        /// example: "hello my friend",' ' turns into "friend my hello"
        /// </summary>
        /// <param name="text1">original text</param>
        /// <param name="mark">cut mark</param>
        /// <returns></returns>
        public string SplitShuffleAndReverseWords(string text1, string mark)
        {
            string[] splitText = text1.Split(mark);
            Array.Reverse(splitText);
            string text2 = string.Join(mark, splitText);

            return text2;
        }

        /// <summary>
        /// Method takes the given text and returns the character of the string at the given position.
        /// If the value is negative start from the end of the string.
        /// example: "hello my friend",1 returns 'e' 
        /// </summary>
        /// <param name="text1">original text</param>
        /// <param name="position">position to pick the character</param>
        /// <returns></returns>
        public char GetValueAtPosition(string text1, int position)
        {
            if (position < 0)
            {
                int newPosition = position + text1.Length;
                return text1[newPosition];
            }

            else
            {
                return text1[position];
            }
        }

        /// <summary>
        /// Method takes the given text and returns the text with the capitulation inversed
        /// example1: "hello my friend" returns "HELLO MY FRIEND" 
        /// example2: "HELLO my FRIEND" returns "hello MY friend"
        /// </summary>
        /// <param name="text1"></param>
        /// <returns></returns>
        public string ChangeTheTextA(string text1)
        {
            char[] charArray = text1.ToCharArray();

            for (int i = 0; i < text1.Length; i++)
            {
                bool upper = Char.IsUpper(charArray[i]);
                bool lower = Char.IsLower(charArray[i]);

                if (upper == true)
                {
                    charArray[i] = Char.ToLower(charArray[i]);
                }

                else if (lower == true)
                {
                    charArray[i] = Char.ToUpper(charArray[i]);
                }
            }

            string text2 = new string(charArray);
            return text2;
        }

        /// <summary>
        /// Method takes the given text and returns the text in upper case
        /// example: "hello my friend" returns "HELLO MY FRIEND" 
        /// </summary>
        /// <param name="text1">mixed case input text</param>
        /// <returns></returns>
        public string ChangeTheTextB(string text1)
        {
            string text2 = text1.ToUpper();
            return text2;
        }



        /// <summary>
        /// Splits the given text using the given mark and 
        /// shuffles the words of the sentence so that the elements are concatinated in reverse order 
        /// using the replament element to connect them.
        /// Now also make sure to keep the dot at the end of the sentence and that the all words in the sentence 
        /// are lower case except for the sentence start 
        /// example: "Hello my friend."," "," " turns into "Friend my hello."
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public string ShuffelTheTextAdvanced(string text1, string mark, string replacement)
        {
            string textNoDot = text1.Replace(".", "");
            string textLower = textNoDot.ToLower();
            string[] words = textLower.Split(mark);
            Array.Reverse(words);
            string textReversed = string.Join(replacement, words);
            string textUpper = char.ToUpper(textReversed[0]) + textReversed.Substring(1);
            string text2 = textUpper + ".";
            return text2;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("This is the combined Test Set For Exercises 2 & 3!");
        }
    }
}
