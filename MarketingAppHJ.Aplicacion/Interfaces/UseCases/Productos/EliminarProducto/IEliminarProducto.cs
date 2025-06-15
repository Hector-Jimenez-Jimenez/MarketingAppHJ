using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.EliminarProducto
{
    /// <summary>  
    /// Define la funcionalidad para eliminar un producto por su identificador.  
    /// </summary>  
    public interface IEliminarProducto
    {
        /// <summary>  
        /// Elimina un producto de forma asíncrona utilizando su identificador único.  
        /// </summary>  
        /// <param name="productoID">El identificador único del producto que se desea eliminar.</param>  
        /// <returns>Una tarea que representa la operación de eliminación.</returns>  
        Task EliminarProductoAsync(string productoID);
    }
}
