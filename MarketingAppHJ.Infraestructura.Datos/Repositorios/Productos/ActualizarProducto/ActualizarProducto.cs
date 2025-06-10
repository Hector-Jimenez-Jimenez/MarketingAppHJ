using Firebase.Database.Query;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ActualizarProducto;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ActualizarProducto
{
    /// <summary>
    /// Clase que implementa la funcionalidad para actualizar un producto en la base de datos en tiempo real de Firebase.
    /// </summary>
    public class ActualizarProducto : IActualizarProducto
    {
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ActualizarProducto"/>.
        /// </summary>
        /// <param name="firebaseRealtimeDatabase">Instancia de la base de datos en tiempo real de Firebase.</param>
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="firebaseRealtimeDatabase"/> es nulo.</exception>
        public ActualizarProducto(IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase ?? throw new ArgumentNullException(nameof(firebaseRealtimeDatabase));
        }

        /// <summary>
        /// Actualiza un producto en la base de datos en tiempo real de Firebase.
        /// </summary>
        /// <param name="producto">El producto que se va a actualizar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task ActualizarProductoAsync(ProductoDto producto)
        {
            await _firebaseRealtimeDatabase.Instance
                .Child($"productos/{producto.Id}")
                .PutAsync(producto);
        }
    }
}
