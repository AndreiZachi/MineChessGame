using MineChess;
using MineChess.Models;
using Newtonsoft.Json.Linq;
using System;

namespace MineChessTests
{
    [TestClass]
    public class BoardTest
    {
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void MethodTest_ShowBoard_Outside()
        {

            var game = new Game();
            game.init();
            Console.WriteLine("Will throw exception");
            game.ShowBoard(10, 10);

        }

        [TestMethod]
        public void MethodTest_ShowBoard_BombNo()
        {
            var game = new Game();
            game.init();

            string[] tmp = new string[game.board.GetLength(0) * game.board.GetLength(1)];


            var bombsFound = ConvertBoardArrayToList(game.board).Where(w => w.StringValue == "X").Count();
            Console.WriteLine(bombsFound);
            Assert.AreEqual(bombsFound, Game.NoBombs); 

        }



        private List<Square> ConvertBoardArrayToList(MineChess.Models.Square[,] board)
        {
            var list = new List<Square>();
            for(int i = Game.MIN; i< Game.MAX; i++)
            {
                for(int j = Game.MIN; j < Game.MAX; j++)
                {
                    list.Add(board[i, j]);
                }
            }
            return list;
        }

        
    }
}