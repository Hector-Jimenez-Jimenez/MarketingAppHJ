using Firebase.Database.Query;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.AgregarProducto;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.AgregarProducto
{
    /// <summary>  
    /// Clase para agregar productos al repositorio.  
    /// </summary>  
    public class AgregarProducto : IAgregarProducto
    {
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;
        public AgregarProducto(IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase ?? throw new ArgumentNullException(nameof(firebaseRealtimeDatabase));
        }

        public async Task AgregarProductoAsync(ProductoDto producto)
        {
            await _firebaseRealtimeDatabase.Instance
                .Child("productos")
                .PostAsync(producto);
        }
    }
}
