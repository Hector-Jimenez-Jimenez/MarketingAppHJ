using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductosCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.CrearPedido;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.GuardarPedido;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Checkout.CrearPedido
{
    /// <summary>
    /// Clase que implementa la lógica para crear un pedido.
    /// </summary>
    public class CrearPedido : ICrearPedido
    {
        private readonly IObtenerCarrito _obtenerCarrito;
        private readonly IBorrarProductosCarrito _borrarProductosCarrito;
        private readonly IGuardarPedido _guardarPedido;

        /// <summary>
        /// Constructor de la clase CrearPedido.
        /// </summary>
        /// <param name="obtenerCarrito">Servicio para obtener el carrito del usuario.</param>
        /// <param name="borrarProductosCarrito">Servicio para borrar los productos del carrito.</param>
        /// <param name="guardarPedido">Servicio para guardar el pedido.</param>
        /// <exception cref="ArgumentNullException">Se lanza si algún parámetro es nulo.</exception>
        public CrearPedido(IObtenerCarrito obtenerCarrito, IBorrarProductosCarrito borrarProductosCarrito, IGuardarPedido guardarPedido)
        {
            _obtenerCarrito = obtenerCarrito ?? throw new ArgumentNullException(nameof(obtenerCarrito));
            _borrarProductosCarrito = borrarProductosCarrito ?? throw new ArgumentNullException(nameof(borrarProductosCarrito));
            _guardarPedido = guardarPedido ?? throw new ArgumentNullException(nameof(guardarPedido));
        }

        /// <summary>
        /// Realiza un pedido basado en el carrito del usuario.
        /// </summary>
        /// <param name="userId">ID del usuario que realiza el pedido.</param>
        /// <param name="direccionEnvio">Dirección de envío del pedido.</param>
        /// <param name="metodoPago">Método de pago seleccionado.</param>
        /// <returns>Un objeto <see cref="PedidoDto"/> que representa el pedido creado.</returns>
        public async Task<PedidoDto> RealizarPedido(string userId, string direccionEnvio, string metodoPago)
        {
            var carrito = await _obtenerCarrito.ObtenerCarritoAsync(userId);

            var pedido = new PedidoDto
            {
                OrderId = Guid.NewGuid().ToString(),
                UserId = userId,
                DireccionEnvio = direccionEnvio,
                MetodoPago = metodoPago,
                Fecha = DateTime.UtcNow,
                Items = carrito.Select(item => new ItemPedidoDto
                {
                    ProductoId = item.ProductoId,
                    Nombre = item.Nombre,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio
                }).ToList()
            };

            await _guardarPedido.GuardarPedidoAsync(pedido);

            await _borrarProductosCarrito.BorrarProductosCarritoAsync(userId);

            return pedido;
        }
    }
}
