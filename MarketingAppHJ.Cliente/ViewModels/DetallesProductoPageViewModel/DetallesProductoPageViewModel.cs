using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
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
        private readonly IObtenerProductoPorId _usecaseObtenerProductoPorId;
        private readonly IAgregarProductoAlCarrito _usecaseAgregarProductoAlCarrito;
        private readonly IObtenerNombreCategoria _usecaseObtenerNombreCategoria;

        [ObservableProperty] private string id;
        [ObservableProperty] private string nombre;
        [ObservableProperty] private string descripcion;
        [ObservableProperty] private decimal precio;
        [ObservableProperty] private string imagenUrl;
        [ObservableProperty] private int stock;
        [ObservableProperty] private string categoriaId;
        [ObservableProperty] private string nombreCategoria;
        [ObservableProperty] private int cantidad = 1;
        public DetallesProductoPageViewModel(IObtenerProductoPorId usecaseObtenerProductoPorId, IAgregarProductoAlCarrito agregarProductoAlCarrito, IObtenerNombreCategoria obtenerNombreCategoria)
        {
            _usecaseObtenerProductoPorId = usecaseObtenerProductoPorId;
            _usecaseAgregarProductoAlCarrito = agregarProductoAlCarrito;
            _usecaseObtenerNombreCategoria = obtenerNombreCategoria;
        }
        public DetallesProductoPageViewModel() { }
        public async void LoadProductById(string id)
        {
            var dto = await _usecaseObtenerProductoPorId.ObtenerProductoPorIdAsync(id);
            if (dto != null)
            {
                Id = dto.Id;
                Nombre = dto.Nombre;
                Descripcion = dto.Descripcion;
                Precio = dto.Precio;
                ImagenUrl = dto.ImagenUrl;
                Stock = dto.Stock;
                CategoriaId = dto.CategoriaId;

                var categoriaDto = await _usecaseObtenerNombreCategoria.ObtenerCategoria(dto.CategoriaId);
                NombreCategoria = categoriaDto.Nombre ?? "Sin categoria";
            }

        }

        [RelayCommand]
        public async Task AgregarAlCarritoAsync()
        {
            var userId = "user1"; 

            var item = new CarritoItemDto
            {
                ProductoId = Id,
                Nombre = Nombre,
                Precio = Precio,
                Cantidad = Cantidad,
                ImagenUrl = ImagenUrl
            };

            await _usecaseAgregarProductoAlCarrito.AgregarAlCarritoAsync(userId, item);

            await Shell.Current.DisplayAlert("Éxito", "Producto agregado al carrito", "OK");
            Cantidad = 1;
            await Shell.Current.GoToAsync("//Catalogo"); // Volver a la página anterior
        }

        [RelayCommand]
        public async Task ComprarAhoraAsync()
        {
            // Lógica para checkout inmediato
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
    }
}
