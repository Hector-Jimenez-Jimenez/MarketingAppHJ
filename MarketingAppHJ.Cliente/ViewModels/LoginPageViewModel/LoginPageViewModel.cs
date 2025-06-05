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
    /// <summary>
    /// ViewModel para la página de inicio de sesión.
    /// </summary>
    public partial class LoginPageViewModel : ObservableObject
    {
        #region Interfaces
        private readonly IIniciarSesion _iniciarSesion;
        private readonly IResetContrasena _resetContrasena;
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        #endregion

        #region Variables
        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="LoginPageViewModel"/>.
        /// </summary>
        /// <param name="iniciarSesion">Servicio para iniciar sesión.</param>
        /// <param name="resetContrasena">Servicio para restablecer la contraseña.</param>
        /// <param name="firebaseAuthentication">Servicio de autenticación de Firebase.</param>
        public LoginPageViewModel(IIniciarSesion iniciarSesion, IResetContrasena resetContrasena, IFirebaseAuthentication firebaseAuthentication)
        {
            _iniciarSesion = iniciarSesion ?? throw new ArgumentNullException(nameof(iniciarSesion));
            _resetContrasena = resetContrasena ?? throw new ArgumentNullException(nameof(resetContrasena));
            _firebaseAuthentication = firebaseAuthentication ?? throw new ArgumentNullException(nameof(firebaseAuthentication));
        }
        /// <summary>
        /// Comando para iniciar sesión.
        /// </summary>
        [RelayCommand]
        public async Task LoginAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                if (mainPage != null)
                {
                    await mainPage.DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                }
            }
            else
            {
                try
                {
                    await _iniciarSesion.IniciarSesionAsync(Email, Password);
                    await Shell.Current.GoToAsync("main");

                    var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                    if (mainPage != null)
                    {
                        await mainPage.DisplayAlert("Success", "Bienvenido de nuevo", "OK");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Login failed: {ex.Message}");

                    var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                    if (mainPage != null)
                    {
                        await mainPage.DisplayAlert("Error", "Credenciales incorrectas. Por favor, intente nuevamente.", "OK");
                    }
                }
                finally
                {
                    Email = string.Empty;
                    Password = string.Empty;
                }
            }
        }

        /// <summary>
        /// Comando para restablecer la contraseña.
        /// </summary>
        [RelayCommand]
        public static async Task ResetPasswordAsync()
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

        /// <summary>
        /// Comando para navegar a la página de registro.
        /// </summary>
        [RelayCommand]
        public static async Task NavigateToRegisterPageAsync()
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
        #endregion
    }
}
