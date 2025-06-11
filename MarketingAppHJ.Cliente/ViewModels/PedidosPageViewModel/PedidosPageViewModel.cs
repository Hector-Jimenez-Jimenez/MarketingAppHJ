using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObservarCambiosPedidos;

namespace MarketingAppHJ.Cliente.ViewModels.PedidosPageViewModel
{
    /// <summary>
    /// ViewModel para gestionar la página de pedidos con paginación.
    /// </summary>
    public partial class PedidosPageViewModel : ObservableObject, IDisposable
    {
        private readonly IObtenerPedidos _obtenerPedidos;
        private readonly IObservarCambiosPedido _observarCambios;
        private readonly IFirebaseAuthentication _firebaseAuth;
        private readonly CompositeDisposable _subs = new();

        private const int PageSize = 10;
        private int _currentPage = 0;
        private bool _isLoading = false;
        private bool _hasMore = true;

        private string UserId => _firebaseAuth.UserId;

        [ObservableProperty]
        private ObservableCollection<PedidoDto> pedidos = new();

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private bool showLoadMore;

        public IRelayCommand LoadPedidosCommand { get; }
        public IRelayCommand LoadMoreCommand { get; }

        public PedidosPageViewModel(
            IObtenerPedidos obtenerPedidos,
            IObservarCambiosPedido observarCambios,
            IFirebaseAuthentication firebaseAuth)
        {
            _obtenerPedidos = obtenerPedidos;
            _observarCambios = observarCambios;
            _firebaseAuth = firebaseAuth;

            LoadPedidosCommand = new AsyncRelayCommand(LoadPedidosAsync);
            LoadMoreCommand = new AsyncRelayCommand(LoadMoreAsync);
        }

        /// <summary>
        /// Carga la primera página de pedidos y se suscribe a los cambios en tiempo real.
        /// </summary>
        public async Task LoadPedidosAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            _currentPage = 0;
            Pedidos.Clear();
            _hasMore = true;
            ShowLoadMore = false;

            await LoadMoreAsync();

            // Suscripción a cambios en tiempo real (solo una vez)
            if (_subs.Count == 0)
            {
                var subscription = _observarCambios
                    .ObservarPedidos(UserId)
                    .Subscribe(OnPedidoCambiado);

                _subs.Add(subscription);
            }

            IsBusy = false;
        }

        /// <summary>
        /// Carga la siguiente página de pedidos.
        /// </summary>
        public async Task LoadMoreAsync()
        {
            if (_isLoading || !_hasMore)
                return;

            _isLoading = true;

            // Cambia aquí por tu método paginado
            var nuevosPedidos = await _obtenerPedidos.ObtenerPedidosAsync(UserId);

            foreach (var pedido in nuevosPedidos)
                Pedidos.Add(pedido);

            _hasMore = nuevosPedidos.Count() == PageSize;
            ShowLoadMore = _hasMore;
            _currentPage++;
            _isLoading = false;
        }

        private void OnPedidoCambiado(FirebaseEvent<PedidoDto> evt)
        {
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

        public void Dispose()
        {
            _subs.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
