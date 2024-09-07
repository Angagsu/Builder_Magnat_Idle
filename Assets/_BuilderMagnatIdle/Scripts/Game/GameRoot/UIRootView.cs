using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.GameRoot
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Transform uiSceneContainer;

        private void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            loadingScreen.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            loadingScreen.SetActive(false);
        }

        public void AttachSceneUI(GameObject sceneUI)
        {
            ClearSceneUI();

            sceneUI.transform.SetParent(uiSceneContainer, false);
        }

        public void ClearSceneUI()
        {
            var childCount = uiSceneContainer.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(uiSceneContainer.GetChild(i).gameObject);
            }
        }
    }
}