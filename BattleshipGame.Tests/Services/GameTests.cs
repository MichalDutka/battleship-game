using BattleshipGame.Models;
using BattleshipGame.Services;
using BattleshipGame.Services.Abstractions;
using Moq;

namespace BattleshipGame.Tests.Services
{
    public class GameTests
    {
        [Fact]
        public void Run_ShouldRunGame()
        {
            var gameInterfaceMock = new Mock<IGameInterface>();
            var gameLogicMock = new Mock<IGameLogic>();
            var coordinateTanslatorMock = new Mock<ICoordinateTranslator>();

            var gameState = new GameState() { PointsLeft = 3 };

            gameLogicMock.Setup(l => l.GenerateBoard())
                .Returns(gameState);
            gameLogicMock.SetupSequence(l => l.CheckIfPointIsValid(It.IsAny<Coordinates>()))
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(true);
            gameLogicMock.Setup(l => l.UpdateState(It.IsAny<GameState>(), It.IsAny<Coordinates>()))
                .Callback(() => gameState.PointsLeft--);

            gameInterfaceMock.Setup(i => i.UpdateGameState(It.IsAny<GameState>()));
            gameInterfaceMock.Setup(i => i.DisplayError(It.IsAny<string>()));
            gameInterfaceMock.Setup(i => i.DisplayEndScreen());

            coordinateTanslatorMock.SetupSequence(t => t.TranslateCoordinates(It.IsAny<string>()))
                .Throws(new ArgumentException())
                .Returns(new Coordinates())
                .Returns(new Coordinates())
                .Returns(new Coordinates())
                .Returns(new Coordinates());
            

            var game = new Game(gameInterfaceMock.Object, gameLogicMock.Object, coordinateTanslatorMock.Object);

            game.Run();

            gameLogicMock.Verify(l => l.GenerateBoard(), Times.Once);
            gameLogicMock.Verify(l => l.CheckIfPointIsValid(It.IsAny<Coordinates>()), Times.Exactly(4));
            gameLogicMock.Verify(l => l.UpdateState(It.IsAny<GameState>(), It.IsAny<Coordinates>()), Times.Exactly(3));

            gameInterfaceMock.Verify(i => i.UpdateGameState(It.IsAny<GameState>()), Times.Exactly(4));
            gameInterfaceMock.Verify(i => i.DisplayError(It.IsAny<string>()), Times.Exactly(2));
            gameInterfaceMock.Verify(i => i.DisplayEndScreen(), Times.Once);

            coordinateTanslatorMock.Verify(t => t.TranslateCoordinates(It.IsAny<string>()), Times.Exactly(5));
        }
    }
}
