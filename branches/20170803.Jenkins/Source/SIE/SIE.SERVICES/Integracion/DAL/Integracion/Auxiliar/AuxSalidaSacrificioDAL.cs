using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System.Xml.Linq;
using System.Linq;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxSalidaSacrificioDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, SalidaSacrificioInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@SalidaSacrificioID", filtro.ID_SalidaSacrificio},
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
        public static Dictionary<string, object> ObtenerParametrosCrear(SalidaSacrificioInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@FEC_SACR", info.FEC_SACR},
							{"@NUM_SALI", info.NUM_SALI},
							{"@NUM_CORR", info.NUM_CORR.PadLeft(3, '0')},
							{"@NUM_PRO", info.NUM_PRO.PadLeft(4,'0')},
							{"@FEC_SALC", info.FEC_SALC},
							{"@HORA_SAL", info.HORA_SAL},
							{"@EDO_COME", info.EDO_COME},
							{"@NUM_CAB", info.NUM_CAB},
							{"@TIP_ANI", info.TIP_ANI},
							{"@KGS_SAL", info.KGS_SAL},
							{"@PRECIO", info.PRECIO},
							{"@ORIGEN", info.ORIGEN},
							{"@CTA_PROVIN", info.CTA_PROVIN},
							{"@PRE_EST", info.PRE_EST},
							{"@ID_SALIDA_SACRIFICIO", info.ID_SalidaSacrificio},
							{"@VENTA_PARA", info.VENTA_PARA},
							{"@COD_PROVEEDOR", info.COD_PROVEEDOR},
							{"@NOTAS", info.NOTAS},
							{"@COSTO_CABEZA", info.COSTO_CABEZA},
							{"@CABEZAS_PROCESADAS", info.CABEZAS_PROCESADAS},
							{"@FICHA_INICIO", info.FICHA_INICIO},
							{"@COSTO_CORRAL", info.COSTO_CORRAL},
							{"@UNI_ENT", info.UNI_ENT},
							{"@UNI_SAL", info.UNI_SAL},
							{"@SYNC", info.SYNC},
							{"@ID_S", info.ID_S},
							{"@SEXO", info.SEXO},
							{"@DIAS_ENG", info.DIAS_ENG},
							{"@FOLIO_ENTRADA_I", info.FOLIO_ENTRADA_I},
							{"@ORIGEN_GANADO", info.ORIGEN_GANADO},
							{"@OrdenSacrificioID", info.OrdenSacrificioID}
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
        /// Obtiene parametros para obtener una salida de sacrificio
        /// </summary>
        /// <param name="movimientosSiap">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosObtenerSalidaSacrificio(List<SalidaSacrificioDetalleInfo> movimientosSiap)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in movimientosSiap
                                 select new XElement("Datos",
                                        new XElement("FolioSalida", detalle.FolioSalida),
                                        new XElement("LoteID", detalle.LoteId),
                                        new XElement("AnimalID", detalle.AnimalId),
                                        new XElement("Arete", detalle.Arete)
                                     ));

                var parametros = new Dictionary<string, object> { { "@Datos", xml.ToString() } };

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
        public static Dictionary<string, object> ObtenerParametrosActualizar(SalidaSacrificioInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@FEC_SACR", info.FEC_SACR},
							{"@NUM_SALI", info.NUM_SALI},
							{"@NUM_CORR", info.NUM_CORR},
							{"@NUM_PRO", info.NUM_PRO},
							{"@FEC_SALC", info.FEC_SALC},
							{"@HORA_SAL", info.HORA_SAL},
							{"@EDO_COME", info.EDO_COME},
							{"@NUM_CAB", info.NUM_CAB},
							{"@TIP_ANI", info.TIP_ANI},
							{"@KGS_SAL", info.KGS_SAL},
							{"@PRECIO", info.PRECIO},
							{"@ORIGEN", info.ORIGEN},
							{"@CTA_PROVIN", info.CTA_PROVIN},
							{"@PRE_EST", info.PRE_EST},
							{"@ID_SALIDA_SACRIFICIO", info.ID_SalidaSacrificio},
							{"@VENTA_PARA", info.VENTA_PARA},
							{"@COD_PROVEEDOR", info.COD_PROVEEDOR},
							{"@NOTAS", info.NOTAS},
							{"@COSTO_CABEZA", info.COSTO_CABEZA},
							{"@CABEZAS_PROCESADAS", info.CABEZAS_PROCESADAS},
							{"@FICHA_INICIO", info.FICHA_INICIO},
							{"@COSTO_CORRAL", info.COSTO_CORRAL},
							{"@UNI_ENT", info.UNI_ENT},
							{"@UNI_SAL", info.UNI_SAL},
							{"@SYNC", info.SYNC},
							{"@ID_S", info.ID_S},
							{"@SEXO", info.SEXO},
							{"@DIAS_ENG", info.DIAS_ENG},
							{"@FOLIO_ENTRADA_I", info.FOLIO_ENTRADA_I},
							{"@ORIGEN_GANADO", info.ORIGEN_GANADO},
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
        /// <param name="idSalidaSacrificio">Identificador de la entidad SalidaSacrificio</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int idSalidaSacrificio)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ID_SalidaSacrificio", idSalidaSacrificio}
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
        ///  Obtiene Parametros por Id
        /// </summary>
        /// <param name="ordenSacrificioId">Identificador de la entidad OrdenSacrificio</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorOrdenSacrificioID(int ordenSacrificioId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrdenSacrificioID", ordenSacrificioId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosPorOrdenSacrificioId(int ordenSacrificioId, int organizacionId, byte aplicaRollBack, int usuarioID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrdenSacrificioID", ordenSacrificioId},
                            {"@OrganizacionID", organizacionId},
                            {"@AplicaRollBack", aplicaRollBack},
                            {"@usuarioID", usuarioID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosEliminarDetalleOrdenSacrificio(List<SalidaSacrificioInfo> orden, int organizacionId, int ordenSacrificioId, int aplicaMarel, int usuarioId)
        {
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                                 from detalle in orden
                                 select new XElement("Lotes",
                                        new XElement("LoteID", detalle.LoteID)
                                     ));

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionId", organizacionId},
                            {"@OrdenSacrificioId", ordenSacrificioId},
                            {"@Xml", xml.ToString()},
                            {"@AplicaMarel", aplicaMarel},
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

        public static Dictionary<string, object> ObtenerParametrosGuardarMarel(List<SalidaSacrificioInfo> ordenSacrificio)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in ordenSacrificio
                                 select new XElement("Datos",
                                        new XElement("LoteID", detalle.LoteID),
                                        new XElement("AnimalID", detalle.AuxiliarId),
                                        new XElement("Arete", detalle.Clasificacion),
                                        new XElement("FechaSacrificio", detalle.FEC_SACR),
                                        new XElement("NumeroSalida", detalle.NUM_SALI),
                                        new XElement("NumeroCorral", detalle.NUM_CORR),
                                        new XElement("NumeroProceso", detalle.NUM_PRO),
                                        new XElement("NumeroCabezas", detalle.NUM_CAB),
                                        new XElement("TipoAnimal", detalle.TIP_ANI),
                                        new XElement("Origen", detalle.ORIGEN),
                                        new XElement("Notas", detalle.NOTAS),
                                        new XElement("DiasEngorda", detalle.DIAS_ENG)
                                     ));

                var parametros = new Dictionary<string, object> { { "@Datos", xml.ToString() } };

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

