using BattleshipGame.Models;
using BattleshipGame.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.Services
{
    public class GameLogic : IGameLogic
    {
        private const int BOARD_SIZE = 10;
        private readonly int[] _shipsToPlace = new int[] { 5, 4, 4 };

        public GameState GenerateBoard()
        {
            var random = new Random();

            var board = new BoardState[BOARD_SIZE, BOARD_SIZE];

            foreach (var shipLength in _shipsToPlace)
            {
                var x = random.Next(10 - shipLength);
                var y = random.Next(10 - shipLength);

                bool horizontal = random.Next() % 2 == 0;

                bool taken = true;

                while (taken)
                {
                    for (int i = 0; i < shipLength; i++)
                    {
                        var status = horizontal ? board[x + i, y] : board[x, y + i];

                        taken = status != BoardState.NotChecked;

                        if (taken)
                        {
                            x = horizontal ? random.Next(BOARD_SIZE - shipLength) : random.Next(BOARD_SIZE);
                            y = !horizontal ? random.Next(BOARD_SIZE - shipLength) : random.Next(BOARD_SIZE);

                            horizontal = random.Next() % 2 == 0;

                            break;
                        }
                    }
                }

                for (int i = 0; i < shipLength; i++)
                {
                    if (horizontal)
                    {
                        board[x + i, y] = BoardState.NotDestroyed;
                    }
                    else
                    {
                        board[x, y + i] = BoardState.NotDestroyed;
                    }
                }
            }

            var gameState = new GameState();
            gameState.Board = board;
            gameState.PointsLeft = gameState.Board.Cast<BoardState>().Count(x => x == BoardState.NotDestroyed);

            return gameState;
        }

        public bool CheckIfPointIsValid(Coordinates coordinates)
        {
            return coordinates.X < BOARD_SIZE
                && coordinates.X >= 0
                && coordinates.Y < BOARD_SIZE
                && coordinates.Y >= 0;
        }

        public void UpdateState(GameState gameState, Coordinates coordinates)
        {

            if (gameState.Board[coordinates.X, coordinates.Y] == BoardState.NotChecked)
            {
                gameState.Board[coordinates.X, coordinates.Y] = BoardState.Empty;
            }
            else if (gameState.Board[coordinates.X, coordinates.Y] == BoardState.NotDestroyed)
            {
                gameState.Board[coordinates.X, coordinates.Y] = BoardState.Destroyed;
            }

            gameState.PointsLeft = gameState.Board.Cast<BoardState>().Count(x => x == BoardState.NotDestroyed);
        }
    }
}
