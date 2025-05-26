using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Dtos
{
    public class CategoriaDto
    {
        /// <summary>
        /// Identificador único de la categoría.
        /// </summary>
        public string Id_Categoria { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de la categoría.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la categoría.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;
    }
}
