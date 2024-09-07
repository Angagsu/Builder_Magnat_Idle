using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root;
using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot;
using Assets._BuilderMagnatIdle.Scripts.Game.MainMenu.Root;
using Assets._BuilderMagnatIdle.Scripts.DI;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;
using Assets._BuilderMagnatIdle.Scripts.Game.MainMenu.Root.View;
using Assets._BuilderMagnatIdle.Scripts.Game.MainMenu;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainMenuRootBinder sceneUIRootPrefab;

    public Observable<MainMenuExitParams> Run(DIContainer mainMenuContainer, MainMenuEnterParams enterParams)
    {
        MainMenuRegistrations.Register(mainMenuContainer, enterParams);
        var mainMenuViewModelsContainer = new DIContainer(mainMenuContainer);
        MainMenuViewModelsRegistrations.Register(mainMenuViewModelsContainer);

        mainMenuViewModelsContainer.Resolve<UIMainMenuRootViewModel>();

        var uiRoot = mainMenuContainer.Resolve<UIRootView>();
        var uiScene = Instantiate(sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSignalSubj);

        Debug.Log($"Main Menu Entry Point: Run Main Menu Scene. Results: {enterParams?.Result}");


        var levelNumber = Random.Range(0, 300);
        var saveFileName = "ololo.save";
        var gameplayEnterParams = new GameplayEnterParams(saveFileName, levelNumber);
        var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
        var exitToGameplaySceneSignal = exitSignalSubj.Select(_ => mainMenuExitParams);

        return exitToGameplaySceneSignal;
    }
}
