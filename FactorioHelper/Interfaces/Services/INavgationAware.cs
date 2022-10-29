namespace FactorioHelper.Interfaces.Services
{
    public interface INavgationAware
    {
        void OnNavigatedTo(object parameter);

        void OnNavigatedFrom();
    }
}