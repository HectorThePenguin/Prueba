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
    internal class TipoCostoDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoCostoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCostoInfo filtro)
        {
            ResultadoInfo<TipoCostoInfo> costoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxTipoCostoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TipoCosto_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    costoLista = MapTipoCostoDAL.ObtenerPorPagina(ds);
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
            return costoLista;
        }

        /// <summary>
        ///     Metodo que actualiza un Tipo Costo
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(TipoCostoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoCostoDAL.ObtenerParametrosActualizar(info);
                Update("[dbo].[TipoCosto_Actualizar]", parameters);
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
        ///     Obtiene un TipoCostoInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        internal TipoCostoInfo ObtenerPorID(int infoId)
        {
            TipoCostoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCostoDAL.ObtenerParametroPorID(infoId);
                DataSet ds = Retrieve("[dbo].[TipoCosto_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCostoDAL.ObtenerPorID(ds);
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
        ///     Metodo que crear un Tipo Costo
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(TipoCostoInfo info)
        {
            int infoId;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCostoDAL.ObtenerParametrosGuardado(info);
                infoId = Create("[dbo].[TipoCosto_Crear]", parameters);
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

            return infoId;
        }

        /// <summary>
        ///  Obtiene una lista de TipoCosto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<TipoCostoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            List<TipoCostoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCostoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoCosto_ObtenerTodos", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCostoDAL.ObtenerTodos(ds);
                }

                return result;
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
        /// Obtiene un registro de TipoCosto
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoCosto</param>
        /// <returns></returns>
        internal TipoCostoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCostoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoCosto_ObtenerPorDescripcion", parameters);
                TipoCostoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCostoDAL.ObtenerPorDescripcion(ds);
                }
                return result;
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
