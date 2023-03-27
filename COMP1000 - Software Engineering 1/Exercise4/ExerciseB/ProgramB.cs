using System;
using System.Collections.Generic;
using System.IO;

namespace Exercise
{
    public class ProgramB
    {
        /// <summary>
        /// opens a local file from a given path such as Exercise.Tests/files/text1.txt
        /// and checks if the file or surrounding folder exists
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>-1 if directory does not exist, 1 if file could not be opened; 0 if everything worked</returns>
        public int AccessFile(string fileName)
        {
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileName);
            var folder = fileName.Split("/");

            if (Directory.Exists(folder[0]))
            {
                if (File.Exists(path))
                {
                    return 0;
                }

                else
                {
                    return 1;
                }
            }

            else
            {
                return -1;
            }
        }

        /// <summary>
        ///  opens a local file from a given path such as Exercise.Tests/files/text1.txt and returns the text contained in the file
        ///  If the file is not present return null instead
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="howMany">number of lines to return</param>
        /// <returns> the first lines from the given file or an empty string array if the file does not exist</returns>
        public string[] ReadFromFile(string fileName, int howMany)
        {
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(path))
            {
                string[] readFile = File.ReadAllLines(path);
                string[] file = new string[howMany];

                for (int i = 0; i < howMany; i++)
                {
                    file[i] = readFile[i];
                }

                return file;
            }

            else
            {
                return null;
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Exercise 4B!");
        }
    }
}
