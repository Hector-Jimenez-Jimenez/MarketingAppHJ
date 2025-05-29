using Firebase.Database.Query;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.GuardarPedido;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Checkout.GuardarPedido
{
    /// <summary>
    /// Clase que implementa la funcionalidad para guardar pedidos en Firebase.
    /// </summary>
    public class GuardarPedido : IGuardarPedido
    {
        private readonly IFirebaseRealtimeDatabase _firebaseClient;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GuardarPedido"/>.
        /// </summary>
        /// <param name="firebaseClient">Cliente de Firebase utilizado para interactuar con la base de datos.</param>
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="firebaseClient"/> es nulo.</exception>
        public GuardarPedido(IFirebaseRealtimeDatabase firebaseClient)
        {
            _firebaseClient = firebaseClient ?? throw new ArgumentNullException(nameof(firebaseClient));
        }

        /// <summary>
        /// Guarda un pedido en la base de datos de Firebase.
        /// </summary>
        /// <param name="pedido">El pedido que se va a guardar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task GuardarPedidoAsync(PedidoDto pedido)
        {
            await _firebaseClient.Instance
                .Child($"pedidos/{pedido.UserId}/pedidos/{pedido.OrderId}")
                .PutAsync(pedido);
        }
    }
}
