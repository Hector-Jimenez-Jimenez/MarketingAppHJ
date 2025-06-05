using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito
{
    /// <summary>
    /// Interfaz para obtener los elementos de un carrito.
    /// </summary>
    public interface IObtenerCarrito
    {
        /// <summary>
        /// Obtiene los elementos del carrito de compras para un usuario específico.
        /// </summary>
        /// <param name="id">El identificador del usuario.</param>
        /// <returns>Una tarea que representa una colección de elementos del carrito.</returns>
        Task<IEnumerable<CarritoItemDto>> ObtenerCarritoAsync(string id);
    }
}
