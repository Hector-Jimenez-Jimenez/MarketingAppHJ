using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductoCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductosCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ModificarCantidadCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObservarCambiosCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;

namespace MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel
{
    public partial class CarritoPageViewModel : ObservableObject
    {
        const string UserId = "user1";

        readonly IObtenerCarrito _ucObtener;
        readonly IObservarCambiosCarrito _ucObservar;
        readonly IBorrarProductoCarrito _ucBorrarItem;
        readonly IBorrarProductosCarrito _ucBorrarTodos;
        readonly IModificarCantidadCarrito _ucModificar;

        [ObservableProperty]
        ObservableCollection<CarritoItemDto> items = new();

        [ObservableProperty]
        decimal totalPrice;

        public CarritoPageViewModel() { }
        public CarritoPageViewModel(
            IObtenerCarrito obtenerCarrito,
            IObservarCambiosCarrito observarCambiosCarrito,
            IBorrarProductoCarrito borrarProductoCarrito,
            IBorrarProductosCarrito borrarProductosCarrito,
            IModificarCantidadCarrito modificarCantidadCarrito)
        {
            _ucObtener = obtenerCarrito;
            _ucObservar = observarCambiosCarrito;
            _ucBorrarItem = borrarProductoCarrito;
            _ucBorrarTodos = borrarProductosCarrito;
            _ucModificar = modificarCantidadCarrito;
        }

        /// <summary>
        /// Carga de una sola vez el estado actual del carrito.
        /// </summary>
        public async Task CargarCarritoAsync()
        {
            var lista = await _ucObtener.ObtenerCarritoAsync(UserId);
            Items.Clear();
            foreach (var dto in lista)
                Items.Add(dto);

            TotalPrice = Items.Sum(i => i.Total);
        }

        /// <summary>
        /// Expone el observable de cambios en tiempo real.
        /// </summary>
        public IObservable<FirebaseEvent<CarritoItemDto>> ObservarCambios() =>
            _ucObservar.ObservarCambios(UserId);

        [RelayCommand]
        public async Task IncrementarCantidadAsync(CarritoItemDto item)
            => await _ucModificar.ModificarCantidadCarritoAsync(UserId, item.ProductoId, item.Cantidad + 1);

        [RelayCommand]
        public async Task DecrementarCantidadAsync(CarritoItemDto item)
        {
            if (item.Cantidad > 1)
                await _ucModificar.ModificarCantidadCarritoAsync(UserId, item.ProductoId, item.Cantidad - 1);
            else
                await _ucBorrarItem.BorrarProductoCarritoAsync(UserId, item.ProductoId);
        }

        [RelayCommand]
        public async Task EliminarItemAsync(string productoId)
            => await _ucBorrarItem.BorrarProductoCarritoAsync(UserId, productoId);

        [RelayCommand]
        public async Task VaciarCarritoAsync()
            => await _ucBorrarTodos.BorrarProductosCarritoAsync(UserId);
    }
}
