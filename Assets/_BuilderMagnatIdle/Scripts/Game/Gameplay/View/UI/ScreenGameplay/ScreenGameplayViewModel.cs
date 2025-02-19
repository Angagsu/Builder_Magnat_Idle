using Assets._BuilderMagnatIdle.Scripts.MVVM.UI;
using R3;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.UI.ScreenGameplay
{
    public class ScreenGameplayViewModel : WindowViewModel
    {
        public override string Id => "ScreenGameplay";

        private readonly GameplayUIManager uiManager;
        private readonly Subject<Unit> exitSceneRequest;



        public ScreenGameplayViewModel(GameplayUIManager uiManager, Subject<Unit> exitSceneRequest)
        {
            this.uiManager = uiManager;
            this.exitSceneRequest = exitSceneRequest;
        }

        public void RequestOpenPopupA()
        {
            uiManager.OpenPopupA();
        }

        public void RequestOpenPopupB()
        {
            uiManager.OpenPopupB();
        }

        public void RequestGoToMainMenu()
        {
            exitSceneRequest.OnNext(Unit.Default);
        }
    }
}