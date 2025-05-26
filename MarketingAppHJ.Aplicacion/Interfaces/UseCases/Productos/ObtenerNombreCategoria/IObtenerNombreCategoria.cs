using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerNombreCategoria
{
    /// <summary>
    /// Interfaz para obtener el nombre de una categoría basada en su identificador.
    /// </summary>
    public interface IObtenerNombreCategoria
    {
        /// <summary>
        /// Obtiene el nombre de una categoría dado su identificador.
        /// </summary>
        /// <param name="IdCategoria">El identificador único de la categoría.</param>
        /// <returns>Un objeto <see cref="CategoriaDto"/> que contiene los detalles de la categoría.</returns>
        public Task<CategoriaDto> ObtenerCategoria(string IdCategoria);
    }
}
