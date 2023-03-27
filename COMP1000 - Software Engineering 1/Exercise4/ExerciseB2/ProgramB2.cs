using System;
using System.Collections.Generic;
using System.IO;

namespace Exercise
{
    public class ProgramB2
    {

        public enum ERRORSTATE { None = 0, FileIssue = -2, CouldNotOpen = -2, DirectoryIssue = -3, CouldNotClose = -4 };

        /// <summary>
        /// storage for the name of the openedFile
        /// </summary>
        public string openedFileName = string.Empty;

        /// <summary>
        /// should be used for remembering which file has been openend in persistent mode
        /// </summary>
        protected StreamReader openedFile = null;

        /// <summary>
        /// Do not modify this method as it allows inspection of the object variable
        /// </summary>
        /// <returns>the variable openedFile that contains the fileName of the persistently openedFile</returns>
        public StreamReader GetOpenedFile()
        {
            return openedFile;
        }

        /// <summary>
        /// checks a local file exists and is readable<br/>
        /// Try to make use of try-catch blocks over other methods
        /// </summary>
        /// <param name="fileName">a local file name</param>
        /// <returns> Returns error state if directory does not exist, if file could not be opened; None if everything worked</returns>
        public ERRORSTATE CheckFileAvailability(string fileName)
        {
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileName);
            var folder = fileName.Split("/");

            if (Directory.Exists(folder[0]))
            {
                if (File.Exists(path))
                {
                    return ERRORSTATE.None;
                }

                else
                {
                    return ERRORSTATE.CouldNotOpen;
                }
            }

            else
            {
                return ERRORSTATE.DirectoryIssue;
            }
        }

        /// <summary>
        /// opens a local file from a given path such as Exercise.Tests/files/text1.txt<br/>
        /// The method reads from the opened file all lines between the given line numbers, from and to. 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="from">first line to read</param>
        /// <param name="to">last line to read</param>
        /// <returns>returns the  lines from the given file or an empty string array if the file does not exist</returns>
        public string[] NonPersistentSpecificLineReading(string fileName, int from, int to)
        {
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(path))
            {
                string[] readFile = File.ReadAllLines(path);
                int howMany = (to - from) + 1;
                string[] file = new string[howMany];

                for (int i = 0; i < howMany; i++)
                {
                    file[i] = readFile[i + from];
                }

                return file;
            }

            else
            {
                return new string[0];
            }
        }

        /// <summary>
        /// opens a local file from a given path such as Exercise.Tests/files/text1.txt
        /// use the provided object variables to remember what you opened
        /// The method makes use of object variables to offer persistent access to data.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>returns an error state if directory does not exist, if file could not be opened, None if everything worked
        /// return error state with file issue if the file is already open</returns>
        public ERRORSTATE PersistentFileOpen(string fileName)
        {
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileName);
            var folder = fileName.Split("/");

            try
            {
                if (openedFileName == fileName)
                {
                    return ERRORSTATE.FileIssue;
                }
                else
                {
                    openedFile = new StreamReader(path);
                    openedFileName = fileName;
                }
            }
            catch
            {
                if (Directory.Exists(folder[0]))
                {
                    return ERRORSTATE.FileIssue;
                }
                else
                {
                    return ERRORSTATE.DirectoryIssue;
                } 
            }

            return ERRORSTATE.None;
        }

        /// <summary>
        /// closes a local file from a given path such as Exercise.Tests/files/text1.txt
        /// use the provided object variables to recall what should be open
        /// The method makes use of object variables to offer persistent access to data.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>returns an error state if file could not be closed, or None if everything worked or if the file never worked a file issue</returns>
        public ERRORSTATE PersistentFileClose(string fileName)
        {
            try
            {
                if (fileName == openedFileName)
                {
                    openedFile.Close();
                    openedFile = null;
                    openedFileName = String.Empty;
                    return ERRORSTATE.None;
                }
                else
                {
                    if (openedFile == null)
                    {
                        return ERRORSTATE.CouldNotClose;
                    }
                    else
                    {
                        return ERRORSTATE.FileIssue;
                    }
                }
            }
            catch
            {
                return ERRORSTATE.CouldNotClose;
            }
        }

        /// <summary>
        /// uses the last opened file and reads a given number of lines
        /// if called a second time it again reads from the beginning of the file
        /// The method makes use of object variables to offer persistent access to data.
        /// </summary>
        /// <param name="numOfLines">the amount of lines to read</param>
        /// <returns>returnslines from the given file or an empty string array if the file does not exist or is not open</returns>
        public string[] PersistentFileRead(int numOfLines)
        {
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, openedFileName);

            if (File.Exists(path))
            {
                string[] readFile = File.ReadAllLines(path);
                string[] file = new string[numOfLines];

                for (int i = 0; i < numOfLines; i++)
                {
                    file[i] = readFile[i];
                }

                return file;
            }
            else
            {
                return new string[0];
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Exercise 4B2!");
        }
    }
}
