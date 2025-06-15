using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.Idiomas;
using MarketingAppHJ.Cliente.Helper;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.IObtenerPerfilUsuario;

namespace MarketingAppHJ.Cliente.ViewModels.SettingsPageViewModel
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        private readonly IFirebaseAuthentication _authService;
        private readonly IIdiomaService _idiomaService;
        private readonly IObtenerPerfilUsuario _perfilUsuarioService;

        private string _idiomaActual;

        public SettingsPageViewModel(IFirebaseAuthentication authService, IIdiomaService idiomaService,IObtenerPerfilUsuario obtenerPerfilUsuario)
        {
            _authService = authService;
            _idiomaService = idiomaService;
            _perfilUsuarioService = obtenerPerfilUsuario;

            var idiomaGuardado = Preferences.Get("Idioma", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            _idiomaActual = idiomaGuardado;
            idiomaSeleccionadoIndex = idiomaGuardado == "en" ? 1 : 0;

            var temaGuardado = Preferences.Get("Tema", "sistema");
            temaSeleccionadoIndex = temaGuardado switch
            {
                "claro" => 1,
                "oscuro" => 2,
                _ => 0
            };
            AplicarTema(temaSeleccionadoIndex);
            CargarPermisosAsync();
        }

        [ObservableProperty]
        private int temaSeleccionadoIndex = 0; // 0: sistema, 1: claro, 2: oscuro

        [ObservableProperty]
        private int idiomaSeleccionadoIndex;

        [ObservableProperty]
        private bool esAdmin;

        private string UserId => _authService.UserId;

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

            Preferences.Set("Tema", tema);
            PerformanceHelper.GuardarTema(tema);
        }

        public async void AplicarIdiomaDesdeVistaAsync()
        {
            var nuevoIdioma = idiomaSeleccionadoIndex switch
            {
                1 => "en",
                _ => "es"
            };

            // Actualiza _idiomaActual con el idioma real del sistema
            _idiomaActual = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            if (nuevoIdioma == _idiomaActual)
                return;

            var confirmar = await Shell.Current.DisplayAlert(
                "Idioma cambiado",
                "Porfavor Reinicie la aplicación para cambiar complentamente el idioma",
                "Aceptar",
                "Cancelar");

            if (!confirmar)
            {
                idiomaSeleccionadoIndex = _idiomaActual == "en" ? 1 : 0;
                return;
            }

            _idiomaService.CambiarIdioma(nuevoIdioma);
            Preferences.Set("Idioma", nuevoIdioma);
            TranslateExtension.RefreshTranslations();

            // Actualiza _idiomaActual después del cambio
            _idiomaActual = nuevoIdioma;
        }

        [RelayCommand]
        private async Task IrAPaginaAdminAsync()
        {
            await Shell.Current.GoToAsync("admin");
        }

        private async Task CargarPermisosAsync()
        {
            var usuario = await _perfilUsuarioService.ObtenerPerfilUsuarioAsync(UserId);
            EsAdmin = usuario.Rol == "admin";
        }
    }
}
