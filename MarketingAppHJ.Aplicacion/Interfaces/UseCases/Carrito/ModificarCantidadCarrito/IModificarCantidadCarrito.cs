using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ModificarCantidadCarrito
{
    /// <summary>
    /// Define un contrato para modificar la cantidad de un producto en el carrito de un usuario.
    /// </summary>
    public interface IModificarCantidadCarrito
    {
        /// <summary>
        /// Modifica la cantidad de un producto en el carrito de un usuario.
        /// </summary>
        /// <param name="userId">El identificador del usuario.</param>
        /// <param name="productId">El identificador del producto.</param>
        /// <param name="cantidad">La nueva cantidad del producto.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task ModificarCantidadCarritoAsync(string userId, string productId, int cantidad);
    }
}
