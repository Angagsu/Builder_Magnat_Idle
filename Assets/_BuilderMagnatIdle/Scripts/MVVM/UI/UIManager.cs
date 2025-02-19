using Assets._BuilderMagnatIdle.Scripts.DI;

namespace Assets._BuilderMagnatIdle.Scripts.MVVM.UI
{
    public abstract class UIManager 
    {
        protected readonly DIContainer Container;
        

        protected UIManager(DIContainer container)
        {
            Container = container;
        }
    }
}