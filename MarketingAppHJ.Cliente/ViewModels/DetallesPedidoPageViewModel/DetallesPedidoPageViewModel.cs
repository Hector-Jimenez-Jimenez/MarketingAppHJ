
using CommunityToolkit.Mvvm.ComponentModel;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidoPorId;

namespace MarketingAppHJ.Cliente.ViewModels.DetallesPedidoPageViewModel
{
    public partial class DetallesPedidoPageViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IObtenerPedidoPorId _obtenerPedidoPorId;
        private readonly IFirebaseAuthentication _authService;

        [ObservableProperty] private PedidoDto pedido;

        public DetallesPedidoPageViewModel(IObtenerPedidoPorId obtenerPedidoPorId, IFirebaseAuthentication authService)
        {
            _obtenerPedidoPorId = obtenerPedidoPorId;
            _authService = authService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("pedidoId", out var idObj) && idObj is string pedidoId)
            {
                var user = _authService.GetInstance().User;
                if (user is null) return;

                var pedido = await _obtenerPedidoPorId.ObtenerDetallesPedidoAsync(user.Uid, pedidoId);
                Pedido = pedido;
            }
        }
    }
}
