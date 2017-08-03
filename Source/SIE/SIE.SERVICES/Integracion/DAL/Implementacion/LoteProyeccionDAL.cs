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
    internal class LoteProyeccionDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de LoteProyeccion
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(LoteProyeccionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteProyeccionDAL.ObtenerParametrosCrear(info);
                int result = Create("LoteProyeccion_Crear", parameters);
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
        /// Metodo para actualizar un registro de LoteProyeccion
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(LoteProyeccionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteProyeccionDAL.ObtenerParametrosActualizar(info);
                Update("LoteProyeccion_Actualizar", parameters);
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
        internal ResultadoInfo<LoteProyeccionInfo> ObtenerPorPagina(PaginacionInfo pagina, LoteProyeccionInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxLoteProyeccionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("LoteProyeccion_ObtenerPorPagina", parameters);
                ResultadoInfo<LoteProyeccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteProyeccionDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de LoteProyeccion
        /// </summary>
        /// <returns></returns>
        internal IList<LoteProyeccionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("LoteProyeccion_ObtenerTodos");
                IList<LoteProyeccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteProyeccionDAL.ObtenerTodos(ds);
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
        internal IList<LoteProyeccionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteProyeccionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("LoteProyeccion_ObtenerTodos", parameters);
                IList<LoteProyeccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteProyeccionDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de LoteProyeccion
        /// </summary>
        /// <param name="loteProyeccionID">Identificador de la LoteProyeccion</param>
        /// <returns></returns>
        internal LoteProyeccionInfo ObtenerPorID(int loteProyeccionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteProyeccionDAL.ObtenerParametrosPorID(loteProyeccionID);
                DataSet ds = Retrieve("LoteProyeccion_ObtenerPorID", parameters);
                LoteProyeccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteProyeccionDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de LoteProyeccion
        /// </summary>
        /// <param name="descripcion">Descripción de la LoteProyeccion</param>
        /// <returns></returns>
        internal LoteProyeccionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteProyeccionDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("LoteProyeccion_ObtenerPorDescripcion", parameters);
                LoteProyeccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteProyeccionDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de LoteProyeccion
        /// </summary>
        /// <param name="lote">Lote del cual se obtendra la proyeccion</param>
        /// <returns></returns>
        internal LoteProyeccionInfo ObtenerPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parameters = AuxLoteProyeccionDAL.ObtenerParametrosObtenerPorLote(lote);
                DataSet ds = Retrieve("LoteProyeccion_ObtenerPorLote", parameters);
                LoteProyeccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteProyeccionDAL.ObtenerPorLote(ds);
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
        /// Obtiene una lista de lotes proyeccion
        /// </summary>
        /// <param name="organizacionId"> </param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal IList<LoteProyeccionInfo> ObtenerPorLoteXML(int organizacionId, IList<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteProyeccionDAL.ObtenerPorLoteXML(organizacionId, lotes);
                DataSet ds = Retrieve("LoteProyeccion_ObtenerPorLoteXML", parameters);
                IList<LoteProyeccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteProyeccionDAL.ObtenerPorLoteXML(ds);
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
        /// Metodo para obtener las proyecciones de los corrales origenes para un corral
        /// </summary>
        /// <param name="loteCorral"></param>
        /// <returns></returns>
        internal List<LoteProyeccionInfo> ObtenerProyeccionDeLotesOrigen(LoteCorralReimplanteInfo loteCorral)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                        {{"@CorralDestinoID", loteCorral.Corral.CorralID},
                         {"@LoteDestinoID", loteCorral.Lote.LoteID}};
                DataSet ds = Retrieve("ReimplanteGanado_ObtenerProyeccionDeLotesOrigen", parametros);
                List<LoteProyeccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteProyeccionDAL.ObtenerProyeccionDeLotesOrigen(ds);
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
        /// Obtiene un registro de LoteProyeccion
        /// </summary>
        /// <param name="lote">Lote del cual se obtendra la proyeccion</param>
        /// <returns></returns>
        internal LoteProyeccionInfo ObtenerPorLoteCompleto(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parameters = AuxLoteProyeccionDAL.ObtenerParametrosObtenerPorLoteCompleto(lote);
                DataSet ds = Retrieve("LoteProyeccion_ObtenerPorLoteCompleto", parameters);
                LoteProyeccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteProyeccionDAL.ObtenerPorLoteCompleto(ds);
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
        /// Metodo para Crear un registro de LoteProyeccion
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int CrearBitacora(LoteProyeccionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteProyeccionDAL.ObtenerParametrosCrearBitacora(info);
                int result = Create("LoteProyeccionBitacora_Crear", parameters);
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
        /// Obtiene un registro de LoteProyeccion
        /// </summary>
        /// <param name="loteProyeccionID">Identificador de la LoteProyeccion</param>
        /// <returns></returns>
        internal LoteProyeccionInfo ObtenerBitacoraPorLoteProyeccionID(int loteProyeccionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteProyeccionDAL.ObtenerParametrosPorLoteProyeccionID(loteProyeccionID);
                DataSet ds = Retrieve("LoteProyeccionBitacora_ObtenerPorLoteProyeccionID", parameters);
                LoteProyeccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteProyeccionDAL.ObtenerPorLoteProyeccionID(ds);
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

