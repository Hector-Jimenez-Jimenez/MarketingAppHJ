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
        /// <param name="usuarioId">El identificador del usuario.</param>
        /// <param name="productoId">El identificador del producto.</param>
        /// <param name="cantidad">La cantidad del producto a agregar.</param>
        /// <returns>Un objeto <see cref="CarritoItemDto"/> que representa el producto agregado al carrito.</returns>
        Task<CarritoItemDto> AgregarAlCarritoAsync(string usuarioId, string productoId, int cantidad);
    }
}
