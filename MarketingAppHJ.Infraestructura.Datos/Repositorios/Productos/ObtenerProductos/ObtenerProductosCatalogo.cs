using Firebase.Database;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerTodosProductos;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ObtenerProductos
{
    /// <summary>
    /// Repositorio para gestionar productos en Firebase.
    /// </summary>
    public class ObtenerProductosCatalogo : IObtenerProductos
    {
        private readonly IFirebaseRealtimeDatabase _firebaseClient;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ObtenerProductosCatalogo"/>.
        /// </summary>
        /// <param name="firebaseClient">Cliente de Firebase Realtime Database.</param>
        public ObtenerProductosCatalogo(IFirebaseRealtimeDatabase firebaseClient) => _firebaseClient = firebaseClient;

        /// <summary>
        /// Obtiene todos los productos del catálogo desde Firebase.
        /// </summary>
        /// <returns>Una colección de objetos <see cref="ProductoDto"/>.</returns>
        public async Task<IEnumerable<ProductoDto>> ObtenerProductosAsync()
        {
            var results = await _firebaseClient.Instance
                .Child("productos")
                .OnceAsync<ProductoDto>();

            return results.Select(s =>
            {
                var p = s.Object;
                p.Id = s.Key;
                return p;
            }).ToList();
        }
    }
}
