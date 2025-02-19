using R3;
using System;

namespace Assets._BuilderMagnatIdle.Scripts.MVVM.UI
{
    public abstract class WindowViewModel : IDisposable
    {
        public Observable<WindowViewModel> CloseRequested => closeRequested;
        public abstract string Id { get; }

        private readonly Subject<WindowViewModel> closeRequested = new();


        public void RequestClose()
        {
            closeRequested.OnNext(this);
        }

        public virtual void Dispose()
        {
            
        }
    }
}