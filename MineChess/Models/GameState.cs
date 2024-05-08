using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineChess.Models
{
    class GameState
    {
        public bool isWon = false;
        public bool isLost = false;
        public bool HasEnded { get => (isWon || isLost); }
    }

}
