using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.ObtenerProductosCarrito
{
    /// <summary>
    /// Clase que implementa la interfaz IObtenerCarrito para obtener los productos del carrito desde Firebase.
    /// </summary>
    public class ObtenerProductosCarrito : IObtenerCarrito
    {
        private readonly IFirebaseRealtimeDatabase _firebaseClient;

        /// <summary>
        /// Constructor de la clase ObtenerProductosCarrito.
        /// </summary>
        /// <param name="firebaseClient">Instancia de FirebaseClient para interactuar con la base de datos.</param>
        public ObtenerProductosCarrito(IFirebaseRealtimeDatabase firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        /// <summary>
        /// Obtiene los productos del carrito para un usuario específico.
        /// </summary>
        /// <param name="id">El identificador del carrito.</param>
        /// <returns>Una lista de objetos CarritoItemDto que representan los productos en el carrito.</returns>
        public async Task<IEnumerable<CarritoItemDto>> ObtenerCarritoAsync(string id)
        {
            var productos = await _firebaseClient.Instance
                .Child($"carritos/{id}/items")
                .OnceAsync<CarritoItemDto>();

            return productos
                .Select(s =>
                {
                    var p = s.Object;
                    p.ProductoId = s.Key;
                    return p;
                })
                .ToList();
        }
    }
}
