using MineChess.Enums;
using MineChess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineChess
{
    public class Game
    {
        public const int NoBombs = 5;
        public const int MIN = 0;
        public const int MAX = 8;


        public Square[,] board = new Square[MAX, MAX];
        public Square[,] emptyBoard = new Square[MAX, MAX];
        public string[] Bombs = new string[NoBombs];
        public GameState gameState = new GameState();

        public int noMoves = 0;
        public int lives = 5;

        int currentRow = 0;
        int currentCol = 0;




        public void init()
        {
            CreateBombs(NoBombs);
            CreateGame();
            ShowBoard(MIN, MIN);
        }

        public void HandlePressKey(ConsoleKey key)
        {

            noMoves++;
            #region Key Pressed 
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (currentRow > MIN)
                    {
                        currentRow--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (currentRow + 1 < MAX)
                    {
                        currentRow++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentCol > MIN)
                    {
                        currentCol--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (currentCol + 1 < MAX)
                    {
                        currentCol++;
                    }
                    break;
            }
            #endregion

            #region Handle Key Pressed
            if (board[currentRow, currentCol].IsBomb)
            {
                lives--;
                emptyBoard[currentRow, currentCol].StringValue = "X";
                emptyBoard[currentRow, currentCol].IsBomb = true;
                if (lives == 0)
                {
                    Console.WriteLine("YOU LOST! FATALITY!");
                    gameState.isLost = true;
                }
            }
            else if (currentRow == 7)
            {
                Console.WriteLine($"YOU WON in {noMoves} moves and with {lives} lives left");
                gameState.isWon = true;
            }
            else if (board[currentRow, currentCol].Value > MIN)
            {
                emptyBoard[currentRow, currentCol].StringValue = board[currentRow, currentCol].Value.ToString();
            }

            else
            {
                emptyBoard[currentRow, currentCol].StringValue = " ";
            }
            ShowBoard(currentRow, currentCol);
            #endregion
        }

        public void ShowBoard(int rowToShow, int colToShow)
        {
            #region Showing the board in console

            if (rowToShow > MAX || colToShow > MAX || rowToShow < MIN || colToShow < MIN)
            {
                throw new IndexOutOfRangeException();
            }
            Console.WriteLine("--------------------------------------------------------");
            for (int i = MIN; i < MAX; i++)
            {
                for (int j = MIN; j < MAX; j++)
                {
                    string showing = "";
                    if (i == rowToShow && j == colToShow && !emptyBoard[i, j].IsBomb)
                    {
                        var symbol = Convert.ToChar(216);
                        showing = $" {symbol}          ";

                    }
                    else
                    {
                        showing = $" {emptyBoard[i, j].StringValue}   ";

                    }
                    Console.Write(showing.Substring(0, 4));
                }
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------------");
            #endregion
        }

        public void CreateGame()
        {
            #region Fill boards
            for (int i = MIN; i < MAX; i++)
            {
                for (int j = MIN; j < MAX; j++)
                {

                    string currentValue = Enum.GetName(typeof(Letter), i) + j;

                    emptyBoard[i, j] = new Square { StringValue = currentValue };
                    if (!Bombs.Contains(currentValue))
                    {
                        board[i, j] = new Square { StringValue = currentValue };
                    }
                    else
                    {
                        board[i, j] = new Square { IsBomb = true, StringValue = "X" };

                    }
                }
            }
            #endregion

            #region Game Rules
            for (int i = MIN; i < MAX; i++)
            {
                for (int j = MIN; j < MAX; j++)
                {
                    if (board[i, j].IsBomb)
                    {
                        if (i - 1 >= MIN)
                        {
                            if (!board[i - 1, j].IsBomb)
                            {
                                board[i - 1, j].Value++;
                            }
                        }
                        if (i + 1 < MAX)
                        {

                            if (!board[i + 1, j].IsBomb)
                            {
                                board[i + 1, j].Value++;
                            }
                        }
                        if (j - 1 >= MIN)
                        {
                            if (!board[i, j - 1].IsBomb)
                            {
                                board[i, j - 1].Value++;
                            }
                        }
                        if (j + 1 < MAX)
                        {
                            if (!board[i, j + 1].IsBomb)
                            {
                                board[i, j + 1].Value++;
                            }
                        }

                        if (i - 1 >= MIN && j - 1 >= MIN)
                        {
                            if (!board[i - 1, j - 1].IsBomb)
                            {
                                board[i - 1, j - 1].Value++;
                            }
                        }

                        if (i + 1 < MAX && j + 1 < MAX)
                        {
                            if (!board[i + 1, j + 1].IsBomb)
                            {
                                board[i + 1, j + 1].Value++;
                            }
                        }

                        if (i - 1 >= MIN && j + 1 < MAX)
                        {
                            if (!board[i - 1, j + 1].IsBomb)
                            {
                                board[i - 1, j + 1].Value++;
                            }
                        }

                        if (i + 1 < MAX && j - 1 >= MIN)
                        {
                            if (!board[i + 1, j - 1].IsBomb)
                            {
                                board[i + 1, j - 1].Value++;
                            }
                        }
                    }
                }
            }
            #endregion
        }


        public void CreateBombs(int bombCount)
        {
            #region Create Random Bombs
            Random rnd = new Random();
            int bombsAdded = 0;
            while (bombsAdded < bombCount)
            {


                var vert = rnd.Next(0, MAX);
                var hor = Enum.GetName(typeof(Letter), rnd.Next(0, 8));

                if (!Bombs.Contains(vert + hor))
                {
                    Bombs[bombsAdded] = hor + vert;
                    bombsAdded++;
                }
            }
            #endregion
        }


    }
}
