using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductoCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.CrearPedido;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.Authentication;

namespace MarketingAppHJ.Cliente.ViewModels.CheckOutPageViewModel
{
    public partial class CheckOutPageViewModel : ObservableObject
    {
        readonly IObtenerCarrito _obtenerCarrito;
        readonly ICrearPedido _realizarPedido;
        readonly IFirebaseAuthentication _authentication;
        readonly IBorrarProductoCarrito _borrarProductoCarrito;
        private string UserId => _authentication.UserId;
        [ObservableProperty]
        ObservableCollection<CarritoItemDto> items = new();

        [ObservableProperty]
        private decimal total;

        [ObservableProperty]
        private string direccionEnvio = string.Empty;

        [ObservableProperty]
        private string metodoPago = "Tarjeta";
        public CheckOutPageViewModel(
            IObtenerCarrito obtenerCarrito,
            ICrearPedido realizarPedido,
            IFirebaseAuthentication authentication)
        {
            _obtenerCarrito = obtenerCarrito ?? throw new ArgumentNullException(nameof(obtenerCarrito));
            _realizarPedido = realizarPedido ?? throw new ArgumentNullException(nameof(realizarPedido));
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
        }

        [RelayCommand]
        public async Task LoadCartAsync()
        {
            var list = await _obtenerCarrito.ObtenerCarritoAsync(UserId);
            Items.Clear();
            foreach (var i in list) Items.Add(i);
            Total = Items.Sum(i => i.Total);
        }

        [RelayCommand]
        public async Task RealizarPedidoAsync()
        {
            try
            {
                // Ejecuta tu use case, que internamente salvará el pedido y borrará el carrito
                await _realizarPedido.RealizarPedido(UserId,direccionEnvio, MetodoPago);
                await Shell.Current.DisplayAlert("Éxito", "Pedido realizado", "OK");
                await Shell.Current.GoToAsync("main");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
