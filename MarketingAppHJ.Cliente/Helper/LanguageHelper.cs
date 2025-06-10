using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Cliente.Helper
{
    /// <summary>  
    /// Proporciona métodos para la traducción de cadenas utilizando recursos.  
    /// </summary>  
    public static class LanguageHelper
    {
        /// <summary>  
        /// Administrador de recursos para acceder a las cadenas de recursos.  
        /// </summary>  
        private static readonly ResourceManager ResourceManager =
            new ResourceManager("MarketingAppHJ.Resources.Strings", Assembly.GetExecutingAssembly());

        /// <summary>  
        /// Traduce una clave de recurso al idioma actual de la interfaz de usuario.  
        /// </summary>  
        /// <param name="clave">La clave de recurso que se desea traducir.</param>  
        /// <returns>La cadena traducida si se encuentra; de lo contrario, devuelve la clave original.</returns>  
        public static string Traducir(string clave)
        {
            return ResourceManager.GetString(clave, CultureInfo.CurrentUICulture) ?? clave;
        }
    }
}
