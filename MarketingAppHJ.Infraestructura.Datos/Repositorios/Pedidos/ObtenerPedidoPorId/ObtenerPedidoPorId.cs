using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidoPorId;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Pedidos.ObtenerPedidoPorId
{
    /// <summary>  
    /// Clase que implementa la funcionalidad para obtener los detalles de un pedido por su ID.  
    /// </summary>  
    public class ObtenerPedidoPorId : IObtenerPedidoPorId
    {
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="ObtenerPedidoPorId"/>.  
        /// </summary>  
        /// <param name="firebaseRealtimeDatabase">Instancia de la base de datos en tiempo real de Firebase.</param>  
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="firebaseRealtimeDatabase"/> es nulo.</exception>  
        public ObtenerPedidoPorId(IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase ?? throw new ArgumentNullException(nameof(firebaseRealtimeDatabase));
        }

        /// <summary>  
        /// Obtiene los detalles de un pedido específico para un usuario dado.  
        /// </summary>  
        /// <param name="userId">ID del usuario.</param>  
        /// <param name="orderId">ID del pedido.</param>  
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene los detalles del pedido o <c>null</c> si no se encuentra.</returns>  
        /// <exception cref="KeyNotFoundException">Se lanza si no se encuentra el pedido con el ID especificado.</exception>  
        public Task<PedidoDto?> ObtenerDetallesPedidoAsync(string userId, string orderId)
        {
            var pedido = _firebaseRealtimeDatabase.Instance
                .Child($"pedidos/{userId}/pedidos/{orderId}")
                .OnceSingleAsync<PedidoDto>();

            if (pedido == null)
            {
                throw new KeyNotFoundException($"No se encontró el pedido con ID {orderId} para el usuario {userId}.");
            }
            else
            {
                return pedido;
            }
        }
    }
}
