using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.EliminarProducto;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.EliminarProducto
{
    /// <summary>  
    /// Implementación del caso de uso para eliminar un producto en la base de datos Firebase.  
    /// </summary>  
    public class EliminarProducto : IEliminarProducto
    {
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="EliminarProducto"/>.  
        /// </summary>  
        /// <param name="firebaseRealtimeDatabase">Instancia de la base de datos Firebase.</param>  
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="firebaseRealtimeDatabase"/> es nulo.</exception>  
        public EliminarProducto(IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase ?? throw new ArgumentNullException(nameof(firebaseRealtimeDatabase));
        }

        /// <summary>  
        /// Elimina un producto de la base de datos Firebase.  
        /// </summary>  
        /// <param name="productoID">El identificador único del producto a eliminar.</param>  
        /// <returns>Una tarea que representa la operación asincrónica.</returns>  
        public async Task EliminarProductoAsync(string productoID)
        {
            await _firebaseRealtimeDatabase.Instance
                .Child($"productos/{productoID}")
                .DeleteAsync();
        }
    }
}
