using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class FechaBL
    {
        /// <summary>
        /// Obtiene la fecha actual
        /// </summary>
        /// <returns></returns>
        internal FechaInfo ObtenerFechaActual()
        {
            FechaInfo info;
            try
            {
                Logger.Info();
                var fechaDAL = new FechaDAL();
                info = fechaDAL.ObtenerFechaActual();
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
            return info;
        }
    }
}
