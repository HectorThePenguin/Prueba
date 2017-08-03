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
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class RepartoAlimentoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de RepartoAlimento
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(RepartoAlimentoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDAL.ObtenerParametrosCrear(info);
                int result = Create("RepartoAlimento_Crear", parameters);
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
        /// Metodo para actualizar un registro de RepartoAlimento
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(RepartoAlimentoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDAL.ObtenerParametrosActualizar(info);
                Update("RepartoAlimento_Actualizar", parameters);
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
        internal ResultadoInfo<RepartoAlimentoInfo> ObtenerPorPagina(PaginacionInfo pagina, RepartoAlimentoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxRepartoAlimentoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("RepartoAlimento_ObtenerPorPagina", parameters);
                ResultadoInfo<RepartoAlimentoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de RepartoAlimento
        /// </summary>
        /// <returns></returns>
        internal IList<RepartoAlimentoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("RepartoAlimento_ObtenerTodos");
                IList<RepartoAlimentoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDAL.ObtenerTodos(ds);
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
        internal IList<RepartoAlimentoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("RepartoAlimento_ObtenerTodos", parameters);
                IList<RepartoAlimentoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de RepartoAlimento
        /// </summary>
        /// <param name="repartoAlimentoID">Identificador de la RepartoAlimento</param>
        /// <returns></returns>
        internal RepartoAlimentoInfo ObtenerPorID(int repartoAlimentoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDAL.ObtenerParametrosPorID(repartoAlimentoID);
                DataSet ds = Retrieve("RepartoAlimento_ObtenerPorID", parameters);
                RepartoAlimentoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de RepartoAlimento
        /// </summary>
        /// <param name="descripcion">Descripción de la RepartoAlimento</param>
        /// <returns></returns>
        internal RepartoAlimentoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("RepartoAlimento_ObtenerPorDescripcion", parameters);
                RepartoAlimentoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de RepartoAlimento
        /// </summary>
        /// <param name="filtro">Descripción de la RepartoAlimento</param>
        /// <returns></returns>
        internal RepartoAlimentoInfo ConsultarRepartos(FiltroCheckListReparto filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDAL.ObtenerParametrosConsultarReparto(filtro);
                DataSet ds = Retrieve("RepartoAlimento_ObtenerPorFiltros", parameters);
                RepartoAlimentoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDAL.ObtenerPorConsultarRepartos(ds);
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
        /// Obtiene un registro de RepartoAlimento
        /// </summary>
        /// <param name="filtro">Descripción de la RepartoAlimento</param>
        /// <returns></returns>
        internal List<RepartoAlimentoInfo> ImprimirRepartos(FiltroCheckListReparto filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDAL.ObtenerParametrosImprimirReparto(filtro);
                DataSet ds = Retrieve("RepartoAlimento_ObtenerImpresion", parameters);
                List<RepartoAlimentoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDAL.ObtenerPorImprimirRepartos(ds);
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

