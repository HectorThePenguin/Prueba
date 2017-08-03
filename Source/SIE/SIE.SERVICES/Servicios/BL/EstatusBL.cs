using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class EstatusBL
    {
        /// <summary>
        ///      Obtiene los Estatus dados de alta
        /// </summary>
        /// <returns> </returns>
        public List<EstatusInfo> ObtenerEstatusTipoEstatus(int tipoEstatus)
        {
            List<EstatusInfo> info;
            try
            {
                Logger.Info();
                var almacenDAL = new EstatusDAL();
                info = almacenDAL.ObtenerEstatusTipoEstatus(tipoEstatus);
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
