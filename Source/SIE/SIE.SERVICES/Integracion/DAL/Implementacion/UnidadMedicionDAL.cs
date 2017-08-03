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
    internal class UnidadMedicionDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada de usuario por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<UnidadMedicionInfo> ObtenerPorPagina(PaginacionInfo pagina, UnidadMedicionInfo filtro)
        {
            ResultadoInfo<UnidadMedicionInfo> unidadMedicionLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxUnidadMedicionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("UnidadMedicion_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    unidadMedicionLista = MapUnidadMedicionDAL.ObtenerPorPagina(ds);
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
            return unidadMedicionLista;
        }

        /// <summary>
        ///     Obtiene una Unidad de Medicion
        /// </summary>
        /// <param name="unidadMedicionId"></param>
        /// <returns></returns>
        internal UnidadMedicionInfo ObtenerPorID(int unidadMedicionId)
        {
            UnidadMedicionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUnidadMedicionDAL.ObtenerParametroPorID(unidadMedicionId);
                DataSet ds = Retrieve("UnidadMedicion_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapUnidadMedicionDAL.ObtenerPorID(ds);
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

        /// <summary>
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<UnidadMedicionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUnidadMedicionDAL.ObtenerTodos(estatus);
                using (IDataReader reader = RetrieveReader("UnidadMedicion_ObtenerTodos", parameters))
                {
                    IList<UnidadMedicionInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapUnidadMedicionDAL.ObtenerTodos(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    return result;
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
        }
    }        
}
