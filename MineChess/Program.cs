using MineChess.Enums;
using MineChess.Models;
using System.Runtime.CompilerServices;


namespace MineChess
{
    public class Program
    {
        #region Fields
        private static Game game = new Game();

        #endregion

        #region Constructor
        static void Main(string[] args)
        {
            Console.WriteLine("May the odds be ever in your favour!");


            game.init();
            
            //.CreateBombs(noBombs);
            while (!game.gameState.HasEnded)
            {
                game.HandlePressKey(Console.ReadKey().Key);
            }
        }
        #endregion    
    }
}



