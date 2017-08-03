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
    internal class OrganizacionDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear un Organizacion
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(OrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosCrear(info);
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
        internal void Actualizar(OrganizacionInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosActualizar(info);
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
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorPaginaCompleto(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerPorPagina]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapOrganizacionDAL.ObtenerPorPagina(ds);
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
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, OrganizacionInfo filtro
                                                              , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            ResultadoInfo<OrganizacionInfo> organizacionLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorFolio(pagina, filtro, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacionLista = MapOrganizacionDAL.ObtenerPorPagina(ds);
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
        internal List<OrganizacionInfo> ObtenerTodasGanaderas()
        {
            List<OrganizacionInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("OrganizacionUsuarioLogueado");
                if (ValidateDataSet(ds))
                {
                    result = MapOrganizacionDAL.ObtenerTodos(ds);
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
        ///     Obtiene una lista de todos los Organizaciones
        /// </summary>
        /// <returns></returns>
        internal List<OrganizacionInfo> ObtenerTodos()
        {
            List<OrganizacionInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Organizacion_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    result = MapOrganizacionDAL.ObtenerTodos(ds);
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
        internal List<OrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            List<OrganizacionInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerTodos]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapOrganizacionDAL.ObtenerTodos(ds);
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
        internal OrganizacionInfo ObtenerPorID(int id)
        {
            OrganizacionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametroPorID(id);
                DataSet ds = Retrieve("Organizacion_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapOrganizacionDAL.ObtenerPorID(ds);
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
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorDependencias(PaginacionInfo pagina, OrganizacionInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<OrganizacionInfo> organizacion = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorTipoOrigen(pagina, filtro, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerPorTipoOrigenPaginado", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacion = MapOrganizacionDAL.ObtenerPorTipoOrigenPaginado(ds);
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
        internal OrganizacionInfo ObtenerPorDependencias(OrganizacionInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            OrganizacionInfo organizacion = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorTipoOrigen(filtro, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerPorTipoOrigen", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacion = MapOrganizacionDAL.ObtenerPorDependenciaId(ds);
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
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaOrigenID(PaginacionInfo pagina, OrganizacionInfo filtro
                                                              , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            ResultadoInfo<OrganizacionInfo> organizacionLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorFolio(pagina, filtro, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerOrganizacionPorOrigenIDPaginado", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacionLista = MapOrganizacionDAL.ObtenerOrganizacionPorOrigenIDPaginado(ds);
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

        internal OrganizacionInfo ObtenerPorDependenciasOrigenID(OrganizacionInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            OrganizacionInfo organizacion = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorTipoOrigen(filtro, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerOrganizacionPorOrigenID", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacion = MapOrganizacionDAL.ObtenerPorDependenciaId(ds);
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

        internal ResultadoInfo<OrganizacionInfo> ObtenerPorEmbarqueTipoOrganizacionPaginado(PaginacionInfo pagina, OrganizacionInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<OrganizacionInfo> organizacion = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosEmbarqueTipoOrganizacion(pagina, organizacionInfo, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerPorEmbarqueTipoOrigenPaginado", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacion = MapOrganizacionDAL.ObtenerPorEmbarqueTipoOrigenPaginado(ds);
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

        internal OrganizacionInfo ObtenerPorEmbarqueTipoOrganizacion(OrganizacionInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            OrganizacionInfo organizacion = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosEmbarqueTipoOrganizacion(organizacionInfo, dependencias);
                DataSet ds = Retrieve("Organizacion_ObtenerPorEmbarqueTipoOrigen", parameters);
                if (ValidateDataSet(ds))
                {
                    organizacion = MapOrganizacionDAL.ObtenerPorEmbarqueTipoOrganizacion(ds);
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
        internal IList<OrganizacionInfo> ObtenerPendientesRecibir(int organizacionId, int estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerPendientesRecibir(organizacionId, estatus);
                DataSet ds = Retrieve("Organizacion_ObtenerPendientesRecibir", parameters);
                IList<OrganizacionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOrganizacionDAL.ObtenerPendientesRecibir(ds);
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
        internal OrganizacionInfo ObtenerPorIdConIva(int id)
        {
            OrganizacionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametroPorIdConIva(id);
                using (IDataReader reader = RetrieveReader("[dbo].[Organizacion_ObtenerPorIDConIva]", parameters))
                {
                    if (ValidateDataReader(reader))
                    {
                        result = MapOrganizacionDAL.ObtenerPorIdConIva(reader);
                    }
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
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Obtiene una Organizacion sociedad y la division por Id
        /// </summary>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerOrganizacionSociedadDivision(int id, SociedadEnum sociedad)
        {
            OrganizacionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerOrganizacionSociedadDivision(id, sociedad);
                using (IDataReader reader = RetrieveReader("[dbo].[Organizacion_ObtenerOrganizacionSociedadDivision]", parameters))
                {
                    if (ValidateDataReader(reader))
                    {
                        result = MapOrganizacionDAL.ObtenerOrganizacionSociedadDivision(reader);
                    }
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
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Obtiene un registro de Organizacion
        /// </summary>
        /// <param name="descripcion">Descripción de la Organizacion</param>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Organizacion_ObtenerPorDescripcion", parameters);
                OrganizacionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOrganizacionDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene una lista páginada de las organizaciones 
        /// por tipo de organización.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaTipoOrganizacion(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorPaginaTipoOrganizacion(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerPorPaginaTipoOrganizacion]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapOrganizacionDAL.ObtenerPorPaginaCompleto(ds);
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
        ///     Obtiene un Organizacion por Id
        /// </summary>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerSoloGanaderaPorID(OrganizacionInfo filtro)
        {
            OrganizacionInfo result = null;
            try
            {
                Logger.Info();

                var condicion = ObtenerPorID(filtro.OrganizacionID);
                
                if(condicion !=null && condicion.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Ganadera.GetHashCode())
                {
                    result = condicion;
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
        /// Obtiene las organizaciones por la lista de organizaciones
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaTiposOrganizaciones(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorPaginaTiposOrganizaciones(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerPorPaginaTiposOrganizacion]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapOrganizacionDAL.ObtenerPorPaginaCompleto(ds);
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
        /// Obtiene una lista de organizaciones por premezcla
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaPremezcla(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorPaginaCompleto(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerPorPaginaPremezcla]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapOrganizacionDAL.ObtenerPorPaginaCompleto(ds);
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
        /// Obtiene las organizaciones por la lista de organizaciones
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaFiltroTipoOrganizacion(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorPaginaFiltroTipoOrganizacion(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerPorPaginaTiposOrganizacion]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapOrganizacionDAL.ObtenerPorPaginaCompleto(ds);
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
        /// Obtiene las organizaciones por la lista de organizaciones
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaFiltroCentrosCadisDescansos(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorPaginaFiltroCentrosCadisDescansos(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerPorPaginaTiposOrganizacion]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapOrganizacionDAL.ObtenerPorPaginaCompleto(ds);
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
        /// Obtiene una organizacion por id y por filtro de tipo organizacion
        /// </summary>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerPorIdFiltroTiposOrganizacion(OrganizacionInfo organizacionInfo)
        {
            OrganizacionInfo result = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosObtenerPorIdFiltroTiposOrganizacion(organizacionInfo);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerPorIDFiltroTipoOrganizacion]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapOrganizacionDAL.ObtenerPorIdFiltroTiposOrganizacion(ds);
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
        /// Obtiene el listado de organizaciones del producto seleccionado
        /// </summary>
        /// <param name="productoId"></param>
        /// <returns></returns>
        internal List<OrganizacionInfo> ObtenerOrganizacionesProductoPremezcla(int productoId)
        {
            List<OrganizacionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosObtenerOrganizacionesDePremezcla(productoId);
                DataSet ds = Retrieve("Premezcla_ObtenerOrganizacionesPorProductoId", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapOrganizacionDAL.ObtenerOrganizacionesPremezcla(ds);
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
        /// Obtiene una organizacion por el AlmacenID
        /// </summary>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerPorAlmacenID(int almacenID)
        {
            OrganizacionInfo result = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorAlmacenID(almacenID);
                DataSet ds = Retrieve("Organizacion_ObtenerPorAlmacenID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapOrganizacionDAL.ObtenerPorAlmacenID(ds);
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

        public List<OrganizacionInfo> ObtenerPorTipoOrganizacion(int tipoOrganizacion)
        {
            List<OrganizacionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosObtenerPorTipoOrganizacion(tipoOrganizacion);
                DataSet ds = Retrieve("Organizacion_ObtenerPorTipoOrganizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapOrganizacionDAL.ObtenerTodos(ds);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaAdministranIngredientes(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOrganizacionDAL.ObtenerParametrosPorPaginaCompleto(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Organizacion_ObtenerPorPaginaAdminstranIngredientes]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapOrganizacionDAL.ObtenerPorPagina(ds);
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