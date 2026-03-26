using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingAppHJ.Dominio.Entidades
{
    /// <summary>  
    /// Representa el detalle de un pedido, incluyendo información sobre el producto, cantidad y precio.  
    /// </summary>  
    public class DetallePedido
    {
        /// <summary>  
        /// Identificador único del detalle del pedido.  
        /// </summary>  
        [Key]
        string Id_Detalle { get; set; } = string.Empty;

        /// <summary>  
        /// Identificador único del pedido al que pertenece este detalle.  
        /// </summary>  
        public string PedidoId { get; set; } = string.Empty;

        /// <summary>  
        /// Identificador único del producto.  
        /// </summary>  
        public string ProductoId { get; set; } = string.Empty;

        /// <summary>  
        /// Cantidad del producto en el pedido.  
        /// </summary>  
        public int Cantidad { get; set; }

        /// <summary>  
        /// Precio unitario del producto.  
        /// </summary>  
        public decimal PrecioUnitario { get; set; }

        [ForeignKey("Id_Pedido")]
        public virtual Pedido Pedido { get; set; }

        [ForeignKey("Id_Producto")]
        public virtual Producto Producto { get; set; }
    }
}
