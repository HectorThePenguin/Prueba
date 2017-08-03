using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class TarifarioDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TarifarioInfo> ObtenerPorPagina(PaginacionInfo pagina, TarifarioInfo filtro)
        {
            ResultadoInfo<TarifarioInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxTarifarioDAL.ObtenerParametrosDetallePorPagina(pagina, filtro);
                DataSet ds = Retrieve("Tarifario_ObtenerPorPaginaEmbarqueTarifa", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapTarifarioDAL.ObtenerPorPagina(ds);
                }
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
            return lista;
        }

        /// <summary>
        ///  Obtiene una Configuracion de Embarque filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<ConfiguracionEmbarqueInfo> ObtenerConfiguracionEmbarqueActivas(TarifarioInfo filtro, EstatusEnum estatus)
        {
            List<ConfiguracionEmbarqueInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTarifarioDAL.ObtenerConfiguracionEmbarqueActivas(filtro, estatus);
                DataSet ds = Retrieve("Tarifario_ObtenerConfiguracionEmbarque", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapTarifarioDAL.ObtenerConfiguracionEmbarqueActivas(ds);
                }
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
            return result;
        }
    }
}
