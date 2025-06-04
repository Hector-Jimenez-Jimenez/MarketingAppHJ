using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObservarCambiosPedidos;
using Microsoft.Maui.Dispatching;

namespace MarketingAppHJ.Cliente.ViewModels.PedidosPageViewModel
{
    public partial class PedidosPageViewModel : ObservableObject, IDisposable
    {
        private readonly IObtenerPedidos _obtenerPedidos;
        private readonly IObservarCambiosPedido _observarCambios;
        private readonly IFirebaseAuthentication _firebaseAuth;
        private readonly CompositeDisposable _subs = new();

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
        string UserId => _firebaseAuth.UserId;

        [ObservableProperty]
        ObservableCollection<PedidoDto> pedidos = new();

        [ObservableProperty]
        bool isBusy;

        public async Task LoadPedidosAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            // 1) Carga inicial de todos los pedidos del usuario
            var lista = await _obtenerPedidos.ObtenerPedidosAsync(UserId);
            Pedidos = new ObservableCollection<PedidoDto>(lista);

            // 2) Suscripción a cambios en tiempo real en "pedidos/{UserId}"
            var subscription = _observarCambios
                .ObservarPedidos(UserId)
                .Subscribe(OnPedidoCambiado);

            _subs.Add(subscription);

            IsBusy = false;
        }

        void OnPedidoCambiado(FirebaseEvent<PedidoDto> evt)
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

        public void Dispose()
        {
            _subs.Dispose();
        }
    }
}
