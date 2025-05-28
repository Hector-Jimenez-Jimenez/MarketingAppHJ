using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.GuardarPedido
{
    /// <summary>
    /// Define la funcionalidad para guardar un pedido en el sistema.
    /// </summary>
    public interface IGuardarPedido
    {
        /// <summary>
        /// Guarda un pedido de forma asíncrona.
        /// </summary>
        /// <param name="pedido">El objeto <see cref="PedidoDto"/> que contiene los detalles del pedido a guardar.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        Task GuardarPedidoAsync(PedidoDto pedido);
    }
}
