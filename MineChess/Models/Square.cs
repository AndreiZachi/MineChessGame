using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineChess.Models
{
    public class Square
    {
        public int Value { get; set; } = 0;
        public bool IsBomb { get; set; } = false;
        public string StringValue { get; set; }

    }
}
