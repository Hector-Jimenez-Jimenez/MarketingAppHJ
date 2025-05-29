using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Dtos
{
    /// <summary>
    /// Representa un elemento de un pedido, incluyendo información del producto, cantidad y precio.
    /// </summary>
    public class ItemPedidoDto
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public string ProductoId { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Precio unitario del producto.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Cantidad del producto solicitada.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Total calculado como el producto del precio y la cantidad.
        /// </summary>
        public decimal Total => Precio * Cantidad;

        public string ImagenUrl { get; set; } = string.Empty;
    }
}
