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
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.GuardarFotoPerfil;
using MarketingAppHJ.Aplicacion.Interfaces.Cloudinary;

namespace MarketingAppHJ.Cliente.ViewModels.CambioDatosPageViewModel
{
    public partial class CambioDatosPageViewModel : ObservableObject
    {
        #region Interfaces
        private readonly IActualizarUsuario _actualizarUsuario;
        private readonly IObtenerPerfilUsuario _obtenerPerfilUsuario;
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;
        private readonly IGuardarImagenPefil _guardarImagenPefil;
        private readonly ICloudinaryService _cloudinaryService;
        /// <summary>
        /// Constructor de la clase CambioDatosPageViewModel.
        /// </summary>
        /// <param name="actualizarUsuario">Interfaz para actualizar los datos del usuario.</param>
        /// <param name="obtenerPerfilUsuario">Interfaz para obtener el perfil del usuario.</param>
        /// <param name="firebaseAuthentication">Interfaz para la autenticación de Firebase.</param>
        /// <param name="guardarImagenPefil">Interfaz para guardar la imagen de perfil del usuario.</param>
        /// <param name="cloudinaryService">Interfaz para el servicio de Cloudinary.</param> <!-- Se agregó esta línea -->
        /// <param name="firebaseRealtimeDatabase">Interfaz para la base de datos en tiempo real de Firebase.</param>
        public CambioDatosPageViewModel(
            IActualizarUsuario actualizarUsuario,
            IObtenerPerfilUsuario obtenerPerfilUsuario,
            IFirebaseAuthentication firebaseAuthentication,
            IGuardarImagenPefil guardarImagenPefil,
            ICloudinaryService cloudinaryService,
            IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _actualizarUsuario = actualizarUsuario;
            _obtenerPerfilUsuario = obtenerPerfilUsuario;
            _firebaseAuthentication = firebaseAuthentication;
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase;
            _guardarImagenPefil = guardarImagenPefil;
            _cloudinaryService = cloudinaryService; // Se agregó esta asignación

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
                await _actualizarUsuario.ActualizarUsuarioAsync(actualizadoDto);
                await Shell.Current.GoToAsync("profile");

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

                    await Shell.Current.GoToAsync("profile");
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
            await Shell.Current.GoToAsync("profile");
        }

        [RelayCommand]
        public async Task SeleccionarYSubirImagenAsync()
        {
            try
            {
                var resultado = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Selecciona tu imagen de perfil",
                    FileTypes = FilePickerFileType.Images
                });

                if (resultado == null)
                {
                    await MostrarAlertaAsync("Aviso", "No se seleccionó ninguna imagen.", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(resultado.FullPath))
                {
                    await MostrarAlertaAsync("Error", "La ruta del archivo está vacía.", "OK");
                    return;
                }

                await MostrarAlertaAsync("Éxito", $"Archivo seleccionado:\n{resultado.FullPath}", "OK");

                using var flujo = await resultado.OpenReadAsync();
                if (flujo == null)
                {
                    await MostrarAlertaAsync("Error", "No se pudo abrir el archivo.", "OK");
                    return;
                }

                var imagenUrl = await _cloudinaryService.SubirImagenAsync(flujo, resultado.FileName);
                NuevaImagen = imagenUrl;
                await _guardarImagenPefil.GuardarImagenPerfil(UserId, imagenUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error {ex.Message}");
            }
            #endregion
        }
    }
}
