using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObservarCambiosPedidos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidos;
using TheMarketingApp.Dominio.Entidades;

namespace MarketingAppHJ.Cliente.ViewModels.PedidosPageViewModel
{
    public partial class PedidosPageViewModel : ObservableObject, IDisposable
    {
        private readonly IObtenerPedidos _obtenerPedidos;
        private readonly IObservarCambiosPedido _observarCambios;
        private readonly IFirebaseAuthentication _firebaseAuth;
        private readonly CompositeDisposable _subs = new();

        [ObservableProperty]
        ObservableCollection<PedidoDto> pedidos = new();

        [ObservableProperty]
        bool isBusy;

        public PedidosPageViewModel(
            IObtenerPedidos obtenerPedidos,
            IObservarCambiosPedido observarCambios,
            IFirebaseAuthentication firebaseAuth)
        {
            _obtenerPedidos = obtenerPedidos;
            _observarCambios = observarCambios;
            _firebaseAuth = firebaseAuth;

            // Carga inicial y suscripción a cambios en tiempo real
            _ = LoadPedidosAsync();
        }

        private string UserId => _firebaseAuth.UserId;

        public async Task LoadPedidosAsync()
        {
            if (IsBusy) return;
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
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var key = evt.Key;
                var dto = evt.Object;
                dto.OrderId = key;

                switch (evt.EventType)
                {
                    case FirebaseEventType.Delete:
                        var eliminable = Pedidos.FirstOrDefault(p => p.OrderId == key);
                        if (eliminable != null)
                            Pedidos.Remove(eliminable);
                        break;

                    case FirebaseEventType.InsertOrUpdate:
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
                        break;
                }
            });
        }

        [RelayCommand]
        public async Task RefreshAsync()
        {
            await LoadPedidosAsync();
        }

        public void Dispose()
        {
            _subs.Dispose();
        }
    }
}
