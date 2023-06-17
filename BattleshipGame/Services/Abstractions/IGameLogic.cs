using BattleshipGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.Services.Abstractions
{
    public interface IGameLogic
    {
        GameState GenerateBoard();
        bool CheckIfPointIsValid(Coordinates coordinates);
        void UpdateState(GameState gameState, Coordinates coordinates);
    }
}
