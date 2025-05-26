using Firebase.Database;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerTodosProductos;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ObtenerProductos
{
    /// <summary>
    /// Repositorio para gestionar productos en Firebase.
    /// </summary>
    public class ObtenerProductosCatalogo : IObtenerProductos
    {
        private readonly FirebaseClient _firebaseClient;

        public ObtenerProductosCatalogo(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerProductosAsync()
        {
            var results = await _firebaseClient
                .Child("productos")
                .OnceAsync<ProductoDto>();

            return results.Select(s => 
            { var p = s.Object;  
                p.Id = s.Key; 
                return p; }).ToList();
        }
    }
}
