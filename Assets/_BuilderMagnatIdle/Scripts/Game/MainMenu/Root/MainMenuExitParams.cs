using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot;

namespace Assets._BuilderMagnatIdle.Scripts.Game.MainMenu.Root
{
    public class MainMenuExitParams 
    {
        public SceneEnterParams TargetSceneEnterParams { get; }

        public MainMenuExitParams(SceneEnterParams targetSceneEnterParams)
        {
            TargetSceneEnterParams = targetSceneEnterParams;
        }
        
    }
}