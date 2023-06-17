using Autofac;
using BattleshipGame.Services;
using BattleshipGame.Services.Abstractions;

IContainer container = ConfigureServices();

var game = container.Resolve<IGame>();

game.Run();

IContainer ConfigureServices()
{
    var builder = new ContainerBuilder();

    builder.RegisterType<Game>()
           .As<IGame>();
    builder.RegisterType<GameLogic>()
           .As<IGameLogic>();
    builder.RegisterType<ConsoleGameInterface>()
           .As<IGameInterface>();
    builder.RegisterType<ConsoleIO>()
           .As<IConsoleIO>();
    builder.RegisterType<CoordinateTranslator>()
        .As<ICoordinateTranslator>();

    var container = builder.Build();
    return container;
}