using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CondicionJaulaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CondicionJaula
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(CondicionJaulaInfo info)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@Descripcion", info.Descripcion},
                                         {"@UsuarioCreacionID", info.UsuarioCreacionID},
                                         {"@Activo", info.Activo},
                                     };
                int result = Create("CondicionJaula_Crear", parameters);
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
        /// Metodo para actualizar un registro de CondicionJaula
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CondicionJaulaInfo info)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@CondicionJaulaID", info.CondicionJaulaID},
                                         {"@Descripcion", info.Descripcion},
                                         {"@UsuarioModificacionID", info.UsuarioModificacionID},
                                         {"@Activo", info.Activo},
                                     };
                Update("CondicionJaula_Actualizar", parameters);
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
        internal ResultadoInfo<CondicionJaulaInfo> ObtenerPorPagina(PaginacionInfo pagina, CondicionJaulaInfo filtro)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@CondicionJaulaID", filtro.CondicionJaulaID},
                                         {"@Descripcion", filtro.Descripcion},
                                         {"@Activo", filtro.Activo},
                                         {"@Inicio", pagina.Inicio},
                                         {"@Limite", pagina.Limite},
                                     };
                DataSet ds = Retrieve("CondicionJaula_ObtenerPorPagina", parameters);
                ResultadoInfo<CondicionJaulaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCondicionJaulaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CondicionJaula
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<CondicionJaulaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<CondicionJaulaInfo> mapeo = MapCondicionJaulaDAL.ObtenerMapeoCondicionJaula();
                IEnumerable<CondicionJaulaInfo> condicionesJaula = GetDatabase().ExecuteSprocAccessor
                    <CondicionJaulaInfo>(
                        "CondicionJaula_ObtenerTodos", mapeo.Build());
                return condicionesJaula;
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
        internal IEnumerable<CondicionJaulaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                IEnumerable<CondicionJaulaInfo> condcionesJaula =
                    ObtenerCondicionesJaula("CondicionJaula_ObtenerTodos", new object[] { estatus.GetHashCode() });
                return condcionesJaula;
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
        /// Obtiene un registro de CondicionJaula
        /// </summary>
        /// <param name="condicionJaulaID">Identificador de la CondicionJaula</param>
        /// <returns></returns>
        internal IEnumerable<CondicionJaulaInfo> ObtenerPorID(int condicionJaulaID)
        {
            try
            {
                Logger.Info();
                IEnumerable<CondicionJaulaInfo> condcionesJaula =
                    ObtenerCondicionesJaula("CondicionJaula_ObtenerPorID", new object[] { condicionJaulaID });
                return condcionesJaula;
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
        /// Obtiene un registro de CondicionJaula
        /// </summary>
        /// <param name="descripcion">Descripción de la CondicionJaula</param>
        /// <returns></returns>
        internal IEnumerable<CondicionJaulaInfo> ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                IEnumerable<CondicionJaulaInfo> condcionesJaula =
                    ObtenerCondicionesJaula("CondicionJaula_ObtenerPorDescripcion", new object[] { descripcion });
                return condcionesJaula;
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
        /// Ejecuta procedimiento almacenado con sus parametros
        /// </summary>
        /// <param name="procedimientoAlmacenado"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        private IEnumerable<CondicionJaulaInfo> ObtenerCondicionesJaula(string procedimientoAlmacenado, object[] parametros)
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<CondicionJaulaInfo> mapeo = MapCondicionJaulaDAL.ObtenerMapeoCondicionJaula();
                IEnumerable<CondicionJaulaInfo> condicionesJaula = GetDatabase().ExecuteSprocAccessor
                    <CondicionJaulaInfo>(procedimientoAlmacenado, mapeo.Build(), parametros);
                return condicionesJaula;
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
 