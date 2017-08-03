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
    public class CamionRepartoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CamionReparto
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(CamionRepartoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionRepartoDAL.ObtenerParametrosCrear(info);
                int result = Create("CamionReparto_Crear", parameters);
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
        /// Metodo para actualizar un registro de CamionReparto
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(CamionRepartoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionRepartoDAL.ObtenerParametrosActualizar(info);
                Update("CamionReparto_Actualizar", parameters);
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
        public ResultadoInfo<CamionRepartoInfo> ObtenerPorPagina(PaginacionInfo pagina, CamionRepartoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCamionRepartoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CamionReparto_ObtenerPorPagina", parameters);
                ResultadoInfo<CamionRepartoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCamionRepartoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CamionReparto
        /// </summary>
        /// <returns></returns>
        public IList<CamionRepartoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CamionReparto_ObtenerTodos");
                IList<CamionRepartoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCamionRepartoDAL.ObtenerTodos(ds);
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
        public IList<CamionRepartoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionRepartoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CamionReparto_ObtenerTodos", parameters);
                IList<CamionRepartoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCamionRepartoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CamionReparto
        /// </summary>
        /// <param name="camionRepartoID">Identificador de la CamionReparto</param>
        /// <returns></returns>
        public CamionRepartoInfo ObtenerPorID(int camionRepartoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionRepartoDAL.ObtenerParametrosPorID(camionRepartoID);
                DataSet ds = Retrieve("CamionReparto_ObtenerPorID", parameters);
                CamionRepartoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCamionRepartoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CamionReparto
        /// </summary>
        /// <param name="descripcion">Descripción de la CamionReparto</param>
        /// <returns></returns>
        public CamionRepartoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionRepartoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CamionReparto_ObtenerPorDescripcion", parameters);
                CamionRepartoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCamionRepartoDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de CamionReparto
        /// </summary>
        /// <param name="organizacionID">Identificador de la Organizacion</param>
        /// <returns></returns>
        public List<CamionRepartoInfo> ObtenerPorOrganizacionID(int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionRepartoDAL.ObtenerParametrosPorOrganizacionID(organizacionID);
                DataSet ds = Retrieve("CamionReparto_ObtenerPorOrganizacionID", parameters);
                List<CamionRepartoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCamionRepartoDAL.ObtenerPorOrganizacionID(ds);
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

