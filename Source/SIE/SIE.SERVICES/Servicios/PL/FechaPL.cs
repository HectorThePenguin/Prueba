using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class FechaPL
    {
        /// <summary>
        /// Obtiene la fecha del servidor de base de datos
        /// </summary>
        /// <returns></returns>
        public FechaInfo ObtenerFechaActual()
        {
            FechaInfo fecha;
            try
            {
                Logger.Info();
                var fechaBl = new FechaBL();
                fecha = fechaBl.ObtenerFechaActual();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return fecha;
        }
    }
}
