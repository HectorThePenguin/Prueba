using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ProveedorAlmacenDAL : DALBase
    {
        /// <summary>
        /// Metodo que obtiene un proveedor almacen por proveedor id
        /// </summary>
        /// <returns></returns>
        internal ProveedorAlmacenInfo ObtenerPorProveedorId(ProveedorInfo proveedorInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxProveedorAlmacenDAL.ObtenerParametrosObtenerPorProveedorId(proveedorInfo);
                var ds = Retrieve("ProveedorAlmacen_ObtenerPorProveedorID", parameters);
                ProveedorAlmacenInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorAlmacenDAL.ObtenerPorProveedorId(ds);
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
        /// Metodo que obtiene un proveedor almacen por proveedor id
        /// </summary>
        /// <returns></returns>
        internal ProveedorAlmacenInfo ObtenerPorAlmacenID(int almacenID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxProveedorAlmacenDAL.ObtenerParametrosObtenerPorAlmacenID(almacenID);
                var ds = Retrieve("ProveedorAlmacen_ObtenerPorAlmacenID", parameters);
                ProveedorAlmacenInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorAlmacenDAL.ObtenerPorAlmacenID(ds);
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
        /// Metodo para Crear un registro de ProveedorAlmacen
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(ProveedorAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorAlmacenDAL.ObtenerParametrosCrear(info);
                int result = Create("ProveedorAlmacen_Crear", parameters);
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
        /// Metodo para actualizar un registro de ProveedorAlmacen
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(ProveedorAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorAlmacenDAL.ObtenerParametrosActualizar(info);
                Update("ProveedorAlmacen_Actualizar", parameters);
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
        /// Metodo que obtiene un proveedor almacen por proveedor id
        /// </summary>
        /// <returns></returns>
        internal ProveedorAlmacenInfo ObtenerPorProveedorTipoAlmacen(ProveedorAlmacenInfo proveedorInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxProveedorAlmacenDAL.ObtenerParametrosObtenerPorProveedorTipoAlmacen(proveedorInfo);
                var ds = Retrieve("ProveedorAlmacen_ObtenerPorProveedorTipoAlmacen", parameters);
                ProveedorAlmacenInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorAlmacenDAL.ObtenerPorProveedorTipoAlmacen(ds);
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
