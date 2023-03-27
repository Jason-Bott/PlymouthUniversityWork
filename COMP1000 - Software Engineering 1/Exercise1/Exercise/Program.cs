using System;
using System.Collections.Generic;

namespace Exercise
{
    public class Writer
    {
        /**
        * Repair the method so that it returns the input instead of an empty string.
        * You need to add and remove some quotation marks in the right place and some semicolons.
        */
        public string FeedbackWhatYouGet(string input)
        {


            return input;
        }

        /**
        * Repair the method so that it returns a fixed value string saying "Nothing" instead of the input.
        * You need to add and remove some quotation marks in the right place and some semicolons.
        */
        public string ReturnFixedValue(string input)
        {
            string value = "Nothing";

            return value;
        }


        /**
        * Repair the method so that a single sentence, integrating all three strings 
        * is returned.
        * You need to add and remove some quotation marks in the right place and some semicolons.
        */
        public string RepairStringsToFormSentence(string begin, string middle, string end)
        {
            string sentence = begin + middle + end;

            return sentence;
        }

        /**
        * Repair the method body so that a single sentence, integrating all three strings 
        * is returned with a single empty space in between the words.
        * You need to add and remove some quotation marks in the right place and some semicolons.
        * You also might need to rearange, remove or add the words to fit the correct order.
        */
        public string RepairStatementsToFormSentence(string end, string middle, string begin)
        {
            string sentence = begin + " " + middle + " " + end;

            return sentence;
        }


        /**
        * Repair the method so that a single sentence, integrating all three strings 
        * is returned in the correct order and with empty spaces in between the words.
        * You need to add and remove some quotation marks in the right place and some semicolons.
        * You should also make sure to remove unnecesary spaces around the sentence fragments.
        * You also might need to rearange, remove or add the words to fit the correct order.
        */
        public string RepairStatementsToFormSentence2(string end, string begin, string middle)
        {
            string sentenceA = begin.Trim() + " " + middle.Trim() + " " + end.Trim();

            return sentenceA;
        }

        /**
         * For each of the sentences take each of the words and form a sentence with spaces in between the words.
         * Return the sentence with a dot at the end.
         */
        public string FormSentenceFromWords(string word1, string word2, string word3)
        {
            string sentenceB = word1 + " " + word2 + " " + word3 + ".";

            return sentenceB;
        }

        /**
         * For each of the sentence fragments check that the sentence 
         * has a dot at the end and that there is a single space between each of the words.
         * You might need to check that there are no extra spaces.
         * Return the created ordered sentence with a dot at the end.
         */
        public string FormSentenceFromInputs(string begin, string middle, string end)
        {
            string sentenceC = begin.Trim() + " " + middle.Trim() + " " + end.Trim() + ".";

            return sentenceC;
        }



        /**
         * Repair the method so that 3 strings are returned.
         * You need to add and remove some quotation marks in the right place and some semicolons.
         */
        public string[] SplitSentence(string sentence)
        {
            char[] seperator = new char[] { ' ' };

            string[] result = sentence.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Without the Tests");
        }
    }
}
