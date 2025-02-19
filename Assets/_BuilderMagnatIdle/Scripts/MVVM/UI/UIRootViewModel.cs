using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets._BuilderMagnatIdle.Scripts.MVVM.UI
{
    public class UIRootViewModel : IDisposable
    {
        public ReadOnlyReactiveProperty<WindowViewModel> OpenedScreen => openedScreen;
        public IObservableCollection<WindowViewModel> OpenedPopups => openedPopups;

        private readonly ReactiveProperty<WindowViewModel> openedScreen = new();
        private readonly ObservableList<WindowViewModel> openedPopups = new();
        private readonly Dictionary<WindowViewModel, IDisposable> popupSubscriptions = new();


        public void Dispose()
        {
            CloseAllPopups();
            openedScreen.Value?.Dispose();
        }

        public void OpenScreen(WindowViewModel screenViewModel)
        {
            openedScreen.Value?.Dispose();
            openedScreen.Value = screenViewModel;
        }

        public void OpenPopup(WindowViewModel popupViewModel)
        {
            if (openedPopups.Contains(popupViewModel))
            {
                return;
            }

            var subscription = popupViewModel.CloseRequested.Subscribe(ClosePopup);
            popupSubscriptions.Add(popupViewModel, subscription);

            openedPopups.Add(popupViewModel);
        }

        public void ClosePopup(WindowViewModel popupViewModel)
        {
            if (openedPopups.Contains(popupViewModel))
            {
                popupViewModel.Dispose();
                openedPopups.Remove(popupViewModel);

                var popupSubscription = popupSubscriptions[popupViewModel];
                popupSubscription?.Dispose();
                popupSubscriptions.Remove(popupViewModel);
            }
        }

        public void ClosePopup(string popupId)
        {
            var openedPopupViewModel = openedPopups.FirstOrDefault(popup => popup.Id == popupId);
            ClosePopup(openedPopupViewModel);
        }

        public void CloseAllPopups()
        {
            foreach (var openedPopup in openedPopups)
            {
                ClosePopup(openedPopup);
            }
        }
    }
}