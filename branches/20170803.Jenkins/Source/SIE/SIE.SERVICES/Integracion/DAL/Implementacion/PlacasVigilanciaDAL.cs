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
    internal class PlacasVigilanciaDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear un Organizacion
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(VigilanciaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoVigilanciaDAL.ObtenerParametrosCrear(info);
                int infoId = Create("[dbo].[Organizacion_Crear]", parameters);
                return infoId;
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

        /// <summary>
        ///     Metodo que actualiza un Organizacion
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(VigilanciaInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProductoVigilanciaDAL.ObtenerParametrosActualizar(info);
                Update("[dbo].[Organizacion_Actualizar]", parameters);
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

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<VigilanciaInfo> ObtenerCamionPorPagina(PaginacionInfo pagina, VigilanciaInfo filtro)
        {
            ResultadoInfo<VigilanciaInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxPlacasVigilancia.ObtenerParametrosCamionPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Vigilancia_Placas]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MAPPlacasVigilanciaDAL.ObtenerCamionPorPagina(ds);
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
        ///     Obtiene una lista de todos los Organizaciones
        /// </summary>
        /// <returns></returns>
        internal List<VigilanciaInfo> ObtenerTodos()
        {
            List<VigilanciaInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("f");
                if (ValidateDataSet(ds))
                {
                    result = MAPPlacasVigilanciaDAL.ObtenerTodos(ds);
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
        ///     Obtiene una lista de Organizacion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<VigilanciaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            List<VigilanciaInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoVigilanciaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerTodos]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MAPPlacasVigilanciaDAL.ObtenerTodos(ds);
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
        /// Obtiene un Organizacion por Id
        /// </summary>
        /// <returns></returns>
        internal VigilanciaInfo ObtenerPorID(VigilanciaInfo vigilancia)
        {
            VigilanciaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPlacasVigilancia.ObtenerParametroPorID(vigilancia);
                DataSet ds = Retrieve("Vigilancia_ObtenerCamionIDProveedorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MAPPlacasVigilanciaDAL.ObtenerPorID(ds);
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