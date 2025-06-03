using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidos;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.RealtimeDatabase;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Pedidos.ObtenerPedidos
{
    /// <summary>
    /// Clase que implementa la funcionalidad para obtener pedidos desde la base de datos Firebase.
    /// </summary>
    public class ObtenerPedidos : IObtenerPedidos
    {
        private readonly FirebaseRealTimeDatabase _firebaseDatabase;

        /// <summary>
        /// Constructor de la clase <see cref="ObtenerPedidos"/>.
        /// </summary>
        /// <param name="firebaseDatabase">Instancia de la base de datos Firebase.</param>
        public ObtenerPedidos(FirebaseRealTimeDatabase firebaseDatabase)
        {
            _firebaseDatabase = firebaseDatabase;
        }

        /// <summary>
        /// Obtiene una lista de pedidos para un usuario específico.
        /// </summary>
        /// <param name="userId">El identificador del usuario.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene una lista de <see cref="PedidoDto"/>.</returns>
        public async Task<IEnumerable<PedidoDto>> ObtenerPedidosAsync(string userId)
        {
            var pedidos = await _firebaseDatabase.Instance
                .Child($"pedidos/{userId}")
                .OnceAsync<PedidoDto>();

            return pedidos.Select(p =>
            {
                var pedido = p.Object;
                pedido.OrderId = p.Key;
                return pedido;
            })
                .OrderByDescending(p => p.Fecha)
                .ToList();
        }
    }
}
