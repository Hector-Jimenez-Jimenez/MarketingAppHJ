using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;

namespace MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel
{
    public partial class CarritoPageViewModel:ObservableObject                           
    {
        private readonly IObtenerCarrito _obtenerCarrito;

        [ObservableProperty]
        ObservableCollection<CarritoItemDto> items = new();

        [ObservableProperty]
        decimal totalPrice;

        string userId= "user1";

        public CarritoPageViewModel(IObtenerCarrito obtenerCarrito)
        {
            _obtenerCarrito = obtenerCarrito;
        }
        public CarritoPageViewModel(){}
        public async Task ObtenerCarrito()
        {
            var carritoItems = await _obtenerCarrito.ObtenerCarritoAsync(userId);
            Items.Clear();
            foreach (var item in carritoItems)
                Items.Add(item);
            TotalPrice = Items.Sum(item => item.Total);
        }

        [RelayCommand]
        public async Task EliminarItemAsync()
        {
            /// Aquí puedes implementar la lógica para eliminar un item del carrito.
        }

        [RelayCommand]
        public async Task EliminarCarritoAsync()
        {
            /// Aquí puedes implementar la lógica para eliminar todo el carrito.
        }
        [RelayCommand]
        public async Task CheckOutAsync()
        {
            /// Aquí puedes implementar la lógica para proceder al checkout.
        }

    }
}
