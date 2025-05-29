using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.CrearPedido;

namespace MarketingAppHJ.Cliente.ViewModels.CheckOutPageViewModel
{
    public partial class CheckOutPageViewModel : ObservableObject
    {
        const string UserId = "user1";
        private readonly IObtenerCarrito _obtenerCarrito;
        private readonly ICrearPedido _crearPedido;

        [ObservableProperty]
        private ObservableCollection<ItemPedidoDto> items;

        [ObservableProperty]
        private decimal total;

        [ObservableProperty]
        private string direccionEnvio = string.Empty;

        [ObservableProperty]
        private string metodoPago = "Tarjeta";

        public CheckOutPageViewModel() { }
        public CheckOutPageViewModel(
            IObtenerCarrito obtenerCarrito,
            ICrearPedido crearPedido)
        {
            _obtenerCarrito = obtenerCarrito;
            _crearPedido = crearPedido;
        }

        /// <summary>
        /// Carga los ítems actuales del carrito para mostrarlos.
        /// </summary>
        public async Task LoadCartAsync()
        {
            var list = await _obtenerCarrito.ObtenerCarritoAsync(UserId);
            Items = new ObservableCollection<ItemPedidoDto>(
                list.Select(i => new ItemPedidoDto
                {
                    ProductoId = i.ProductoId,
                    Nombre = i.Nombre,
                    Precio = i.Precio,
                    Cantidad = i.Cantidad,
                    ImagenUrl = i.ImagenUrl
                    
                }));
            Total = Items.Sum(i => i.Total);
        }

        [RelayCommand]
        public async Task RealizarPedidoAsync()
        {
            if (string.IsNullOrWhiteSpace(DireccionEnvio))
            {
                var currentPage = GetCurrentPage();
                if (currentPage != null)
                {
                    await currentPage.DisplayAlert(
                        "Validación",
                        "Por favor ingresa una dirección de envío.",
                        "OK");
                }
                return;
            }

            // Llamada al UseCase
            var order = await _crearPedido.RealizarPedido(
                UserId,
                DireccionEnvio,
                MetodoPago);

            // Confirmación
            var confirmationPage = GetCurrentPage();
            if (confirmationPage != null)
            {
                await confirmationPage.DisplayAlert(
                    "Pedido realizado",
                    $"Tu pedido con id: {order.OrderId} se ha creado correctamente.\nTotal: {order.Total:C}",
                    "OK");
            }

            // Volver al catálogo
            await Shell.Current.GoToAsync("//Catalogo");
        }

        private Page? GetCurrentPage()
        {
            var currentApplication = Application.Current;
            if (currentApplication == null)
            {
                return null;
            }

            var currentWindow = currentApplication.Windows.FirstOrDefault();
            if (currentWindow == null)
            {
                return null;
            }

            return currentWindow.Page;
        }
    }
}
