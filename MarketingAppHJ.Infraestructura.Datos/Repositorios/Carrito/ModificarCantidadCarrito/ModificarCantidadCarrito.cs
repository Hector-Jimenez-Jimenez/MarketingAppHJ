using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.AgregarProductoAlCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ModificarCantidadCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.ModificarCantidadCarrito
{
    /// <summary>
    /// Clase que implementa la funcionalidad para modificar la cantidad de un producto en el carrito.
    /// </summary>
    public class ModificarCantidadCarrito : IModificarCantidadCarrito
    {
        private readonly IObtenerCarrito _obtenerProductosCarrito;
        private readonly IAgregarProductoAlCarrito _agregarProductoAlCarrito;

        /// <summary>
        /// Constructor de la clase <see cref="ModificarCantidadCarrito"/>.
        /// </summary>
        /// <param name="obtenerProductosCarrito">Servicio para obtener los productos del carrito.</param>
        /// <param name="agregarProductoAlCarrito">Servicio para agregar productos al carrito.</param>
        public ModificarCantidadCarrito(IObtenerCarrito obtenerProductosCarrito, IAgregarProductoAlCarrito agregarProductoAlCarrito)
        {
            _agregarProductoAlCarrito = agregarProductoAlCarrito;
            _obtenerProductosCarrito = obtenerProductosCarrito;
        }

        /// <summary>
        /// Modifica la cantidad de un producto en el carrito del usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <param name="productId">Identificador del producto.</param>
        /// <param name="cantidad">Nueva cantidad del producto.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task ModificarCantidadCarritoAsync(string userId, string productId, int cantidad)
        {
            var actuales = (await _obtenerProductosCarrito.ObtenerCarritoAsync(userId)).ToList();
            var existente = actuales.FirstOrDefault(p => p.ProductoId == productId) ?? new CarritoItemDto { ProductoId = productId, Cantidad = 0 };

            existente.Cantidad = cantidad;

            await _agregarProductoAlCarrito.AgregarAlCarritoAsync(userId, existente);
        }
    }
}
