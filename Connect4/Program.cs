using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
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
                    case "0":
                    case "zero":
                    case "ai":
                    case "1":
                    case "one":
                        {
                            //playerCount = 1;
                            //p1 = 1;
                            //p2 = 0;
                            //confirm = true;

                            Console.WriteLine("AI not yet implemented.");
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
                Board.Display();
                win = Board.CheckWin(currentPlayer);
                  
                if (!win)
                {
                    if (currentPlayer == p1)
                    {
                        currentPlayer = p2;
                    }
                    else
                    {
                        currentPlayer = p1;
                    }
                }
            }
            Console.WriteLine("Winner: Player {0}", currentPlayer);
            Console.WriteLine(Environment.NewLine);
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
                    string input = Console.ReadLine();
                    if (Int32.TryParse(input, out intInput))
                    {
                        if (intInput >= 0 && intInput <= 6)
                        {
                            if (Board.CheckValid(intInput) == true)
                            {
                                confirm = true;
                            }
                            else
                            {
                                Console.WriteLine("Player {0}: No more space available in column {1}. Try again.", id, intInput);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Player {0}: Invalid input. Select an integer between 0 and 6.", id);
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

        public static string[] markers = { "A", "O", "X" }; // AI, human1, human2
        public static string[,] board = new string[iRowLength, jColLength];
        public static bool[,] occupied = new bool[iRowLength, jColLength];

        public static void Initialize()
        {
            for (int i = 0; i < iRowLength; i++)
            {
                for (int j = 0; j < jColLength; j++)
                {
                    board[i, j] = boardFill;
                    occupied[i, j] = false;
                }
            }
        }

        public static void Display()
        {
            //number labels
            Console.Write(Environment.NewLine);
            for (int j = 0; j < jColLength; j++)
            {
                Console.Write(String.Format("{0} ", j));
            }
            Console.Write(Environment.NewLine);
            //board
            for (int i = iRowLength - 1; i >= 0; i--)
            {
                for (int j = 0; j < jColLength; j++)
                {
                    Console.Write(String.Format("{0} ", board[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
            Console.Write(Environment.NewLine);
        }

        public static void Update(int j, int playerID)
        {
            //string[] markers = { "A", "O", "X" }; // AI, human1, human2
            int i = 0;

            //while (board[i, j] != boardFill)
            //{
            //    i += 1;
            //}
            while (occupied[i, j] == true)
            {
                i += 1;
            }
            board[i, j] = markers[playerID];
            occupied[i, j] = true;
        }

        public static bool CheckValid(int j)
        {
            bool tf = false;
            for (int i = 0; i < iRowLength; i++)
            {
                if (occupied[i, j] == true)
                {
                    tf = false;
                }
                else
                {
                    tf = true;
                }
            }
            //I don't feel like figuring out why I can't just put returns in the if statement
            return tf;
        }

        public static bool CheckWin(int playerID)
        {
            int i;
            int j;
            //check horizontal
            for (i = 0; i < iRowLength; i++)
            {
                j = 0;
                
                //if ((Board.board[i, j] == Board.markers[playerID]) && (Board.board[i, j + 1] == Board.markers[playerID]) && (Board.board[i, j + 2] == Board.markers[playerID]) && (Board.board[i, j] + 3 == Board.markers[playerID]))
                //{
                //    return true;
                //}

                //I'm sure there's some stupid obvious reason for the above to not work, but:

                if ((Board.board[i, j] == Board.markers[playerID]))
                {
                    if ((Board.board[i, j + 1] == Board.markers[playerID]))
                    {
                        if ((Board.board[i, j + 2] == Board.markers[playerID]))
                        {
                            if ((Board.board[i, j + 3] == Board.markers[playerID]))
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (j < jColLength - 4)
                {
                    j++;
                }
            }

            //check vertical
            for (j = 0; j < jColLength; j++)
            {
                i = 0;
                if (Board.board[i, j] == Board.markers[playerID])
                {
                    if (Board.board[i + 1, j] == Board.markers[playerID])
                    {
                        if (Board.board[i + 2, j] == Board.markers[playerID])
                        {
                            if (Board.board[i + 3, j] == Board.markers[playerID])
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (i < iRowLength - 4)
                {
                    i++;
                }
            }

            //check diagonal ( / and \ )
            //assume imax < jmax
            // positive slope
            for (j = 0; j < jColLength - 3; j++)
            {
                i = 0;
                if (Board.board[i, j] == Board.markers[playerID])
                {
                    if (Board.board[i + 1, j + 1] == Board.markers[playerID])
                    {
                        if (Board.board[i + 2, j + 2] == Board.markers[playerID])
                        {
                            if (Board.board[i + 3, j + 3] == Board.markers[playerID])
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (i < iRowLength - 4)
                {
                    i++;
                }
            }
            // negative slope
            for (j = 3; j < jColLength; j++)
            {
                i = 0;
                if (Board.board[i, j] == Board.markers[playerID])
                {
                    if (Board.board[i + 1, j - 1] == Board.markers[playerID])
                    {
                        if (Board.board[i + 2, j - 2] == Board.markers[playerID])
                        {
                            if (Board.board[i + 3, j - 3] == Board.markers[playerID])
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (i < iRowLength - 4)
                {
                    i++;
                }
            }

            return false;
        }
    }
}


