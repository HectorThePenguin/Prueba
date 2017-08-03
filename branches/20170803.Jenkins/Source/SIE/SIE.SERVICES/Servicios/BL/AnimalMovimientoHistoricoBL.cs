using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class AnimalMovimientoHistoricoBL
    {
        /// <summary>
        /// Obtiene los movimientos de muerte
        /// </summary>
        /// <param name="fechaMuerte"></param>
        /// <returns></returns>
        public IEnumerable<AnimalMovimientoInfo> ObtenerMovimientosMuertes(DateTime fechaMuerte)
        {
            IEnumerable<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animalMovimientoHistoricoDAL = new AnimalMovimientoHistoricoDAL();
                result = animalMovimientoHistoricoDAL.ObtenerMovimientosMuertes(fechaMuerte);
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
