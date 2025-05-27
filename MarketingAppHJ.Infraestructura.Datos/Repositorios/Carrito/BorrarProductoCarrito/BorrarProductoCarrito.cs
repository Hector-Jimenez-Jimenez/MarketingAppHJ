using Firebase.Database;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductoCarrito;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.BorrarProductoCarrito
{
    public class BorrarProductoCarrito : IBorrarProductoCarrito
    {
        private readonly FirebaseClient _client;

        public BorrarProductoCarrito(FirebaseClient client)
        {
            _client = client;
        }

        public async Task BorrarProductoCarritoAsync(string userId, string IdProducto)
        {
            await _client
                .Child($"carritos/{userId}/items/{IdProducto}")
                .DeleteAsync();
        }
    }
}
