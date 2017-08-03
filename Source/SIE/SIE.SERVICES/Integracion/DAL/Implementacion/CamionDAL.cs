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
    internal class CamionDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear un Camion
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(CamionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametrosCrear(info);
                Create("Camion_Crear", parameters);
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
        ///     Metodo que actualiza un Camion
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(CamionInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametrosActualizar(info);
                Update("Camion_Actualizar", parameters);
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
        internal ResultadoInfo<CamionInfo> ObtenerPorPagina(PaginacionInfo pagina, CamionInfo filtro)
        {
            ResultadoInfo<CamionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Camion_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapCamionDAL.ObtenerPorPagina(ds);
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
        ///     Obtiene una lista de todos los Camiones
        /// </summary>
        /// <returns></returns>
        internal List<CamionInfo> ObtenerTodos()
        {
            List<CamionInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Camion_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    result = MapCamionDAL.ObtenerTodos(ds);
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
        ///     Obtiene una lista de todos los Camiones
        /// </summary>
        /// <returns></returns>
        internal List<CamionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            List<CamionInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Camion_ObtenerTodos", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCamionDAL.ObtenerTodos(ds);
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
        ///     Obtiene un Camion por Id
        /// </summary>
        /// <returns></returns>
        internal CamionInfo ObtenerPorID(int id)
        {
            CamionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametroPorID(id);
                DataSet ds = Retrieve("Camion_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCamionDAL.ObtenerPorID(ds);
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
        ///     Obtiene los Camiones de un Proveedor
        /// </summary>
        /// <param name="proveedorId">Id del Proveedor del que se consultaran sus camiones</param>
        /// <returns></returns>
        internal List<CamionInfo> ObtenerPorProveedorID(int proveedorId)
        {
            List<CamionInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametroPorProveedorId(proveedorId);
                DataSet ds = Retrieve("Camion_ObtenerPorProveedorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCamionDAL.ObtenerPorProveedorID(ds);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="Dependencias"> </param>
        /// <returns></returns>
        internal ResultadoInfo<CamionInfo> ObtenerPorPagina(PaginacionInfo pagina, CamionInfo filtro, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<CamionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametrosPorPagina(pagina, filtro, Dependencias);
                DataSet ds = Retrieve("Camion_ObtenerPorPaginaProveedorId", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapCamionDAL.ObtenerPorDependencias(ds);
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
        ///      Obtiene un Camion
        /// </summary>
        /// <returns> </returns>
        internal CamionInfo ObtenerPorInfo(CamionInfo camion, IList<IDictionary<IList<string>, object>> dependencias)
        {
            CamionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametroPorInfoDependencias(camion, dependencias);
                DataSet ds = Retrieve("Camion_ObtenerPorCamionIdProveedorId", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCamionDAL.ObtenerPorInfoDependencias(ds);
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
        /// Obtiene un registro por su descripción
        /// </summary>
        /// <param name="descripcion">Descripción</param>
        /// <returns></returns>
        internal CamionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Camion_ObtenerPorDescripcion", parameters);
                CamionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCamionDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un camion por su clave
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CamionInfo ObtenerPorCamion(CamionInfo filtro)
        {
            CamionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametrosPorDescripcion(filtro.PlacaCamion);
                DataSet ds = Retrieve("Camion_ObtenerPorDescripcion", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCamionDAL.ObtenerPorDescripcion(ds);
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

    /*    public CamionInfo ObtenerCamionPorProveedor(int proveedorId)
        {
            
        }*/

        /// <summary>
        /// Obtiene un camion por id enviando un parametro CamionInfo
        /// </summary>
        /// <returns></returns>
        internal CamionInfo ObtenerPorId(CamionInfo camionInfo)
        {
            CamionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametroPorId(camionInfo);
                DataSet ds = Retrieve("Camion_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCamionDAL.ObtenerPorID(ds);
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
        /// Obtiene un camion por proveedorID y camionID
        /// </summary>
        /// <param name="camionInfo"></param>
        /// <returns></returns>
        internal CamionInfo ObtenerPorProveedorIdCamionId(CamionInfo camionInfo)
        {
            CamionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametroObtenerPorProveedorIdCamionId(camionInfo);
                DataSet ds = Retrieve("Camion_ObtenerPorProveedorIDCamionID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCamionDAL.ObtenerPorID(ds);
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
        /// Obtiene un camion por placa
        /// </summary>
        /// <param name="placaCamion"></param>
        /// <returns></returns>
        internal CamionInfo ObtenerPorPlaca(string placaCamion)
        {
            CamionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerParametroPorPlaca(placaCamion);
                DataSet ds = Retrieve("Camion_ObtenerPorPlaca", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCamionDAL.ObtenerPorPlaca(ds);
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
