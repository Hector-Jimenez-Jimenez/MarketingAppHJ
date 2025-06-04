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
        #region Interfaces
        readonly IObtenerCarrito _obtenerCarrito;
        readonly ICrearPedido _realizarPedido;
        readonly IFirebaseAuthentication _authentication;
        readonly IBorrarProductoCarrito _borrarProductoCarrito;
        #endregion

        #region Variables
        private string UserId => _authentication.UserId;
        [ObservableProperty]
        ObservableCollection<CarritoItemDto> items = new();

        [ObservableProperty]
        private decimal total;

        [ObservableProperty]
        private string direccionEnvio = string.Empty;

        [ObservableProperty]
        private string metodoPago = "Tarjeta";
        #endregion

        #region Constructor
        public CheckOutPageViewModel(
            IObtenerCarrito obtenerCarrito,
            ICrearPedido realizarPedido,
            IBorrarProductoCarrito borrarProductoCarrito,
            IFirebaseAuthentication authentication)
        {
            _obtenerCarrito = obtenerCarrito ?? throw new ArgumentNullException(nameof(obtenerCarrito));
            _realizarPedido = realizarPedido ?? throw new ArgumentNullException(nameof(realizarPedido));
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
            _borrarProductoCarrito = borrarProductoCarrito ?? throw new ArgumentNullException(nameof(borrarProductoCarrito));
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Carga los productos del carrito
        /// </summary>
        /// <returns>La carga terminada</returns>
        [RelayCommand]
        public async Task LoadCartAsync()
        {
            var list = await _obtenerCarrito.ObtenerCarritoAsync(UserId);
            Items.Clear();
            foreach (var i in list) Items.Add(i);
            Total = Items.Sum(i => i.Total);
        }

        /// <summary>
        /// Realiza el pedido
        /// </summary>
        /// <returns> El pedido realizado</returns>
        [RelayCommand]
        public async Task RealizarPedidoAsync()
        {
            try
            {
                await _realizarPedido.RealizarPedido(UserId,DireccionEnvio, MetodoPago);
                await Shell.Current.DisplayAlert("Éxito", "Pedido realizado", "OK");
                await Shell.Current.GoToAsync("main");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Navega a la página anterior.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica de navegación.</returns>
        [RelayCommand]
        public static async Task Volver()
        {
            await Shell.Current.GoToAsync("..");
        }
        #endregion
    }
}
