using Firebase.Database.Query;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.AgregarProductoAlCarrito;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.AgregarProductoAlCarrito
{
    /// <summary>
    /// Clase que implementa la funcionalidad para agregar productos al carrito de un usuario.
    /// </summary>
    public class AgregarProdcutoAlCarrito : IAgregarProductoAlCarrito
    {
        private readonly IFirebaseRealtimeDatabase _firebaseClient;

        /// <summary>
        /// Constructor de la clase <see cref="AgregarProdcutoAlCarrito"/>.
        /// </summary>
        /// <param name="firebaseClient">Instancia de <see cref="IFirebaseRealtimeDatabase"/> para interactuar con la base de datos Firebase.</param>
        public AgregarProdcutoAlCarrito(IFirebaseRealtimeDatabase firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        /// <summary>
        /// Agrega un producto al carrito de un usuario y devuelve la lista actualizada de productos en el carrito.
        /// </summary>
        /// <param name="usuarioId">El identificador del usuario.</param>
        /// <param name="item">El producto a agregar al carrito.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene la lista de productos en el carrito.</returns>
        public async Task<IEnumerable<CarritoItemDto>> AgregarAlCarritoAsync(string usuarioId, CarritoItemDto item)
        {
            CarritoItemDto? existente = null;
            try
            {
                existente = await _firebaseClient.Instance
                    .Child($"carritos/{usuarioId}/items/{item.ProductoId}")
                    .OnceSingleAsync<CarritoItemDto>();
            }
            catch
            {
                existente = null;
            }

            if (existente != null)
            {
                item.Cantidad += existente.Cantidad;
            }

            await _firebaseClient.Instance
                .Child($"carritos/{usuarioId}/items/{item.ProductoId}")
                .PutAsync(item);

            var firebaseItems = await _firebaseClient.Instance
                .Child($"carritos/{usuarioId}/items")
                .OnceAsync<CarritoItemDto>();

            return firebaseItems
                .Select(s =>
                {
                    var dto = s.Object;
                    dto.ProductoId = s.Key;
                    return dto;
                })
                .ToList();
        }
    }
}
