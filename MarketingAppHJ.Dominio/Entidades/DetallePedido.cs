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
        [ForeignKey("Id_Pedido")]
        public string Id_Pedido { get; set; } = string.Empty;

        /// <summary>  
        /// Identificador único del producto.  
        /// </summary>  
        [ForeignKey("Id_Producto")]
        public string Id_Producto { get; set; } = string.Empty;

        /// <summary>  
        /// Cantidad del producto en el pedido.  
        /// </summary>  
        public int Cantidad { get; set; }

        /// <summary>  
        /// Precio unitario del producto.  
        /// </summary>  
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }
    }
}
