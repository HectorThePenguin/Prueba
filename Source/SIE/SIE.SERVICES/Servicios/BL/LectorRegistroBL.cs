using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class LectorRegistroBL
    {
        /// <summary>
        /// Obtiene un registro de la tabla LectorRegistro por lote
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public LectorRegistroInfo ObtenerLectorRegistro(LoteInfo lote, DateTime fecha)
        {
            try
            {
                Logger.Info();
                var lectorRegistroDAL = new LectorRegistroDAL();
                LectorRegistroInfo lectorRegistro = lectorRegistroDAL.ObtenerLectorRegistro(lote,fecha);
                return lectorRegistro;
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
        }
    }
}
