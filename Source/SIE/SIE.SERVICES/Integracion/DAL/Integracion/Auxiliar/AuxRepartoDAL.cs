using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxRepartoDAL
    {

        /// <summary>
        /// Obtiene parametros para obtener el reparto por id
        /// </summary>
        /// <param name="reparto"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroPorId(RepartoInfo reparto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoID", reparto.RepartoID},
                            {"@OrganizacionID", reparto.OrganizacionID}
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
        /// Obtiene parametros para obtener el reparto por lote
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="fechaReparto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorLote(LoteInfo lote, DateTime fechaReparto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", lote.LoteID},
                            {"@OrganizacionID", lote.OrganizacionID},
                            {"@Activo", (int)EstatusEnum.Activo},
                            {"@Fecha", fechaReparto}
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
        /// Obtiene parametros para obtener el detalle del reparto
        /// </summary>
        /// <param name="reparto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroDetallePorLote(RepartoInfo reparto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoID", reparto.RepartoID},
                            {"@Activo", (int)EstatusEnum.Activo}
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
        /// Obtiene los parametros necesarios para obtener los dias de retiro
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroDiasRetiro(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteId", lote.LoteID},
                            {"@TipoFinalizacion", (int)TipoFormula.Retiro},
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
        /// Obtiene los parametros para obtener detalle de los reparto de un lote con los tipo de formula produccion y finalizacion
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroDetalleRepartoLoteYformulasProduccionFinalizacion(LoteInfo lote)
        {
            try
            {
                Logger.Info();


                var tiposFormulas = new List<int> { (int)TipoFormula.Produccion, (int)TipoFormula.Finalizacion };

                var xml =
                    new XElement("ROOT",
                                 from formula in tiposFormulas
                                 select new XElement("TiposFormulas",
                                                     new XElement("TipoFormulaID",
                                                                  formula)
                                     ));


                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteId", lote.LoteID},
                            {"@OrganizacionID", lote.OrganizacionID},
                            {"@XMLTiposFormula", xml.ToString()}
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
        /// Obtiene los parametros necesarios para obtener la ordenes de reparto de la fecha actual
        /// </summary>
        /// <param name="lote">Lote del cual se consultara la orden de reparto</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroObtenerRepartoActual(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", lote.OrganizacionID}
                            
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
        /// Obtiene los parametros necesarios para obtener el consumo total del dia
        /// </summary>
        /// <param name="organizacionId">Organizacion</param>
        /// <param name="corral">Lote</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroConsumoTotalDia(int organizacionId, CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                            {"@CorralID",corral.CorralID}
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
        /// Obtiene los parametros necesarios para obtener el peso de llegada
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroPesoLlegada(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", lote.LoteID},
                            {"@OrganizacionID", lote.OrganizacionID}
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
        /// Obtiene los parametros necesarios para insertar o actualizar la orden de reparto
        /// </summary>
        /// <param name="ordenReparto"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroGenerarOrdenReparto(OrdenRepartoAlimentacionInfo ordenReparto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoID", ordenReparto.Reparto.RepartoID},
                            {"@OrganizacionID", ordenReparto.OrganizacionID},
                            {"@CorralID", ordenReparto.Corral.CorralID},
                            {"@LoteID", ordenReparto.Lote!=null?ordenReparto.Lote.LoteID:0},
                            {"@Fecha", ordenReparto.Reparto.Fecha},
                            {"@PesoInicio", ordenReparto.Reparto.PesoInicio},
                            {"@PesoProyectado", ordenReparto.Reparto.PesoProyectado},
                            {"@DiasEngorda", ordenReparto.Reparto.DiasEngorda},
                            {"@PesoRepeso", ordenReparto.Reparto.PesoRepeso},
                            {"@UsuarioCreacionID", ordenReparto.UsuarioID},
                            {"@RepartoDetalleID", ordenReparto.DetalleOrdenReparto.RepartoDetalleID},
                            {"@TipoServicioID", ordenReparto.DetalleOrdenReparto.TipoServicioID},
                            {"@FormulaIDProgramada",ordenReparto.DetalleOrdenReparto.FormulaIDProgramada},
                            {"@CantidadProgramada", ordenReparto.DetalleOrdenReparto.CantidadProgramada},
                            {"@Cabezas", ordenReparto.DetalleOrdenReparto.Cabezas},
                            {"@EstadoComederoID", ordenReparto.DetalleOrdenReparto.EstadoComederoID}   
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
        /// Obtiene los parametros para obtener el reparto por operador
 		/// </summary>
 		/// <param name="operador"></param>
 		/// <param name="corral"></param>
 		/// <returns></returns>
        internal static Dictionary<string, object> ObtenerRepartoPorOperadorId(OperadorInfo operador, CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OperadorID", operador.OperadorID},
                            {"@OrganizacionID", operador.Organizacion.OrganizacionID},
                            {"@CodigoCorral", corral.Codigo ?? ""}
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
        /// Obteiene los parametros necesarios para obtener un reparto de un lote y una fecha especifica
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="lote">Lote del reparto</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroRepartoPorFecha(DateTime fecha, LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", lote.OrganizacionID},
                            {"@LoteID", lote.LoteID},
                            {"@Fecha", fecha}
                            
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
        /// Obtiene los parametros para obtener el reparto por operador
        /// </summary>
        /// <param name="operadorId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTotalesRepartoPorOperadorId(int operadorId, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OperadorID", operadorId},
                            {"@OrganizacionID", organizacionId}
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
        /// Obtiene los parametros necesarios para obtener el avance del reparto
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroAvanceReparto(int usuarioId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@UsuarioID", usuarioId}
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
        /// Obtiene los parametros necesarios para reportar el avance del reparto
        /// </summary>
        /// <param name="reporteAvance">Reparto</param>
        /// <returns>Diccionario de los parametros</returns>
        public static Dictionary<string, object> ObtenerParametroReporteAvanceReparto(RepartoAvanceInfo reporteAvance)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@UsuarioID", reporteAvance.UsuarioID},
                            {"@Seccion", reporteAvance.Seccion},
                            {"@TotalCorrales", reporteAvance.TotalCorrales},
                            {"@TotalCorralesSeccion", reporteAvance.TotalCorralesSeccion},
                            {"@TotalCorralesProcesados", reporteAvance.TotalCorralesProcesados},
                            {"@TotalCorralesProcesadosSeccion", reporteAvance.TotalCorralesProcesadosSeccion},
                            {"@PorcentajeSeccion", reporteAvance.PorcentajeSeccion},
                            {"@PorcentajeTotal", reporteAvance.PorcentajeTotal},
                            {"@EstatusError", reporteAvance.EstatusError}
                            
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
        /// Obtiene los parametros necesario para guardar los cambios al detalle del reparto
        /// </summary>
        /// <param name="cambiosDetalle"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarCambiosRepartoDetalle(List<CambiosReporteInfo> cambiosDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from detalle in cambiosDetalle
                                select
                                    new XElement("RepartoDetalle", 
                                            new XElement("OrganizacionID", detalle.OrganizacionID),
                                            new XElement("RepartoID", detalle.RepartoID),
                                            new XElement("Lote", detalle.Lote),
                                            new XElement("TipoServicioID", detalle.TipoServicioID),
                                            new XElement("FormulaIDProgramada", detalle.FormulaIDProgramada),
                                            new XElement("CantidadProgramada", detalle.CantidadProgramada),
                                            new XElement("EstadoComederoID", detalle.EstadoComederoID),
                                            new XElement("Observaciones", detalle.Observaciones),
                                            new XElement("FechaReparto", detalle.FechaReparto.SoloFecha()),
                                            new XElement("UsuarioModificacionID", detalle.UsuarioModificacionID)
                                    )


                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlReparto", xml.ToString()}
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
        /// Obtiene los parametros necesarios para guardar el estatus de distribucion de alimentos
        /// </summary>
        /// <param name="corral"></param>
        /// <param name="estatusDistribucion"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GuardarEstatusDistribucion(CorralInfo corral, int estatusDistribucion)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CodigoCorral", corral.Codigo},
                            {"@EstatusDistribucion", estatusDistribucion},
                            {"@OrganizacionID", corral.Organizacion.OrganizacionID},
                            {"@UsuarioID", corral.UsuarioCreacionID}
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
        /// Obtiene los parametros necesarios para obtner el detalle de todas las orden del dia
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroDetallePorDia(int organizacionId, DateTime fecha)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                            {"@Fecha", fecha},
                            {"@Activo", (int)EstatusEnum.Activo}
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
        /// Obtiene los parametros necesarios para actualizar los datos obtenidos del datalink
        /// </summary>
        /// <param name="cambiosDatalink"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarDataLink(List<DataLinkInfo> cambiosDatalink)
        {
            try
            {
                var x = cambiosDatalink.Where(n => n.FormulaServida == null || n.CamionReparto == null || n.Reparto == null);
                ////TODO: Agrupar datos invalidos y registrarlos
                if (x.Any())
                { }
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from detalle in cambiosDatalink
                                select
                                    new XElement("RepartoDetalle",
                                            new XElement("FormulaIDServida", detalle.FormulaServida.FormulaId),
                                            new XElement("CantidadServida", detalle.KilosServidos),
                                            new XElement("HoraReparto", detalle.Hora),
                                            new XElement("CamionRepartoID", detalle.CamionReparto.CamionRepartoID),
                                            new XElement("UsuarioModificacionID", detalle.UsuarioID),
                                            new XElement("RepartoID", detalle.Reparto.RepartoID),
                                            new XElement("TipoServicioID", detalle.TipoServicio)
                                            
                                    )


                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Servido",EstatusRepartoEnum.Servido},
                            {"@XmlRepartoDetalle", xml.ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametroObtenerRepartoDetallePorOrganizacionID(OrganizacionInfo organizacion, DateTime fecha)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacion.OrganizacionID},
                            {"@Fecha", fecha}
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
        /// Obtiene los parametros necesarios para insertar o actualizar la orden de reparto Manual
        /// </summary>
        /// <param name="ordenReparto"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroGenerarOrdenRepartoManual(OrdenRepartoAlimentacionInfo ordenReparto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoID", ordenReparto.Reparto.RepartoID},
                            {"@OrganizacionID", ordenReparto.OrganizacionID},
                            {"@LoteID", ordenReparto.Lote.LoteID},
                            {"@Fecha", ordenReparto.Reparto.Fecha},
                            {"@PesoInicio", ordenReparto.Reparto.PesoInicio},
                            {"@PesoProyectado", ordenReparto.Reparto.PesoProyectado},
                            {"@DiasEngorda", ordenReparto.Reparto.DiasEngorda},
                            {"@PesoRepeso", ordenReparto.Reparto.PesoRepeso},
                            {"@UsuarioCreacionID", ordenReparto.UsuarioID},

                            {"@TipoServicioID", ordenReparto.DetalleOrdenReparto.TipoServicioID},
                            {"@FormulaIDProgramada",ordenReparto.DetalleOrdenReparto.FormulaIDProgramada},
                            {"@FormulaIDServida",ordenReparto.DetalleOrdenReparto.FormulaIDServida},
                            {"@CantidadProgramada", ordenReparto.DetalleOrdenReparto.CantidadProgramada},
                            {"@CantidadServida", ordenReparto.DetalleOrdenReparto.CantidadServida},
                            {"@HoraReparto", ordenReparto.DetalleOrdenReparto.HoraReparto},
                            {"@Cabezas", ordenReparto.DetalleOrdenReparto.Cabezas},
                            {"@EstadoComederoID", ordenReparto.DetalleOrdenReparto.EstadoComederoID},
                            {"@CorralID", ordenReparto.Corral.CorralID}
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
        /// Se mapean los parametros para obtener Reparto Por Tipo Servicio Fecha
        /// </summary>
        /// <param name="corteGanadoGuardarInfo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroObtenerRepartoPorTipoServicioFecha(CorteGanadoGuardarInfo corteGanadoGuardarInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", corteGanadoGuardarInfo.EntradaGanado.OrganizacionID},
                            {"@TipoFormulaID", corteGanadoGuardarInfo.TipoFormula.TipoFormulaID},
                            {"@TipoServicioID", corteGanadoGuardarInfo.TipoServicioInfo.TipoServicioId},
                            {"@LoteID", corteGanadoGuardarInfo.LoteOrigen.LoteID},
                            {"@FechaEntrada", corteGanadoGuardarInfo.EntradaGanado.FechaEntrada},
                            {"@Activo", corteGanadoGuardarInfo.EntradaGanado.Activo},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametroGuardarImporte(RepartoDetalleInfo repartoDetalleInfo, AlmacenInventarioInfo inventarioInfo, LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoID", repartoDetalleInfo.RepartoID},
                            {"@LoteID", lote.LoteID},
                            {"@TipoServicio", repartoDetalleInfo.TipoServicioID},
                            {"@Prorrateo", repartoDetalleInfo.Prorrateo},
                            {"@PrecioPromedio", inventarioInfo.PrecioPromedio},
                            {"@UsuarioModificacionID",inventarioInfo.UsuarioModificacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametroGuardarImporteXML(List<RepartoDetalleInfo> listaRepartos)
        {
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                                 from reparto in listaRepartos
                                 select
                                     new XElement("Repartos",
                                                  new XElement("RepartoID", reparto.RepartoID),
                                                  new XElement("LoteID", reparto.LoteID),
                                                  new XElement("TipoServicio", reparto.TipoServicioID),
                                                  new XElement("Prorrateo", reparto.Prorrateo),
                                                  new XElement("PrecioPromedio", reparto.PrecioPromedio),
                                                  new XElement("UsuarioModificacionID", reparto.UsuarioCreacionID)));

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartosXML", xml.ToString()}
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado Reparto_ObtenerRepartoFechaCompleto
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroRepartoPorFechaCompleto(DateTime fecha, IList<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Fecha", fecha}
                        };
                var xml =
                    new XElement("ROOT",
                                 from lote in lotes
                                 select
                                     new XElement("Lotes",
                                                  new XElement("LoteID", lote.LoteID)));
                parametros.Add("@XmlLote", xml.ToString());
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion del
        /// procedimiento almacenado PolizaConsumoAlimento_ObtenerDatos
        /// </summary>
        /// <param name="movimientoDetalles"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPolizaConsumo(List<AlmacenMovimientoDetalle> movimientoDetalles, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID}
                        };
                var xml =
                    new XElement("ROOT",
                                 from movimientos in movimientoDetalles
                                 select
                                     new XElement("Movimientos",
                                                  new XElement("Movimiento", movimientos.AlmacenMovimientoID)));
                parametros.Add("@XmlMovimiento", xml.ToString());
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
        /// <param name="repartoDetalle">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosGuardarRepartoDetalle(List<RepartoDetalleInfo> repartoDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in repartoDetalle
                                 select
                                     new XElement("RepartoDetalle",
                                                  new XElement("RepartoID", detalle.RepartoID),
                                                  new XElement("TipoServicioID", detalle.TipoServicioID),
                                                  new XElement("FormulaIDProgramada", detalle.FormulaIDProgramada),
                                                  new XElement("FormulaIDServida", detalle.FormulaIDServida),
                                                  new XElement("CantidadProgramada", detalle.CantidadProgramada),
                                                  new XElement("CantidadServida", detalle.CantidadServida),
                                                  new XElement("HoraReparto", detalle.HoraReparto),
                                                  new XElement("CostoPromedio", detalle.CostoPromedio),
                                                  new XElement("Importe", detalle.Importe),
                                                  new XElement("Servido", detalle.Servido),
                                                  new XElement("Cabezas", detalle.Cabezas),
                                                  new XElement("EstadoComederoID", detalle.EstadoComederoID),
                                                  new XElement("CamionRepartoID", detalle.CamionRepartoID),
                                                  new XElement("Observaciones", detalle.Observaciones),
                                                  new XElement("Activo", detalle.Activo.GetHashCode()),
                                                  new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID)
                                                  ));


                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlRepartoDetalle", xml.ToString()}
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
        public static Dictionary<string, object> ObtenerParametrosCrear(RepartoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OrganizacionID", info.OrganizacionID},
                            {"@CorralID", info.Corral.CorralID},
							{"@LoteID", info.LoteID},
							{"@Fecha", info.Fecha},
							{"@PesoInicio", info.PesoInicio},
							{"@PesoProyectado", info.PesoProyectado},
							{"@DiasEngorda", info.DiasEngorda},
							{"@PesoRepeso", info.PesoRepeso},
							{"@Activo", info.Activo.GetHashCode()},
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

        internal static Dictionary<string, object> ObtenerParametrosGenerarOrdenRepartoConfiguracionAjustes(List<CambiosReporteInfo> cambiosDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in cambiosDetalle
                                 select
                                     new XElement("RepartoGrabar",
                                        new XElement("RepartoID", detalle.RepartoID),
                                        new XElement("RepartoDetalleIdManiana", detalle.RepartoDetalleIdManiana),
                                        new XElement("RepartoDetalleIdTarde", detalle.RepartoDetalleIdTarde),
                                        new XElement("OrganizacionID", detalle.OrganizacionID),
                                        new XElement("CorralCodigo", detalle.CorralInfo.Codigo),
                                        new XElement("Lote", detalle.Lote),
                                        new XElement("TipoServicioID", detalle.TipoServicioID),
                                        new XElement("FormulaIDProgramada", detalle.FormulaIDProgramada),
                                        new XElement("CantidadProgramada", detalle.CantidadProgramada),
                                        new XElement("EstadoComederoID", detalle.EstadoComederoID),
                                        new XElement("Observaciones", detalle.Observaciones),
                                        new XElement("FechaReparto", detalle.FechaReparto.SoloFecha()),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioModificacionID),
                                        new XElement("Servido", detalle.Servido),
                                        new XElement("ValidaPorcentaje", detalle.ValidaPorcentaje),
                                        new XElement("InactivarDetalle", detalle.InactivarDetalle),
                                        new XElement("CambiarLote", detalle.CambiarLote)
                                        ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlReparto", xml.ToString()}
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
        /// Obteiene los parametros necesarios para obtener un reparto de un lote y una fecha especifica
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="corral">Lote del reparto</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroRepartoPorFechaCorral(DateTime fecha, CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", corral.Organizacion.OrganizacionID},
                            {"@CorralID", corral.CorralID},
                            {"@Fecha", fecha}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosActualizarFormulaRepartoConfiguracionAjustes(List<CambiosReporteInfo> cambiosDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in cambiosDetalle
                                 select
                                     new XElement("RepartoGrabar",
                                        new XElement("RepartoID", detalle.RepartoID),
                                        new XElement("RepartoDetalleIdManiana", detalle.RepartoDetalleIdManiana),
                                        new XElement("RepartoDetalleIdTarde", detalle.RepartoDetalleIdTarde),
                                        new XElement("OrganizacionID", detalle.OrganizacionID),
                                        new XElement("CorralCodigo", detalle.CorralInfo.Codigo),
                                        new XElement("Lote", detalle.Lote),
                                        new XElement("TipoServicioID", detalle.TipoServicioID),
                                        new XElement("FormulaIDProgramada", detalle.FormulaIDProgramada),
                                        new XElement("CantidadProgramada", detalle.CantidadProgramada),
                                        new XElement("EstadoComederoID", detalle.EstadoComederoID),
                                        new XElement("Observaciones", detalle.Observaciones),
                                        new XElement("FechaReparto", detalle.FechaReparto.SoloFecha()),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioModificacionID),
                                        new XElement("Servido", detalle.Servido),
                                        new XElement("ValidaPorcentaje", detalle.ValidaPorcentaje)
                                        ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlReparto", xml.ToString()}
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
        /// <param name="repartoDetalle">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosActualizarAlmacenMovimientoReparto(List<RepartoDetalleInfo> repartoDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in repartoDetalle
                                 select
                                     new XElement("RepartoDetalle",
                                                  new XElement("RepartoDetalleID", detalle.RepartoDetalleID),
                                                  new XElement("AlmacenMovimientoID", detalle.AlmacenMovimientoID),
                                                  new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID)
                                                  ));


                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoDetalleXML", xml.ToString()}
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
        /// Obtiene parametros para obtener los repartos por la lista de ids
        /// </summary>
        /// <param name="repartosID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroPorRepartosId(List<long> repartosID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from reparto in repartosID
                                 select
                                     new XElement("Reparto",
                                                  new XElement("RepartoID", reparto)
                                                  ));


                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoXML", xml.ToString()},
                            {"@OrganizacionID", organizacionID},
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
        /// Obtiene los repartos ajustados en el dia
        /// </summary>
        /// <param name="fechaReparto">Fecha del Reparto</param>
        /// <param name="organizacionID">Organizacion ID</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroRepartosAjustados(DateTime fechaReparto, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Fecha", fechaReparto},
                            {"@OrganizacionID", organizacionID},
                            {"@Activo",EstatusEnum.Activo.GetHashCode()}
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
        /// <param name="repartoDetalle">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosActualizarCantidadProgramadaReparto(List<RepartoDetalleInfo> repartoDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in repartoDetalle
                                 select
                                     new XElement("RepartoDetalle",
                                                  new XElement("RepartoDetalleID", detalle.RepartoDetalleID),
                                                  new XElement("CantidadProgramada", detalle.CantidadProgramada),
                                                  new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID)
                                                  ));


                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoDetalleXML", xml.ToString()}
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
        /// ejecucion del procedimiento almacendo Reparto_ObtenerPorLoteOrganizacion
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorLoteOrganizacion(int loteID, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", loteID},
                            {"@OrganizacionID", organizacionId}
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
        /// Obtiene los parametros necesarios para obtener el peso de llegada
        /// </summary>
        /// <param name="lotesXml"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroPesoLlegadaXML(IList<LoteInfo> lotesXml, int organizacionID)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from lote in lotesXml
                                 select
                                     new XElement("Lotes",
                                                  new XElement("LoteID", lote.LoteID)
                                                  ));


                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlLote", xml.ToString()},
                            {"@OrganizacionID", organizacionID},
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
        /// Obteiene los parametros necesarios para obtener un reparto de un lote y una fecha especifica
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="lotesXml">Lote del reparto</param>
        /// <param name="organizacionID">Organizacion del Reparto</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroRepartoPorFechaXML(DateTime fecha, IList<LoteInfo> lotesXml, int organizacionID)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from lote in lotesXml
                                 select
                                     new XElement("Lotes",
                                                  new XElement("LoteID", lote.LoteID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@XmlLote", xml.ToString()},
                            {"@Fecha", fecha}
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
        /// Obtiene los parametros necesarios para obtener el consumo total del dia
        /// </summary>
        /// <param name="organizacionId">Organizacion</param>
        /// <param name="corralesXml">Lote</param>
        /// <param name="fecha">fecha del reparto</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroConsumoTotalDiaXML(int organizacionId, IList<CorralInfo> corralesXml, DateTime fecha)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from corral in corralesXml
                                select
                                    new XElement("Corrales",
                                                 new XElement("CorralID", corral.CorralID)
                                                 ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                            {"@Fecha", fecha},
                            {"@XmlCorrales", xml.ToString()}
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
        /// ejecucion del procedimiento almacendo Reparto_ObtenerPorLoteOrganizacion
        /// </summary>
        /// <param name="corralID"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorCorralOrganizacion(int corralID, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CorralID", corralID},
                            {"@OrganizacionID", organizacionId}
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
        /// ejecucion del procedimiento almacendo Reparto_ObtenerConsumoPendiente
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosConsumoPendiente(AplicacionConsumoModel filtros)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Fecha", filtros.FechaConsumo},
                            {"@OrganizacionID", filtros.Organizacion.OrganizacionID}
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
        /// Obteiene los parametros necesarios para obtener un reparto de un lote y una fecha especifica
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="organizacionID">organizacion del reparto</param>
        /// <param name="corrales">corrales del reparto</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroRepartosPorFechaCorrales(DateTime fecha,int organizacionID, List<CorralInfo> corrales)
        {
            try
            {
                Logger.Info();
                var xml =
                  new XElement("ROOT",
                               from corral in corrales
                               select
                                   new XElement("Corrales",
                                                new XElement("CorralID", corral.CorralID)
                                                ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@CorralesXML", xml.ToString()},
                            {"@Fecha", fecha}
                            
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
        /// Obtiene los parametros necesario para guardar los cambios al detalle del reparto
        /// </summary>
        /// <param name="cambiosDetalle"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarRepartosServicioCorrales(List<CambiosReporteInfo> cambiosDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in cambiosDetalle
                                 select
                                     new XElement("RepartoGrabar",
                                        new XElement("RepartoID", detalle.RepartoID),
                                        new XElement("RepartoDetalleIdManiana", detalle.RepartoDetalleIdManiana),
                                        new XElement("RepartoDetalleIdTarde", detalle.RepartoDetalleIdTarde),
                                        new XElement("OrganizacionID", detalle.OrganizacionID),
                                        new XElement("CorralCodigo", detalle.CorralInfo.Codigo),
                                        new XElement("Lote", detalle.Lote),
                                        new XElement("TipoServicioID", detalle.TipoServicioID),
                                        new XElement("FormulaIDProgramada", detalle.FormulaIDProgramada),
                                        new XElement("CantidadProgramada", detalle.CantidadProgramada),
                                        new XElement("EstadoComederoID", detalle.EstadoComederoID),
                                        new XElement("Observaciones", detalle.Observaciones),
                                        new XElement("FechaReparto", detalle.FechaReparto.SoloFecha()),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioModificacionID),
                                        new XElement("Servido", detalle.Servido)
                                        ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlReparto", xml.ToString()}
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
        /// Obteiene los parametros necesarios para obtener un reparto de un lote y una fecha especifica
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="corral">Lote del reparto</param>
        /// <param name="tipoServicioID">servicio a consultar</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroRepartoPorFechaCorralServicio(DateTime fecha, CorralInfo corral, int tipoServicioID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", corral.Organizacion.OrganizacionID},
                            {"@CorralID", corral.CorralID},
                            {"@Fecha", fecha},
                            {"@TipoServicioID", tipoServicioID}
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
