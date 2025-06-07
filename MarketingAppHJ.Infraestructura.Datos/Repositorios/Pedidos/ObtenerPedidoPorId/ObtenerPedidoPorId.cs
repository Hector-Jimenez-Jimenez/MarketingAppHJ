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
    public class ObtenerPedidoPorId : IObtenerPedidoPorId
    {
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;

        public ObtenerPedidoPorId(IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase ?? throw new ArgumentNullException(nameof(firebaseRealtimeDatabase));
        }

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
