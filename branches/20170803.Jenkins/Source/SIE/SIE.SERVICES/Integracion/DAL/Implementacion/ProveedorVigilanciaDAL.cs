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
    internal class ProveedorVigilanciaDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear un Organizacion
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(VigilanciaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosCrear(info);
                int infoId = Create("[dbo].[Organizacion_Crear]", parameters);
                return infoId;
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
        ///     Metodo que actualiza un Organizacion
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(VigilanciaInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosActualizar(info);
                Update("[dbo].[Organizacion_Actualizar]", parameters);
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
        internal ResultadoInfo<VigilanciaInfo> ObtenerProveedoresProductoPorPagina(PaginacionInfo pagina, VigilanciaInfo filtro)
        {
            ResultadoInfo<VigilanciaInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosProveedoresProductoPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Vigilancia_ProveedorProducto]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorVigilanciaDAL.ObtenerProveedorProductoPorPagina(ds);
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
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"></param>
        /// <returns></returns>
        internal ResultadoInfo<VigilanciaInfo> ObtenerPorPagina(PaginacionInfo pagina, VigilanciaInfo filtro
                                                              , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            ResultadoInfo<VigilanciaInfo> organizacionLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosPorFolio(pagina, filtro, dependencias);
                DataSet ds = Retrieve("Vigilancia_Proveedor", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacionLista = MapProveedorVigilanciaDAL.ObtenerPorPagina(ds);
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
            return organizacionLista;
        }

        /// <summary>
        ///     Obtiene una lista de todos los Organizaciones
        /// </summary>
        /// <returns></returns>
        internal List<VigilanciaInfo> ObtenerTodos()
        {
            List<VigilanciaInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("f");
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorVigilanciaDAL.ObtenerTodos(ds);
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
        ///     Obtiene una lista de Organizacion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<VigilanciaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            List<VigilanciaInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerTodos(estatus);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerTodos]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorVigilanciaDAL.ObtenerTodos(ds);
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
        ///     Obtiene un Organizacion por Id
        /// </summary>
        /// <returns></returns>
        internal VigilanciaInfo ObtenerPorID(int id)
        {
            VigilanciaInfo result = null;
            try
            {
                Logger.Info();
                
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametroPorID(id);
                DataSet ds = Retrieve("Vigilancia_Proveedor_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorVigilanciaDAL.ObtenerPorID(ds);
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
        /// Obtiene Organizaciones Paginadas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"></param>
        /// <returns></returns>
        internal ResultadoInfo<VigilanciaInfo> ObtenerPorDependencias(PaginacionInfo pagina, VigilanciaInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<VigilanciaInfo> organizacion = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosPorTipoOrigen(pagina, filtro, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerPorTipoOrigenPaginado", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacion = MapProveedorVigilanciaDAL.ObtenerPorTipoOrigenPaginado(ds);
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
            return organizacion;
        }

        /// <summary>
        /// Obtiene una Organizacion por sus dependencias
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="dependencias"></param>
        /// <returns></returns>
        internal VigilanciaInfo ObtenerPorDependencias(VigilanciaInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            VigilanciaInfo organizacion = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosPorTipoOrigen(filtro, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerPorTipoOrigen", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacion = MapProveedorVigilanciaDAL.ObtenerPorDependenciaId(ds);
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
            return organizacion;
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"></param>
        /// <returns></returns>
        internal ResultadoInfo<VigilanciaInfo> ObtenerPorPaginaOrigenID(PaginacionInfo pagina, VigilanciaInfo filtro
                                                              , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            ResultadoInfo<VigilanciaInfo> organizacionLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosPorFolio(pagina, filtro, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerOrganizacionPorOrigenIDPaginado", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacionLista = MapProveedorVigilanciaDAL.ObtenerOrganizacionPorOrigenIDPaginado(ds);
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
            return organizacionLista;
        }

        internal VigilanciaInfo ObtenerPorDependenciasOrigenID(VigilanciaInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            VigilanciaInfo organizacion = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosPorTipoOrigen(filtro, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerOrganizacionPorOrigenID", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacion = MapProveedorVigilanciaDAL.ObtenerPorDependenciaId(ds);
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
            return organizacion;
        }

        internal ResultadoInfo<VigilanciaInfo> ObtenerPorEmbarqueTipoOrganizacionPaginado(PaginacionInfo pagina, VigilanciaInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<VigilanciaInfo> organizacion = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosEmbarqueTipoOrganizacion(pagina, organizacionInfo, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerPorEmbarqueTipoOrigenPaginado", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacion = MapProveedorVigilanciaDAL.ObtenerPorEmbarqueTipoOrigenPaginado(ds);
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
            return organizacion;
        }

        internal VigilanciaInfo ObtenerPorEmbarqueTipoOrganizacion(VigilanciaInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            VigilanciaInfo organizacion = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosEmbarqueTipoOrganizacion(organizacionInfo, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerPorEmbarqueTipoOrigen", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacion = MapProveedorVigilanciaDAL.ObtenerPorEmbarqueTipoOrganizacion(ds);
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
            return organizacion;
        }

        /// <summary>
        ///     Obtiene una lista de organizaciones que tengan embarques pendientes por recibir
        /// </summary>
        /// <param name="organizacionId">Identificador de la organización</param>
        /// <param name="estatus">Estatus del embarque </param>
        /// <returns></returns>
        internal IList<VigilanciaInfo> ObtenerPendientesRecibir(int organizacionId, int estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerPendientesRecibir(organizacionId, estatus);
                DataSet ds = Retrieve("Organizacion_ObtenerPendientesRecibir", parameters);
                IList<VigilanciaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorVigilanciaDAL.ObtenerPendientesRecibir(ds);
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
        ///     Obtiene un Organizacion por Id
        /// </summary>
        /// <returns></returns>
        internal VigilanciaInfo ObtenerPorIdConIva(int id)
        {
            VigilanciaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametroPorIdConIva(id);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerPorIDConIva]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorVigilanciaDAL.ObtenerPorIdConIva(ds);
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
        /// Obtiene un registro de Organizacion
        /// </summary>
        /// <param name="descripcion">Descripción de la Organizacion</param>
        /// <returns></returns>
        internal VigilanciaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorVigilancia.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Organizacion_ObtenerPorDescripcion", parameters);
                VigilanciaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorVigilanciaDAL.ObtenerPorDescripcion(ds);
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