using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.Models
{
    public class GameState
    {
        public BoardState[,] Board { get; set; }
        public int PointsLeft { get; set; }
    }
}
