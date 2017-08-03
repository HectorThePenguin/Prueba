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
    internal class IndicadorDAL :DALBase
    {
        /// <summary>
        /// Obtiene el indicador por su identificador.
        /// </summary>
        /// <param name="indicadorInfo"></param>
        /// <returns></returns>
        internal IndicadorInfo ObtenerPorId(IndicadorInfo indicadorInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxIndicadorDAL.ObtenerParametrosObtenerPorId(indicadorInfo);
                var ds = Retrieve("Indicador_ObtenerPorId", parameters);
                IndicadorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorDAL.ObtenerPorId(ds);
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

        internal IndicadorInfo ObtenerPorContratoID(IndicadorInfo indicadorInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxIndicadorDAL.ObtenerParametrosObtenerPorId(indicadorInfo);
                var ds = Retrieve("Indicador_ObtenerPorContratoID", parameters);
                IndicadorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorDAL.ObtenerPorId(ds);
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
        /// Metodo para Crear un registro de Indicador
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(IndicadorInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorDAL.ObtenerParametrosCrear(info);
                int result = Create("Indicador_Crear", parameters);
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
        /// Metodo para actualizar un registro de Indicador
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(IndicadorInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorDAL.ObtenerParametrosActualizar(info);
                Update("Indicador_Actualizar", parameters);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<IndicadorInfo> ObtenerPorPagina(PaginacionInfo pagina, IndicadorInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxIndicadorDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Indicador_ObtenerPorPagina", parameters);
                ResultadoInfo<IndicadorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Indicador
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Indicador_ObtenerTodos");
                IList<IndicadorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorDAL.ObtenerTodos(ds);
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Indicador_ObtenerTodos", parameters);
                IList<IndicadorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Indicador
        /// </summary>
        /// <param name="descripcion">Descripción de la Indicador</param>
        /// <returns></returns>
        public IndicadorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Indicador_ObtenerPorDescripcion", parameters);
                IndicadorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorDAL.ObtenerPorDescripcion(ds);
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
