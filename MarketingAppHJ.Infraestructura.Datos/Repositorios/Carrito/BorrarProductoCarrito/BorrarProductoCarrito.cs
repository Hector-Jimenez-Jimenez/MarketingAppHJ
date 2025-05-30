using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductoCarrito;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.BorrarProductoCarrito
{
    /// <summary>  
    /// Clase responsable de borrar un producto del carrito de compras en la base de datos en tiempo real de Firebase.  
    /// </summary>  
    public class BorrarProductoCarrito : IBorrarProductoCarrito
    {
        private readonly IFirebaseRealtimeDatabase _client;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="BorrarProductoCarrito"/> con el cliente de Firebase.  
        /// </summary>  
        /// <param name="client">Instancia del cliente de Firebase para interactuar con la base de datos en tiempo real.</param>  
        public BorrarProductoCarrito(IFirebaseRealtimeDatabase client)
        {
            _client = client;
        }

        /// <summary>  
        /// Borra un producto específico del carrito de compras de un usuario en la base de datos en tiempo real de Firebase.  
        /// </summary>  
        /// <param name="userId">Identificador único del usuario.</param>  
        /// <param name="IdProducto">Identificador único del producto a borrar.</param>  
        /// <returns>Una tarea que representa la operación asincrónica de borrado.</returns>  
        public async Task BorrarProductoCarritoAsync(string userId, string IdProducto)
        {
            await _client.Instance
                .Child($"carritos/{userId}/items/{IdProducto}")
                .DeleteAsync();
        }
    }
}
