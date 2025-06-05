using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.Registro;

namespace MarketingAppHJ.Cliente.ViewModels.RegisterPageViewModel
{
    public partial class RegisterPageViewModel : ObservableObject
    {
        private readonly IRegistro _registro;
        [ObservableProperty]
        private string nombre = string.Empty;
        [ObservableProperty]
        private string apellidos = string.Empty;
        [ObservableProperty]
        private string email = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;
        [ObservableProperty]
        private string confirmPassword = string.Empty;
        [ObservableProperty]
        private string telefono = string.Empty;
        [ObservableProperty]
        private string direccion = string.Empty;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RegisterPageViewModel"/> con la dependencia especificada.
        /// </summary>
        /// <param name="registro">Instancia de <see cref="IRegistro"/> utilizada para manejar el registro de usuarios.</param>
        public RegisterPageViewModel(IRegistro registro)
        {
            _registro = registro;
        }

        /// <summary>
        /// Registra un nuevo usuario de forma asíncrona.
        /// </summary>
        /// <returns>Una tarea que representa la operación de registro.</returns>
        [RelayCommand]
        public async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await Shell.Current.DisplayAlert("Error", "Rellena todos los cambios requeridos", "OK");
                return;
            }
            if (Password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlert("Error", "Las constraseñas no coinciden", "OK");
                return;
            }
            try
            {
                await _registro.RegistrarUsuarioAsync(Email, Password, Nombre, Apellidos, Direccion, Telefono);
                await Shell.Current.DisplayAlert("Éxito", "Registro Completado!", "OK");
                await Shell.Current.GoToAsync("main");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
            }
        }

        /// <summary>
        /// Cancela el registro y navega a la página de inicio de sesión.
        /// </summary>
        /// <returns>Una tarea que representa la operación de cancelación.</returns>
        [RelayCommand]
        public static async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("login");
        }
    }
}       