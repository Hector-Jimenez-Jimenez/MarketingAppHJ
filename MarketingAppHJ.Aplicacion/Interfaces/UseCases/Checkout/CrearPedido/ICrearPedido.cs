using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.CrearPedido
{
    /// <summary>
    /// Define la operación para crear un pedido.
    /// </summary>
    public interface ICrearPedido
    {
        /// <summary>
        /// Crea un pedido para un usuario específico.
        /// </summary>
        /// <param name="userId">El identificador del usuario que realiza el pedido.</param>
        /// <param name="direccionEnvio">La dirección de envío del pedido.</param>
        /// <param name="metodoPago">El método de pago utilizado para el pedido.</param>
        /// <returns>Un objeto <see cref="PedidoDto"/> que representa el pedido creado.</returns>
        public Task<PedidoDto> RealizarPedido(string userId, string direccionEnvio, string metodoPago);
    }
}
