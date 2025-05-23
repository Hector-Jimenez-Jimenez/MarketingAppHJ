using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.ObtenerTodosProductos
{
    /// <summary>
    /// Define un caso de uso para obtener una lista de productos.
    /// </summary>
    public interface IObtenerProductos
    {
        /// <summary>
        /// Obtiene una lista de productos de forma asíncrona.
        /// </summary>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado contiene una colección de objetos <see cref="ProductoDto"/>.</returns>
        Task<IEnumerable<ProductoDto>> ObtenerProductosAsync();
    }
}
