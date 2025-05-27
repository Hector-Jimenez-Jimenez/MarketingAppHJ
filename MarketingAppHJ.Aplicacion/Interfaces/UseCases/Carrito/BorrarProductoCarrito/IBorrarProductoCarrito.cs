using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductoCarrito
{
    /// <summary>
    /// Define la funcionalidad para eliminar un producto del carrito de un usuario.
    /// </summary>
    public interface IBorrarProductoCarrito
    {
        /// <summary>
        /// Elimina un producto del carrito de un usuario.
        /// </summary>
        /// <param name="userId">El identificador del usuario.</param>
        /// <param name="IdProducto">El identificador del producto a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task BorrarProductoCarritoAsync(string userId, string IdProducto);
    }
}
