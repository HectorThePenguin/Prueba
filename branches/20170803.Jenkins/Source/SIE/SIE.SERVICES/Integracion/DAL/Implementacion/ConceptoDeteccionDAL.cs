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
    internal class ConceptoDeteccionDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de ConceptoDeteccion
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(ConceptoDeteccionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConceptoDeteccionDAL.ObtenerParametrosCrear(info);
                int result = Create("ConceptoDeteccion_Crear", parameters);
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
        /// Metodo para actualizar un registro de ConceptoDeteccion
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(ConceptoDeteccionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConceptoDeteccionDAL.ObtenerParametrosActualizar(info);
                Update("ConceptoDeteccion_Actualizar", parameters);
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
        internal ResultadoInfo<ConceptoDeteccionInfo> ObtenerPorPagina(PaginacionInfo pagina, ConceptoDeteccionInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxConceptoDeteccionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ConceptoDeteccion_ObtenerPorPagina", parameters);
                ResultadoInfo<ConceptoDeteccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConceptoDeteccionDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de ConceptoDeteccion
        /// </summary>
        /// <returns></returns>
        internal IList<ConceptoDeteccionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ConceptoDeteccion_ObtenerTodos");
                IList<ConceptoDeteccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConceptoDeteccionDAL.ObtenerTodos(ds);
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
        internal IList<ConceptoDeteccionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConceptoDeteccionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("ConceptoDeteccion_ObtenerTodos", parameters);
                IList<ConceptoDeteccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConceptoDeteccionDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de ConceptoDeteccion
        /// </summary>
        /// <param name="conceptoDeteccionID">Identificador de la ConceptoDeteccion</param>
        /// <returns></returns>
        internal ConceptoDeteccionInfo ObtenerPorID(int conceptoDeteccionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConceptoDeteccionDAL.ObtenerParametrosPorID(conceptoDeteccionID);
                DataSet ds = Retrieve("ConceptoDeteccion_ObtenerPorID", parameters);
                ConceptoDeteccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConceptoDeteccionDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de ConceptoDeteccion
        /// </summary>
        /// <param name="descripcion">Descripción de la ConceptoDeteccion</param>
        /// <returns></returns>
        internal ConceptoDeteccionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConceptoDeteccionDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("ConceptoDeteccion_ObtenerPorDescripcion", parameters);
                ConceptoDeteccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConceptoDeteccionDAL.ObtenerPorDescripcion(ds);
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

