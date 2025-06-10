using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ActualizarProducto
{
    /// <summary>
    /// Define la funcionalidad para actualizar la información de un producto en el sistema.
    /// </summary>
    public interface IActualizarProducto
    {
        /// <summary>
        /// Actualiza la información de un producto en el sistema.
        /// </summary>
        /// <param name="producto">Objeto DTO que contiene los datos del producto a actualizar. <see cref="ProductoDto"/></param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task ActualizarProductoAsync(ProductoDto producto);
    }
}
