using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace GameDev
{
    /**
     * The main class of the Dungeon Game Application
     * 
     * You may add to your project other classes which are referenced.
     * Complete the templated methods and fill in your code where it says "Your code here".
     * Do not rename methods or variables which already eDist or change the method parameters.
     * You can do some checks if your project still aligns with the spec by running the tests in UnitTest1
     * 
     * For Questions do contact us!
     */
    public class Game
    {
        /**
         * use the following to store and control the movement 
         */
        public enum PlayerActions { NOTHING, NORTH, EAST, SOUTH, WEST, PICKUP, ATTACK, DROP, QUIT };
        private PlayerActions action = PlayerActions.NOTHING;
        public enum GameState { UNKOWN, STOP, RUN, START, INIT };
        private GameState status = GameState.INIT;

        // maps 
        private char[][] originalMap = new char[0][];
        private char[][] workingMap = new char[0][];

        // healths and monster positions
        private bool showM1Health = false;
        private int m1Health = 3;
        private int m1Coins = 0;
        private int[] m1Position = new int[2];
        private bool showM2Health = false;
        private int m2Health = 3;
        private int m2Coins = 0;
        private int[] m2Position = new int[2];
        private int playerHealth = 2;

        //loaded file
        private string mapName = string.Empty;

        //Game completed bool
        private bool Stop = false;

        /**
        * tracks if the game is running
        */
        private bool advanced = false;

        private string currentMap;


        /**
         * Reads user input from the Console
         * 
         * Please use and implement this method to read the user input.
         * 
         * Return the input as string to be further processed
         * 
         */
        private string ReadUserInput()
        {
            //modify
            ConsoleKeyInfo cki;
            string input = "";

            if (status == GameState.RUN)
            {
                cki = Console.ReadKey();
                input = cki.Key.ToString();
            }
            else
            {
                input = Console.ReadLine();
            }

            return input;
        }

        private int counter = -1;

        /// <summary>
        /// Returns the number of steps a player made on the current map. The counter only counts steps, not actions.
        /// </summary>
        public int GetStepCounter()
        {
            //modify
            return counter;
 
        }

        /**
         * Processed the user input string
         * 
         * takes apart the user input and does control the information flow
         *  * initializes the map ( you must call InitializeMap)
         *  * starts the game when user types in Play
         *  * sets the correct playeraction which you will use in the Update
         *  
         *  DO NOT read any information from command line in here but only act upon what the method receives.
         */
        public void ProcessUserInput(string input)
        {
            //modify
            string[] words = input.Split(" ");  //Splits input into an array

            if (status == GameState.RUN)
            {
                if (input.ToLower() == "w")
                {
                    action = PlayerActions.NORTH;
                    counter++;
                }

                else if (input.ToLower() == "a")
                {
                    action = PlayerActions.WEST;
                    counter++;
                }

                else if (input.ToLower() == "s")
                {
                    action = PlayerActions.SOUTH;
                    counter++;
                }

                else if (input.ToLower() == "d")
                {
                    action = PlayerActions.EAST;
                    counter++;
                }
                else if (input.ToLower() == "z")
                {
                    action = PlayerActions.PICKUP;
                }
                else if (input.ToLower() == "q")
                {
                    action = PlayerActions.ATTACK;
                }
                else if (input.ToLower() == "start")
                {
                    action = PlayerActions.NOTHING;
                    counter = 0;
                }
                else
                {
                    action = PlayerActions.NOTHING;
                    Console.Clear();
                    Console.WriteLine("Unkown Command");
                }
            }

            else if (words[0].ToLower() == "load")             //If the first word in the input was "load"
            {
                if (words.Length < 2)
                {
                    Console.Clear();
                    Console.WriteLine("No map file given");
                }
                else
                {
                    mapName = words[1];                         //Stores mapname in private variable
                    bool loadMap = LoadMapFromFile(mapName);    //Call method to load map
                }
            }

            else if (input.ToLower() == "replay")
            {
                m1Health = 3;
                m2Health = 3;
                playerHealth = 2;
                bool loadMap = LoadMapFromFile(mapName);
                status = GameState.RUN;
                counter = 0;
            }

            else if (input.ToLower() == "start" || input.ToLower() == "play")
            {
                if (originalMap.Length > 0)
                {
                    m1Health = 3;
                    m1Coins = 0;
                    m2Health = 3;
                    m2Coins = 0;
                    playerHealth = 2;
                    status = GameState.RUN;
                    counter = 0;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("No Map Loaded");
                }
            }

            else if (input.ToLower() == "advanced")
            {
                if (advanced == true)
                {
                    Console.Clear();
                    Console.WriteLine("Now in normal mode");
                    advanced = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Now in advanced mode");
                    advanced = true;
                }
            }

            else if (input.ToLower() == "quit")
            {
                status = GameState.STOP;
            }

            else
            {
                action = PlayerActions.NOTHING;
                Console.Clear();
                Console.WriteLine("Unkown Command");
            }
        }

        /**
         * The Main Game Loop. 
         * It updates the game state.
         * 
         * This is the method where you implement your game logic and alter the state of the map/game
         * use playeraction to determine how the character should move/act
         * the input should tell the loop if the game is active and the state should advance
         * 
         * Returns true if the game could be updated and is ongoing
         */
        public bool Update(GameState status)
        {
            //modify
            if (status == GameState.RUN)
            {
                Console.Clear();
                int[] position = GetPlayerPosition();

                //Monster
                //Action

                
                if (advanced == true && counter > 0)
                {
                    //Monster 1
                    bool differentAction = false;
                    bool alreadyMoved = false;
                    bool attackPlayer = false;
                    if (m1Health > 0)
                    {
                        if (originalMap[m1Position[0]][m1Position[1]] == char.Parse("C"))
                        {
                            m1Health++;
                            m1Coins++;
                            originalMap[m1Position[0]][m1Position[1]] = char.Parse(".");
                        }

                        if (position[1] < m1Position[1])
                        {
                            for (int i = m1Position[1]; i >= position[1]; i--)
                            {
                                if (workingMap[m1Position[0]][i] == char.Parse("#"))
                                {
                                    differentAction = true;
                                }
                            }

                            if (differentAction == true)
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0]][m1Position[1] - 1] == char.Parse("M"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0]][m1Position[1] - 1] == char.Parse("D"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0]][m1Position[1] - 1] == char.Parse("@"))
                            {
                                attackPlayer = true;
                            }

                            else
                            {
                                workingMap[m1Position[0]][m1Position[1]] = char.Parse(".");
                                workingMap[m1Position[0]][m1Position[1] - 1] = char.Parse("M");
                                m1Position[1] = m1Position[1] - 1;
                                alreadyMoved = true;
                            }
                        }
                        else if (alreadyMoved == false)
                        {
                            differentAction = true;
                        }


                        if (differentAction == true && attackPlayer == false && position[0] > m1Position[0])
                        {
                            differentAction = false;
                            if (workingMap[m1Position[0] + 1][m1Position[1]] == char.Parse("#"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0] + 1][m1Position[1]] == char.Parse("M"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0] + 1][m1Position[1]] == char.Parse("D"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0] + 1][m1Position[1]] == char.Parse("@"))
                            {
                                attackPlayer = true;
                            }

                            else
                            {
                                workingMap[m1Position[0]][m1Position[1]] = char.Parse(".");
                                workingMap[m1Position[0] + 1][m1Position[1]] = char.Parse("M");
                                m1Position[0] = m1Position[0] + 1;
                                alreadyMoved = true;
                            }
                        }
                        else if (alreadyMoved == false && attackPlayer == false)
                        {
                            differentAction = true;
                        }


                        if (differentAction == true && attackPlayer == false && position[0] < m1Position[0])
                        {
                            differentAction = false;
                            if (workingMap[m1Position[0] - 1][m1Position[1]] == char.Parse("#"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0] - 1][m1Position[1]] == char.Parse("M"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0] - 1][m1Position[1]] == char.Parse("D"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0] - 1][m1Position[1]] == char.Parse("@"))
                            {
                                attackPlayer = true;
                            }

                            else
                            {
                                workingMap[m1Position[0]][m1Position[1]] = char.Parse(".");
                                workingMap[m1Position[0] - 1][m1Position[1]] = char.Parse("M");
                                m1Position[0] = m1Position[0] - 1;
                                alreadyMoved = true;
                            }
                        }
                        else if (alreadyMoved == false && attackPlayer == false)
                        {
                            differentAction = true;
                        }


                        if (differentAction == true && attackPlayer == false)
                        {
                            differentAction = false;
                            if (workingMap[m1Position[0]][m1Position[1] + 1] == char.Parse("#"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0]][m1Position[1] + 1] == char.Parse("M"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0]][m1Position[1] + 1] == char.Parse("D"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m1Position[0]][m1Position[1] + 1] == char.Parse("@"))
                            {
                                attackPlayer = true;
                            }

                            else
                            {
                                workingMap[m1Position[0]][m1Position[1]] = char.Parse(".");
                                workingMap[m1Position[0]][m1Position[1] + 1] = char.Parse("M");
                                m1Position[1] = m1Position[1] + 1;
                                alreadyMoved = true;
                            }

                        }

                        if (attackPlayer == true)
                        {
                            playerHealth--;
                            if (playerHealth == 0)
                            {
                                Console.WriteLine("Game Over!");
                                Stop = true;
                            }
                        }
                    }



                    //Monster 2
                    differentAction = false;
                    alreadyMoved = false;
                    attackPlayer = false;
                    if (m2Health > 0)
                    {
                        if (originalMap[m2Position[0]][m2Position[1]] == char.Parse("C"))
                        {
                            m2Health++;
                            m2Coins++;
                            originalMap[m2Position[0]][m2Position[1]] = char.Parse(".");
                        }

                        if (position[1] < m2Position[1])
                        {
                            for (int i = m2Position[1]; i >= position[1]; i--)
                            {
                                if (workingMap[m2Position[0]][i] == char.Parse("#"))
                                {
                                    differentAction = true;
                                }
                            }

                            if (differentAction == true)
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0]][m2Position[1] - 1] == char.Parse("M"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0]][m2Position[1] - 1] == char.Parse("D"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0]][m2Position[1] - 1] == char.Parse("@"))
                            {
                                attackPlayer = true;
                            }

                            else
                            {
                                workingMap[m2Position[0]][m2Position[1]] = char.Parse(".");
                                workingMap[m2Position[0]][m2Position[1] - 1] = char.Parse("M");
                                m2Position[1] = m2Position[1] - 1;
                                alreadyMoved = true;
                            }
                        }
                        else if (alreadyMoved == false)
                        {
                            differentAction = true;
                        }


                        if (differentAction == true && attackPlayer == false && position[0] > m2Position[0])
                        {
                            differentAction = false;
                            if (workingMap[m2Position[0] + 1][m2Position[1]] == char.Parse("#"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0] + 1][m2Position[1]] == char.Parse("M"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0] + 1][m2Position[1]] == char.Parse("D"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0] + 1][m2Position[1]] == char.Parse("@"))
                            {
                                attackPlayer = true;
                            }

                            else
                            {
                                workingMap[m2Position[0]][m2Position[1]] = char.Parse(".");
                                workingMap[m2Position[0] + 1][m2Position[1]] = char.Parse("M");
                                m2Position[0] = m2Position[0] + 1;
                                alreadyMoved = true;
                            }
                        }
                        else if (alreadyMoved == false && attackPlayer == false)
                        {
                            differentAction = true;
                        }


                        if (differentAction == true && attackPlayer == false && position[0] < m2Position[0])
                        {
                            differentAction = false;
                            if (workingMap[m2Position[0] - 1][m2Position[1]] == char.Parse("#"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0] - 1][m2Position[1]] == char.Parse("M"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0] - 1][m2Position[1]] == char.Parse("D"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0] - 1][m2Position[1]] == char.Parse("@"))
                            {
                                attackPlayer = true;
                            }

                            else
                            {
                                workingMap[m2Position[0]][m2Position[1]] = char.Parse(".");
                                workingMap[m2Position[0] - 1][m2Position[1]] = char.Parse("M");
                                m2Position[0] = m2Position[0] - 1;
                                alreadyMoved = true;
                            }
                        }
                        else if (alreadyMoved == false && attackPlayer == false)
                        {
                            differentAction = true;
                        }


                        if (differentAction == true && attackPlayer == false)
                        {
                            differentAction = false;
                            if (workingMap[m2Position[0]][m2Position[1] + 1] == char.Parse("#"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0]][m2Position[1] + 1] == char.Parse("M"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0]][m2Position[1] + 1] == char.Parse("D"))
                            {
                                differentAction = true;
                            }

                            else if (workingMap[m2Position[0]][m2Position[1] + 1] == char.Parse("@"))
                            {
                                attackPlayer = true;
                            }

                            else
                            {
                                workingMap[m2Position[0]][m2Position[1]] = char.Parse(".");
                                workingMap[m2Position[0]][m2Position[1] + 1] = char.Parse("M");
                                m2Position[1] = m2Position[1] + 1;
                                alreadyMoved = true;
                            }
                        }

                        if (attackPlayer == true)
                        {
                            playerHealth--;
                            if (playerHealth == 0)
                            {
                                Console.WriteLine("Game Over!");
                                Stop = true;
                            }
                        }
                    }
                }


                //Player
                //Action
                showM1Health = false;
                showM2Health = false;

                if (action == PlayerActions.NORTH)
                {
                    if (workingMap[position[0] - 1][position[1]] == char.Parse("#"))
                    {
                        Console.WriteLine("There is a wall blocking your path");
                    }

                    else if (workingMap[position[0] - 1][position[1]] == char.Parse("M"))
                    {
                        Console.WriteLine("There is a monster blocking your path");
                    }

                    else if (workingMap[position[0] - 1][position[1]] == char.Parse("D"))
                    {
                        Console.WriteLine("Congratulations you have escaped the dungeon!");
                        workingMap[position[0]][position[1]] = char.Parse(".");
                        workingMap[position[0] - 1][position[1]] = char.Parse("@");
                        Stop = true;
                        action = PlayerActions.NOTHING;
                    }

                    else
                    {
                        if (originalMap[position[0]][position[1]] == char.Parse("C"))
                        {
                            workingMap[position[0]][position[1]] = char.Parse("C");
                        }
                        else
                        {
                            workingMap[position[0]][position[1]] = char.Parse(".");
                        }

                        workingMap[position[0] - 1][position[1]] = char.Parse("@");
                    }
                }

                else if (action == PlayerActions.SOUTH)
                {
                    if (workingMap[position[0] + 1][position[1]] == char.Parse("#"))
                    {
                        Console.WriteLine("There is a wall blocking your path");
                    }

                    else if (workingMap[position[0] + 1][position[1]] == char.Parse("M"))
                    {
                        Console.WriteLine("There is a monster blocking your path");
                    }

                    else if (workingMap[position[0] + 1][position[1]] == char.Parse("D"))
                    {
                        Console.WriteLine("Congratulations you have escaped the dungeon!");
                        workingMap[position[0]][position[1]] = char.Parse(".");
                        workingMap[position[0] + 1][position[1]] = char.Parse("@");
                        Stop = true;
                        action = PlayerActions.NOTHING;
                    }

                    else
                    {
                        if (originalMap[position[0]][position[1]] == char.Parse("C"))
                        {
                            workingMap[position[0]][position[1]] = char.Parse("C");
                        }
                        else
                        {
                            workingMap[position[0]][position[1]] = char.Parse(".");
                        }

                        workingMap[position[0] + 1][position[1]] = char.Parse("@");
                    }
                }

                else if (action == PlayerActions.WEST)
                {
                    if (workingMap[position[0]][position[1] - 1] == char.Parse("#"))
                    {
                        Console.WriteLine("There is a wall blocking your path");
                    }

                    else if (workingMap[position[0]][position[1] - 1] == char.Parse("M"))
                    {
                        Console.WriteLine("There is a monster blocking your path");
                    }

                    else if (workingMap[position[0]][position[1] - 1] == char.Parse("D"))
                    {
                        Console.WriteLine("Congratulations you have escaped the dungeon!");
                        workingMap[position[0]][position[1]] = char.Parse(".");
                        workingMap[position[0]][position[1] - 1] = char.Parse("@");
                        Stop = true;
                        action = PlayerActions.NOTHING;
                    }

                    else
                    {
                        if (originalMap[position[0]][position[1]] == char.Parse("C"))
                        {
                            workingMap[position[0]][position[1]] = char.Parse("C");
                        }
                        else
                        {
                            workingMap[position[0]][position[1]] = char.Parse(".");
                        }

                        workingMap[position[0]][position[1] - 1] = char.Parse("@");
                    }
                }

                else if (action == PlayerActions.EAST)
                {
                    if (workingMap[position[0]][position[1] + 1] == char.Parse("#"))
                    {
                        Console.WriteLine("There is a wall blocking your path");
                    }

                    else if (workingMap[position[0]][position[1] + 1] == char.Parse("M"))
                    {
                        Console.WriteLine("There is a monster blocking your path");
                    }

                    else if (workingMap[position[0]][position[1] + 1] == char.Parse("D"))
                    {
                        Console.WriteLine("Congratulations you have escaped the dungeon!");
                        workingMap[position[0]][position[1]] = char.Parse(".");
                        workingMap[position[0]][position[1] + 1] = char.Parse("@");
                        Stop = true;
                        action = PlayerActions.NOTHING;
                    }

                    else
                    {
                        if (originalMap[position[0]][position[1]] == char.Parse("C"))
                        {
                            workingMap[position[0]][position[1]] = char.Parse("C");
                        }
                        else
                        {
                            workingMap[position[0]][position[1]] = char.Parse(".");
                        }

                        workingMap[position[0]][position[1] + 1] = char.Parse("@");
                    }
                }

                else if (action == PlayerActions.PICKUP)
                {
                    if (advanced == true)
                    {
                        if (originalMap[position[0]][position[1]] == char.Parse("C"))
                        {
                            playerHealth += 1;
                        }
                    }

                    if (originalMap[position[0]][position[1]] == char.Parse("C"))
                    {
                        originalMap[position[0]][position[1]] = char.Parse(".");
                    }
                }

                else if (action == PlayerActions.ATTACK)
                {
                    if (advanced == true)
                    {
                        if (workingMap[position[0] + 1][position[1]] == char.Parse("M"))
                        {
                            if (position[0] + 1 == m1Position[0] && position[1] == m1Position[1])
                            {
                                showM1Health = true;
                                m1Health -= 1;
                                if (m1Health == 0)
                                {
                                    if (m1Coins == 1)
                                    {
                                        workingMap[position[0] + 1][position[1]] = char.Parse("C");
                                        originalMap[position[0] + 1][position[1]] = char.Parse("C");
                                    }

                                    else if (m1Coins > 1)
                                    {
                                        for (int i = 0; i < m1Coins; i++)
                                        {
                                            workingMap[position[0] + 1 + i][position[1]] = char.Parse("C");
                                            originalMap[position[0] + 1 + i][position[1]] = char.Parse("C");
                                        }
                                    }

                                    else
                                    {
                                        workingMap[position[0] + 1][position[1]] = char.Parse(".");
                                    }
                                }
                            }
                            else
                            {
                                showM2Health = true;
                                m2Health -= 1;
                                if (m2Health == 0)
                                {
                                    if (m2Coins == 1)
                                    {
                                        workingMap[position[0] + 1][position[1]] = char.Parse("C");
                                        originalMap[position[0] + 1][position[1]] = char.Parse("C");
                                    }

                                    else if (m2Coins > 1)
                                    {
                                        for (int i = 0; i < m2Coins; i++)
                                        {
                                            workingMap[position[0] + 1 + i][position[1]] = char.Parse("C");
                                            originalMap[position[0] + 1 + i][position[1]] = char.Parse("C");
                                        }
                                    }

                                    else
                                    {
                                        workingMap[position[0] + 1][position[1]] = char.Parse(".");
                                    }
                                }
                            }
                        }
                        else if (workingMap[position[0] - 1][position[1]] == char.Parse("M"))
                        {
                            if (position[0] - 1 == m1Position[0] && position[1] == m1Position[1])
                            {
                                showM1Health = true;
                                m1Health -= 1;
                                if (m1Health == 0)
                                {
                                    if (m1Coins == 1)
                                    {
                                        workingMap[position[0] - 1][position[1]] = char.Parse("C");
                                        originalMap[position[0] - 1][position[1]] = char.Parse("C");
                                    }

                                    else if (m1Coins > 1)
                                    {
                                        for (int i = 0; i < m1Coins; i++)
                                        {
                                            workingMap[position[0] - 1 - i][position[1]] = char.Parse("C");
                                            originalMap[position[0] - 1 - i][position[1]] = char.Parse("C");
                                        }
                                    }

                                    else
                                    {
                                        workingMap[position[0] - 1][position[1]] = char.Parse(".");
                                    }
                                }
                            }
                            else
                            {
                                showM2Health = true;
                                m2Health -= 1;
                                if (m2Health == 0)
                                {
                                    if (m2Coins == 1)
                                    {
                                        workingMap[position[0] - 1][position[1]] = char.Parse("C");
                                        originalMap[position[0] - 1][position[1]] = char.Parse("C");
                                    }

                                    else if (m2Coins > 1)
                                    {
                                        for (int i = 0; i < m2Coins; i++)
                                        {
                                            workingMap[position[0] - 1 - i][position[1]] = char.Parse("C");
                                            originalMap[position[0] - 1 - i][position[1]] = char.Parse("C");
                                        }
                                    }

                                    else
                                    {
                                        workingMap[position[0] - 1][position[1]] = char.Parse(".");
                                    }
                                }
                            }
                        }
                        else if (workingMap[position[0]][position[1] + 1] == char.Parse("M"))
                        {
                            if (position[0] == m1Position[0] && position[1] + 1 == m1Position[1])
                            {
                                showM1Health = true;
                                m1Health -= 1;
                                if (m1Health == 0)
                                {
                                    if (m1Coins == 1)
                                    {
                                        workingMap[position[0]][position[1] + 1] = char.Parse("C");
                                        originalMap[position[0]][position[1] + 1] = char.Parse("C");
                                    }

                                    else if (m1Coins > 1)
                                    {
                                        for (int i = 0; i < m1Coins; i++)
                                        {
                                            workingMap[position[0]][position[1] + 1 + i] = char.Parse("C");
                                            originalMap[position[0]][position[1] + 1 + i] = char.Parse("C");
                                        }
                                    }

                                    else
                                    {
                                        workingMap[position[0]][position[1] + 1] = char.Parse(".");
                                    }
                                }
                            }
                            else
                            {
                                showM2Health = true;
                                m2Health -= 1;
                                if (m2Health == 0)
                                {
                                    if (m2Coins == 1)
                                    {
                                        workingMap[position[0]][position[1] + 1] = char.Parse("C");
                                        originalMap[position[0]][position[1] + 1] = char.Parse("C");
                                    }

                                    else if (m2Coins > 1)
                                    {
                                        for (int i = 0; i < m2Coins; i++)
                                        {
                                            workingMap[position[0]][position[1] + 1 + i] = char.Parse("C");
                                            originalMap[position[0]][position[1] + 1 + i] = char.Parse("C");
                                        }
                                    }

                                    else
                                    {
                                        workingMap[position[0]][position[1] + 1] = char.Parse(".");
                                    }
                                }
                            }
                        }
                        else if (workingMap[position[0]][position[1] - 1] == char.Parse("M"))
                        {
                            if (position[0] == m1Position[0] && position[1] - 1 == m1Position[1])
                            {
                                showM1Health = true;
                                m1Health -= 1;
                                if (m1Health == 0)
                                {
                                    if (m1Coins == 1)
                                    {
                                        workingMap[position[0]][position[1] - 1] = char.Parse("C");
                                        originalMap[position[0]][position[1] - 1] = char.Parse("C");
                                    }

                                    else if (m1Coins > 1)
                                    {
                                        for (int i = 0; i < m1Coins; i++)
                                        {
                                            workingMap[position[0]][position[1] - 1 - i] = char.Parse("C");
                                            originalMap[position[0]][position[1] - 1 - i] = char.Parse("C");
                                        }
                                    }

                                    else
                                    {
                                        workingMap[position[0]][position[1] - 1] = char.Parse(".");
                                    }
                                }
                            }
                            else
                            {
                                showM2Health = true;
                                m2Health -= 1;
                                if (m2Health == 0)
                                {
                                    if (m2Coins == 1)
                                    {
                                        workingMap[position[0]][position[1] - 1] = char.Parse("C");
                                        originalMap[position[0]][position[1] - 1] = char.Parse("C");
                                    }

                                    else if (m2Coins > 1)
                                    {
                                        for (int i = 0; i < m2Coins; i++)
                                        {
                                            workingMap[position[0]][position[1] - 1 - i] = char.Parse("C");
                                            originalMap[position[0]][position[1] - 1 - i] = char.Parse("C");
                                        }
                                    }

                                    else
                                    {
                                        workingMap[position[0]][position[1] - 1] = char.Parse(".");
                                    }
                                }
                            }
                        }
                    }
                }

                return true;
            }
            else if (status == GameState.START)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * The Main Visual Output element. 
         * It draws the new map after the player did something onto the screen.
         * 
         * This is the method where you implement your the code to draw the map ontop the screen
         * and show the move to the user. 
         * 
         * The method returns true if the game is running and it can draw something, false otherwise.
        */
        public bool PrintMapToConsole()
        {
            //modify
            if (Stop == true)
            {
                status = GameState.STOP;
                Stop = false;
            }

            if (status == GameState.START)
            {
                Console.Clear();
                Console.WriteLine("Contents of Map: ");
                string mapLine = "";
                for (int i = 0; i < originalMap.Length; i++)
                {
                    for (int j = 0; j < originalMap[i].Length; j++)
                    {
                        mapLine = (mapLine + originalMap[i][j]);
                    }
                    Console.WriteLine(mapLine);
                    mapLine = "";
                }

                return true;
            }

            else if (status == GameState.RUN)
            {
                for (int i = 0; i < originalMap.Length; i++)
                {
                    for (int j = 0; j < originalMap[i].Length; j++)
                    {
                        if (workingMap[i][j] == char.Parse("P"))
                        {
                            workingMap[i][j] = char.Parse("@");
                        }
                    }
                }

                char[][] mapToPrint = GetCurrentMapState();
                string mapLine = "";
                for (int i = 0; i < mapToPrint.Length; i++)
                {
                    for (int j = 0; j < mapToPrint[i].Length; j++)
                    {
                        mapLine = (mapLine + mapToPrint[i][j]);
                    }
                    Console.WriteLine(mapLine);
                    mapLine = "";
                }
                return true;
            }

            else if (status == GameState.STOP && advanced == true)
            {
                bool answered = false;
                while (answered == false)
                {
                    Console.WriteLine("Would you like to Replay or Quit");
                    string result = Console.ReadLine();

                    if (result.ToLower() == "quit")
                    {
                        status = GameState.STOP;
                        answered = true;
                    }
                    else if (result.ToLower() == "replay")
                    {
                        status = GameState.INIT;
                        answered = true;
                    }
                    else
                    {
                        Console.WriteLine("That was not an option");
                    }
                }
                workingMap = new char[0][];
                originalMap = new char[0][];
            }

            return false;
        }
        /**
         * Additional Visual Output element. 
         * It draws the flavour texts and additional information after the map has been printed.
         * 
         * This is the method does not need to be used unless you want to output somethign else after the map onto the screen.
         * 
         */
        public bool PrintExtraInfo()
        {
            //modify
            if (status == GameState.RUN)
            {
                Console.WriteLine("Number of steps: " + counter);

                if (advanced == true)
                {
                    Console.Write("Health: ");
                    for (int i = 0; i < playerHealth; i++)
                    {
                        Console.Write("♥");
                    }
                    Console.Write("\n");

                    if (showM1Health == true)
                    {
                        Console.Write("Monster: ");
                        for (int i = 0; i < m1Health; i++)
                        {
                            Console.Write("♥");
                        }
                        Console.Write("\n");
                    }

                    if (showM2Health == true)
                    {
                        Console.Write("Monster: ");
                        for (int i = 0; i < m2Health; i++)
                        {
                            Console.Write("♥");
                        }
                        Console.Write("\n");
                    }
                }
                return true;
            }
            
            return false;
        }

        /**
        * Map and GameState get initialized
        * mapName references a file name 
        * Do not use abosolute paths but use the files which are relative to the eDecutable.
        * 
        * Create a private object variable for storing the map in Game and using it in the game.
        */
        public bool LoadMapFromFile(String mapName)
        {
            //modify 
            try
            {
                Console.Clear();
                for (int i = 0; i < originalMap.Length; i++)
                {
                    Array.Resize(ref originalMap[i], 0);
                    Array.Resize(ref workingMap[i], 0);
                }

                Array.Resize(ref originalMap, 0);
                Array.Resize(ref workingMap, 0);

                string mapsFolder = "maps";     //Set variable with the folder name where maps are located
                var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, mapsFolder, mapName); //Combine a variable with the directory of the requested map
                string[] readMap = File.ReadAllLines(path);   //Read the map adding each line as a string in an array

                int counter = 0;
                for (int i = 0; i < readMap.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(readMap[i]))
                    {
                        continue;
                    }
                    else
                    {
                        counter++;
                    }
                }

                Array.Resize(ref originalMap, originalMap.Length + counter);
                Array.Resize(ref workingMap, workingMap.Length + counter);


                string[] map = new string[counter];
                for (int i = 0; i < map.Length; i++)
                {
                    map[i] = readMap[i];
                }

                for (int i = 0; i < map.Length; i++)
                {
                    Array.Resize(ref originalMap[i], map[i].Length);
                    Array.Resize(ref workingMap[i], map[i].Length);
                }

                for (int i = 0; i < map.Length; i++)
                {
                    string line = map[i];
                    for (int j = 0; j < line.Length; j++)
                    {
                        originalMap[i][j] = line[j];
                        workingMap[i][j] = line[j];
                    }
                }

                int mNum = 1;
                for (int i = 0; i < originalMap.Length; i++)
                {
                    for (int j = 0; j < originalMap[i].Length; j++)
                    {
                        if (originalMap[i][j] == char.Parse("M"))
                        {
                            if (mNum == 1)
                            {
                                m1Position[0] = i;
                                m1Position[1] = j;
                                mNum++;
                            }
                            else
                            {
                                m2Position[0] = i;
                                m2Position[1] = j;
                            }
                        }
                    }
                }

                status = GameState.START;
                return true;
            }
            
            catch
            {
                Console.WriteLine("Map could not be found");
                return false;
            }
        }

        /**
         * Returns a representation of the currently loaded map
         * before any move was made.
         * This map should not change when the player moves
         */
        public char[][] GetOriginalMap()
        {
            //modify
            return originalMap;
        }

        /*
         * Returns the current map state and contains the player's move
         * without altering it 
         */
        public char[][] GetCurrentMapState()
        {
            //modify
            return workingMap;
        }

        /**
         * Returns the current position of the player on the map
         * 
         * The first value is the y coordinate and the second is the x coordinate on the map
         */
        public int[] GetPlayerPosition()
        {
            //modify
            for (int i = 0; i < workingMap.Length; i++)
            {
                for (int j = 0; j < workingMap[i].Length; j++)
                {
                    if (workingMap[i][j] == char.Parse("@"))
                    {
                        return new int[2] {i, j};
                    }
                }
            }

            for (int i = 0; i < workingMap.Length; i++)
            {
                for (int j = 0; j < workingMap[i].Length; j++)
                {
                    if (workingMap[i][j] == char.Parse("P"))
                    {
                        return new int[2] { i, j };
                    }
                }
            }

            return new int[2];
        }

        /**
        * Returns the next player action
        * 
        * This method does not alter any internal state
        */
        public int GetPlayerAction()
        {
            //modify
            if (action == PlayerActions.NOTHING)
            {
                return 0;
            }
            else if (action == PlayerActions.NORTH)
            {
                return 1;
            }
            else if (action == PlayerActions.EAST)
            {
                return 2;
            }
            else if (action == PlayerActions.SOUTH)
            {
                return 3;
            }
            else if (action == PlayerActions.WEST)
            {
                return 4;
            }
            else if (action == PlayerActions.PICKUP)
            {
                return 5;
            }
            else if (action == PlayerActions.ATTACK)
            {
                return 6;
            }
            else if (action == PlayerActions.DROP)
            {
                return 7;
            }
            else if (action == PlayerActions.QUIT)
            {
                return 8;
            }

            return -1;
        }

        public GameState GameIsRunning()
        {
            //modify
            return status;
        }

        /**
         * Main method and Dntry point to the program
         * ####
         * Do not change! 
        */
        static void Main(string[] args)
        {
            Game crawler = new Game();

            string input = string.Empty;
            Console.WriteLine("Welcome to the Commandline Dungeon!" + Environment.NewLine +
                "May your Quest be filled with riches!" + Environment.NewLine);

            // Loops through the input and determines when the game should quit
            while (crawler.GameIsRunning() != GameState.STOP && crawler.GameIsRunning() != GameState.UNKOWN)
            {
                Console.Write("Your Command: ");
                input = crawler.ReadUserInput();
                Console.WriteLine(Environment.NewLine);
                crawler.ProcessUserInput(input);
                crawler.Update(crawler.GameIsRunning());
                crawler.PrintMapToConsole();
                crawler.PrintExtraInfo();
            }

            Console.WriteLine("See you again" + Environment.NewLine +
                "In the CMD Dungeon! ");
        }
    }
}