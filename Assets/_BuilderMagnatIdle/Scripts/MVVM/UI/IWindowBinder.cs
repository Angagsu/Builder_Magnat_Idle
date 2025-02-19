namespace Assets._BuilderMagnatIdle.Scripts.MVVM.UI
{
    public interface IWindowBinder 
    {
        void Bind(WindowViewModel viewModel);

        void Close();
    }
}