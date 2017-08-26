using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    // 6 x 7 board (i x j)
    class Connect4
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Enter command (start, help, exit)");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "start":
                        {
                            Game.Start();
                            break;
                        }
                    case "help":
                        {
                            Console.WriteLine("To be implemented");
                            break;
                        }
                    case "exit":
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid command.");
                            break;
                        }
                }
            }
        }
    }


    class Game
    {
        public static void Start()
        {
            bool win = false;
            bool confirm = false;
            string input;
            int playerCount;
            // 0 = AI, 1 = first human, 2 = second human
            int p1 = 0;
            int p2 = 0;
            int currentPlayer;
            
            
            #region playerCount
            Console.WriteLine("Starting Connect Four. How many players?");
            while (!confirm)
            {
                input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "1":
                    case "one":
                        {
                            playerCount = 1;
                            p1 = 1;
                            p2 = 0;
                            confirm = true;
                            break;
                        }
                    case "2":
                    case "two":
                        {
                            playerCount = 2;
                            p1 = 1;
                            p2 = 2;
                            confirm = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid command. How many players? (one or two)");
                            break;
                        }
                }
            }

            #endregion

            //Board setup
            //Board board = new Board();
            Board.Initialize();
            Console.WriteLine("Initialized board:\n");
            Board.Display();

            Console.WriteLine("Starting game!");
            currentPlayer = p1;
            Turn turn = new Turn();
            while (!win)
            {
                turn.Player(currentPlayer);
                if (currentPlayer == p1)
                {
                    currentPlayer = p2;
                }
                else
                {
                    currentPlayer = p1;
                }
                Board.Display();
            }
        }
    }

    class Turn
    {
        int intInput;
        bool confirm = false;
        public void Player(int id)
        {
            if (id != 0)
            {
                confirm = false;
                Console.WriteLine("Player {0}: Select column to play", id);
                while (!confirm)
                {
                    //TODO: check to make sure column isn't full... possibly lose turn via Board class if i > 6
                    string input = Console.ReadLine();
                    if (Int32.TryParse(input, out intInput))
                    {
                        if (intInput >= 0 && intInput <= 6)
                        {
                            confirm = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Player {0}: Invalid input. Select an integer between 0 and 6.", id);
                    }
                }

               Board.Update(intInput, id);
            }
            else
            {
                Console.WriteLine("AI not yet implemented");
            }
        }
    }

    class Board
    {
        const int iRowLength = 6;
        const int jColLength = 7;
        const string boardFill = "-";
        public static string[,] board = new string[iRowLength, jColLength];

        public static void Initialize()
        {
            for (int i = 0; i < iRowLength; i++)
            {
                for (int j = 0; j < jColLength; j++)
                {
                    board[i, j] = boardFill;
                }
            }
        }

        public static void Display()
        {
            for (int i = 0; i < iRowLength; i++)
            {
                for (int j = 0; j < jColLength; j++)
                {
                    Console.Write(String.Format("{0} ", board[i, j]));
                }
                //Console.Write(Environment.NewLine + Environment.NewLine);
                Console.Write(Environment.NewLine);
            }
        }

        public static void Update(int j, int playerID)
        {
            string[] markers = { "A", "O", "X" }; // AI, human1, human2
            int i = 0;

            //while (board[i, j] != boardFill)
            //{
            //    i += 1;
            //}

            board[i, j] = markers[playerID];
        }

        bool CheckWin(int playerID)
        {
            //To be implemented
            return false;
        }
    }
}


