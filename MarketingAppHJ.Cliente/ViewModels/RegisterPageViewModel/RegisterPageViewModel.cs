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
        private string email = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;
        [ObservableProperty]
        private string confirmPassword = string.Empty;

        public RegisterPageViewModel(IRegistro registro)
        {
            _registro = registro;
        }
        public RegisterPageViewModel() { }
        [RelayCommand]
        public async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await Shell.Current.DisplayAlert("Error", "Rellena todos los cambios", "OK");
                return;
            }
            if (Password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlert("Error", "Las constraseñas no coinciden", "OK");
                return;
            }
            try
            {
                await _registro.RegistrarUsuarioAsync(Email, Password);
                await Shell.Current.DisplayAlert("Éxito", "Registro Completado!", "OK");
                await Shell.Current.GoToAsync("main");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("login");
        }
    }
}       