using BattleshipGame.Models;
using BattleshipGame.Services;
using BattleshipGame.Services.Abstractions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.Tests.Services
{
    public class GameLogicTests
    {
        [Fact]
        public void GenerateBoard_ShoudGenerateBoard()
        {
            var gameLogic = new GameLogic();

            var result = gameLogic.GenerateBoard();

            result.Board.Cast<BoardState>().Count(x => x == BoardState.NotChecked).Should().Be(87);
            result.Board.Cast<BoardState>().Count(x => x == BoardState.NotDestroyed).Should().Be(13);
            result.Board.Cast<BoardState>().Count(x => x == BoardState.Empty).Should().Be(0);
            result.Board.Cast<BoardState>().Count(x => x == BoardState.Destroyed).Should().Be(0);
            result.PointsLeft.Should().Be(13);
        }

        [Fact]
        public void CheckIfPointIsValid_PointIsValid_ShouldRetunrTrue()
        {
            var gameLogic = new GameLogic();

            var result = gameLogic.CheckIfPointIsValid(new Coordinates() { X = 5, Y = 4 });

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(11, 5)]
        [InlineData(5, 11)]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        public void CheckIfPointIsValid_PointIsInvalid_ShouldRetunrFalse(int x, int y)
        {
            var gameLogic = new GameLogic();

            var result = gameLogic.CheckIfPointIsValid(new Coordinates() { X = x, Y = y });

            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateState_HitEmptyPosition_ShouldUpdateGameState()
        {
            var gameState = new GameState()
            {
                Board = new BoardState[10, 10],
                PointsLeft = 1
            };

            gameState.Board[4, 4] = BoardState.NotChecked;
            gameState.Board[5, 5] = BoardState.NotDestroyed;

            var gameLogic = new GameLogic();

            gameLogic.UpdateState(gameState, new Coordinates() { X = 4, Y = 4 });

            gameState.Board[4,4].Should().Be(BoardState.Empty);
            gameState.PointsLeft.Should().Be(1);
        }

        [Fact]
        public void UpdateState_HitShipPosition_ShouldUpdateGameState()
        {
            var gameState = new GameState()
            {
                Board = new BoardState[10, 10],
                PointsLeft = 1
            };

            gameState.Board[4, 4] = BoardState.NotChecked;
            gameState.Board[5, 5] = BoardState.NotDestroyed;

            var gameLogic = new GameLogic();

            gameLogic.UpdateState(gameState, new Coordinates() { X = 5, Y = 5 });

            gameState.Board[5, 5].Should().Be(BoardState.Destroyed);
            gameState.PointsLeft.Should().Be(0);
        }

        [Fact]
        public void UpdateState_HitEmptyPositionAgain_ShouldUpdateGameState()
        {
            var gameState = new GameState()
            {
                Board = new BoardState[10, 10],
                PointsLeft = 1
            };

            gameState.Board[4, 4] = BoardState.Empty;
            gameState.Board[5, 5] = BoardState.NotDestroyed;

            var gameLogic = new GameLogic();

            gameLogic.UpdateState(gameState, new Coordinates() { X = 4, Y = 4 });

            gameState.Board[4, 4].Should().Be(BoardState.Empty);
            gameState.PointsLeft.Should().Be(1);
        }

        [Fact]
        public void UpdateState_HitShipPositionAgain_ShouldUpdateGameState()
        {
            var gameState = new GameState()
            {
                Board = new BoardState[10, 10],
                PointsLeft = 0
            };

            gameState.Board[4, 4] = BoardState.NotChecked;
            gameState.Board[5, 5] = BoardState.Destroyed;

            var gameLogic = new GameLogic();

            gameLogic.UpdateState(gameState, new Coordinates() { X = 5, Y = 5 });

            gameState.Board[5, 5].Should().Be(BoardState.Destroyed);
            gameState.PointsLeft.Should().Be(0);
        }

    }
}
