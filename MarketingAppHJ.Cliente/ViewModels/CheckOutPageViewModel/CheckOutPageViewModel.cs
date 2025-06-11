using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.CrearPedido;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ActualizarUsuario;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.IObtenerPerfilUsuario;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerProductosPorId;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ActualizarProducto;

namespace MarketingAppHJ.Cliente.ViewModels.CheckOutPageViewModel
{
    public partial class CheckOutPageViewModel : ObservableObject
    {
        #region Interfaces
        readonly IObtenerCarrito _obtenerCarrito;
        readonly ICrearPedido _realizarPedido;
        readonly IFirebaseAuthentication _authentication;
        readonly IObtenerPerfilUsuario _obtenerPerfilUsuario;
        readonly IActualizarUsuario _actualizarUsuario;
        readonly IObtenerProductoPorId _obtenerProductoPorId;
        readonly IActualizarProducto _actualizarProducto;
        #endregion

        #region Variables
        private string UserId => _authentication.UserId;
        [ObservableProperty]
        ObservableCollection<CarritoItemDto> items = new();

        [ObservableProperty]
        private decimal total;

        [ObservableProperty]
        private string nuevaDireccionEnvio = string.Empty;

        [ObservableProperty]
        private string direccionEnvioAntigua = string.Empty;

        [ObservableProperty]
        private string metodoPago = "Tarjeta";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor principal para inicializar las dependencias.
        /// </summary>
        public CheckOutPageViewModel(IObtenerCarrito obtenerCarrito,ICrearPedido realizarPedido,IFirebaseAuthentication authentication, IObtenerPerfilUsuario obtenerPerfilUsuario, IActualizarUsuario actualizarUsuario, IObtenerProductoPorId obtenerProductoPorId, IActualizarProducto actualizarProducto)
        {
            _obtenerCarrito = obtenerCarrito ?? throw new ArgumentNullException(nameof(obtenerCarrito));
            _realizarPedido = realizarPedido ?? throw new ArgumentNullException(nameof(realizarPedido));
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
            _obtenerPerfilUsuario = obtenerPerfilUsuario ?? throw new ArgumentNullException(nameof(obtenerPerfilUsuario));
            _actualizarUsuario = actualizarUsuario ?? throw new ArgumentNullException(nameof(actualizarUsuario));
            _obtenerProductoPorId = obtenerProductoPorId ?? throw new ArgumentNullException(nameof(obtenerProductoPorId));
            _actualizarProducto = actualizarProducto ?? throw new ArgumentNullException(nameof(actualizarProducto));

            LoadCartAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Obitene la direccion del usuario en base a su perfil
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica de la obtencion de la dirección.</returns>
        public async Task CargaDireccion()
        {
            try
            {
                var perfil = await _obtenerPerfilUsuario.ObtenerPerfilUsuarioAsync(UserId);
                if (perfil != null)
                {
                    DireccionEnvioAntigua = perfil.Direccion;
                    NuevaDireccionEnvio = string.Empty;

                }
            }
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo cargar perfil: {ex.Message}", "OK");
            }
        }

        /// <summary>
        /// Actualiza la dirección de envío del usuario en su perfil.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica de actualización de la dirección.</returns>
        public async Task ActualizacionDireccion()
        {
            var perfil = await _obtenerPerfilUsuario.ObtenerPerfilUsuarioAsync(UserId);
            if (perfil != null)
            {
                if (!string.IsNullOrWhiteSpace(NuevaDireccionEnvio))
                {
                    var dto = new UsuarioDto
                    {
                        Id_Usuario = UserId,
                        Direccion = NuevaDireccionEnvio,
                        Apellidos = perfil.Apellidos,
                        Nombre = perfil.Nombre,
                        AvatarUrl = perfil.AvatarUrl,
                        Email = perfil.Email,
                        FechaNacimiento = perfil.FechaNacimiento,
                        FechaRegistro = perfil.FechaRegistro,
                        Telefono = perfil.Telefono,
                        Username = perfil.Username,
                    };
                    await _actualizarUsuario.ActualizarUsuarioAsync(dto);
                }
                else
                {
                    var dto = new UsuarioDto
                    {
                        Id_Usuario = UserId,
                        Direccion = perfil.Direccion,
                        Apellidos = perfil.Apellidos,
                        Nombre = perfil.Nombre,
                        AvatarUrl = perfil.AvatarUrl,
                        Email = perfil.Email,
                        FechaNacimiento = perfil.FechaNacimiento,
                        FechaRegistro = perfil.FechaRegistro,
                        Telefono = perfil.Telefono,
                        Username = perfil.Username,
                    };
                    await _actualizarUsuario.ActualizarUsuarioAsync(dto);
                }
            }
        }
        /// <summary>
        /// Carga los productos del carrito y el usuario, validando stock disponible.
        /// </summary>
        /// <returns>La carga terminada</returns>
        [RelayCommand]
        public async Task LoadCartAsync()
        {
            var list = await _obtenerCarrito.ObtenerCarritoAsync(UserId);
            Items.Clear();
            var productosSinStock = new List<string>();
            foreach (var i in list)
            {
                var producto = await _obtenerProductoPorId.ObtenerProductoPorIdAsync(i.ProductoId);
                if (producto == null || producto.Stock == 0)
                {
                    productosSinStock.Add(i.Nombre);
                    continue;
                }
                Items.Add(i);
            }
            Total = Items.Sum(i => i.Total);
            await CargaDireccion();

            if (productosSinStock.Any())
            {
                var nombres = string.Join(", ", productosSinStock);
                await Shell.Current.DisplayAlert("Producto(s) sin stock", $"Los siguientes productos no están disponibles y han sido eliminados del carrito: {nombres}", "OK");
            }
        }

        /// <summary>
        /// Realiza el pedido
        /// </summary>
        /// <returns> El pedido realizado</returns>
        [RelayCommand]
        public async Task RealizarPedidoAsync()
        {
            if (Items.Count > 0)
            {
                try
                {
                    await ActualizacionDireccion();
                    await ActualizarStock();
                    string direccionEnvio = string.IsNullOrWhiteSpace(NuevaDireccionEnvio)
                        ? DireccionEnvioAntigua
                        : NuevaDireccionEnvio;
                    await _realizarPedido.RealizarPedido(UserId, direccionEnvio, MetodoPago);
                    await Shell.Current.DisplayAlert("Éxito", "Pedido realizado", "OK");
                    await Shell.Current.GoToAsync("main");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            else
                await Shell.Current.DisplayAlert("Error", "No se puede añadir un pedido vacio", "Okey");
        }

        /// <summary>
        /// Navega a la página anterior.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica de navegación.</returns>
        [RelayCommand]
        public static async Task Volver()
        {
            await Shell.Current.GoToAsync("carrito");
        }

        /// <summary>
        /// Actualiza el stock de los productos en la base de datos después de realizar un pedido.
        /// Si el stock llega a 0, el producto seguirá existiendo en la BD pero no aparecerá en el catálogo.
        /// </summary>
        public async Task ActualizarStock()
        {
            foreach (var item in Items)
            {
                var producto = await _obtenerProductoPorId.ObtenerProductoPorIdAsync(item.ProductoId);
                if (producto != null)
                {
                    // Validar que el stock no sea negativo
                    producto.Stock = Math.Max(0, producto.Stock - item.Cantidad);
                    await _actualizarProducto.ActualizarProductoAsync(producto);
                }
            }
        }
        #endregion
    }
}
