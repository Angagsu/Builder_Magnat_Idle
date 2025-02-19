using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.MVVM.UI
{
    public class WindowsContainer : MonoBehaviour
    {
        [SerializeField] private Transform screensContainer;
        [SerializeField] private Transform popupsContainer;

        private readonly Dictionary<WindowViewModel, IWindowBinder> openedPopupBinders = new();
        private IWindowBinder openedScreenBinder;


        public void OpenPopup<T>(T viewModel) where T : WindowViewModel
        {
            var prefabPath = GetPrefabPath(viewModel);
            var prefab = Resources.Load<GameObject>(prefabPath);
            var createdPopup = Instantiate(prefab, popupsContainer);
            var binder = createdPopup.GetComponent<IWindowBinder>();

            binder.Bind(viewModel);
            openedPopupBinders.Add(viewModel, binder);
        }

        public void ClodePopup<T>(T popupViewModel) where T : WindowViewModel
        {
            var binder = openedPopupBinders[popupViewModel];

            binder?.Close();
            openedPopupBinders.Remove(popupViewModel);
        }

        public void OpenScreen(WindowViewModel viewModel)
        {
            if (viewModel == null)
            {
                return;
            }

            openedScreenBinder?.Close();

            var prefabPath = GetPrefabPath(viewModel);
            var prefab = Resources.Load<GameObject>(prefabPath);
            var createdPopup = Instantiate(prefab, screensContainer);
            var binder = createdPopup.GetComponent<IWindowBinder>();

            binder.Bind(viewModel);
            openedScreenBinder = binder;
        }

        private static string GetPrefabPath(WindowViewModel viewModel)
        {
            return $"Prefabs/UI/{viewModel.Id}";
        }
    }
}