using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipGame.Models;

namespace BattleshipGame.Services.Abstractions
{
    public interface IGameInterface
    {
        void UpdateGameState(GameState gameState);
        string WaitForInput(string message = "");
        void DisplayError(string message);
        void DisplayEndScreen();
    }
}
