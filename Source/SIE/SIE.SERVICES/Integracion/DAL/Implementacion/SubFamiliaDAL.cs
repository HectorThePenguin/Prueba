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
    internal class SubFamiliaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de SubFamilia
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(SubFamiliaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSubFamiliaDAL.ObtenerParametrosCrear(info);
                int result = Create("SubFamilia_Crear", parameters);
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
        /// Metodo para actualizar un registro de SubFamilia
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(SubFamiliaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSubFamiliaDAL.ObtenerParametrosActualizar(info);
                Update("SubFamilia_Actualizar", parameters);
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
        internal ResultadoInfo<SubFamiliaInfo> ObtenerPorPagina(PaginacionInfo pagina, SubFamiliaInfo filtro)
        {
            ResultadoInfo<SubFamiliaInfo> subFamiliaLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxSubFamiliaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("SubFamilia_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    subFamiliaLista = MapSubFamiliaDAL.ObtenerPorPagina(ds);
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
            return subFamiliaLista;
        }

        /// <summary>
        ///     Obtiene una SubFamilia
        /// </summary>
        /// <param name="subFamiliaId"></param>
        /// <returns></returns>
        internal SubFamiliaInfo ObtenerPorID(int subFamiliaId)
        {
            SubFamiliaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSubFamiliaDAL.ObtenerParametroPorID(subFamiliaId);
                DataSet ds = Retrieve("SubFamilia_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapSubFamiliaDAL.ObtenerPorID(ds);
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
        internal IList<SubFamiliaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSubFamiliaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("SubFamilia_ObtenerTodos", parameters);
                IList<SubFamiliaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSubFamiliaDAL.ObtenerTodos(ds);
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
        internal IList<SubFamiliaInfo> Centros_ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSubFamiliaDAL.Centros_ObtenerTodos(estatus);
                DataSet ds = Retrieve("SubFamiliaCentros_ObtenerTodos", parameters);
                IList<SubFamiliaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSubFamiliaDAL.Centros_ObtenerTodos(ds);
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
        internal IList<SubFamiliaInfo> ObtenerPorFamiliaID(int familiaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSubFamiliaDAL.ObtenerPorFamiliaID(familiaID);
                DataSet ds = Retrieve("SubFamilia_ObtenerPorFamiliaID", parameters);
                IList<SubFamiliaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSubFamiliaDAL.ObtenerPorFamiliaID(ds);
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
        /// Obtiene un registro de SubFamilia
        /// </summary>
        /// <param name="descripcion">Descripción de la SubFamilia</param>
        /// <param name="familiaId"> </param>
        /// <returns></returns>
        internal SubFamiliaInfo ObtenerPorDescripcion(string descripcion, int familiaId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSubFamiliaDAL.ObtenerParametrosPorDescripcion(descripcion, familiaId);
                DataSet ds = Retrieve("SubFamilia_ObtenerPorDescripcion", parameters);
                SubFamiliaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSubFamiliaDAL.ObtenerPorDescripcion(ds);
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
        ///     Obtiene un SubFamilia por familia
        /// </summary>
        /// <param name="subFamilia"></param>
        /// <returns></returns>
        internal SubFamiliaInfo ObtenerPorIDPorFamilia(SubFamiliaInfo subFamilia)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSubFamiliaDAL.ObtenerParametrosPorFamilia(subFamilia);
                DataSet ds = Retrieve("SubFamilia_ObtenerPorFamilia", parameters);
                SubFamiliaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSubFamiliaDAL.ObtenerPorIDPorFamilia(ds);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<SubFamiliaInfo> ObtenerPorPaginaPorFamilia(PaginacionInfo pagina, SubFamiliaInfo filtro)
        {
            ResultadoInfo<SubFamiliaInfo> subFamiliaLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxSubFamiliaDAL.ObtenerParametrosPorPaginaPorFamilia(pagina, filtro);
                DataSet ds = Retrieve("SubFamilia_ObtenerPorPaginaPorFamilia", parameters);
                if (ValidateDataSet(ds))
                {
                    subFamiliaLista = MapSubFamiliaDAL.ObtenerPorPaginaPorFamilia(ds);
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
            return subFamiliaLista;
        }
    }
}
