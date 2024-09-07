using Assets._BuilderMagnatIdle.Scripts.Game.MainMenu.Root;


namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root
{
    public class GameplayExitParams 
    {
        public MainMenuEnterParams MainMenuEnterParams { get; }
        
        public GameplayExitParams(MainMenuEnterParams mainMenuEnterParams)
        {
            MainMenuEnterParams = mainMenuEnterParams;
        }
    }
}