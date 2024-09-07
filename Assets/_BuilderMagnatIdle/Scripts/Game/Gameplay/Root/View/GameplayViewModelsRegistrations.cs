using Assets._BuilderMagnatIdle.Scripts.DI;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Services;


namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root.View
{
    public static class GameplayViewModelsRegistrations 
    {
        public static void Register(DIContainer container)
        {
            container.RegisterFactory(c => new UIGameplayRootViewModel(c.Resolve<SomeGameplayService>())).AsSingle();
            container.RegisterFactory(c => new WorldGameplayRootViewModel()).AsSingle();
        }
    }
}