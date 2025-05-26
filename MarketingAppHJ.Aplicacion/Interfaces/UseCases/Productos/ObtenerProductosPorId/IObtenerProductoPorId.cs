using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerProductosPorId
{
    /// <summary>
    /// Define un caso de uso para obtener un producto por su identificador único.
    /// </summary>
    public interface IObtenerProductoPorId
    {
        /// <summary>
        /// Obtiene un producto por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único del producto.</param>
        /// <returns>Un objeto <see cref="ProductoDto"/> que representa el producto.</returns>
        Task<ProductoDto> ObtenerProductoPorIdAsync(string id);
    }
}
