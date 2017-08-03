using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxPesajeMateriaPrimaDAL
    {
        /// <summary>
        /// Metodo que obtiene los parametros utilizados para crear un registro
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@ProgramacionMateriaPrimaID", pesajeMateriaPrimaInfo.ProgramacionMateriaPrimaID},
								{"@ProveedorChoferID", pesajeMateriaPrimaInfo.ProveedorChoferID},
                                {"@CamionID", pesajeMateriaPrimaInfo.CamionID},
                                {"@PesoBruto", pesajeMateriaPrimaInfo.PesoBruto},
                                {"@PesoTara", pesajeMateriaPrimaInfo.PesoTara},
                                {"@Piezas", pesajeMateriaPrimaInfo.Piezas},
                                {"@TipoPesajeID", pesajeMateriaPrimaInfo.TipoPesajeID},
                                {"@UsuarioIDSurtido", pesajeMateriaPrimaInfo.UsuarioIDSurtido},
                                {"@FechaSurtido", pesajeMateriaPrimaInfo.FechaSurtido},
                                {"@UsuarioIDRecibe", pesajeMateriaPrimaInfo.UsuarioIDRecibe},
                                {"@FechaRecibe", pesajeMateriaPrimaInfo.FechaRecibe},
                                {"@EstatusID", pesajeMateriaPrimaInfo.EstatusID},
                                {"@Activo",pesajeMateriaPrimaInfo.Activo.GetHashCode()},
                                {"@UsuarioCreacionID", pesajeMateriaPrimaInfo.UsuarioCreacionID}
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
        /// Obtiene parametro detalle por 
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorTicketPedido(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Ticket", pesajeMateriaPrimaInfo.Ticket},
                                {"@PedidoID", pesajeMateriaPrimaInfo.PedidoID},
                                {"@Activo", pesajeMateriaPrimaInfo.Activo}
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
        /// Obtiene los parametros para obtener los pesajes
        /// </summary>
        /// <param name="programacionMateriaPrimaId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorProgramacionMateriaPrimaId(int programacionMateriaPrimaId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
					    {"@ProgramacionMateriaPrimaId", programacionMateriaPrimaId}
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
        /// Actualiza todos los campos del pesaje ( se consulta primero en base al Id y se sobre Escribe)
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerActualizarPesajePorId(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@PesajeMateriaPrimaID", pesajeMateriaPrimaInfo.PesajeMateriaPrimaID},
								{"@ProgramacionMateriaPrimaID", pesajeMateriaPrimaInfo.ProgramacionMateriaPrimaID},
								{"@ProveedorChoferID", pesajeMateriaPrimaInfo.ProveedorChoferID},
                                {"@Ticket", pesajeMateriaPrimaInfo.Ticket},
                                {"@CamionID", pesajeMateriaPrimaInfo.CamionID},
                                {"@PesoBruto", pesajeMateriaPrimaInfo.PesoBruto},
                                {"@PesoTara", pesajeMateriaPrimaInfo.PesoTara},
                                {"@Piezas", pesajeMateriaPrimaInfo.Piezas},
                                {"@TipoPesajeID", pesajeMateriaPrimaInfo.TipoPesajeID},
                                {"@UsuarioIDSurtido", pesajeMateriaPrimaInfo.UsuarioIDSurtido},
                                {"@FechaSurtido", pesajeMateriaPrimaInfo.FechaSurtido},
                                {"@UsuarioIDRecibe", pesajeMateriaPrimaInfo.UsuarioIDRecibe},
                                {"@FechaRecibe", pesajeMateriaPrimaInfo.FechaRecibe},
                                {"@EstatusID", pesajeMateriaPrimaInfo.EstatusID},
                                {"@Activo",pesajeMateriaPrimaInfo.Activo.GetHashCode()},
                                {"@UsuarioModificacionID", pesajeMateriaPrimaInfo.UsuarioModificacionID},
                                {"@AlmacenMovimientoOrigenId", pesajeMateriaPrimaInfo.AlmacenMovimientoOrigenId},
                                {"@AlmacenMovimientoDestinoId", pesajeMateriaPrimaInfo.AlmacenMovimientoDestinoId}
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado PaseProceso_ObtenerDatosPoliza
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerValoresPolizaPaseProceso(int folioPedido, int organizacionID, string xmlLote)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@FolioPedido", folioPedido},
                            {"@XmlLote", xmlLote},
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado PaseProceso_ObtenerDatosPolizaReimpresion
        /// </summary>
        /// <param name="folioPedido"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerValoresPolizaPaseProcesoReimpresion(int folioPedido, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                            {"@FolioPedido", folioPedido},
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
        /// Obtiene los parametros para obtener el pesaje de materia prima por id
        /// </summary>
        /// <param name="pesaje"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorId(PesajeMateriaPrimaInfo pesaje)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
					    {"@PesajeMateriaPrimaId", pesaje.PesajeMateriaPrimaID}
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado PaseProceso_ObtenerDatosPolizaReimpresion
        /// </summary>
        /// <param name="movimientos"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerFoliosPaseProceso(List<long> movimientos)
        {
            try
            {
                Logger.Info();
                var xml =
                  new XElement("ROOT",
                               from tipoCorral in movimientos
                               select
                                   new XElement("Movimientos",
                                                new XElement("AlmacenMovimientoID", tipoCorral))
                                   );
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlMovimientos", xml.ToString()}
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
