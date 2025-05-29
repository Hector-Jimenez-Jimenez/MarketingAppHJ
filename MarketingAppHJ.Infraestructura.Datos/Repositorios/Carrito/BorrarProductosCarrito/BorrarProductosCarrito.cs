using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductosCarrito;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.BorrarProductosCarrito
{
    /// <summary>
    /// Clase responsable de borrar los productos del carrito de un usuario en la base de datos Firebase.
    /// </summary>
    public class BorrarProductosCarrito : IBorrarProductosCarrito
    {
        private readonly IFirebaseRealtimeDatabase _firebaseClient;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BorrarProductosCarrito"/>.
        /// </summary>
        /// <param name="firebaseClient">Cliente de Firebase utilizado para interactuar con la base de datos.</param>
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="firebaseClient"/> es nulo.</exception>
        public BorrarProductosCarrito(IFirebaseRealtimeDatabase firebaseClient)
        {
            _firebaseClient = firebaseClient ?? throw new ArgumentNullException(nameof(firebaseClient));
        }

        /// <summary>
        /// Borra todos los productos del carrito de un usuario específico.
        /// </summary>
        /// <param name="userId">El identificador del usuario cuyo carrito será borrado.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task BorrarProductosCarritoAsync(string userId)
        {
            await _firebaseClient.Instance
                .Child($"carritos/{userId}/items")
                .DeleteAsync();
        }
    }
}
