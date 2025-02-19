using Assets._BuilderMagnatIdle.Scripts.DI;
using Assets._BuilderMagnatIdle.Scripts.Game.Common;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands.Handlers;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Services;
using Assets._BuilderMagnatIdle.Scripts.Game.Settings;
using Assets._BuilderMagnatIdle.Scripts.Game.State;
using Assets._BuilderMagnatIdle.Scripts.Game.State.cmd;
using R3;
using System.Linq;



namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root
{
    public static class GameplayRegistrations 
    {
        public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
        {
            var gameStateProvider = container.Resolve<IGameStateProvider>();
            var gameState = gameStateProvider.GameState;

            var settingProvider = container.Resolve<ISettingsProvider>();
            var gameSettings = settingProvider.GameSettings;

            container.RegisterInstance(AppConstants.EXIT_SCENE_REQUEST_TAG, new Subject<Unit>());

            var cmd = new CommandProcessor(gameStateProvider);
            cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameState));
            cmd.RegisterHandler(new CmdCreateMapStateHandler(gameState, gameSettings));
            cmd.RegisterHandler(new CmdResourcesAddHandler(gameState));
            cmd.RegisterHandler(new CmdResourcesSpendhandler(gameState));
            container.RegisterInstance<ICommandProcessor>(cmd);

            var loadingMapId = gameplayEnterParams.MapId;
            var loadingMap = gameState.Maps.FirstOrDefault(m => m.Id == loadingMapId);

            if (loadingMap == null)
            {
                var command = new CmdCreateMapState(loadingMapId);
                var success = cmd.Process(command);

                if (!success)
                {
                    throw new System.Exception($"Couldn't create map state with id: ${loadingMapId}");
                }

                loadingMap = gameState.Maps.First(m => m.Id == loadingMapId);
            }

            container.RegisterFactory(_ => new BuildingsService(loadingMap.Buildings, gameSettings.BuildingsSettings, cmd)).AsSingle();

            container.RegisterFactory(_ => new ResourcesService(gameState.Resources, cmd)).AsSingle();
        }
    }
}