using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidos
{
    public interface IObtenerPedidos
    {
        Task<IEnumerable<PedidoDto>> ObtenerPedidosAsync(string userId);
    }
}
