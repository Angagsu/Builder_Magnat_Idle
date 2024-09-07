using R3;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    private Subject<Unit> exitSceneSignalSubj;

    public void HandleGoToGameplayButtonClick()
    {
        exitSceneSignalSubj?.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubj)
    {
        this.exitSceneSignalSubj = exitSceneSignalSubj;
    }
}
