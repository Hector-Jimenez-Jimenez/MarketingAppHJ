using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.AgregarProducto
{
    /// <summary>  
    /// Define la operación para agregar un producto.  
    /// </summary>  
    public interface IAgregarProducto
    {
        /// <summary>  
        /// Agrega un producto de manera asíncrona.  
        /// </summary>  
        /// <param name="producto">El producto a agregar.</param>  
        /// <returns>Una tarea que representa la operación asíncrona.</returns>  
        Task AgregarProductoAsync(ProductoDto producto);
    }
}
