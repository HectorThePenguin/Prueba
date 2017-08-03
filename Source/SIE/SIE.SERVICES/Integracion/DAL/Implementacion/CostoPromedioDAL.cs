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
    internal class CostoPromedioDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CostoPromedio
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(CostoPromedioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoPromedioDAL.ObtenerParametrosCrear(info);
                int result = Create("CostoPromedio_Crear", parameters);
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
        /// Metodo para actualizar un registro de CostoPromedio
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CostoPromedioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoPromedioDAL.ObtenerParametrosActualizar(info);
                Update("CostoPromedio_Actualizar", parameters);
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
        internal ResultadoInfo<CostoPromedioInfo> ObtenerPorPagina(PaginacionInfo pagina, CostoPromedioInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCostoPromedioDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CostoPromedio_ObtenerPorPagina", parameters);
                ResultadoInfo<CostoPromedioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCostoPromedioDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CostoPromedio
        /// </summary>
        /// <returns></returns>
        internal IList<CostoPromedioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CostoPromedio_ObtenerTodos");
                IList<CostoPromedioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCostoPromedioDAL.ObtenerTodos(ds);
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
        internal IList<CostoPromedioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoPromedioDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CostoPromedio_ObtenerTodos", parameters);
                IList<CostoPromedioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCostoPromedioDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CostoPromedio
        /// </summary>
        /// <param name="costoPromedioID">Identificador de la CostoPromedio</param>
        /// <returns></returns>
        internal CostoPromedioInfo ObtenerPorID(int costoPromedioID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoPromedioDAL.ObtenerParametrosPorID(costoPromedioID);
                DataSet ds = Retrieve("CostoPromedio_ObtenerPorID", parameters);
                CostoPromedioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCostoPromedioDAL.ObtenerPorID(ds);
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
        /// Obtiene una entidad por su organización y costo
        /// </summary>
        /// <param name="organizacionId">Organización </param>
        /// <param name="costoId">Costo </param>
        /// <returns></returns>
        internal CostoPromedioInfo ObtenerPorOrganizacionCosto(int organizacionId , int costoId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoPromedioDAL.ObtenerPorOrganizacionCosto(organizacionId, costoId)
                ;
                DataSet ds = Retrieve("CostoPromedio_ObtenerPorOrganizacionCosto", parameters);
                CostoPromedioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCostoPromedioDAL.ObtenerPorOrganizacionCosto(ds);
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

