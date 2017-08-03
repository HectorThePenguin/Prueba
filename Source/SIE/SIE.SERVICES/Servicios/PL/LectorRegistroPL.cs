using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Servicios.PL
{
    public class LectorRegistroPL
    {
        /// <summary>
        /// Obtiene un registro de la tabla LectorRegistro por lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public LectorRegistroInfo ObtenerLectorRegistro(LoteInfo lote,DateTime fecha)
        {
            LectorRegistroInfo lectorRegistro;
            try
            {
                Logger.Info();
                var lectorRegistroBL = new LectorRegistroBL();
                lectorRegistro = lectorRegistroBL.ObtenerLectorRegistro(lote, fecha);
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
            return lectorRegistro;
        }
    }
}
