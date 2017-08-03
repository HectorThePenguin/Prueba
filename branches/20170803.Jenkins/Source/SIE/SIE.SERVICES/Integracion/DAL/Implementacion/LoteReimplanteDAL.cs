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
    internal class LoteReimplanteDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de LoteReimplante
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(LoteReimplanteInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteReimplanteDAL.ObtenerParametrosCrear(info);
                int result = Create("LoteReimplante_Crear", parameters);
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
        /// Metodo para actualizar un registro de LoteReimplante
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(LoteReimplanteInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteReimplanteDAL.ObtenerParametrosActualizar(info);
                Update("LoteReimplante_Actualizar", parameters);
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
        internal ResultadoInfo<LoteReimplanteInfo> ObtenerPorPagina(PaginacionInfo pagina, LoteReimplanteInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxLoteReimplanteDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("LoteReimplante_ObtenerPorPagina", parameters);
                ResultadoInfo<LoteReimplanteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteReimplanteDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de LoteReimplante
        /// </summary>
        /// <returns></returns>
        internal IList<LoteReimplanteInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("LoteReimplante_ObtenerTodos");
                IList<LoteReimplanteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteReimplanteDAL.ObtenerTodos(ds);
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
        internal IList<LoteReimplanteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteReimplanteDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("LoteReimplante_ObtenerTodos", parameters);
                IList<LoteReimplanteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteReimplanteDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de LoteReimplante
        /// </summary>
        /// <param name="loteReimplanteID">Identificador de la LoteReimplante</param>
        /// <returns></returns>
        internal LoteReimplanteInfo ObtenerPorID(int loteReimplanteID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteReimplanteDAL.ObtenerParametrosPorID(loteReimplanteID);
                DataSet ds = Retrieve("LoteReimplante_ObtenerPorID", parameters);
                LoteReimplanteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteReimplanteDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de LoteReimplante
        /// </summary>
        /// <param name="descripcion">Descripción de la LoteReimplante</param>
        /// <returns></returns>
        internal LoteReimplanteInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteReimplanteDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("LoteReimplante_ObtenerPorDescripcion", parameters);
                LoteReimplanteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteReimplanteDAL.ObtenerPorDescripcion(ds);
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
        /// Metodo para Crear un registro de LoteReimplante
        /// </summary>
        /// <param name="listaReimplantes">Valores de la lista a guardar</param>
        internal int GuardarListaReimplantes(List<LoteReimplanteInfo> listaReimplantes)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteReimplanteDAL.ObtenerParametrosGuardadoLoteReimplantes(listaReimplantes);
                int result = Create("LoteReimplante_GuardarReimplantes", parameters);
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
        /// Obtiene el lote reimplante por lote id
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal LoteReimplanteInfo ObtenerPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parameters = AuxLoteReimplanteDAL.ObtenerParametrosPorLote(lote);
                var ds = Retrieve("LoteReimplante_ObtenerPorLote", parameters);
                LoteReimplanteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteReimplanteDAL.ObtenerPorlote(ds);
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
        /// Obtiene el lote reimplante por lote id
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal List<LoteReimplanteInfo> ObtenerListaPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parameters = AuxLoteReimplanteDAL.ObtenerParametrosPorLote(lote);
                var ds = Retrieve("LoteReimplante_ObtenerPorLote", parameters);
                List<LoteReimplanteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteReimplanteDAL.ObtenerListaPorlote(ds);
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
        /// Obtiene una lista de lotes reimplante
        /// </summary>
        /// <param name="organizacionId"> </param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal IList<LoteReimplanteInfo> ObtenerPorLoteXML(int organizacionId, IList<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteReimplanteDAL.ObtenerParametrosPorLoteXML(
                    organizacionId, lotes);
                var ds = Retrieve("LoteReimplante_ObtenerPorLoteXML", parameters);
                IList<LoteReimplanteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteReimplanteDAL.ObtenerPorloteXML(ds);
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

