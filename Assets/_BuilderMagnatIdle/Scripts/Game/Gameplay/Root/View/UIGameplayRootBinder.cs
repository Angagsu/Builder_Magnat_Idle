using R3;
using UnityEngine;

public class UIGameplayRootBinder : MonoBehaviour
{
    private Subject<Unit> exitSceneSignalSubj;

    public void HandleGoToMainMenuButtonClick()
    {
        exitSceneSignalSubj?.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubj)
    {
        this.exitSceneSignalSubj = exitSceneSignalSubj; 
    }
}
