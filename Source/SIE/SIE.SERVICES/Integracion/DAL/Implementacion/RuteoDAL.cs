using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class RuteoDAL : DALBase
    {
       

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<RuteoInfo> ObtenerPorPagina(PaginacionInfo pagina, RuteoInfo filtro)
        {
            ResultadoInfo<RuteoInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxRuteoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("AdministracionRuteo_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapRuteoDAL.ObtenerPorPagina(ds);
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
        ///     Obtiene un ruteo por id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal RuteoInfo ObtenerPorID(int filtro)
        {
            RuteoInfo lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxRuteoDAL.ObtenerParametrosPorID(filtro);
                DataSet ds = Retrieve("AdministracionRuteo_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapRuteoDAL.ObtenerPorID(ds);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<RuteoDetalleInfo> ObtenerDetallePorPagina(PaginacionInfo pagina, RuteoInfo filtro)
        {
            ResultadoInfo<RuteoDetalleInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxRuteoDAL.ObtenerParametrosDetallePorPagina(pagina, filtro);
                DataSet ds = Retrieve("AdministracionRuteoDetalle_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapRuteoDAL.ObtenerDetallePorPagina(ds);
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
    }
}
