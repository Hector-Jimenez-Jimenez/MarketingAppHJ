using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketingAppHJ.Aplicacion.Interfaces.Idiomas
{
    /// <summary>  
    /// Proporciona métodos para gestionar el idioma de la aplicación.  
    /// </summary>  
    public interface IIdiomaService
    {
        /// <summary>  
        /// Cambia el idioma de la aplicación.  
        /// </summary>  
        /// <param name="idioma">El código del idioma al que se desea cambiar.</param>  
        void CambiarIdioma(string idioma);
    }
}
