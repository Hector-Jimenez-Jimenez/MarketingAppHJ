using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObservarCambiosPedidos
{
    /// <summary>
    /// Interfaz para observar los cambios en los pedidos de un usuario específico.
    /// </summary>
    public interface IObservarCambiosPedido
    {
        /// <summary>
        /// Observa los cambios en los pedidos asociados a un usuario.
        /// </summary>
        /// <param name="userId">El identificador del usuario cuyos pedidos se desean observar.</param>
        /// <returns>Un observable que emite eventos de Firebase relacionados con los pedidos.</returns>
        IObservable<FirebaseEvent<PedidoDto>> ObservarPedidos(string userId);
    }
}
