using MineChess.Enums;
using MineChess.Models;
using System.Runtime.CompilerServices;

class Program
{
    #region Fields
    static Square[,] board = new Square[8, 8];
    static Square[,] emptyBoard = new Square[8, 8];
    static string[] Bombs = new string[5];
    static GameState game = new GameState();
    static int noMoves = 0;
    static int lives = 5;
    static int noBombs = 5;
    static int currentRow = 0;
    static int currentCol = 0;
    #endregion

    #region Constructor
    static void Main(string[] args)
    {
        Console.WriteLine("May the odds be ever in your favour!");

        CreateBombs(noBombs);
        CreateGame();
        ShowBoard(0, 0);
        while (!game.HasEnded)
        {
            HandlePressKey(Console.ReadKey().Key);            
        }
    }
    #endregion
    private static void HandlePressKey(ConsoleKey key)
    {

        noMoves++;
        #region Key Pressed 
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.UpArrow:
                if (currentRow > 0)
                {
                    currentRow--;
                }
                break;
            case ConsoleKey.DownArrow:
                if (currentRow < 7)
                {
                    currentRow++;
                }
                break;
            case ConsoleKey.LeftArrow:
                if (currentCol > 0)
                {
                    currentCol--;
                }
                break;
            case ConsoleKey.RightArrow:
                if (currentCol < 7)
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
                game.isLost = true;
            }
        }else if (currentRow == 7)
        {
            Console.WriteLine($"YOU WON in {noMoves} moves and with {lives} lives left");
            game.isWon = true;
        }
        else if (board[currentRow, currentCol].Value > 0)
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

    private static void ShowBoard(int currentRow, int currentCol)
    {
        #region Showing the board in console
        Console.WriteLine("--------------------------------------------------------");
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                string showing = "";
                if (i == currentRow && j == currentCol && !emptyBoard[i, j].IsBomb)
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

    private static void CreateBombs(int bombCount)
    {
        #region Create Random Bombs
        Random rnd = new Random();
        int bombsAdded = 0;
        while (bombsAdded < bombCount)
        {


            var vert = rnd.Next(0, 8);
            var hor = Enum.GetName(typeof(Letter), rnd.Next(0, 8));

            if (!Bombs.Contains(vert + hor))
            {
                Bombs[bombsAdded] = hor + vert;
                bombsAdded++;
            }
        }
        #endregion
    }

    private static void CreateGame()
    {
        #region Fill boards
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
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
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {                
                if (board[i, j].IsBomb)
                {
                    if (i - 1 >= 0)
                    {
                        if (!board[i - 1, j].IsBomb)
                        {
                            board[i - 1, j].Value++;
                        }
                    }
                    if (i + 1 < 8)
                    {

                        if (!board[i + 1, j].IsBomb)
                        {
                            board[i + 1, j].Value++;
                        }
                    }
                    if (j - 1 >= 0)
                    {
                        if (!board[i , j - 1].IsBomb)
                        {
                            board[i, j - 1].Value++;
                        }
                    }
                    if (j + 1 < 8)
                    {
                        if (!board[i, j + 1].IsBomb)
                        {
                            board[i, j + 1].Value++;
                        }
                    }

                    if(i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (!board[i - 1, j - 1].IsBomb)
                        {
                            board[i - 1, j - 1].Value++;
                        }
                    }

                    if (i + 1 < 8 && j + 1 < 8)
                    {
                        if (!board[i + 1, j + 1].IsBomb)
                        {
                            board[i + 1, j + 1].Value++;
                        }
                    }

                    if (i - 1 >= 0 && j + 1 < 8)
                    {
                        if (!board[i - 1, j + 1].IsBomb)
                        {
                            board[i - 1, j + 1].Value++;
                        }
                    }

                    if (i + 1 < 8 && j - 1 >= 0)
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

}




