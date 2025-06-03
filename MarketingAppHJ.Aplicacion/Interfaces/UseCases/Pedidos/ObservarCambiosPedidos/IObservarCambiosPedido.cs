using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObservarCambiosPedidos
{
    public interface IObservarCambiosPedido
    {
        IObservable<FirebaseEvent<PedidoDto>> ObservarPedidos(string userId);
    }
}
