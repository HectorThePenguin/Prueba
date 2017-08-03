using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxSolicitudProductoDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, SolicitudProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@SolicitudProductoID", filtro.SolicitudProductoID},
                            {"@Activo", filtro.Activo},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosCrear(SolicitudProductoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@FolioSolicitud", info.FolioSolicitud},
							{"@FechaSolicitud", info.FechaSolicitud},
							{"@UsuarioIDSolicita", info.UsuarioSolicita.UsuarioID},
							{"@EstatusID", info.Estatus.EstatusId},
							{"@UsuarioIDAutoriza", info.UsuarioAutoriza.UsuarioID},
							{"@FechaAutorizado", info.FechaAutorizado},
							{"@UsuarioIDEntrega", info.UsuarioEntrega.UsuarioID},
							{"@FechaEntrega", info.FechaEntrega},
							{"@CentroCostoID", info.CentroCosto.CentroCostoID},
							{"@AlmacenID", info.Almacen.AlmacenID},
                            {"@AlmacenMovimientoID", info.AlmacenMovimientoID},
							{"@ObservacionUsuarioEntrega", info.ObservacionUsuarioEntrega},
							{"@ObservacionUsuarioAutoriza", info.ObservacionUsuarioAutoriza},
							{"@Activo", info.Activo},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(SolicitudProductoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@SolicitudProductoID", info.SolicitudProductoID},
							{"@OrganizacionID", info.OrganizacionID},
							{"@FolioSolicitud", info.FolioSolicitud},
							{"@FechaSolicitud", info.FechaSolicitud},
							{"@UsuarioIDSolicita", info.UsuarioIDSolicita},
							{"@EstatusID", info.EstatusID},
							{"@UsuarioIDAutoriza", info.UsuarioIDAutoriza},
							{"@FechaAutorizado", info.FechaAutorizado},
							{"@UsuarioIDEntrega", info.UsuarioIDEntrega},
							{"@FechaEntrega", info.FechaEntrega},
							{"@CentroCostoID", info.CentroCostoID},
							{"@AlmacenID", info.AlmacenID},
                            {"@AlmacenMovimientoID", info.AlmacenMovimientoID},
							{"@ObservacionUsuarioEntrega", info.ObservacionUsuarioEntrega},
							{"@ObservacionUsuarioAutoriza", info.ObservacionUsuarioAutoriza},
							{"@Activo", info.Activo},
                            {"@UsuarioModificacionID", info.UsuarioModificacionID},
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
        ///  Obtiene Parametros por Id
        /// </summary>
        /// <param name="solicitudProductoID">Identificador de la entidad SolicitudProducto</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int solicitudProductoID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@SolicitudProductoID", solicitudProductoID}
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
        /// Obtiene Parametro pora filtrar por estatus 
        /// </summary> 
        /// <param name="estatus">Representa si esta activo el registro </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@Activo", estatus}
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
        public static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@SolicitudProductoID", descripcion}
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
        /// Obtiene los parametros necesarios para la
        /// ejecucion del procedimiento almacenado SolicitudProducto_ObtenerConciliacionMovimientosSIAP
        /// </summary>
        /// <param name="almacenMovimientos"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerConciliacionPorAlmacenXML(List<AlmacenMovimientoInfo> almacenMovimientos)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in almacenMovimientos
                                 select new XElement("AlmacenMovimiento",
                                                     new XElement("AlmacenMovimientoID", detalle.AlmacenMovimientoID)
                                     ));
                var parametros = new Dictionary<string, object> {{"@XmlAlmacenMovimiento", xml.ToString()}};
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary> 
        /// Obtiene parametro por número de documento 
        /// </summary> 
        /// <param name="numeroDocumento">Descripción de la entidad </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorNumeroDocumento(string numeroDocumento)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@NumeroDocumento", numeroDocumento}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosGuardarAretes(string numeroDocumento, int usuarioId, int organizacionId, List<AreteInfo> aretes)
        {
            try
            {
                Logger.Info();

                var xml =
                   new XElement("ROOT",
                                from s in aretes
                                select new XElement("DATOS",
                                                    new XElement("NumeroArete", s.Arete),
                                                    new XElement("TipoArete", s.Tipo.GetHashCode())
                                    ));
                
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@NumeroDocumento", numeroDocumento},
                                {"@Aretes", xml.ToString()},
                                {"@OrganizacionId", organizacionId},
                                {"@UsuarioId", usuarioId}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    
        public static Dictionary<string, object> ObtenerParametrosValidarAretesDuplicados(int organizacionId, List<AreteInfo> aretes)
        {
            try
            {
                Logger.Info();

                var xml =
                   new XElement("ROOT",
                                from s in aretes
                                select new XElement("DATOS",
                                                    new XElement("NumeroArete", s.Arete),
                                                    new XElement("TipoArete", s.Tipo.GetHashCode())
                                    ));

                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@Aretes", xml.ToString()},
                                {"@OrganizacionId", organizacionId}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    
    }
}

