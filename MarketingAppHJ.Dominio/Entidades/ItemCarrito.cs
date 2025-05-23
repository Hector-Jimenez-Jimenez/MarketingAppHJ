using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingApp.Dominio.Entidades
{
    /// <summary>
    /// Representa un ítem dentro del carrito de compras.
    /// </summary>
    public class ItemCarrito
    {
        /// <summary>
        /// Identificador único del item del carrito.
        /// </summary>
        public string Id_Carrito { get; set; } = string.Empty;

        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public string ProductoId { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad del producto en el carrito.
        /// </summary>
        public int Cantidad { get; set; }
    }
}
