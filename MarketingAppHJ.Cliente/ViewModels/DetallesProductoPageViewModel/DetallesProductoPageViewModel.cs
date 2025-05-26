using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.AgregarProductoAlCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.ObtenerProductosPorId;
using TheMarketingApp.Dominio.Entidades;

namespace MarketingAppHJ.Cliente.ViewModels.DetallesProductoPageViewModel
{
    /// <summary>
    /// ViewModel para la página de detalles del producto.
    /// </summary>
    public partial class DetallesProductoPageViewModel : ObservableObject
    {
        private readonly IObtenerProductoPorId _usecase;
        private readonly IAgregarProductoAlCarrito _usecaseAgregarProductoAlCarrito;

        [ObservableProperty] private string id;
        [ObservableProperty] private string nombre;
        [ObservableProperty] private string descripcion;
        [ObservableProperty] private decimal precio;
        [ObservableProperty] private string imagenUrl;
        [ObservableProperty] private int stock;
        [ObservableProperty] private string categoria;
        [ObservableProperty] private int cantidad = 1;

        public DetallesProductoPageViewModel(IObtenerProductoPorId usecase, IAgregarProductoAlCarrito agregarProductoAlCarrito)
        {
            _usecase = usecase;
            _usecaseAgregarProductoAlCarrito = agregarProductoAlCarrito;
        }
        public DetallesProductoPageViewModel() { }
        public async void LoadProductById(string id)
        {
            var dto = await _usecase.ObtenerProductoPorIdAsync(id);
            if (dto != null)
            {
                Id = dto.Id;
                Nombre = dto.Nombre;
                Descripcion = dto.Descripcion;
                Precio = dto.Precio;
                ImagenUrl = dto.ImagenUrl;
                Stock = dto.Stock;
                Categoria = dto.CategoriaId;
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
