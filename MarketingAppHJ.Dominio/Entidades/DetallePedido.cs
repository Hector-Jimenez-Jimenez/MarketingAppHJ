using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingApp.Dominio.Entidades
{
    /// <summary>  
    /// Representa el detalle de un pedido, incluyendo información sobre el producto, cantidad y precio.  
    /// </summary>  
    public class DetallePedido
    {
        /// <summary>  
        /// Identificador único del detalle del pedido.  
        /// </summary>  
        public string Id_Detalle { get; set; } = string.Empty;

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
    }
}
