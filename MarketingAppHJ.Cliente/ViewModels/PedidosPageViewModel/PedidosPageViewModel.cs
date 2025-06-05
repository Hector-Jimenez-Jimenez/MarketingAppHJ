using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObservarCambiosPedidos;

namespace MarketingAppHJ.Cliente.ViewModels.PedidosPageViewModel
{
    /// <summary>
    /// ViewModel para gestionar la página de pedidos.
    /// </summary>
    public partial class PedidosPageViewModel : ObservableObject, IDisposable
    {
        private readonly IObtenerPedidos _obtenerPedidos;
        private readonly IObservarCambiosPedido _observarCambios;
        private readonly IFirebaseAuthentication _firebaseAuth;
        private readonly CompositeDisposable _subs = new();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PedidosPageViewModel"/>.
        /// </summary>
        /// <param name="obtenerPedidos">Servicio para obtener pedidos.</param>
        /// <param name="observarCambios">Servicio para observar cambios en los pedidos.</param>
        /// <param name="firebaseAuth">Servicio de autenticación de Firebase.</param>
        public PedidosPageViewModel(
            IObtenerPedidos obtenerPedidos,
            IObservarCambiosPedido observarCambios,
            IFirebaseAuthentication firebaseAuth)
        {
            _obtenerPedidos = obtenerPedidos;
            _observarCambios = observarCambios;
            _firebaseAuth = firebaseAuth;
        }

        // Capturamos el UserId del usuario autenticado
        private string UserId => _firebaseAuth.UserId;

        [ObservableProperty]
        private ObservableCollection<PedidoDto> pedidos = new();

        [ObservableProperty]
        private bool isBusy;

        /// <summary>
        /// Carga los pedidos del usuario autenticado y se suscribe a los cambios en tiempo real.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task LoadPedidosAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var lista = await _obtenerPedidos.ObtenerPedidosAsync(UserId);
            Pedidos = new ObservableCollection<PedidoDto>(lista);

            var subscription = _observarCambios
                .ObservarPedidos(UserId)
                .Subscribe(OnPedidoCambiado);

            _subs.Add(subscription);

            IsBusy = false;
        }

        private void OnPedidoCambiado(FirebaseEvent<PedidoDto> evt)
        {
            // Actualizar UI en hilo principal
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var key = evt.Key;

                switch (evt.EventType)
                {
                    case FirebaseEventType.Delete:
                        var aEliminar = Pedidos.FirstOrDefault(p => p.OrderId == key);
                        if (aEliminar != null)
                            Pedidos.Remove(aEliminar);
                        break;

                    case FirebaseEventType.InsertOrUpdate:
                        if (evt.Object is not null)
                        {
                            var dto = evt.Object;
                            dto.OrderId = key;

                            var existente = Pedidos.FirstOrDefault(p => p.OrderId == key);
                            if (existente != null)
                            {
                                // Si ya existía, lo reemplazamos en la misma posición:
                                var idx = Pedidos.IndexOf(existente);
                                Pedidos[idx] = dto;
                            }
                            else
                            {
                                Pedidos.Insert(0, dto);
                            }
                        }
                        break;
                }
            });
        }

        /// <summary>
        /// Libera los recursos utilizados por la instancia de <see cref="PedidosPageViewModel"/>.
        /// </summary>
        public void Dispose()
        {
            _subs.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
