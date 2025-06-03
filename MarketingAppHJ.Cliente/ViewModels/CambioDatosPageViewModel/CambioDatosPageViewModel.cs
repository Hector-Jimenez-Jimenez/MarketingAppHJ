using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ActualizarUsuario;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.IObtenerPerfilUsuario;

namespace MarketingAppHJ.Cliente.ViewModels.CambioDatosPageViewModel
{
    public partial class CambioDatosPageViewModel : ObservableObject
    {
        private readonly IActualizarUsuario _actualizarUsuario;
        private readonly IObtenerPerfilUsuario _obtenerPerfilUsuario;
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        public CambioDatosPageViewModel(IActualizarUsuario actualizarUsuario, IObtenerPerfilUsuario obtenerPerfilUsuario, IFirebaseAuthentication firebaseAuthentication)
        {
            _actualizarUsuario = actualizarUsuario;
            _obtenerPerfilUsuario = obtenerPerfilUsuario;
            _firebaseAuthentication = firebaseAuthentication;
        }

        private string userId => _firebaseAuthentication.UserId;

        [ObservableProperty]
        private string nombre = string.Empty;
        [ObservableProperty]
        private string apellidos = string.Empty;
        [ObservableProperty]
        private string username = string.Empty;
        [ObservableProperty]
        private string email = string.Empty;
        [ObservableProperty]
        private string direccion = string.Empty;
        [ObservableProperty]
        private string telefono = string.Empty;
        [ObservableProperty]
        private DateTime fechaNacimiento = DateTime.Today;
        [ObservableProperty]
        private string imagenUrl = string.Empty;
        [ObservableProperty]
        private bool isBusy;

        public async Task CargarDatosUsuarioAsync()
        {
            IsBusy = true;
            try
            {
                var usuario = await _obtenerPerfilUsuario.ObtenerPerfilUsuarioAsync(userId);
                if (usuario != null)
                {
                    Nombre = usuario.Nombre;
                    Apellidos = usuario.Apellidos;
                    Username = usuario.Username;
                    Email = usuario.Email;
                    Direccion = usuario.Direccion;
                    Telefono = usuario.Telefono;
                    FechaNacimiento = usuario.FechaNacimiento == default ? DateTime.Today : usuario.FechaNacimiento;
                    ImagenUrl = usuario.AvatarUrl;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GuardarCambiosAsync()
        {
            var usuario = new UsuarioDto
            {
                Id_Usuario = userId,
                Nombre = Nombre,
                Apellidos = Apellidos,
                Username = Username,
                Email = Email,
                Direccion = Direccion ?? string.Empty,
                Telefono = Telefono ?? string.Empty,
                FechaNacimiento = FechaNacimiento,
                AvatarUrl = ImagenUrl ?? string.Empty
            };

            try
            {
                await _actualizarUsuario.ActualizarUsuarioAsync(usuario);

                var currentPage = App.Current?.Windows.FirstOrDefault()?.Page;
                if (currentPage != null)
                {
                    await currentPage.DisplayAlert("Éxito", "Los datos se han actualizado correctamente.", "OK");
                }
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                var currentPage = App.Current?.Windows.FirstOrDefault()?.Page;
                if (currentPage != null)
                {
                    await currentPage.DisplayAlert("Error", $"No se pudieron actualizar los datos: {ex.Message}", "OK");
                }
            }

            IsBusy = false;
        }

        [RelayCommand]
        public async Task CancelarAsync()
        {
            IsBusy = false;
            await Shell.Current.GoToAsync("..");
        }
    }
}
