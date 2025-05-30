using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductoCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductosCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ModificarCantidadCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObservarCambiosCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.CrearPedido;

namespace MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel
{
    public partial class CarritoPageViewModel : ObservableObject
    {
        readonly IFirebaseAuthentication _authentication;
        readonly IBorrarProductosCarrito _borrarProductosCarrito;
        readonly IBorrarProductoCarrito _borrarProductoCarrito;
        readonly IModificarCantidadCarrito _modificarCantidadCarrito;
        readonly IObservarCambiosCarrito _observarCambiosCarrito;
        readonly IObtenerCarrito _obtenerCarrito;
        readonly ICrearPedido _crearPedido;
        readonly CompositeDisposable _subs = new();

        [ObservableProperty]
        ObservableCollection<CarritoItemDto> items = new();

        [ObservableProperty]
        decimal totalPrice;

        private string UserId => _authentication.UserId;

        public CarritoPageViewModel()
        {
            // Constructor vacío para permitir la inyección de dependencias
        }
        public CarritoPageViewModel(IFirebaseAuthentication firebase, IBorrarProductoCarrito borrarProductoCarrito, IBorrarProductosCarrito borrarProductosCarrito, IModificarCantidadCarrito modificarCantidadCarrito, 
                                    IObservarCambiosCarrito observarCambiosCarrito, IObtenerCarrito obtenerCarrito, ICrearPedido crearPedido)
        {
            _authentication = firebase ?? throw new ArgumentNullException(nameof(firebase));
            _borrarProductoCarrito = borrarProductoCarrito ?? throw new ArgumentNullException(nameof(borrarProductoCarrito));
            _borrarProductosCarrito = borrarProductosCarrito ?? throw new ArgumentNullException(nameof(borrarProductosCarrito));
            _modificarCantidadCarrito = modificarCantidadCarrito ?? throw new ArgumentNullException(nameof(modificarCantidadCarrito));
            _observarCambiosCarrito = observarCambiosCarrito ?? throw new ArgumentNullException(nameof(observarCambiosCarrito));
            _obtenerCarrito = obtenerCarrito ?? throw new ArgumentNullException(nameof(obtenerCarrito));
            _crearPedido = crearPedido ?? throw new ArgumentNullException(nameof(crearPedido));

            // Suscripción en tiempo real
            var sub = _observarCambiosCarrito
                .ObservarCambios(UserId)
                .Subscribe(evt => OnCartChanged(evt));
            _subs.Add(sub);
        }

        public void OnCartChanged(FirebaseEvent<CarritoItemDto> evt)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var key = evt.Key;
                switch (evt.EventType)
                {
                    case FirebaseEventType.InsertOrUpdate:
                        var dto = evt.Object;
                        if (dto is null)
                            return;              
                        dto.ProductoId = key;   

                        var exist = Items.FirstOrDefault(i => i.ProductoId == key);
                        if (exist != null)
                        {
                            Items[Items.IndexOf(exist)] = dto;
                        }
                        else
                        {
                            Items.Add(dto);
                        }
                        break;

                    case FirebaseEventType.Delete:
                        var toRemove = Items.FirstOrDefault(i => i.ProductoId == key);
                        if (toRemove != null)
                            Items.Remove(toRemove);
                        break;
                }

                TotalPrice = Items.Sum(i => i.Total);
            });
        }

        [RelayCommand]
        public async Task LoadCartAsync()
        {
            var list = await _obtenerCarrito.ObtenerCarritoAsync(UserId);
            Items.Clear();
            foreach (var i in list) Items.Add(i);
            TotalPrice = Items.Sum(i => i.Total);
        }

        [RelayCommand]
        public async Task RemoveItemAsync(string productId)
            => await _borrarProductoCarrito.BorrarProductoCarritoAsync(UserId,productId);

        [RelayCommand]
        public async Task ClearCartAsync()
            => await _borrarProductosCarrito.BorrarProductosCarritoAsync(UserId);

        [RelayCommand]
        public async Task CheckoutAsync()
        {
            await Shell.Current.GoToAsync("checkout");
        }

        public void Dispose() => _subs.Dispose();
    }
}
