using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.AgregarProductoAlCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerNombreCategoria;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerProductosPorId;
using TheMarketingApp.Dominio.Entidades;

namespace MarketingAppHJ.Cliente.ViewModels.DetallesProductoPageViewModel
{
    /// <summary>
    /// ViewModel para la página de detalles del producto.
    /// </summary>
    public partial class DetallesProductoPageViewModel : ObservableObject
    {
        #region Interfaces
        private readonly IObtenerProductoPorId _ObtenerProductoPorId;
        private readonly IAgregarProductoAlCarrito _AgregarProductoAlCarrito;
        private readonly IObtenerNombreCategoria _ObtenerNombreCategoria;
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        private readonly IObtenerCarrito _obtenerCarrito;
        #endregion

        #region Variables
        private string UserId => _firebaseAuthentication.UserId;

        [ObservableProperty] private string id = string.Empty;
        [ObservableProperty] private string nombre = string.Empty;
        [ObservableProperty] private string descripcion = string.Empty;
        [ObservableProperty] private decimal precio;
        [ObservableProperty] private string imagenUrl = string.Empty;
        [ObservableProperty] private int stock;
        [ObservableProperty] private string categoriaId = string.Empty;
        [ObservableProperty] private string nombreCategoria = string.Empty;
        [ObservableProperty] private int cantidad = 1;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor para inicializar el ViewModel de detalles del producto.
        /// </summary>
        /// <param name="usecaseObtenerProductoPorId">Caso de uso para obtener producto por ID.</param>
        /// <param name="agregarProductoAlCarrito">Caso de uso para agregar producto al carrito.</param>
        /// <param name="obtenerNombreCategoria">Caso de uso para obtener el nombre de la categoría.</param>
        /// <param name="firebaseAuthentication">Servicio de autenticación Firebase.</param>
        public DetallesProductoPageViewModel(
            IObtenerProductoPorId usecaseObtenerProductoPorId,
            IAgregarProductoAlCarrito agregarProductoAlCarrito,
            IObtenerNombreCategoria obtenerNombreCategoria,
            IObtenerCarrito obtenerCarrito,
            IFirebaseAuthentication firebaseAuthentication)
        {
            _ObtenerNombreCategoria = obtenerNombreCategoria ?? throw new ArgumentNullException(nameof(obtenerNombreCategoria));
            _ObtenerProductoPorId = usecaseObtenerProductoPorId ?? throw new ArgumentNullException(nameof(usecaseObtenerProductoPorId));
            _AgregarProductoAlCarrito = agregarProductoAlCarrito ?? throw new ArgumentNullException(nameof(agregarProductoAlCarrito));
            _firebaseAuthentication = firebaseAuthentication ?? throw new ArgumentNullException(nameof(firebaseAuthentication));
            _obtenerCarrito = obtenerCarrito ?? throw new ArgumentNullException(nameof(obtenerCarrito));
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Carga el producto seleccionado.
        /// </summary>
        /// <param name="id">Id del producto a cargar.</param>
        public async void LoadProductById(string id)
        {
            var dto = await _ObtenerProductoPorId.ObtenerProductoPorIdAsync(id);
            if (dto != null)
            {
                Id = dto.Id;
                Nombre = dto.Nombre;
                Descripcion = dto.Descripcion;
                Precio = dto.Precio;
                ImagenUrl = dto.ImagenUrl;
                Stock = dto.Stock;
                CategoriaId = dto.CategoriaId;

                var categoriaDto = await _ObtenerNombreCategoria.ObtenerCategoria(dto.CategoriaId);
                NombreCategoria = categoriaDto.Nombre ?? "Sin categoria";
            }
        }

        /// <summary>
        /// Agrega el objeto seleccionado al carrito.
        /// </summary>
        /// <returns>La operación de agregación completada.</returns>
        [RelayCommand]
        public async Task AgregarAlCarritoAsync()
        {
            // Obtener el carrito actual del usuario
            var carritoActual = await _obtenerCarrito.ObtenerCarritoAsync(UserId);
            int cantidadEnCarrito = carritoActual
                .Where(i => i.ProductoId == Id)
                .Sum(i => i.Cantidad);

            // Validar si ya tiene el máximo permitido
            if (cantidadEnCarrito >= Stock)
            {
                await Shell.Current.DisplayAlert("Aviso", "No puedes añadir más unidades, has alcanzado el stock disponible para este producto.", "OK");
                return;
            }

            // Validar que la cantidad a añadir no supere el stock
            if (cantidadEnCarrito + Cantidad > Stock)
            {
                await Shell.Current.DisplayAlert("Aviso", $"Solo puedes añadir {Stock - cantidadEnCarrito} unidad(es) más de este producto.", "OK");
                return;
            }

            var item = new CarritoItemDto
            {
                ProductoId = Id,
                Nombre = Nombre,
                Precio = Precio,
                Cantidad = Cantidad,
                ImagenUrl = ImagenUrl
            };

            await _AgregarProductoAlCarrito.AgregarAlCarritoAsync(UserId, item);

            await Shell.Current.DisplayAlert("Éxito", "Producto agregado al carrito", "OK");
            Cantidad = 1;
            await Shell.Current.GoToAsync("main");
        }

        /// <summary>
        /// Realiza la operación de compra inmediata del producto seleccionado.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        [RelayCommand]
        public async Task ComprarAhoraAsync()
        {
            await Task.CompletedTask;
            await Shell.Current.DisplayAlert("Información", "Funcionalidad de compra aún no implementada.", "OK");
        }

        /// <summary>
        /// Incrementa la cantidad del producto seleccionado en 1, siempre que no exceda el stock disponible.
        /// </summary>
        [RelayCommand]
        public void Incrementar()
        {
            if (Cantidad < Stock)
                Cantidad++;
        }

        /// <summary>
        /// Decrementa la cantidad del producto seleccionado en 1, siempre que no sea menor que 1.
        /// </summary>
        [RelayCommand]
        public void Decrementar()
        {
            if (Cantidad > 1)
                Cantidad--;
        }
        #endregion
    }
}
