using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductoCarrito;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.BorrarProductoCarrito
{
    public class BorrarProductoCarrito : IBorrarProductoCarrito
    {
        private readonly IFirebaseRealtimeDatabase _client;

        public BorrarProductoCarrito(IFirebaseRealtimeDatabase client)
        {
            _client = client;
        }

        public async Task BorrarProductoCarritoAsync(string userId, string IdProducto)
        {
            await _client.Instance
                .Child($"carritos/{userId}/items/{IdProducto}")
                .DeleteAsync();
        }
    }
}
