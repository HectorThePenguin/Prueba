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
    internal class ChoferVigilanciaDAL : DALBase
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
                Dictionary<string, object> parameters = AuxChoferVigilanciaDAL.ObtenerParametrosCrear(info);
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
                Dictionary<string, object> parameters = AuxChoferVigilanciaDAL.ObtenerParametrosActualizar(info);
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
                Dictionary<string, object> parameters = AuxChoferVigilanciaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Vigilancia_ChoferObtenerPorPagina", parameters); 
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
                Dictionary<string, object> parameters = AuxChoferVigilanciaDAL.ObtenerTodos(estatus);
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
        /// Obtiene un chofer por Id
        /// </summary>
        /// <returns></returns>
        internal ChoferInfo ObtenerPorID(ChoferInfo chofer)
        {
            ChoferInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxChoferVigilanciaDAL.ObtenerParametroPorID(chofer); 
                DataSet ds = Retrieve("Vigilancia_Chofer_ObtenerPorID", parameters);
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
                Dictionary<string, object> parameters = AuxChoferVigilanciaDAL.ObtenerParametrosPorDescripcion(descripcion);
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
    }
}
