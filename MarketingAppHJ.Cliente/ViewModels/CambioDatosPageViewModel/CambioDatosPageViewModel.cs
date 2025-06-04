using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ActualizarUsuario;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.IObtenerPerfilUsuario;
using Firebase.Auth;
using Firebase.Database.Query;

namespace MarketingAppHJ.Cliente.ViewModels.CambioDatosPageViewModel
{
    public partial class CambioDatosPageViewModel : ObservableObject
    {
        #region Interfaces
        private readonly IActualizarUsuario _actualizarUsuario;
        private readonly IObtenerPerfilUsuario _obtenerPerfilUsuario;
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;
        #endregion

        #region Constructor
        public CambioDatosPageViewModel(
            IActualizarUsuario actualizarUsuario,
            IObtenerPerfilUsuario obtenerPerfilUsuario,
            IFirebaseAuthentication firebaseAuthentication,
            IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _actualizarUsuario = actualizarUsuario;
            _obtenerPerfilUsuario = obtenerPerfilUsuario;
            _firebaseAuthentication = firebaseAuthentication;
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase;

            _ = CargarDatosUsuarioAsync();
        }
        #endregion

        #region Variables
        private string UserId => _firebaseAuthentication.UserId;

        //Propiedades Orignales
        [ObservableProperty] 
        private string nombreOriginal = string.Empty;
        [ObservableProperty] 
        private string apelliodosOrignales = string.Empty;
        [ObservableProperty] 
        private string usernameOriginal = string.Empty;
        [ObservableProperty] 
        private string emailOriginal = string.Empty;
        [ObservableProperty]
        private string direccionOriginal = string.Empty;
        [ObservableProperty] 
        private string telefonoOriginal = string.Empty;
        [ObservableProperty] 
        private DateTime fechaNacimientoOrignal = DateTime.Today;
        [ObservableProperty] 
        private string imagenOriginal = string.Empty;

        // Propiedades Nuevas 
        [ObservableProperty] 
        private string nuevoNombre = string.Empty;
        [ObservableProperty] 
        private string nuevosApellidos = string.Empty;
        [ObservableProperty] 
        private string nuevoUsername = string.Empty;
        [ObservableProperty] 
        private string nuevoEmail = string.Empty;
        [ObservableProperty] 
        private string nuevaDireccion = string.Empty;
        [ObservableProperty] 
        private string nuevoTelefono = string.Empty;
        [ObservableProperty] 
        private DateTime nuevaFechaNacimiento = DateTime.Today;
        [ObservableProperty] 
        private string nuevaImagen = string.Empty;
        [ObservableProperty] 
        private bool isBusy;
        #endregion

        #region Métodos

