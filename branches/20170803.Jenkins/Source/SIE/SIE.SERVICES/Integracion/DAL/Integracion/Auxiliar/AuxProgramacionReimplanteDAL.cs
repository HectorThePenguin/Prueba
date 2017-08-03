using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Base.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    /// <summary>
    /// Clase auxiliar de parametros de Programacion de reimplante
    /// </summary>
    internal class AuxProgramacionReimplanteDAL
    {
        /// <summary>
        /// obtiene los parametros por OrganizacionID
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacionId(int organizacionId)
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
        /// Obtiene los parametros para realizar el guardado de la orden de reimplante
        /// </summary>
        /// <param name="orden"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardado(OrdenReimplanteInfo orden)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in orden.CorralesDestino
                                 select new XElement("CorralesDestino",
                                        new XElement("CorralesDestinoID", detalle.CorralID),
                                        new XElement("PuntaChica", Convert.ToInt16(detalle.PuntaChica))
                                     ));

                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", orden.OrganizacionId},
                                     {"@Fecha", orden.FechaReimplanteSeleccionado},
                                     {"@LoteId",orden.LoteId},
                                     //{"@CorralDestinoId", orden.CorralDestinoSeleccionado.CorralID},
                                     {"@ProductoId", orden.ProductoSeleccionado.ProductoId},
                                     {"@UsuarioId", orden.UsuarioCreacion},
                                     {"@XMLCorralDestino", xml.ToString()},
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
        /// obtiene los parametros Programaion Reimplante
        /// </summary>
        /// <param name="folioProgReimplante"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosProgramacionReimplante(int folioProgReimplante)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@FolioProgReimplante", folioProgReimplante}
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
        /// obtiene los parametros Programaion Reimplante
        /// </summary>
        /// <param name="fechaReal"></param>
        /// <param name="loteReimplante"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosFechaReal(String fechaReal, LoteReimplanteInfo loteReimplante)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@NoLote", loteReimplante.LoteReimplanteID},
                                     {"@TipoMovimiento",loteReimplante.TipoMovimientoID},
                                     {"@FolioEntrada",loteReimplante.FolioEntrada},
                                     {"@NumCabezas",loteReimplante.NumCabezas},
                                     {"@LoteReimplanteID",loteReimplante.LoteReimplanteID}
                                     
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
        /// Obtiene la lista de parametros para la validacion de programacion de reimplante
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidacionProgramacion(DateTime selectedDate, int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionId},
                                     {"@Fecha", selectedDate.Date}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosProgramacionReimplantePorLoteID(LoteInfo lote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", lote.OrganizacionID},
                                     {"@LoteID", lote.LoteID}
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado
        /// ProgramacionReimplante_EliminarProgramacionReimplanteXML
        /// </summary>
        /// <param name="corralesProgramados"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosProgramacionReimplanteXML(List<ProgramacionReinplanteInfo> corralesProgramados)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from reimplante in corralesProgramados
                                 select new XElement("Reimplante",
                                        new XElement("FolioProgramacionID", reimplante.FolioProgramacionID),
                                        new XElement("UsuarioModificacionID", reimplante.UsuarioModificacionID)));
                parametros = new Dictionary<string, object>
                                 {
                                     {"@XmlReimplante", xml.ToString()},
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado
        /// ReimplanteGanado_CierreReimplante
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCerrarProgramacionReimplante(int organizacionID, int usuarioID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID},
                                     {"@UsuarioID", usuarioID},
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
