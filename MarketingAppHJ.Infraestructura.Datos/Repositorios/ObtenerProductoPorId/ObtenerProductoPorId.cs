using Firebase.Database;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.ObtenerProductosPorId;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.ObtenerProductoPorId
{
    /// <summary>
    /// Caso de uso para obtener un producto por su identificador.
    /// </summary>
    public class ObtenerProductoPorId : IObtenerProductoPorId
    {
        private readonly FirebaseClient _client;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ObtenerProductoPorId"/>.
        /// </summary>
        /// <param name="firebase">El cliente de Firebase utilizado para acceder a los datos.</param>
        public ObtenerProductoPorId(FirebaseClient firebase)
        {
            _client = firebase;
        }

        /// <summary>
        /// Obtiene un producto por su identificador de forma asíncrona.
        /// </summary>
        /// <param name="id">El identificador del producto.</param>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado contiene el producto.</returns>
        public async Task<ProductoDto> ObtenerProductoPorIdAsync(string id)
        {
            var productos = await _client
                .Child("productos")
                .OnceAsync<ProductoDto>();

            var productosDisponibles = productos.Select(x => x.Key).ToList;

            var producto = productos.FirstOrDefault(x => x.Key == id);
            if (producto == null)
            {
                Console.WriteLine($"No se encontró el producto con ID: {id}");
            }

            var dto = producto.Object;
            dto.Id = producto.Key;
            return dto;

        }
    }
}
