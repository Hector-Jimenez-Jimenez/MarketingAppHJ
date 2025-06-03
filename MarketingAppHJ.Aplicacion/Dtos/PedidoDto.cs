using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Dtos
{
    /// <summary>
    /// Representa un pedido realizado por un usuario.
    /// </summary>
    public class PedidoDto
    {
        /// <summary>
        /// Identificador único del pedido.
        /// </summary>
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// Identificador único del usuario que realizó el pedido.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Fecha en la que se realizó el pedido.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Dirección de envío del pedido.
        /// </summary>
        public string DireccionEnvio { get; set; } = string.Empty;

        /// <summary>
        /// Método de pago utilizado para el pedido.
        /// </summary>
        public string MetodoPago { get; set; } = string.Empty;

        /// <summary>
        /// Lista de los ítems incluidos en el pedido.
        /// </summary>
        public List<ItemPedidoDto> Items { get; set; } = new List<ItemPedidoDto>();

        public decimal Total => Items != null ? Items.Sum(i => i.Total) : 0m;
    }
}
