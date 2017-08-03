using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxOrdenSacrificioDAL
    {
        /// <summary>
        ///     Metodo para obtener los parametros para guardar una orden de sacrificio
        /// </summary>
        /// <param name="ordenSacrificio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarOrdenSacrificio(OrdenSacrificioInfo ordenSacrificio)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrdenSacrificioID", ordenSacrificio.OrdenSacrificioID},
                            {"@TipoFolioID", (int)TipoFolio.OrdenSacrificio},
                            {"@OrganizacionID", ordenSacrificio.OrganizacionID},
                            {"@Observaciones", ordenSacrificio.Observacion},
                            {"@EstatusID", (int)Estatus.OrdenSacrificioPendiente},
                            {"@Activo", (int)EstatusEnum.Activo},
                            {"@FechaOrden", ordenSacrificio.FechaOrden},
                            {"@UsuarioCreacionID", ordenSacrificio.UsuarioCreacion},
                            
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
        ///     Metodo para obtener los parametros para guardar una orden de sacrificio
        /// </summary>
        /// <param name="ordenSacrificio">Orden de sacrificio</param>
        /// <param name="detalleOrden">Detalle de la orden de sacrificio</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarDetalleOrdenSacrificio(OrdenSacrificioInfo ordenSacrificio, IList<OrdenSacrificioDetalleInfo> detalleOrden)
        {
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                                 from detalle in detalleOrden
                                 select new XElement("DetallOrdenSacrificio",
                                        new XElement("OrdenSacrificioDetalleID", detalle.OrdenSacrificioDetalleID),
                                        new XElement("CorraletaID", detalle.Corraleta.CorralID),
                                        new XElement("CorraletaCodigo", detalle.CorraletaCodigo),
                                        new XElement("Proveedor", detalle.Proveedor.Descripcion),
                                        new XElement("Clasificacion", detalle.Clasificacion),
                                        new XElement("LoteID", detalle.Lote.LoteID),
                                        new XElement("CabezasLote", detalle.Cabezas),
                                        new XElement("DiasEngordaGrano", detalle.DiasEngordaGrano),
                                        new XElement("DiasRetiro", detalle.DiasRetiro),
                                        new XElement("CabezasSacrificio", detalle.CabezasASacrificar),
                                        new XElement("Turno", (int)detalle.Turno),
                                        new XElement("Activo", (int)EstatusEnum.Activo),
                                        new XElement("Orden", detalle.Orden),
                                        new XElement("TipoGanadoID", detalle.TipoGanadoID)
                                     ));


                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrdenSacrificioID", ordenSacrificio.OrdenSacrificioID},
                            {"@FolioSalida", 0},
                            {"@Activo", (int)EstatusEnum.Activo},
                            {"@UsuarioCreacionID", ordenSacrificio.UsuarioCreacion},
                            {"@DetalleOrdenSacrificio", xml.ToString()}
                            
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
        /// Obtiene los parametros necesarios para obtener la orden de sacrificio del dia actual
        /// </summary>
        /// <param name="ordenSacrificio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosOrdenSacrificioDelDia(OrdenSacrificioInfo ordenSacrificio)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", ordenSacrificio.OrganizacionID},
                            {"@FechaOrden", ordenSacrificio.FechaOrden},
                            {"@EstatusID", ordenSacrificio.EstatusID},
                            {"@Activo", (int)EstatusEnum.Activo},
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
        /// Obteiene los parametros necesarios para obtener el detalle de una orden de sacrificio
        /// </summary>
        /// <param name="ordenSacrificio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosOrdenSacrificioDetalle(OrdenSacrificioInfo ordenSacrificio)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrdenSacrificioID", ordenSacrificio.OrdenSacrificioID},
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
        /// Obteiene los parametros necesarios para desactiva el detalle de una orden de sacrificio
        /// </summary>
        /// <param name="ordenSacrificioDetalle"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEliminarOrdenSacrificioDetalle(IList<OrdenSacrificioDetalleInfo> ordenSacrificioDetalle,int idUsuario)
        {
            try
            {
                Logger.Info();

                var xml =
                   new XElement("ROOT",
                                from detalle in ordenSacrificioDetalle
                                select new XElement("DetallOrdenSacrificio",
                                       new XElement("OrdenSacrificioDetalleID", detalle.OrdenSacrificioDetalleID),
                                       new XElement("Activo", (int)EstatusEnum.Inactivo)
                                    ));

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@UsuarioModificacion", idUsuario },
                            {"@DetalleOrdenSacrificio", xml.ToString() }
                            
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
        /// Obtiene los parametros para obtener los dias de engorda 70
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroDiasEngorda70(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteId", lote.LoteID},
                            {"@TipoProduccion", (int)TipoFormula.Produccion},
                            {"@TipoFinalizacion", (int)TipoFormula.Finalizacion},
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
        /// Obtiene los parametros necesarios para obtener las cabezas que se encuentran en otras ordenes
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtnerCabezasDiferentesOrdenes(LoteInfo lote, int ordenSacrificioId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteId", lote.LoteID},
                            {"@OrdenRepartoActual", ordenSacrificioId}
                           
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
        /// Obtiene los parametros necesarios generar 
        /// el xml que servirá de interfaz para el Control de Piso
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarOrdenSacrificioInterfazSPI(IList<SalidaSacrificioInfo> lista)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from salida in lista
                                 select new XElement("SalidaSacrificio",
                                                     new XElement("FEC_SACR", salida.FEC_SACR),
                                                     new XElement("NUM_SALI", salida.NUM_SALI),
                                                     new XElement("NUM_CORR", salida.NUM_CORR),
                                                     new XElement("NUM_PRO", salida.NUM_PRO),
                                                     new XElement("FEC_SALC", salida.FEC_SALC),
                                                     new XElement("HORA_SAL", salida.HORA_SAL),
                                                     new XElement("EDO_COME", salida.EDO_COME),
                                                     new XElement("NUM_CAB", salida.NUM_CAB),
                                                     new XElement("TIP_ANI", salida.TIP_ANI),
                                                     new XElement("KGS_SAL", salida.KGS_SAL),
                                                     new XElement("PRECIO", salida.PRECIO),
                                                     new XElement("ORIGEN", salida.ORIGEN),
                                                     new XElement("CTA_PROVIN", salida.CTA_PROVIN),
                                                     new XElement("PRE_EST", salida.PRE_EST),
                                                     new XElement("ID_SALIDA_SACRIFICIO", salida.ID_SalidaSacrificio),
                                                     new XElement("VENTA_PARA", salida.VENTA_PARA),
                                                     new XElement("COD_PROVEEDOR", salida.COD_PROVEEDOR),
                                                     new XElement("NOTAS", salida.NOTAS),
                                                     new XElement("COSTO_CABEZA", salida.COSTO_CABEZA),
                                                     new XElement("CABEZAS_PROCESADAS", salida.CABEZAS_PROCESADAS),
                                                     new XElement("FICHA_INICIO", salida.FICHA_INICIO),
                                                     new XElement("COSTO_CORRAL", salida.COSTO_CORRAL),
                                                     new XElement("UNI_ENT", salida.UNI_ENT),
                                                     new XElement("UNI_SAL", salida.UNI_SAL),
                                                     new XElement("SYNC", salida.SYNC),
                                                     new XElement("ID_S", salida.ID_S),
                                                     new XElement("SEXO", salida.SEXO),
                                                     new XElement("DIAS_ENG", salida.DIAS_ENG),
                                                     new XElement("FOLIO_ENTRADA_I", salida.FOLIO_ENTRADA_I),
                                                     new XElement("ORIGEN_GANADO", salida.ORIGEN_GANADO),
                                                     new XElement("TIPO_SALIDA", salida.TIPO_SALIDA) 

                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"xmSalidaSacrificio", xml.ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametroDetalleOrden(string fecha, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            { "@FechaOrden" , fecha}
                            ,{"@OrganizacionID",organizacionId}
                        };
                return parametros;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametroConsultarEstatusMarel(string fechaSacrificio, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                Logger.Info();
                var xml =
                new XElement("ROOT",
                             from d in detalle
                             select new XElement("Lotes",
                                    new XElement("LoteID", d.Lote.LoteID),
                                    new XElement("Lote", d.Lote.Lote)
                                 ));


                var parametros = new Dictionary<string, object> { { "@FEC_SACR", fechaSacrificio }, { "@Xml", xml.ToString() } };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametroValidarCabezasActualesVsCabezasSacrificar(int organizacionId, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                Logger.Info();
                var xml =
                  new XElement("ROOT",
                               from d in detalle
                               select new XElement("Lotes",
                                      new XElement("LoteID", d.Lote.LoteID),
                                      new XElement("CabezasSacrificar", d.CabezasASacrificar)
                                   ));
                var parametros = new Dictionary<string, object> { { "@OrganizacionID", organizacionId }, { "@Xml", xml.ToString() } };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametroCorralConLotesActivos(int organizacionId, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                Logger.Info();
                var xml =
                  new XElement("ROOT",
                               from d in detalle
                               select new XElement("Lotes",
                                      new XElement("LoteID", d.Lote.LoteID)
                                   ));
                var parametros = new Dictionary<string, object> { { "@OrganizacionID", organizacionId }, { "@Xml", xml.ToString() } };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametroResumenSacrificioSiap(List<ControlSacrificioInfo> resumenSacrificio)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT",
                               from d in resumenSacrificio
                               select new XElement("Sacrificio",
                                      new XElement("FEC_SACR", d.FechaSacrificio),
                                      new XElement("NUM_CORR", d.NumeroCorral),
                                      new XElement("NUM_PRO", d.NumeroProceso),
                                      new XElement("TAG_ARETE", d.Arete),
                                      new XElement("TIPO_DE_GANADO", d.TipoGanadoId),
                                      new XElement("LOTEID", d.LoteId),
                                      new XElement("ANIMALID", d.AnimalId),
                                      new XElement("NUM_CORR_INNOVA", d.CorralInnova),
                                      new XElement("PO_INNOVA", d.Po),
                                      new XElement("Consecutivo_Sacrificio", d.Consecutivo),

                                      new XElement("Indicador_Noqueo", d.Noqueo.GetHashCode()),
                                      new XElement("Indicador_Piel_Sangre", d.PielSangre.GetHashCode()),
                                      new XElement("Indicador_Piel_Descarnada", d.PielDescarnada.GetHashCode()),
                                      new XElement("Indicador_Viscera", d.Viscera.GetHashCode()),
                                      new XElement("Indicador_Inspeccion", d.Inspeccion.GetHashCode()),
                                      new XElement("Indicador_Canal_Completa", d.CanalCompleta.GetHashCode()),
                                      new XElement("Indicador_Canal_Caliente", d.CanalCaliente.GetHashCode()),
                                      new XElement("OrganizacionId", d.OrganizacionID.GetHashCode())
                                   ));

                var parametros = new Dictionary<string, object> { { "@Sacrificio", xml.ToString() } };
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
