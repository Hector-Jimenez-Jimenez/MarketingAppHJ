using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.IObtenerPerfilUsuario;

namespace MarketingAppHJ.Cliente.ViewModels.ProfilePageViewModel
{
    public partial class ProfilePageViewModel : ObservableObject
    {
        private readonly IObtenerPerfilUsuario _obtenerPerfilUsuario;
        private readonly IFirebaseAuthentication _firebaseAuthentication;

        [ObservableProperty]
        string nombre = string.Empty;
        [ObservableProperty]
        string apellido = string.Empty;
        [ObservableProperty]
        string email = string.Empty;
        [ObservableProperty]
        string direccion = string.Empty;
        [ObservableProperty]
        string telefono = string.Empty;
        [ObservableProperty]
        string fotoPerfil = string.Empty;
        [ObservableProperty]
         string iniciales = string.Empty;
        public ProfilePageViewModel() { }
        public ProfilePageViewModel(
            IObtenerPerfilUsuario obtenerPerfilUsuario,
            IFirebaseAuthentication firebaseAuthentication)
        {
            _obtenerPerfilUsuario = obtenerPerfilUsuario ?? throw new ArgumentNullException(nameof(obtenerPerfilUsuario));
            _firebaseAuthentication = firebaseAuthentication ?? throw new ArgumentNullException(nameof(firebaseAuthentication));
        }
        private string UserId => _firebaseAuthentication.UserId;

        [RelayCommand]
        public async Task CargarPerfilAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    throw new InvalidOperationException("El usuario no está autenticado.");
                }
                var usuario = await _obtenerPerfilUsuario.ObtenerPerfilUsuarioAsync(UserId);
                if (usuario != null)
                {
                    Nombre = usuario.Nombre;
                    Apellido = usuario.Apellidos;
                    Email = usuario.Email;
                    Direccion = usuario.Direccion;
                    Telefono = usuario.Telefono;
                    FotoPerfil = usuario.AvatarUrl;
                }
                if (string.IsNullOrEmpty(FotoPerfil))
                {
                    var fn = Nombre.Trim();
                    var ln = Apellido.Trim();
                    if (fn.Length > 0 && ln.Length > 0)
                        Iniciales = $"{fn[0]}{ln[0]}".ToUpper();
                    else if (fn.Length > 1)
                        Iniciales = fn.Substring(0, 2).ToUpper();
                    else
                        Iniciales = fn.Length > 0 ? fn[0].ToString().ToUpper() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores, por ejemplo, mostrar un mensaje al usuario
                Console.WriteLine($"Error al cargar el perfil: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task VerPedidosAsync()
        {
            await Shell.Current.GoToAsync("pedidos");
        }

        [RelayCommand]
        private async Task CambiarDatos()
        {
            await Shell.Current.GoToAsync("changedata");
        }
    }
}
