using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Cliente.Helper;

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
        private int temaSeleccionadoIndex = 0; // 0: sistema, 1: claro, 2: oscuro

        [ObservableProperty]
        private int idiomaSeleccionadoIndex = 0; // 0: Español, 1: Inglés

        [RelayCommand]
        private async Task NavegarACambiarDatosAsync()
        {
            await Shell.Current.GoToAsync("changedata");
        }

        [RelayCommand]
        private async Task CerrarSesionAsync()
        {
            var instance = _authService?.GetInstance();
            if (instance != null)
            {
                instance.SignOut();
                await Shell.Current.GoToAsync("login");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo cerrar sesión.", "OK");
            }
        }

        public void AplicarTema(int index)
        {
            var tema = index switch
            {
                1 => "claro",
                2 => "oscuro",
                _ => "sistema"
            };

            Application.Current.UserAppTheme = tema switch
            {
                "claro" => AppTheme.Light,
                "oscuro" => AppTheme.Dark,
                _ => AppTheme.Unspecified
            };

            PerformanceHelper.GuardarTema(tema);
        }

        public void AplicarIdioma(int index)
        {
            var idioma = index switch
            {
                1 => "en",
                _ => "es"
            };
        }
    }
}
