using System;
using System.Collections.Generic;


namespace Exercise
{
    public class Program1
    {
        public int ReturnNumercial(string a, char b, int c)
        {
            // From the given set of inputs return the numercial object

            return c;
        }

        public object PicksLetter(string a, char b, int c)
        {
            // From the given set of inputs return the primitive letter object

            return b;  
        }

        public string RetrievesShorestText(string a, string b, string c)
        {
            // From the given set of inputs return the shortest text object

            int lengthA = a.Length;
            int lengthB = b.Length;
            int lengthC = c.Length;

            if (lengthA < lengthB && lengthA < lengthC)
            {
                return a;
            }

            else if (lengthB < lengthC)
            {
                return b;
            }

            else
            {
                return c;
            }
        }

        public int CalculatSumA(int number1, int number2, string number3)
        {
            // Calculate the sum of all numeric objects and return it

            int sum = number1 + number2;
            return sum;

        }

        public float CalculatSumB(float number1, char number2, int number3, float number4)
        {
            // Calculate the sum of all numeric objects and return it

            float sum = number1 + number3 + number4;
            return sum;
        }

        public float CalculatSum3(float [] numbers)
        {
            // Calculate the sum of all numeric objects and return it
            float sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        public string ReturnFirstSubText(string text1, char character)
        {
            // From the the given text, split the text using the 
            // given character and return the first text part 

            string[] splitText = text1.Split(character);
            return splitText[0];
        }

        public string ReturnThirdSubText(string text1, char character, int n)
        {
            // From the the given text, split it using the 
            // given character and return the <n>th text part  

            string[] splitText = text1.Split(character);
            return splitText[n-1];
        }

        public int CutCounter(string text1, char splitChar)
        {
            // Split the given text using the given character 
            // and return the number of cuts made in the text
            string[] splitText = text1.Split(splitChar);
            int numberCuts = splitText.Length - 1;
            return numberCuts;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("This is the combined Test Set For Exercises 2 & 3!");



            

        }
    }
}
