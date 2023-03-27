using System;
using System.Collections.Generic;
using System.IO;

namespace Exercise
{
    /// <summary>
    /// Program should implement and complete the IMovement interface given in IMovement.cs
    /// </summary>
    public class ProgramA : IMovement
    {
        public int Move(char direction)
        {
            if (direction.ToString().ToLower() == "w" || direction.ToString().ToLower() == "i")
            {
                return 0;
            }
            else if (direction.ToString().ToLower() == "a" || direction.ToString().ToLower() == "j")
            {
                return 3;
            }
            else if (direction.ToString().ToLower() == "s" || direction.ToString().ToLower() == "k")
            {
                return 1;
            }
            else if (direction.ToString().ToLower() == "d" || direction.ToString().ToLower() == "l")
            {
                return 2;
            }

            return -1;
        }

        public IMovement.Compass Move(int direction)
        {
            if (direction == 0)
            {
                return IMovement.Compass.North;
            }
            else if (direction == 1)
            {
                return IMovement.Compass.South;
            }
            else if (direction == 2)
            {
                return IMovement.Compass.East;
            }
            else if (direction == 3)
            {
                return IMovement.Compass.West;
            }

            return IMovement.Compass.North;
        }

        public int Move(IMovement.Compass direction)
        {
            if (direction == IMovement.Compass.North)
            {
                return 0;
            }
            else if (direction == IMovement.Compass.South)
            {
                return 1;
            }
            else if (direction == IMovement.Compass.East)
            {
                return 2;
            }
            else if (direction == IMovement.Compass.West)
            {
                return 3;
            }

            return -1;
        }

        public string CapitaliseText(string[] message)
        {
            string[] strings = new string[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                char[] word = new char[message[i].Length];

                for (int j = 0; j < message[i].Length; j++)
                {
                    word[j] = message[i][j];
                }

                for (int j = 0; j < message[i].Length; j++)
                {
                    word[j] = char.ToUpper(word[j]);
                }

                string theWord = new string(word);
                strings[i] = theWord;
            }

            string sentence = string.Join(" ", strings);
            return sentence;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Exercise 5A!");
        }


    }
}
