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
    internal class FamiliaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Familia
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(FamiliaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFamiliaDAL.ObtenerParametrosCrear(info);
                int result = Create("Familia_Crear", parameters);
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
        /// Metodo para actualizar un registro de Familia
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(FamiliaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFamiliaDAL.ObtenerParametrosActualizar(info);
                Update("Familia_Actualizar", parameters);
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
        ///     Obtiene un lista paginada de usuario por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<FamiliaInfo> ObtenerPorPagina(PaginacionInfo pagina, FamiliaInfo filtro)
        {
            ResultadoInfo<FamiliaInfo> familiaLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxFamiliaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Familia_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    familiaLista = MapFamiliaDAL.ObtenerPorPagina(ds);
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
            return familiaLista;
        }

        /// <summary>
        ///     Obtiene una Familia
        /// </summary>
        /// <param name="familiaId"></param>
        /// <returns></returns>
        internal FamiliaInfo ObtenerPorID(int familiaId)
        {
            FamiliaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFamiliaDAL.ObtenerParametroPorID(familiaId);
                DataSet ds = Retrieve("Familia_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapFamiliaDAL.ObtenerPorID(ds);
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<FamiliaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFamiliaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Familia_ObtenerTodos", parameters);
                IList<FamiliaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFamiliaDAL.ObtenerTodos(ds);
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
        internal IList<FamiliaInfo> Centros_ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFamiliaDAL.Centros_ObtenerTodos(estatus);
                DataSet ds = Retrieve("FamiliaCentros_ObtenerTodos", parameters);
                IList<FamiliaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFamiliaDAL.Centros_ObtenerTodos(ds);
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
        /// Obtiene un registro de Familia
        /// </summary>
        /// <param name="descripcion">Descripción de la Familia</param>
        /// <returns></returns>
        internal FamiliaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFamiliaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Familia_ObtenerPorDescripcion", parameters);
                FamiliaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFamiliaDAL.ObtenerPorDescripcion(ds);
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
