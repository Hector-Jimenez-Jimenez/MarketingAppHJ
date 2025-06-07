using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;

namespace MarketingAppHJ.Cliente.ViewModels.SettingsPageViewModel
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        private readonly IFirebaseAuthentication _authService;

        public SettingsPageViewModel(IFirebaseAuthentication authService)
        {
            _authService = authService;
        }

        [ObservableProperty]
        private int temaSeleccionadoIndex = 0;

        [RelayCommand]
        private async Task NavegarACambiarDatosAsync()
        {
            await Shell.Current.GoToAsync("changedata");
        }

        [RelayCommand]
        private async Task CerrarSesionAsync()
        {
            var firebaseAuthClient = _authService.GetInstance();
            if (firebaseAuthClient.User != null)
            {
                firebaseAuthClient.SignOut();
            }
            await Shell.Current.GoToAsync("login");
        }

        public void AplicarTema(int index)
        {
            Application.Current.UserAppTheme = index switch
            {
                1 => AppTheme.Light,
                2 => AppTheme.Dark,
                _ => AppTheme.Unspecified
            };
        }
    }
}
