using Assets._BuilderMagnatIdle.Scripts.DI;
using Assets._BuilderMagnatIdle.Scripts.Game.Common;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.UI.PopupA;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.UI.PopupB;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.UI.ScreenGameplay;
using Assets._BuilderMagnatIdle.Scripts.MVVM.UI;
using R3;


namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.UI
{
    public class GameplayUIManager : UIManager
    {
        private readonly Subject<Unit> exitSceneRequest;


        public GameplayUIManager(DIContainer container) : base(container) 
        {
            exitSceneRequest = container.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
        }
        
        public ScreenGameplayViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel(this, exitSceneRequest);
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }

        public PopupAViewModel OpenPopupA()
        {
            var popupA = new PopupAViewModel();
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenPopup(popupA);

            return popupA;
        }

        public PopupBViewModel OpenPopupB()
        {
            var popupB = new PopupBViewModel();
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenPopup(popupB);

            return popupB;
        }
    }
}