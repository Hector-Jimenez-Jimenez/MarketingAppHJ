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
        #region Interfaces
        readonly IFirebaseAuthentication _authentication;
        readonly IBorrarProductosCarrito _borrarProductosCarrito;
        readonly IBorrarProductoCarrito _borrarProductoCarrito;
        readonly IModificarCantidadCarrito _modificarCantidadCarrito;
        readonly IObservarCambiosCarrito _observarCambiosCarrito;
        readonly IObtenerCarrito _obtenerCarrito;
        readonly ICrearPedido _crearPedido;
        readonly CompositeDisposable _subs = new();
        #endregion

        #region Variables
        [ObservableProperty]
        ObservableCollection<CarritoItemDto> items = new();

        [ObservableProperty]
        decimal totalPrice;

        private string UserId => _authentication.UserId;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CarritoPageViewModel"/>.
        /// </summary>
        /// <param name="firebase">Servicio de autenticación de Firebase.</param>
        /// <param name="borrarProductoCarrito">Servicio para borrar un producto del carrito.</param>
        /// <param name="borrarProductosCarrito">Servicio para borrar todos los productos del carrito.</param>
        /// <param name="modificarCantidadCarrito">Servicio para modificar la cantidad de un producto en el carrito.</param>
        /// <param name="observarCambiosCarrito">Servicio para observar cambios en el carrito.</param>
        /// <param name="obtenerCarrito">Servicio para obtener el carrito del usuario.</param>
        /// <param name="crearPedido">Servicio para crear un pedido.</param>
        /// <exception cref="ArgumentNullException">Se lanza si algún parámetro es nulo.</exception>
        public CarritoPageViewModel(
            IFirebaseAuthentication firebase,
            IBorrarProductoCarrito borrarProductoCarrito,
            IBorrarProductosCarrito borrarProductosCarrito,
            IModificarCantidadCarrito modificarCantidadCarrito,
            IObservarCambiosCarrito observarCambiosCarrito,
            IObtenerCarrito obtenerCarrito,
            ICrearPedido crearPedido)
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
        #endregion

        #region Métodos
        /// <summary>
        /// Maneja las acciones en el carrito de compras.
        /// </summary>
        /// <param name="evt"> Accion Realizada</param>
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

        /// <summary>
        /// Carga el carrito de compras del usuario actual desde Firebase.
        /// </summary>
        /// <returns> La carga de datos del carrito de compras </returns>
        [RelayCommand]
        public async Task LoadCartAsync()
        {
            var list = await _obtenerCarrito.ObtenerCarritoAsync(UserId);
            Items.Clear();
            foreach (var i in list) Items.Add(i);
            TotalPrice = Items.Sum(i => i.Total);
        }

        /// <summary>
        /// Elimina un producto del carrito de compras del usuario actual.
        /// </summary>
        /// <param name="productId"> Id del producto a eliminar</param>
        /// <returns> La eliminación del producto en el carrito </returns>
        [RelayCommand]
        public async Task RemoveItemAsync(string productId)
            => await _borrarProductoCarrito.BorrarProductoCarritoAsync(UserId,productId);

        /// <summary>
        /// Elimina todos los productos del carrito de compras del usuario actual.
        /// </summary>
        /// <returns> La limpieza del carrito</returns>
        [RelayCommand]
        public async Task ClearCartAsync()
        {
            await _borrarProductosCarrito.BorrarProductosCarritoAsync(UserId);
            Items.Clear();
            TotalPrice = 0m;
        }
        /// <summary>
        /// Navega a la página de checkout para procesar el pedido.
        /// </summary>
        /// <returns> El envio a la pagina de Checkout </returns>
        [RelayCommand]
        public async Task CheckoutAsync()
        {
            await Shell.Current.GoToAsync("checkout");
        }

        /// <summary>
        /// Libera los recursos utilizados por el ViewModel.
        /// </summary>
        public void Dispose() => _subs.Dispose();

        /// <summary>
        /// Incrementa la cantidad de un producto en el carrito de compras del usuario actual.
        /// </summary>
        /// <param name="item"> Item al que se le va a aumentar la cantidad</param>
        /// <returns></returns>
        [RelayCommand]
        public async Task IncrementarCantidadAsync(CarritoItemDto item)
        {
            var nueva = item.Cantidad + 1;
            await _modificarCantidadCarrito.ModificarCantidadCarritoAsync(UserId, item.ProductoId, nueva);
        }

        /// <summary>
        /// Decrementa la cantidad de un producto en el carrito de compras del usuario actual.
        /// </summary>
        /// <param name="item"> Carrito al que se le va a quitar la cantidad </param>
        /// <returns></returns>
        [RelayCommand]
        public async Task DecrementarCantidadAsync(CarritoItemDto item)
        {
            if (item.Cantidad > 1)
            {
                var nueva = item.Cantidad - 1;
                await _modificarCantidadCarrito.ModificarCantidadCarritoAsync(UserId, item.ProductoId, nueva);
            }
            else
            {
                await _borrarProductoCarrito.BorrarProductoCarritoAsync(UserId, item.ProductoId);
            }
        }
        #endregion
    }
}
