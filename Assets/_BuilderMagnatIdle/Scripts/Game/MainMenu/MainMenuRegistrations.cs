using Assets._BuilderMagnatIdle.Scripts.DI;
using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot.Services;
using Assets._BuilderMagnatIdle.Scripts.Game.MainMenu.Root;
using Assets._BuilderMagnatIdle.Scripts.Game.MainMenu.Services;


namespace Assets._BuilderMagnatIdle.Scripts.Game.MainMenu
{
    public static class MainMenuRegistrations 
    {
        public static void Register(DIContainer container, MainMenuEnterParams mainMenuEnterParams)
        {
            container.RegisterFactory(c => new SomeMainMenuService(c.Resolve<SomeCommonService>())).AsSingle();
        }
    }
}