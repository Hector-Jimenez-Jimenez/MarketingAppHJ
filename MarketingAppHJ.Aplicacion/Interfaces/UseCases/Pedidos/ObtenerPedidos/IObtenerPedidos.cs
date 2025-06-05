using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidos
{
    /// <summary>
    /// Interfaz para obtener los pedidos de un usuario.
    /// </summary>
    public interface IObtenerPedidos
    {
        /// <summary>
        /// Obtiene una lista de pedidos asociados a un usuario específico.
        /// </summary>
        /// <param name="userId">El identificador del usuario.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene una colección de objetos PedidoDto.</returns>
        Task<IEnumerable<PedidoDto>> ObtenerPedidosAsync(string userId);
    }
}
