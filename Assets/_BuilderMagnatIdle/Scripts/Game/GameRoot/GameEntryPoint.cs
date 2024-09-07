using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root;
using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot;
using Assets._BuilderMagnatIdle.Scripts.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using R3;
using Assets._BuilderMagnatIdle.Scripts.Game.MainMenu.Root;
using Assets._BuilderMagnatIdle.Scripts.DI;
using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot.Services;
using Assets._BuilderMagnatIdle.Scripts.Game.State;


public class GameEntryPoint 
{
    private static GameEntryPoint instance;
    private Coroutines coroutines;
    private UIRootView uiRoot;
    private readonly DIContainer rootContainer = new();
    private DIContainer cachedSceneContainer;


    private GameEntryPoint()
    {
        coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>(path: "UIRoot");
        uiRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(uiRoot.gameObject);
        rootContainer.RegisterInstance(uiRoot);

        var gameStateProvider = new PlayerPrefsGameStateProvider();
        gameStateProvider.LoadSettingsState();
        rootContainer.RegisterInstance<IGameStateProvider>(gameStateProvider);

        rootContainer.RegisterFactory(_ => new SomeCommonService()).AsSingle();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutostartGame()
    {
        Application.targetFrameRate = 30;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        instance = new GameEntryPoint();
        instance.RunGame();
    }

    private void RunGame()
    {
#if UNITY_EDITOR

        var sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == Scenes.GAMEPLAY)
        {

            var enterParams = new GameplayEnterParams("ddd.save", 1);

            coroutines.StartCoroutine(LoadAndStartGameplay(enterParams));

            return;
        }

        if (sceneName == Scenes.MAIN_MENU)
        {
            coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        if (sceneName != Scenes.BOOT)
        {
            return;
        }

#endif

        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }


    private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams)
    {
        uiRoot.ShowLoadingScreen();
        cachedSceneContainer?.Dispose();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAMEPLAY);

        yield return new WaitForSeconds(1);

        var isGameStateLoaded = false;
        rootContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
        yield return new WaitUntil(() => isGameStateLoaded);

        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        var gameplayContainer = cachedSceneContainer = new DIContainer(rootContainer);

        sceneEntryPoint.Run(gameplayContainer, enterParams).Subscribe(gameplayExitParmas =>
        {
            coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParmas.MainMenuEnterParams));
        });

        uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null) 
    {
        uiRoot.ShowLoadingScreen();
        cachedSceneContainer?.Dispose();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        yield return new WaitForSeconds(1);

        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
        var mainMenuContainer = cachedSceneContainer = new DIContainer(rootContainer);

        sceneEntryPoint.Run(mainMenuContainer, enterParams).Subscribe(mainMenuExitParams =>
        {
            var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

            if (targetSceneName == Scenes.GAMEPLAY)
            {
                coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
            }
        });

        uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
