using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;



namespace SIE.Services.Servicios.PL
{
    public class ConciliacionPL
    {

        /// <summary>
        /// Metodo  que  obtiene una lista de conciliacion  por tipo de poliza
        /// </summary>
        /// <param name="TipoPoliza"></param>
        /// <returns></returns>
        public IList<PolizasIncorrectasInfo> ConciliacionTipoPoliza(int TipoPoliza)
        {
            try
            {
                Logger.Info();
                var conciliacionBL = new ConciliacionBL();
                IList<PolizasIncorrectasInfo> result = conciliacionBL.ConciliacionTipoPoliza(TipoPoliza);
                return result;
            }
            catch(ExcepcionGenerica)
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
        /// Metodo que Obtiene una lista  de conciliacion detalle por tipo poliza
        /// </summary>
        /// <param name="TipoPoliza"></param>
        /// <returns></returns>
        public IList<ConciliacionInfo> ConciliacionDetalle(int TipoPoliza)
        {
            try
            {
                Logger.Info();
                var conciliacionBL = new ConciliacionBL();
                 IList<ConciliacionInfo> result = conciliacionBL.ConciliacionDetalle(TipoPoliza);
                 return result;
            }
                     catch(ExcepcionGenerica)
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
