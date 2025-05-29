using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.LogOut
{
    /// <summary>  
    /// Define una interfaz para cerrar la sesión del usuario actual.  
    /// </summary>  
    public interface ILogOut
    {
        /// <summary>  
        /// Cierra la sesión del usuario actual.  
        /// </summary>  
        /// <returns>Una tarea que representa la operación asincrónica.</returns>  
        void CerrarSesionAsync();
    }
}
