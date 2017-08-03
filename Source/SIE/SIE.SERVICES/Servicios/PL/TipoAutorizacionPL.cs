using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TipoAutorizacionPL
    {
        /// <summary>
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public List<MovimientosAutorizarModel> ObtenerMovimientosAutorizacion()
        {
            try
            {
                Logger.Info();
                var movimientoBl = new TipoAutorizacionBL();
                return movimientoBl.ObtenerMovimientosAutorizacion();
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
        public int ValidarPreCondiciones()
        {
            var result = 0;
            try
            {
                Logger.Info();
                var precondicionesBl = new TipoAutorizacionBL();
                result = precondicionesBl.ValidarPreCondiciones();
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