        /// <summary>
        /// Método que carga los datos del Usuario
        /// </summary>
        /// <returns>Si se ha podido realizar la accion o no</returns>
        public async Task CargarDatosUsuarioAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var dto = await _obtenerPerfilUsuario.ObtenerPerfilUsuarioAsync(UserId);
                if (dto != null)
                {
                    NombreOriginal = dto.Nombre;
                    ApelliodosOrignales = dto.Apellidos;
                    UsernameOriginal = dto.Username;
                    EmailOriginal = dto.Email;
                    DireccionOriginal = dto.Direccion;
                    TelefonoOriginal = dto.Telefono;
                    FechaNacimientoOrignal = dto.FechaNacimiento == default
                        ? DateTime.Today
                        : dto.FechaNacimiento;
                    ImagenOriginal = dto.AvatarUrl ?? string.Empty;
                    
                    NuevoNombre = string.Empty;
                    NuevosApellidos = string.Empty;
                    NuevoUsername = string.Empty;
                    NuevoEmail = string.Empty;
                    NuevaDireccion = string.Empty;
                    NuevoTelefono = string.Empty;
                    NuevaFechaNacimiento = FechaNacimientoOrignal;
                    NuevaImagen = string.Empty;
                }
            }
            catch (Exception ex)
            {
                await MostrarAlertaAsync("Error", $"No se pudo cargar perfil: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Metodo para llamar a la página actual
        /// </summary>
        /// <returns>Estructura básica para usar en este caso los displayalerts</returns>
        private static Page? GetMainPage() =>
            Application.Current?.Windows.FirstOrDefault()?.Page;

        /// <summary>
        /// Método para Mostrar la alerta o el DisplayAlert
        /// </summary>
        /// <param name="titulo"> titulo de la ventana </param>
        /// <param name="mensaje"> mensaje a mostrar</param>
        /// <param name="boton"> mensaje del botón para cerrarla</param>
        /// <returns></returns>
        private static async Task MostrarAlertaAsync(string titulo, string mensaje, string boton)
        {
            var page = GetMainPage();
            if (page != null)
                await page.DisplayAlert(titulo, mensaje, boton);
        }

        /// <summary>
        /// Método que te guarda los cambios realizados en el Cambio de Datos
        /// </summary>
        /// <returns> La resolucion del si se han podido guardar los cambios</returns>
        [RelayCommand]
        public async Task GuardarCambiosAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            var nombreFinal = string.IsNullOrWhiteSpace(NuevoNombre)
                ? NombreOriginal
                : NuevoNombre.Trim();

            var apellidosFinal = string.IsNullOrWhiteSpace(NuevosApellidos)
                ? ApelliodosOrignales
                : NuevosApellidos.Trim();

            var usernameFinal = string.IsNullOrWhiteSpace(NuevoUsername)
                ? UsernameOriginal
                : NuevoUsername.Trim();

            var emailFinal = string.IsNullOrWhiteSpace(NuevoEmail)
                ? EmailOriginal
                : NuevoEmail.Trim();

            var direccionFinal = string.IsNullOrWhiteSpace(NuevaDireccion)
                ? DireccionOriginal
                : NuevaDireccion.Trim();

            var telefonoFinal = string.IsNullOrWhiteSpace(NuevoTelefono)
                ? TelefonoOriginal
                : NuevoTelefono.Trim();

            var fechaNacimientoFinal = (NuevaFechaNacimiento == default
                    || NuevaFechaNacimiento == FechaNacimientoOrignal)
                ? FechaNacimientoOrignal
                : NuevaFechaNacimiento;

            if (string.IsNullOrWhiteSpace(nombreFinal)
                || string.IsNullOrWhiteSpace(apellidosFinal)
                || string.IsNullOrWhiteSpace(usernameFinal)
                || string.IsNullOrWhiteSpace(emailFinal))
            {
                await MostrarAlertaAsync(
                    "Atención",
                    "Los campos Nombre, Apellidos, Usuario y Email son obligatorios.",
                    "OK");
                IsBusy = false;
                return;
            }

            if (!emailFinal.Contains("@") || !emailFinal.Contains("."))
            {
                await MostrarAlertaAsync(
                    "Atención",
                    "Introduce un correo válido.",
                    "OK");
                IsBusy = false;
                return;
            }

            if (!string.IsNullOrWhiteSpace(telefonoFinal))
            {
                if (!telefonoFinal.All(char.IsDigit))
                {
                    await MostrarAlertaAsync(
                        "Atención",
                        "El teléfono solo puede contener dígitos.",
                        "OK");
                    IsBusy = false;
                    return;
                }
            }
            var actualizadoDto = new UsuarioDto
            {
                Id_Usuario = UserId,
                Nombre = nombreFinal,
                Apellidos = apellidosFinal,
                Username = usernameFinal,
                Email = emailFinal,
                Direccion = direccionFinal,
                Telefono = telefonoFinal,
                FechaNacimiento = fechaNacimientoFinal,
                AvatarUrl = string.IsNullOrWhiteSpace(NuevaImagen)
                    ? ImagenOriginal
                    : NuevaImagen.Trim()
            };

            try
            {
                if (!emailFinal.Equals(EmailOriginal, StringComparison.OrdinalIgnoreCase))
                {
                    var firebaseUser = _firebaseAuthentication.GetInstance();
                    if (firebaseUser == null)
                    {
                        await MostrarAlertaAsync("Error", "Usuario no autenticado.", "OK");
                        IsBusy = false;
                        return;
                    }

                    try
                    {
                        firebaseUser.User.Info.Email = (emailFinal);
                    }
                    catch (FirebaseAuthException faEx) when (faEx.Reason == AuthErrorReason.LoginCredentialsTooOld)
                    {
                        await MostrarAlertaAsync(
                            "Error",
                            "Por razones de seguridad debes volver a iniciar sesión para cambiar el correo.",
                            "OK");
                        IsBusy = false;
                        return;
                    }
                    catch (Exception exAuth)
                    {
                        await MostrarAlertaAsync("Error", $"No se pudo cambiar el correo: {exAuth.Message}", "OK");
                        IsBusy = false;
                        return;
                    }
                }

                await _actualizarUsuario.ActualizarUsuarioAsync(actualizadoDto);

                if (!emailFinal.Equals(EmailOriginal, StringComparison.OrdinalIgnoreCase))
                {
                    await _firebaseRealtimeDatabase.Instance
                        .Child($"usuarios/{UserId}")
                        .PatchAsync(new { email = emailFinal });

                    await MostrarAlertaAsync("Éxito", "Tus datos se han guardado correctamente.", "OK");

                    NombreOriginal = actualizadoDto.Nombre;
                    ApelliodosOrignales = actualizadoDto.Apellidos;
                    UsernameOriginal = actualizadoDto.Username;
                    EmailOriginal = actualizadoDto.Email;
                    DireccionOriginal = actualizadoDto.Direccion;
                    TelefonoOriginal = actualizadoDto.Telefono;
                    FechaNacimientoOrignal = actualizadoDto.FechaNacimiento;
                    ImagenOriginal = actualizadoDto.AvatarUrl;

                    NuevoNombre = string.Empty;
                    NuevosApellidos = string.Empty;
                    NuevoUsername = string.Empty;
                    NuevoEmail = string.Empty;
                    NuevaDireccion = string.Empty;
                    NuevoTelefono = string.Empty;
                    NuevaFechaNacimiento = FechaNacimientoOrignal;
                    NuevaImagen = string.Empty;

                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                await MostrarAlertaAsync("Error", $"No se pudo guardar: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Método para cancelar la modificacion
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task CancelarModificacionAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
        #endregion
    }
}
