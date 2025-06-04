using System.Threading.Tasks;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObservarCambiosPedidos;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.RealtimeDatabase;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Pedidos.ObservarCambiosPedidos
{
    /// <summary>
    /// Clase que implementa la interfaz para observar cambios en los pedidos de un usuario.
    /// </summary>
    public class ObservarCambiosPedidos : IObservarCambiosPedido
    {
        private readonly FirebaseRealTimeDatabase _firebaseDatabase;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ObservarCambiosPedidos"/>.
        /// </summary>
        /// <param name="firebaseDatabase">Instancia de la base de datos en tiempo real de Firebase.</param>
        public ObservarCambiosPedidos(FirebaseRealTimeDatabase firebaseDatabase)
        {
            _firebaseDatabase = firebaseDatabase;
        }

        /// <summary>
        /// Observa los cambios en los pedidos de un usuario específico.
        /// </summary>
        /// <param name="userId">El identificador del usuario cuyos pedidos se van a observar.</param>
        /// <returns>Un observable que emite eventos de Firebase con los datos de los pedidos.</returns>
        IObservable<FirebaseEvent<PedidoDto>> IObservarCambiosPedido.ObservarPedidos(string userId)
        {
            return _firebaseDatabase.Instance
                .Child($"pedidos/{userId}/pedidos")
                .AsObservable<PedidoDto>();
        }
    }
}
