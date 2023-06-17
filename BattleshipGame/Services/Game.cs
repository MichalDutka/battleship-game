using BattleshipGame.Models;
using BattleshipGame.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.Services
{
    public class Game : IGame
    {
        private readonly IGameInterface _gameInterface;
        private readonly IGameLogic _gameLogic;
        private readonly ICoordinateTranslator _coordinateTranslator;
        private GameState _gameState;

        public Game(IGameInterface gameInterface, IGameLogic gameLogic, ICoordinateTranslator coordinateTranslator)
        {
            _gameInterface = gameInterface;
            _gameLogic = gameLogic;
            _coordinateTranslator = coordinateTranslator;
        }

        public void Run()
        {
            _gameState = _gameLogic.GenerateBoard();

            _gameInterface.UpdateGameState(_gameState);

            GameLoop();

            _gameInterface.DisplayEndScreen();
        }

        private void GameLoop()
        {
            while (_gameState.PointsLeft > 0)
            {
                Coordinates coordinates;

                try
                {
                    coordinates = _coordinateTranslator.TranslateCoordinates(_gameInterface.WaitForInput("Provide coordinates:"));
                }
                catch (ArgumentException)
                {
                    _gameInterface.DisplayError("Invalid coordinates!");

                    continue;
                }

                if (!_gameLogic.CheckIfPointIsValid(coordinates))
                {
                    _gameInterface.DisplayError("Invalid coordinates!");

                    continue;
                }

                _gameLogic.UpdateState(_gameState, coordinates);

                _gameInterface.UpdateGameState(_gameState);
            }
        }
    }
}
