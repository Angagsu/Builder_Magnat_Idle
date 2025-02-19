using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot;
using Assets._BuilderMagnatIdle.Scripts.Game.MainMenu.Root;
using Assets._BuilderMagnatIdle.Scripts.DI;
using R3;
using UnityEngine;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root.View;
using System.Linq;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.UI;
using Assets._BuilderMagnatIdle.Scripts.Game.Common;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder sceneUIRootPrefab;
        [SerializeField] private WorldGameplayRootBinder worldRootBinder;

        public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
        {
            GameplayRegistrations.Register(gameplayContainer, enterParams);
            var gameplayViewModelsContainer = new DIContainer(gameplayContainer);
            GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);

            InitWorld(gameplayViewModelsContainer);
            InitUI(gameplayViewModelsContainer);
            
            Debug.Log($"Gameplay Entry Point: level to load = {enterParams.MapId}");

            var mainMenuEnterParams = new MainMenuEnterParams("Fatality");
            var exitParams = new GameplayExitParams(mainMenuEnterParams);
            var exitSceneRequest = gameplayContainer.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
            var exitToMainMenuSceneSignal = exitSceneRequest.Select(_ => exitParams);

            return exitToMainMenuSceneSignal;
        }


        private void InitWorld(DIContainer viewsContainer)
        {
            worldRootBinder.Bind(viewsContainer.Resolve<WorldGameplayRootViewModel>());
        }


        private void InitUI(DIContainer viewsContainer)
        {
            var uiRoot = viewsContainer.Resolve<UIRootView>();
            var uiSceneRootBinder = Instantiate(sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

            var uiSceneRootViewModel = viewsContainer.Resolve<UIGameplayRootViewModel>();
            uiSceneRootBinder.Bind(uiSceneRootViewModel);

            var uiManager = viewsContainer.Resolve<GameplayUIManager>();
            uiManager.OpenScreenGameplay();
        }
    }
}