using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.IniciarSesion;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.ResetConstraseña;

namespace MarketingAppHJ.Cliente.ViewModels.LoginPageViewModel
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly IIniciarSesion _iniciarSesion;
        private readonly IResetContrasena _resetContrasena;

        [ObservableProperty]
        private string email = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;

        public LoginPageViewModel(IIniciarSesion iniciarSesion, IResetContrasena resetContrasena)
        {
            _resetContrasena = resetContrasena;
            _iniciarSesion = iniciarSesion;
        }

        public LoginPageViewModel() { }

        [RelayCommand]
        public async Task LoginAsync()
        {
            try
            {
                await _iniciarSesion.IniciarSesionAsync(Email, Password);
                await Shell.Current.DisplayAlert("Success", "Login successful!", "OK");
                await Shell.Current.GoToAsync("main");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                if (ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
                {
                    await Shell.Current.DisplayAlert("Error", "No se han encontrado los credenciales", "Entendido");
                }
            }
        }

        [RelayCommand]
        public async Task ResetPasswordAsync()
        {
            try
            {
                await Shell.Current.GoToAsync("reset");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Password reset failed: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task NavigateToRegisterPageAsync()
        {
            try
            {
                await Shell.Current.GoToAsync("register");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation to register page failed: {ex.Message}");
            }
        }
    }
}
