using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectFourth
{
    public class Program
    {
        GameBoard _board;
        Player _player;
        AI _computer;
        string _activePlayer;
        bool _playing;

        static void Main(string[] args)
        {
            Program p = new Program(); // Program Object to call functions an access variables


            p.InitializeGame();
            Console.WriteLine("Hello and welcome to Connect Fourth.");
            Console.Write("Please Enter Your Name: ");
            p._player.Name = Console.ReadLine();
            Console.Write("\nThank you {0} do you want to be Red or Black?", p._player.Name);
            string choice = Console.ReadLine();
            Console.Write("\n");
            if (choice.ToLower() == "red")
            {
                p._player.Color = "R";
                p._computer.Color = "B";
                Console.WriteLine("Good. You will be Red represented by R.");
                Console.WriteLine("The computer will be Black represented by B.");
            }
            else
            {
                p._player.Color = "B";
                p._computer.Color = "R";
            }

            Console.Write("Do you want to play against a normal or hard computer(N or H):");
            string difficulty = Console.ReadLine();
            if (difficulty.ToLower() == "h")
            {
                p._computer.Diffculty = "hard";
            }

            Console.WriteLine("\n\nComputer: Lets play a game.\n\n\n");

            p.PrintBoard();
            p.PlayGame();

            while (p._playing)
            {
                p._board.SetBoard();
                p.PrintBoard();
                p.PlayGame();
            }
        }

        private void InitializeGame()
        {
            _board = new GameBoard();
            _board.SetBoard();
            _player = new Player();
            _computer = new AI();
            _activePlayer = "player";
            _playing = true;
        }

        private void PrintBoard()
        {            
            for (int i = 0; i < _board.GetBoardHeight(); i++)
            {
                Console.Write("|");
                for (int j = 0; j < _board.GetBoardWidth(); j++)
                {
                    Console.Write(" {0} |", _board.GetSpace(j, i).SpaceColor);
                }
                Console.Write("\n");
                Console.WriteLine("_____________________________");
            }
            Console.WriteLine("  0   1   2   3   4   5   6");
        }

        private void PlayGame()
        {
            bool victory = false;

            while (!victory)
            {
                if (_activePlayer == "player")
                {
                    bool goodMove = false;
                    while (!goodMove)
                    {
                        int columnChoice = 0;
                        Console.WriteLine("\nYour turn {0}.", _player.Name);
                        Console.Write("Enter the number of the column you want to place your piece in: ");
                        columnChoice = Convert.ToInt32(Console.ReadLine());
                        if (!_board.MakeMove(columnChoice, _player.Color))
                        {
                            Console.WriteLine("Not a valid move.");
                        }
                        else
                        {
                            goodMove = true;
                            _activePlayer = "computer";
                        }
                    }
                    PrintBoard();
                    string result = _board.VictoryCheck();
                    if (result == _player.Color)
                    {
                        victory = true;
                        EndGame("player");
                    }
                }
                else
                {
                    bool goodCompMove = false;
                    while (!goodCompMove)
                    {
                        if (_board.MakeMove(_computer.PickSpot(_board, _player), _computer.Color))
                        {
                            goodCompMove = true;
                            _activePlayer = "player";
                            Console.Write("\n\n");
                            PrintBoard();
                        }
                    }


                    string result = _board.VictoryCheck();
                    if (result == _computer.Color)
                    {
                        victory = true;
                        EndGame("computer");
                    }
                }
                if (!BoardSpotsLeft())
                {
                    victory = true;
                    EndGame("draw");
                }
            }
        }

        private void EndGame(string winner)
        {
            if (winner == "player")
            {
                Console.WriteLine("Congratulations {0}. You have defeated the {1} computer.", _player.Name, _computer.Diffculty);
            }
            else if (winner == "computer")
            {
                Console.WriteLine("{0} you are just another notch on the {1} computer's belt.", _player.Name, _computer.Diffculty);
            }
            else
            {
                Console.WriteLine("No one wins. {0} and the {1} computer have played to a tie.", _player.Name, _computer.Diffculty);
            }
            Console.Write("\n\nDo you want to play again?(Y/N): ");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "n")
            {
                _playing = false;
            }
        }

        private bool BoardSpotsLeft()
        {
            foreach (BoardSpace space in _board.Board)
            {
                if (space.SpaceColor == "-")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
