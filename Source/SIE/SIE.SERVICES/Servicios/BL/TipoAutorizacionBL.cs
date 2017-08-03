using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class TipoAutorizacionBL
    {
        /// <summary>
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<MovimientosAutorizarModel> ObtenerMovimientosAutorizacion()
        {
            try
            {
                Logger.Info();
                var movimientoDal = new TipoAutorizacionDAL();
                return movimientoDal.ObtenerMovimientosAutorizacion();
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
        /// <summary>
        /// Valida que se cumplan las precondiciones
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal int ValidarPreCondiciones()
        {
            var result = 0;
            try
            {
                Logger.Info();
                var precondicionesDal = new TipoAutorizacionDAL();
                result = precondicionesDal.ValidarPreCondiciones();
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
            return result;
        }
    }
}
