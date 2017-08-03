using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class PremezclaDistribucionDetalleBL
    {
        /// <summary>
        /// Guarda una nueva premezcla distribucion de ingredientes
        /// </summary>
        /// <param name="premezclaDistribucionDetalle"></param>
        /// <returns></returns>
        internal PremezclaDistribucionDetalleInfo GuardarPremezclaDistribucionDetalle(PremezclaDistribucionDetalleInfo premezclaDistribucionDetalle)
        {
            try
            {
                var premezclaDistribucionDal = new PremezclaDistribucionDetalleDAL();
                return premezclaDistribucionDal.GuardarPremezclaDistribucionDetalle(premezclaDistribucionDetalle);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
