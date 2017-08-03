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
    internal class ChoferDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear un chofer
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(ChoferInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxChoferDAL.ObtenerParametrosCrear(info);
                Create("Chofer_Crear", parameters);
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
        ///     Metodo que actualiza un chofer
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(ChoferInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxChoferDAL.ObtenerParametrosActualizar(info);
                Update("Chofer_Actualizar", parameters);
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
        internal ResultadoInfo<ChoferInfo> ObtenerPorPagina(PaginacionInfo pagina, ChoferInfo filtro)
        {
            ResultadoInfo<ChoferInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxChoferDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Chofer_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapChoferDAL.ObtenerPorPagina(ds);
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
        /// Obtiene un lista paginada de choferes por proveedor
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ChoferInfo> ObtenerChoferesDeProveedorPorPagina(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ChoferInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxChoferDAL.ObtenerParametrosObtenerChoferesDeProveedorPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ChoferPorProveedor_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapChoferDAL.ObtenerPorPagina(ds);
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
        ///     Obtiene una lista de todos los choferes
        /// </summary>
        /// <returns></returns>
        internal List<ChoferInfo> ObtenerTodos()
        {
            List<ChoferInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Chofer_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    result = MapChoferDAL.ObtenerTodos(ds);
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
        ///     Obtiene una lista de Choferes filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<ChoferInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxChoferDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Chofer_ObtenerTodos", parameters);
                List<ChoferInfo> result = null;

                if (ValidateDataSet(ds))
                {
                    result = MapChoferDAL.ObtenerTodos(ds);
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
        ///     Obtiene un chofer por Id
        /// </summary>
        /// <returns></returns>
        internal ChoferInfo ObtenerPorID(int id)
        {
            ChoferInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxChoferDAL.ObtenerParametroPorID(id);
                DataSet ds = Retrieve("Chofer_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapChoferDAL.ObtenerPorID(ds);
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
        /// Obtiene el chofer por su identificador 
        /// </summary>
        /// <param name="choferInfo"></param>
        /// <returns></returns>
        internal ChoferInfo ObtenerPorID(ChoferInfo choferInfo)
        {
            ChoferInfo result = null;
            try
            {
                Logger.Info();                
                Dictionary<string, object> parameters = AuxChoferDAL.ObtenerParametroPorID(choferInfo);
                DataSet ds = Retrieve("Chofer_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapChoferDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Chofer
        /// </summary>
        /// <param name="descripcion">Descripción de la Chofer</param>
        /// <returns></returns>
        internal ChoferInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxChoferDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Chofer_ObtenerPorDescripcion", parameters);
                ChoferInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapChoferDAL.ObtenerPorDescripcion(ds);
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
        internal ResultadoInfo<ChoferInfo> ObtenerFormulaChoferPorPagina(PaginacionInfo pagina, ChoferInfo filtro)
        {
            ResultadoInfo<ChoferInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxChoferDAL.ObtenerParametrosFormulaChoferPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Chofer_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapChoferDAL.ObtenerPorPagina(ds);
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
    }
}
