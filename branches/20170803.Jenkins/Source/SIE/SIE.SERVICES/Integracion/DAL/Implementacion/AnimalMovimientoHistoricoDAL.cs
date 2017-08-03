using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AnimalMovimientoHistoricoDAL : DALBase
    {
        /// <summary>
        /// Obtiene los movimientos de muerte
        /// </summary>
        /// <param name="fechaMuerte"></param>
        /// <returns></returns>
        internal IEnumerable<AnimalMovimientoInfo> ObtenerMovimientosMuertes(DateTime fechaMuerte)
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<AnimalMovimientoInfo> mapeoAnimal =
                    MapAnimalMovimientosHistoricoDAL.ObtenerMapeoAnimalHistorico();
                IEnumerable<AnimalMovimientoInfo> movimientosMuerte = GetDatabase().ExecuteSprocAccessor
                    <AnimalMovimientoInfo>(
                        "AnimalMovimientoHistorico_ObtenerMuertesPorFecha", mapeoAnimal.Build(),
                        new object[] {fechaMuerte});
                return movimientosMuerte;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
