using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Services;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root.View
{
    public class UIGameplayRootViewModel 
    {
        private readonly SomeGameplayService someGameplayService;

        public UIGameplayRootViewModel(SomeGameplayService someGameplayService)
        {
            this.someGameplayService = someGameplayService;
        }
    }
}