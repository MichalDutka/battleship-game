using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipGame.Models;
using BattleshipGame.Services.Abstractions;
using ConsoleTables;

namespace BattleshipGame.Services
{

    public class ConsoleGameInterface : IGameInterface
    {
        private readonly Dictionary<BoardState, char> _boardSymbols = new Dictionary<BoardState, char>()
        {
            { BoardState.NotChecked, ' ' },
            { BoardState.NotDestroyed, ' ' },
            { BoardState.Destroyed, 'X' },
            { BoardState.Empty, 'O' },
        };
        private readonly IConsoleIO _console;

        public ConsoleGameInterface(IConsoleIO console)
        {
            _console = console;
        }

        public void UpdateGameState(GameState gameState)
        {
            _console.Clear();

            var columns = new List<string>() { "\\" };
            columns.AddRange(Enumerable.Range(1, gameState.Board.GetLength(0)).Select(x => ((char)(x + 64)).ToString()));

            var table = new ConsoleTable(columns.ToArray());

            for (int i = 0; i < gameState.Board.GetLength(1); i++)
            {
                var rowData = new List<string>() { (i+1).ToString() };

                rowData.AddRange(Enumerable.Range(0, gameState.Board.GetLength(1))
                        .Select(x => _boardSymbols[gameState.Board[x, i]].ToString()));

                table.AddRow(rowData.ToArray());
            }
            table.Write(Format.Alternative);

            _console.WriteLine($"Points Left: {gameState.PointsLeft}");
        }
        public string WaitForInput(string message = "")
        {
            var result = "";

            while (string.IsNullOrEmpty(result))
            {
                _console.Write($"{message} ");
                result = _console.ReadLine();
            }

            return result;
        }
        public void DisplayError(string message)
        {
            _console.WriteLine(message);
        }

        public void DisplayEndScreen()
        {
            _console.Clear();
            _console.WriteLine("You Win!");
            _console.WriteLine("Congratulations");
        }

    }
}
