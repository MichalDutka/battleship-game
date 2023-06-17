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
    public class ConsoleGameInterfaceTests
    {
        private readonly Mock<IConsoleIO> _consoleIOMock = new Mock<IConsoleIO>();
        private readonly List<string> _consoleOutput = new List<string>();

        public ConsoleGameInterfaceTests()
        {
            _consoleIOMock.Setup(c => c.Clear());
            _consoleIOMock.Setup(c => c.Write(It.IsAny<string>()))
                .Callback<string>(s => _consoleOutput.Add(s));
            _consoleIOMock.Setup(c => c.WriteLine(It.IsAny<string>()))
                .Callback<string>(s => _consoleOutput.Add(s));
        }

        [Fact]
        public void UpdateGameState_ShouldUpdateGameState()
        {
            var gameState = new GameState()
            {
                Board = new BoardState[10, 10],
                PointsLeft = 10
            };

            var gameInterface = new ConsoleGameInterface(_consoleIOMock.Object);

            gameInterface.UpdateGameState(gameState);

            _consoleOutput.Should().HaveCount(1);
            _consoleOutput[0].Should().Be("Points Left: 10");

            _consoleIOMock.Verify(c => c.Clear(), Times.Once);
            _consoleIOMock.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public void WaitForInput_InputIsProvided_ShouldReturnInput()
        {
            _consoleIOMock.Setup(c => c.ReadLine())
                .Returns("TEST INPUT");

            var gameInterface = new ConsoleGameInterface(_consoleIOMock.Object);

            var result = gameInterface.WaitForInput("TEST:");

            result.Should().Be("TEST INPUT");
            _consoleOutput.Should().HaveCount(1);
            _consoleOutput[0].Should().Be("TEST: ");

            _consoleIOMock.Verify(c => c.Write(It.IsAny<string>()), Times.Once);
            _consoleIOMock.Verify(c => c.ReadLine(), Times.Once);
        }

        [Fact]
        public void WaitForInput_InputIsNotProvided_ShouldWaintUntilInputIsProvidedAndReturnInput()
        {
            _consoleIOMock.SetupSequence(c => c.ReadLine())
                .Returns("")
                .Returns((string)null)
                .Returns("TEST INPUT");

            var gameInterface = new ConsoleGameInterface(_consoleIOMock.Object);

            var result = gameInterface.WaitForInput("TEST:");

            result.Should().Be("TEST INPUT");
            _consoleOutput.Should().HaveCount(3);
            _consoleOutput[0].Should().Be("TEST: ");
            _consoleOutput[1].Should().Be("TEST: ");
            _consoleOutput[2].Should().Be("TEST: ");

            _consoleIOMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(3));
            _consoleIOMock.Verify(c => c.ReadLine(), Times.Exactly(3));
        }

        [Fact]
        public void DisplayError_ShouldDisplayError()
        {
            var gameInterface = new ConsoleGameInterface(_consoleIOMock.Object);

            gameInterface.DisplayError("TEST");

            _consoleOutput.Should().HaveCount(1);
            _consoleOutput[0].Should().Be("TEST");

            _consoleIOMock.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DisplayEndScreen_ShouldDisplayEndScreen()
        {
            var gameInterface = new ConsoleGameInterface(_consoleIOMock.Object);

            gameInterface.DisplayEndScreen();

            _consoleOutput.Should().HaveCount(2);
            _consoleOutput[0].Should().Be("You Win!");
            _consoleOutput[1].Should().Be("Congratulations");

            _consoleIOMock.Verify(c => c.Clear(), Times.Once);
            _consoleIOMock.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Exactly(2));
        }

    }
}
