using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxOrganizacionDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaCompleto(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Metodo que obtiene los parametros para lista pagina de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolio( PaginacionInfo pagina,
                                                                            OrganizacionInfo filtro,
                                                                            IList<IDictionary<IList<String>, Object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtro.OrganizacionID},
                            {"@Descripcion", filtro.Descripcion},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite},
                        };

                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(OrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@TipoOrganizacionID", info.TipoOrganizacion.TipoOrganizacionID},
							{"@Descripcion", info.Descripcion},
                            {"@Direccion", info.Direccion},
							{"@RFC", info.RFC},
							{"@IvaID", info.Iva.IvaID},
                            {"@Activo", info.Activo},
                            {"@Division", info.Division},
                            {"@Sociedad", info.Sociedad},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
                            {"@ZonaID", info.Zona.ZonaID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Obtiene parametros para actualizar
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(OrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OrganizacionID", info.OrganizacionID},
							{"@TipoOrganizacionID", info.TipoOrganizacion.TipoOrganizacionID},
							{"@Descripcion", info.Descripcion},
                            {"@Direccion", info.Direccion},
							{"@RFC", info.RFC},
							{"@IvaID", info.Iva.IvaID},
                            {"@Activo", info.Activo},
                            {"@Division", info.Division},
                            {"@Sociedad", info.Sociedad},
                            {"@UsuarioModificacionID", info.UsuarioModificacionID},
                            {"@ZonaID", info.Zona.ZonaID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPorFolio(OrganizacionInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object> {{"@OrganizacionID", filtro.OrganizacionID}};
                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPorTipoOrigen(OrganizacionInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = 
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtro.OrganizacionID}
                        };

                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPorTipoOrigen(PaginacionInfo pagina, OrganizacionInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtro.OrganizacionID},
                            {"@Descripcion", filtro.Descripcion},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
                        };

                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosEmbarqueTipoOrganizacion(PaginacionInfo pagina, OrganizacionInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionInfo.OrganizacionID},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
                        };

                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosEmbarqueTipoOrganizacion(OrganizacionInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = 
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionInfo.OrganizacionID}
                        };

                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        ///     Obtiene Parametros para traer las organizaciones que tengan embarques pendientes de recibir
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPendientesRecibir(int organizacionId, int estatus)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                            {"@Estatus", estatus}
                        };

                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorIdConIva(int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionId}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros id y sociedad
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="sociedad"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerOrganizacionSociedadDivision(int organizacionId, SociedadEnum sociedad)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionId},
                                     {"@SociedadID", sociedad.GetHashCode()}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        ///     Obtiene Parametros pora filtrar por estatus
        /// </summary>
        /// <param name="estatus">Representa si esta activo el registro </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
							{
								{"@Activo", estatus.GetHashCode()}
							};
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary> 
        /// Obtiene Parametro pora filtrar por descripción 
        /// </summary> 
        /// <param name="descripcion">Descripción de la entidad </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Descripcion", descripcion}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosPorPaginaTipoOrganizacion(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@TipoOrganizacion",filtro.TipoOrganizacion.TipoOrganizacionID},
                        {"@Division",filtro.Division}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        /// Obtiene los parametros para obtener las organizaciones por tipos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaTiposOrganizaciones(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                   new XElement("ROOT",
                           new XElement("TiposOrganizaciones",
                               new XElement("TipoOrganizacionID", (int)TipoOrganizacion.Ganadera)),
                           new XElement("TiposOrganizaciones",
                               new XElement("TipoOrganizacionID", (int)TipoOrganizacion.Centro)),
                            new XElement("TiposOrganizaciones",
                               new XElement("TipoOrganizacionID", (int)TipoOrganizacion.Praderas)),
                            new XElement("TiposOrganizaciones",
                               new XElement("TipoOrganizacionID", (int)TipoOrganizacion.Descanso)),
                            new XElement("TiposOrganizaciones",
                               new XElement("TipoOrganizacionID", (int)TipoOrganizacion.Cadis))
                           );
                
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@XmlTiposOrganizacion",xml.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para obtener las organizaciones por tipos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaFiltroTipoOrganizacion(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                new XElement("ROOT",
                             from info in filtro.ListaTiposOrganizacion
                             select
                                 new XElement("TiposOrganizaciones",
                                              new XElement("TipoOrganizacionID", info.TipoOrganizacionID))
                                 );
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@XmlTiposOrganizacion",xml.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para obtener las organizaciones por centros, cadis, descansos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaFiltroCentrosCadisDescansos(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                 new XElement("ROOT",
                         new XElement("TiposOrganizaciones",
                             new XElement("TipoOrganizacionID", (int)TipoOrganizacion.Descanso)),
                         new XElement("TiposOrganizaciones",
                             new XElement("TipoOrganizacionID", (int)TipoOrganizacion.Centro)),
                          new XElement("TiposOrganizaciones",
                             new XElement("TipoOrganizacionID", (int)TipoOrganizacion.Cadis))
                         );

                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@XmlTiposOrganizacion",xml.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene una organizacion por id y por tipos de organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorIdFiltroTiposOrganizacion(OrganizacionInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                new XElement("ROOT",
                             from info in filtro.ListaTiposOrganizacion
                             select
                                 new XElement("TiposOrganizaciones",
                                              new XElement("TipoOrganizacionID", info.TipoOrganizacionID))
                                 );
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@Activo", filtro.Activo},
                        {"@XmlTiposOrganizacion",xml.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para obtener las organizaciones que pertenecen a la premezcla seleccinada
        /// </summary>
        /// <param name="productoId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerOrganizacionesDePremezcla(int productoId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoId", productoId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtiene los parametros para obtener las organizaciones que pertenecen a la premezcla seleccinada
        /// </summary>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorAlmacenID(int almacenID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        public static Dictionary<string, object> ObtenerParametrosObtenerPorTipoOrganizacion(int tipoOrganizacion)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", EstatusEnum.Activo.GetHashCode()},
                            {"@TipoOrganizacionID", tipoOrganizacion}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        public static Dictionary<string, object> ObtenerParametrosObtenerSociedadMoneda(int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionId},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }

    
}
