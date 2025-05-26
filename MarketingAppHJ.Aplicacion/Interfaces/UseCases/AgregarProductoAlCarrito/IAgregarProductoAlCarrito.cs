using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.AgregarProductoAlCarrito
{
    /// <summary>
    /// Define un caso de uso para agregar un producto al carrito de compras de un usuario.
    /// </summary>
    public interface IAgregarProductoAlCarrito
    {
        /// <summary>
        /// Agrega un producto al carrito de compras de un usuario.
        /// </summary>
        /// <param name="usuarioId">El identificador único del usuario.</param>
        /// <param name="item">El producto que se desea agregar al carrito.</param>
        /// <returns>Una lista de elementos actualizada en el carrito del usuario.</returns>
        Task<IEnumerable<CarritoItemDto>> AgregarAlCarritoAsync(string usuarioId, CarritoItemDto item);
    }
}
