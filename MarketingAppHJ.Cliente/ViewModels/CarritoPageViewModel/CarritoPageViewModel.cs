using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
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
using Microsoft.Maui.Dispatching;

namespace MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel
{
    public partial class CarritoPageViewModel : ObservableObject, IDisposable
    {
        const string UserId = "user1";

        readonly IObtenerCarrito _ucObtener;
        readonly IObservarCambiosCarrito _ucObservar;
        readonly IBorrarProductoCarrito _ucBorrarItem;
        readonly IBorrarProductosCarrito _ucBorrarTodos;
        readonly CompositeDisposable _subs = new();

        [ObservableProperty]
        ObservableCollection<CarritoItemDto> items = new();

        [ObservableProperty]
        decimal totalPrice;

        public CarritoPageViewModel(
            IObtenerCarrito obtenerCarrito,
            IObservarCambiosCarrito observarCambiosCarrito,
            IBorrarProductoCarrito borrarProductoCarrito,
            IBorrarProductosCarrito borrarProductosCarrito)
        {
            _ucObtener = obtenerCarrito;
            _ucObservar = observarCambiosCarrito;
            _ucBorrarItem = borrarProductoCarrito;
            _ucBorrarTodos = borrarProductosCarrito;
            // Suscribimos en tiempo real a Insert / Update / Delete
            var sub = _ucObservar
                .ObservarCambios(UserId)
                .Subscribe(AlCambiarCarrito);
            _subs.Add(sub);
        }

        public CarritoPageViewModel() { }
        /// <summary>
        /// Carga inicial del carrito.
        /// </summary>
        public async Task CargarCarritoAsync()
        {
            var lista = await _ucObtener.ObtenerCarritoAsync(UserId);
            Items.Clear();
            foreach (var dto in lista)
                Items.Add(dto);
            TotalPrice = Items.Sum(i => i.Total);
        }

        void AlCambiarCarrito(FirebaseEvent<CarritoItemDto> evt)
        {
            // Ejecutamos en el hilo de UI
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var key = evt.Key;
                var dto = evt.Object;
                if (string.IsNullOrEmpty(key) || dto is null)
                    return;

                dto.ProductoId = key;
                switch (evt.EventType)
                {
                    case FirebaseEventType.InsertOrUpdate:
                        var existente = Items.FirstOrDefault(i => i.ProductoId == key);
                        if (existente != null)
                        {
                            var idx = Items.IndexOf(existente);
                            Items[idx] = dto;
                        }
                        else
                        {
                            Items.Add(dto);
                        }
                        break;

                    case FirebaseEventType.Delete:
                        var eliminado = Items.FirstOrDefault(i => i.ProductoId == key);
                        if (eliminado != null)
                            Items.Remove(eliminado);
                        break;
                }

                TotalPrice = Items.Sum(i => i.Total);
            });
        }

        [RelayCommand]
        public async Task EliminarItemAsync(string productoId)
            => await _ucBorrarItem.BorrarProductoCarritoAsync(UserId, productoId);

        [RelayCommand]
        public async Task VaciarCarritoAsync()
            => await _ucBorrarTodos.BorrarProductosCarritoAsync(UserId);

        public void Dispose() => _subs.Dispose();
    }
}
