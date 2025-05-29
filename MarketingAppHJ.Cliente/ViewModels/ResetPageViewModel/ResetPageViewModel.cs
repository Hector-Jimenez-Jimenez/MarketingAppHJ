using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.ResetConstraseña;

namespace MarketingAppHJ.Cliente.ViewModels.ResetPageViewModel
{
    public partial class ResetPageViewModel : ObservableObject
    {
        private readonly IResetContrasena _resetContrasena;

        [ObservableProperty]
        private string email = string.Empty;

        public ResetPageViewModel(IResetContrasena resetContrasena)
        {
            _resetContrasena = resetContrasena ?? throw new ArgumentNullException(nameof(resetContrasena));
        }

        public ResetPageViewModel() { }

        [RelayCommand]
        public async Task ResetPasswordAsync()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor, ingresa tu correo electrónico.", "OK");
                return;
            }
            try
            {
                await _resetContrasena.ResetearContrasenaAsync(Email);
                await Shell.Current.DisplayAlert("Éxito", "Se ha enviado un correo para restablecer tu contraseña.", "OK");
                await Shell.Current.GoToAsync("//login");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo restablecer la contraseña: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("//login");

        }
    }
}
