using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.ResetConstraseña;

namespace MarketingAppHJ.Cliente.ViewModels.ResetPageViewModel
{
    /// <summary>
    /// ViewModel para la página de restablecimiento de contraseña.
    /// </summary>
    public partial class ResetPageViewModel : ObservableObject
    {
        private readonly IResetContrasena _resetContrasena;

        [ObservableProperty]
        private string email = string.Empty;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ResetPageViewModel"/>.
        /// </summary>
        /// <param name="resetContrasena">Instancia del caso de uso para restablecer contraseñas.</param>
        public ResetPageViewModel(IResetContrasena resetContrasena) =>
            _resetContrasena = resetContrasena ?? throw new ArgumentNullException(nameof(resetContrasena));

        /// <summary>
        /// Comando para restablecer la contraseña.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
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
                await Shell.Current.GoToAsync("login");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo restablecer la contraseña: {ex.Message}", "OK");
            }
        }

        /// <summary>
        /// Comando para cancelar y regresar a la página de inicio de sesión.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        [RelayCommand]
        public async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("login");
        }
    }
}
