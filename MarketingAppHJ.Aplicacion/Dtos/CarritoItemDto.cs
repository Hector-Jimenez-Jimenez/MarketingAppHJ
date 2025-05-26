using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Dtos
{
    /// <summary>
    /// Representa un item dentro del carrito de compras.
    /// </summary>
    public partial class CarritoItemDto
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
        /// URL de la imagen del producto.
        /// </summary>
        public string ImagenUrl { get; set; } = string.Empty;

        /// <summary>
        /// Precio unitario del producto.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Cantidad del producto en el carrito.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Total calculado como el precio unitario multiplicado por la cantidad.
        /// </summary>
        public decimal Total => Precio * Cantidad;
    }
}
