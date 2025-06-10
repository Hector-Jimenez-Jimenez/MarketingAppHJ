using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Cliente.Helper
{
    public static class PerformanceHelper
    {
        /// <summary>
        /// Aplica el tema guardado al iniciar la aplicación.
        /// </summary>
        public static void AplicarTemaPersistido()
        {
            var temaGuardado = Preferences.Get("app_tema", "sistema");
            Application.Current!.UserAppTheme = temaGuardado switch
            {
                "claro" => AppTheme.Light,
                "oscuro" => AppTheme.Dark,
                _ => AppTheme.Unspecified
            };
        }

        /// <summary>
        /// Guarda la preferencia del tema del usuario.
        /// </summary>
        public static void GuardarTema(string tema)
        {
            Preferences.Set("app_tema", tema);
        }

        /// <summary>
        /// Guarda y aplica el idioma preferido del usuario.
        /// </summary>
        public static void GuardarIdioma(string codigoISO)
        {
            Preferences.Set("idioma", codigoISO);
            AplicarIdioma(codigoISO);
        }

        /// <summary>
        /// Aplica el idioma global de la aplicación.
        /// </summary>
        public static void AplicarIdiomaPersistido()
        {
            var codigo = Preferences.Get("idioma", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            AplicarIdioma(codigo);
        }

        private static void AplicarIdioma(string codigoISO)
        {
            var cultura = new CultureInfo(codigoISO);
            Thread.CurrentThread.CurrentCulture = cultura;
            Thread.CurrentThread.CurrentUICulture = cultura;
        }

        /// <summary>
        /// Configuraciones de rendimiento globales.
        /// </summary>
        public static void ConfigurarOptimizaciónGlobal()
        {
            // GC para menos pausas de recolección
            //GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;

            // Aplicar idioma guardado
            AplicarIdiomaPersistido();
        }
    }
}
