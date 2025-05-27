using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductosCarrito
{
    /// <summary>
    /// Define la funcionalidad para eliminar todos los productos del carrito de un usuario.
    /// </summary>
    public interface IBorrarProductosCarrito
    {
        /// <summary>
        /// Elimina todos los productos del carrito de un usuario.
        /// </summary>
        /// <param name="userId">El identificador del usuario.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task BorrarProductosCarritoAsync(string userId);
    }
}
