using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class EstatusPL
    {
        /// <summary>
        ///      Obtiene el estatus del almacen movimiento
        /// </summary>
        /// <returns> </returns>
        public List<EstatusInfo> ObtenerEstatusTipoEstatus(int tipoEstatus)
        {
            List<EstatusInfo> info;
            try
            {
                Logger.Info();
                var estatusBL = new EstatusBL();
                info = estatusBL.ObtenerEstatusTipoEstatus(tipoEstatus);
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
