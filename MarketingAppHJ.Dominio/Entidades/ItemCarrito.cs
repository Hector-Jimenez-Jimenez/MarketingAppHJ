using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingAppHJ.Dominio.Entidades
{
    /// <summary>
    /// Representa un ítem dentro del carrito de compras.
    /// </summary>
    public class ItemCarrito
    {
        /// <summary>
        /// Identificador único del item del carrito.
        /// </summary>
        [Key]
        public string Id_Carrito { get; set; } = string.Empty;

        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public string ProductoId { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad del producto en el carrito.
        /// </summary>
        public int Cantidad { get; set; }

        [ForeignKey("Id_Producto")]
        public virtual Producto Producto { get; set; }
    }
}
