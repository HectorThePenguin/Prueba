using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxEmbarqueDAL
    {
        /// <summary>
        /// Metodo que obtiene los parametros para obtener un listado de entradas de ganado activas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, FiltroEmbarqueInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                    {      
                        {"@EmbarqueID", filtro.EmbarqueID},
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@FolioEmbarque", filtro.FolioEmbarque},
                        {"@OrganizacionOrigenID", filtro.OrganizacionOrigenID},
                        {"@OrganizacionDestinoID", filtro.OrganizacionDestinoID},
                        {"@TipoOrganizacionOrigenID", filtro.TipoOrganizacionOrigenID},
                        {"@FechaSalida", filtro.FechaSalida},
                        {"@FechaLlegada", filtro.FechaLlegada},
                        {"@Estatus", filtro.Estatus},
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
        ///     Obtiene parametros para crear
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", info.Organizacion.OrganizacionID},
                                     {"@TipoEmbarqueID", info.TipoEmbarque.TipoEmbarqueID},
                                     {"@Estatus", info.Estatus},
                                     {"@UsuarioCreacionID", info.UsuarioCreacionID},
                                     {"@TipoFolio", TipoFolio.Embarque.GetHashCode()},
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
        ///     Obtiene parametros para actualizar
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@EmbarqueID", info.EmbarqueID},
                                     {"@OrganizacionID", info.Organizacion.OrganizacionID},
                                     {"@FolioEmbarque", info.FolioEmbarque},
                                     {"@TipoEmbarqueID", info.TipoEmbarque.TipoEmbarqueID},
                                     {"@Estatus", info.Estatus},
                                     {"@UsuarioModificacionID", info.UsuarioModificacionID},
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="embarqueId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int embarqueId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueID", embarqueId}
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
        ///     Obtiene Parametros por EmbarqueID 
        /// </summary>
        /// <param name="embarqueId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEscalasPorEmbarqueID(int embarqueId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueID", embarqueId}
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
        ///     Obtiene Parametros del embarque por su folio
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorFolioEmbarque(int filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEmbarque", filtro}
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
        ///     Obtiene parametros para crear el detalle de la programación de embarque
        /// </summary>
        /// <param name="listaProgramacionEscalas"></param>
        /// <param name="embarqueId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardadoProgramacionEscalas(
            IEnumerable<EmbarqueDetalleInfo> listaProgramacionEscalas, int embarqueId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                                 from programacion in listaProgramacionEscalas
                                 select new XElement("Embarque",
                                                     new XElement("EmbarqueDetalleID",
                                                                  programacion.EmbarqueDetalleID),
                                                     new XElement("EmbarqueID", embarqueId),
                                                     new XElement("ProveedorID", programacion.Proveedor.ProveedorID),
                                                     new XElement("ChoferID", programacion.Chofer.ChoferID),
                                                     new XElement("JaulaID", programacion.Jaula.JaulaID),
                                                     new XElement("CamionID", programacion.Camion.CamionID),
                                                     new XElement("OrganizacionOrigenID",
                                                                  programacion.OrganizacionOrigen.OrganizacionID),
                                                     new XElement("OrganizacionDestinoID",
                                                                  programacion.OrganizacionDestino.OrganizacionID),
                                                     new XElement("FechaSalida", DateTimeOffset.Parse(programacion.FechaSalida.ToString()).DateTime),
                                                     new XElement("FechaLlegada", DateTimeOffset.Parse(programacion.FechaLlegada.ToString()).DateTime),
                                                     new XElement("Orden", programacion.Orden),
                                                     new XElement("Recibido", programacion.Recibido.GetHashCode()),
                                                     new XElement("Activo", programacion.Activo.GetHashCode()),
                                                     new XElement("UsuarioCreacionID", programacion.UsuarioCreacionID),
                                                     new XElement("UsuarioModificacionID",
                                                                  programacion.UsuarioModificacionID),
                                                     new XElement("Comentarios", programacion.Comentarios),
                                                     new XElement("Horas", programacion.Horas),
                                                     new XElement("Kilometros", programacion.Kilometros ?? 0)
                                     ));

                parametros = new Dictionary<string, object>
                    {
                        {"@XmlEscala", xml.ToString()}
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
        ///     Obtiene parametros para crear el detalle de costos de la programación de embarque
        /// </summary>
        /// <param name="listaProgramacionCostos"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardadoProgramacionCostos(
            IEnumerable<CostoEmbarqueDetalleInfo> listaProgramacionCostos)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from costo in listaProgramacionCostos
                                 select new XElement("CostoEmbarqueDetalle",
                                                     new XElement("CostoEmbarqueDetalleID", costo.CostoEmbarqueDetalleID),
                                                     new XElement("EmbarqueDetalleID", costo.EmbarqueDetalleID),
                                                     new XElement("CostoID", costo.Costo.CostoID),
                                                     new XElement("Orden", costo.Orden),
                                                     new XElement("Importe", costo.Importe),
                                                     new XElement("Activo", costo.Activo.GetHashCode()),
                                                     new XElement("UsuarioCreacionID", costo.UsuarioCreacionID),
                                                     new XElement("UsuarioModificacionID", costo.UsuarioModificacionID)
                                     ));

                parametros = new Dictionary<string, object>
                    {
                        {"@XmlCosto", xml.ToString()}
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
        /// Obtiene los parametros para actualizar el estatus a recibido del registro detalle
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEstatusDetalle(EntradaGanadoInfo entradaGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueID", entradaGanado.EmbarqueID},
                        {"@OrganizacionOrigenID", entradaGanado.OrganizacionOrigenID},
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
        /// Obtiene los parametros para actualizar el estatus a recibido del registro padre
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEstatus(EntradaGanadoInfo entradaGanado, Estatus estatus)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueID", entradaGanado.EmbarqueID},                        
                        {"@Estatus", estatus},                        
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
        /// Obtiene los parametros para actualizar el estatus a recibido del registro padre
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPendientesRecibir(EntradaGanadoInfo entradaGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueID", entradaGanado.EmbarqueID},                                                                
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
        /// Obtiene los Parametros para la Ejecucion del Procedimiento Embarque_ObtenerPorFolioEmbarqueOrganizacion
        /// </summary>
        /// <param name="folioEmbarqueId">Folio de Embarque</param>
        /// <param name="organizacionId">Organizacion a la que Pertenece el Folio de Embarque</param>
        /// <returns>Diccionario con los Parametros {@FolioEmbarque, @OrganizacionID} </returns>
        internal static Dictionary<string, object> ObtenerParametroPorFolioEmbarqueOrganizacion(int folioEmbarqueId, int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEmbarque", folioEmbarqueId},                        
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

        internal static Dictionary<string, object> ObtenerParametrosPendientesPorPagina(PaginacionInfo pagina, FiltroEmbarqueInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                    {   
                        {"@FolioEmbarque", filtro.FolioEmbarque},                           
                        {"@TipoOrganizacion", filtro.TipoOrganizacionOrigenID},
                        {"@OrganizacionOrigenID", filtro.OrganizacionOrigenID},                        
                        {"@OrganizacionID", filtro.OrganizacionID},                        
                        {"@Estatus", filtro.Estatus},
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

    }
}