using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito
{
    public interface IObtenerCarrito
    {
        Task <IEnumerable<CarritoItemDto>> ObtenerCarritoAsync(string id);
    }
}
