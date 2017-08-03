using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class OrganizacionBL
    {
        /// <summary>
        ///     Metodo que guarda una organización
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(OrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                int result = info.OrganizacionID;
                if (info.OrganizacionID == 0)
                {
                    result = organizacionDAL.Crear(info);
                }
                else
                {
                    organizacionDAL.Actualizar(info);
                }
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad Organizacion por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                OrganizacionInfo result = organizacionDAL.ObtenerPorDescripcion(descripcion);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
            ResultadoInfo<OrganizacionInfo> result;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                result = organizacionDAL.ObtenerPorPagina(pagina, filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="dependencias"> </param>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerPorDependencias(OrganizacionInfo filtro
                                                     , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            OrganizacionInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                resultadoOrganizacion = organizacionDAL.ObtenerPorDependencias(filtro, dependencias);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoOrganizacion;
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"> </param>
        /// <returns></returns>
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorDependencias(PaginacionInfo pagina, OrganizacionInfo filtro
                                                                    , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                resultadoOrganizacion = organizacionDAL.ObtenerPorDependencias(pagina, filtro, dependencias);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoOrganizacion;
        }

        /// <summary>
        ///     Obtiene una lista de las Organizaciones
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>
        internal IList<OrganizacionInfo> ObtenerTodos()
        {
            IList<OrganizacionInfo> lista;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                lista = organizacionDAL.ObtenerTodos();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Obtiene una lista de Organizacion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<OrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                IList<OrganizacionInfo> lista = organizacionDAL.ObtenerTodos(estatus);

                return lista;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Obtiene un organización por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerPorID(int organizacionId)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                info = organizacionDAL.ObtenerPorID(organizacionId);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"> </param>
        /// <returns></returns>
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaOrigenID(PaginacionInfo pagina, OrganizacionInfo filtro
                                                                    , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                resultadoOrganizacion = organizacionDAL.ObtenerPorPaginaOrigenID(pagina, filtro, dependencias);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoOrganizacion;
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="dependencias"> </param>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerPorDependenciasOrigenID(OrganizacionInfo filtro
                                                     , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            OrganizacionInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                resultadoOrganizacion = organizacionDAL.ObtenerPorDependenciasOrigenID(filtro, dependencias);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoOrganizacion;
        }

        internal ResultadoInfo<OrganizacionInfo> ObtenerPorEmbarqueTipoOrganizacionPaginado(PaginacionInfo pagina, OrganizacionInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                resultadoOrganizacion = organizacionDAL.ObtenerPorEmbarqueTipoOrganizacionPaginado(pagina, organizacionInfo, dependencias);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoOrganizacion;
        }

        internal OrganizacionInfo ObtenerPorEmbarqueTipoOrganizacion(OrganizacionInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            OrganizacionInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                resultadoOrganizacion = organizacionDAL.ObtenerPorEmbarqueTipoOrganizacion(organizacionInfo, dependencias);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoOrganizacion;
        }

        /// <summary>
        ///     Obtiene una lista de organizaciones que tengan embarques pendientes por recibir
        /// </summary>
        /// <param name="organizacionId">Identificador de la organzación</param>
        /// <param name="estatus">Estatus del embarque </param>
        /// <returns></returns>
        internal IList<OrganizacionInfo> ObtenerPendientesRecibir(int organizacionId, int estatus)
        {
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                IList<OrganizacionInfo> lista = organizacionDAL.ObtenerPendientesRecibir(organizacionId, estatus);
               
                return lista;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        ///     Obtiene un organización por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerPorIdConIva(int organizacionId)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                info = organizacionDAL.ObtenerPorIdConIva(organizacionId);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        ///     Obtiene una organización la sociedad y la division por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerOrganizacionSociedadDivision(int organizacionId, SociedadEnum sociedad)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                info = organizacionDAL.ObtenerOrganizacionSociedadDivision(organizacionId, sociedad);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        /// Obtiene por id
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public OrganizacionInfo ObtenerPorIDFiltroTipoOrganizacion(int organizacionId)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                info = organizacionDAL.ObtenerPorID(organizacionId);
                if (info != null)
                {
                    if (info.TipoOrganizacion.TipoOrganizacionID != (int)TipoOrganizacion.Ganadera)
                    {
                        info = null;
                    }
                }   
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        /// Obtiene por id solo centros
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public OrganizacionInfo ObtenerPorIDSoloCentros(int organizacionId)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                info = organizacionDAL.ObtenerPorID(organizacionId);
                if (info != null)
                {
                    if (info.TipoOrganizacion.TipoOrganizacionID != (int)TipoOrganizacion.Centro)
                    {
                        info = null;
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        public OrganizacionInfo ObtenerPorIDSoloCentrosCadisDescansos(int organizacionId)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                info = organizacionDAL.ObtenerPorID(organizacionId);
                if (info != null)
                {
                    if (info.TipoOrganizacion.TipoOrganizacionID != (int)TipoOrganizacion.Centro && info.TipoOrganizacion.TipoOrganizacionID != (int)TipoOrganizacion.Cadis && info.TipoOrganizacion.TipoOrganizacionID != (int)TipoOrganizacion.Descanso)
                    {
                        info = null;
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        public ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaTipoOrganizacion(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> result;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                result = organizacionDAL.ObtenerPorPaginaTipoOrganizacion(pagina, filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }


       
        /// <summary>
        ///     Obtiene un organización por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerSoloGanaderaPorID(OrganizacionInfo filtro)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                info = organizacionDAL.ObtenerSoloGanaderaPorID(filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        /// Obtner organizaciones por tipos de organizacion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaPorTiposOrganizacion(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> result;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                result = organizacionDAL.ObtenerPorPaginaTiposOrganizaciones(pagina, filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtener por pagina de premezcla
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaPremezcla(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> result;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                result = organizacionDAL.ObtenerPorPaginaPremezcla(pagina, filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtener organizaciones por tipos de organizacion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaFiltroTipoOrganizacion(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> result;
            try
            {
                Logger.Info();
                var organizacionDal = new OrganizacionDAL();
                result = organizacionDal.ObtenerPorPaginaFiltroTipoOrganizacion(pagina, filtro);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un organización por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="organizacionInfo"></param>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerPorIdFiltroTiposOrganizacion(OrganizacionInfo organizacionInfo)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionDal = new OrganizacionDAL();
                info = organizacionDal.ObtenerPorIdFiltroTiposOrganizacion(organizacionInfo);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        /// Obtiene el listado de organizaciones del producto seleccionado
        /// </summary>
        /// <param name="productoId"></param>
        /// <returns></returns>
        internal List<OrganizacionInfo> ObtenerOrganizacionesProductoPremezcla(int productoId)
        {
            List<OrganizacionInfo> result;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                result = organizacionDAL.ObtenerOrganizacionesProductoPremezcla(productoId);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        ///     Obtiene una lista de las Organizaciones de tipo ganaderas
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>
        internal IList<OrganizacionInfo> ObtenerTipoGanaderas()
        {
            IList<OrganizacionInfo> lista;
            try
            {
                Logger.Info();
                lista = ObtenerTodos();
                
                lista = lista.Where(p => p.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Ganadera.GetHashCode()).ToList();

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        ///     Obtiene una lista de las Organizaciones de tipo centros, cadis, descansos
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>

        internal ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaCentrosCadisDescansos(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> result;
            try
            {
                Logger.Info();
                var organizacionDal = new OrganizacionDAL();
                result = organizacionDal.ObtenerPorPaginaFiltroCentrosCadisDescansos(pagina, filtro);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un organización por su Id
        /// </summary>
        /// <returns></returns>
        internal IList<OrganizacionInfo> ObtenerTipoCentros()
        {
            IList<OrganizacionInfo> lista;
            try
            {
                Logger.Info();
                lista = ObtenerTodos();
                lista = lista.Where(c => c.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Centro.GetHashCode()).ToList();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
            return lista;
        }


        /// <summary>
        /// Obtiene un organización por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal OrganizacionInfo ObtenerPorAlmacenID(int almacenID)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionDal = new OrganizacionDAL();
                info = organizacionDal.ObtenerPorAlmacenID(almacenID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        /// Obtiene  una lista de las Organizaciones de tipo Centros, Cadis, Centros
        /// </summary>
        /// <returns></returns>
        public OrganizacionInfo ObtenerTipoCentrosCadisDescansos(OrganizacionInfo organizacion)
        {
            OrganizacionInfo info = null;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                if (organizacion.Division != ".")
                {
                    info = organizacionDAL.ObtenerPorID(organizacion.OrganizacionID);
                    if (info != null)
                    {
                        if ((info.TipoOrganizacion.TipoOrganizacionID != (int)TipoOrganizacion.Centro && info.TipoOrganizacion.TipoOrganizacionID != (int)TipoOrganizacion.Cadis && info.TipoOrganizacion.TipoOrganizacionID != (int)TipoOrganizacion.Descanso) || info.Division != organizacion.Division)
                        {
                            info = null;
                        }
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        public List<OrganizacionInfo> ObtenerPorTipoOrganizacion(int tipoOrganizacion)
        {
            List<OrganizacionInfo> lista;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                lista = organizacionDAL.ObtenerPorTipoOrganizacion(tipoOrganizacion);
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
            ResultadoInfo<OrganizacionInfo> result;
            try
            {
                Logger.Info();
                var organizacionDAL = new OrganizacionDAL();
                result = organizacionDAL.ObtenerPorPaginaAdministranIngredientes(pagina, filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        ///     Obtiene una lista de las Organizaciones de tipo ganaderas, Cadis y Descanso
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>
        internal IList<OrganizacionInfo> ObtenerTipoGanaderasCadisDescanso()
        {
            IList<OrganizacionInfo> lista;
            try
            {
                Logger.Info();
                lista = ObtenerTodos();

                lista = lista.Where(p => p.Activo == EstatusEnum.Activo && 
                    (p.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Ganadera.GetHashCode()
                    || p.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Cadis.GetHashCode()
                    || p.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Descanso.GetHashCode())).ToList();

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        ///     Obtiene una lista de las Organizaciones Origen
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>
        internal IList<OrganizacionInfo> ObtenerOrganizacionesOrigen()
        {
            IList<OrganizacionInfo> lista;
            try
            {
                Logger.Info();
                lista = ObtenerTodos();

                lista = lista.Where(p => p.Activo == EstatusEnum.Activo &&
                    (p.TipoOrganizacion.TipoOrganizacionID != TipoOrganizacion.Corporativo.GetHashCode())).ToList();

            }
            catch (ExcepcionGenerica)
            {
                throw;
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