using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.AgregarProductoAlCarrito;
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
        #endregion

        #region variables
        private string UserId => _firebaseAuthentication.UserId;

        [ObservableProperty] private string id;
        [ObservableProperty] private string nombre;
        [ObservableProperty] private string descripcion;
        [ObservableProperty] private decimal precio;
        [ObservableProperty] private string imagenUrl;
        [ObservableProperty] private int stock;
        [ObservableProperty] private string categoriaId;
        [ObservableProperty] private string nombreCategoria;
        [ObservableProperty] private int cantidad = 1;
        #endregion

        #region Constructor
        public DetallesProductoPageViewModel(IObtenerProductoPorId usecaseObtenerProductoPorId, IAgregarProductoAlCarrito agregarProductoAlCarrito, IObtenerNombreCategoria obtenerNombreCategoria, IFirebaseAuthentication firebaseAuthentication)
        {
            _ObtenerNombreCategoria = obtenerNombreCategoria ?? throw new ArgumentNullException(nameof(obtenerNombreCategoria));
            _ObtenerProductoPorId = usecaseObtenerProductoPorId ?? throw new ArgumentNullException(nameof(usecaseObtenerProductoPorId));
            _AgregarProductoAlCarrito = agregarProductoAlCarrito ?? throw new ArgumentNullException(nameof(agregarProductoAlCarrito));
            _firebaseAuthentication = firebaseAuthentication ?? throw new ArgumentNullException(nameof(firebaseAuthentication));
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Carga el producto seleccionado
        /// </summary>
        /// <param name="id">Id del producto a cargar</param>
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
        /// Agrega el objeto seleccionado al carrtio
        /// </summary>
        /// <returns> La operación de agregación completada</returns>
        [RelayCommand]
        public async Task AgregarAlCarritoAsync()
        { 

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

        [RelayCommand]
        public async Task ComprarAhoraAsync()
        {
            //En un futuro
        }

        [RelayCommand]
        public void Incrementar()
        {
            if (Cantidad < Stock)
                Cantidad++;
        }

        [RelayCommand]
        public void Decrementar()
        {
            if (Cantidad > 1)
                Cantidad--;
        }
        #endregion
    }
}
