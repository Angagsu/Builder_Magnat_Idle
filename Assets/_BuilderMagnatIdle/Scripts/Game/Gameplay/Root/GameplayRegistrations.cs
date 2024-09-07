using Assets._BuilderMagnatIdle.Scripts.DI;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Services;
using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot.Services;
using Assets._BuilderMagnatIdle.Scripts.Game.State;


namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root
{
    public static class GameplayRegistrations 
    {
        public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
        {
            container.RegisterFactory(c => new SomeGameplayService(
                c.Resolve<IGameStateProvider>().GameState,
                c.Resolve<SomeCommonService>())).AsSingle();
        }
    }
}