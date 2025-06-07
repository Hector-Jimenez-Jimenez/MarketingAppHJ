using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidoPorId
{
    /// <summary>
    /// Define la funcionalidad para obtener los detalles de un pedido por su identificador.
    /// </summary>
    public interface IObtenerPedidoPorId
    {
        /// <summary>
        /// Obtiene un pedido por su identificador.
        /// </summary>
        /// <param name="orderId">El identificador del pedido.</param>
        /// <param name="userId">El identificador del usuario que realizó el pedido.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene el pedido solicitado.</returns>
        Task<PedidoDto?> ObtenerDetallesPedidoAsync(string userId, string orderId);
    }
}
