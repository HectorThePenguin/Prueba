using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProgramacionMateriaPrimaDAL
    {
        /// <summary>
        /// Devuelve los parametros necesarios para guardar la programacion de materia prima.
        /// </summary>
        /// <param name="listaProgramacion"></param>
        /// <returns></returns>
        internal static Dictionary<String, Object> ObtenerParametrosGuardarProgramacionMateriaPrima(List<ProgramacionMateriaPrimaInfo> listaProgramacion )
        {
            Dictionary<string, object> parametros;

            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                      from programacion in listaProgramacion
                      select
                          new XElement("ProgramacionMateriaPrima",
                               new XElement("PedidoDetalleID", programacion.PedidoDetalleId),
                               new XElement("OrganizacionID", programacion.Organizacion.OrganizacionID),
                               new XElement("AlmacenID", programacion.Almacen.AlmacenID),
                               new XElement("AlmacenInventarioLoteID", programacion.InventarioLoteOrigen.AlmacenInventarioLoteId),
                               new XElement("CantidadProgramada", programacion.CantidadProgramada),
                               new XElement("UsuarioCreacionID", programacion.UsuarioCreacion.UsuarioID),
                               new XElement("Observaciones",programacion.Observaciones)
                        )
                );

                parametros =
                    new Dictionary<string, object>
                        {
                            {"@XML_Programacion", xml.ToString()}
                        };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }

            return parametros;

        }
        /// <summary>
        /// Devuelve los parametros para consultar la programacion materia prima por pedido detalle
        /// </summary>
        /// <param name="pedidoDetalle"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerProgramacionMateriaPrima(PedidoDetalleInfo pedidoDetalle)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@PedidoDetalleId", pedidoDetalle.PedidoDetalleId}
                        
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
        /// Obtiene parametros para el metodo que actualiza la cantidad entregada
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarCantidadEntregada(ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProgramacionMateriaPrimaID", programacionMateriaPrimaInfo.ProgramacionMateriaPrimaId},
                        {"@CantidadEntregada", programacionMateriaPrimaInfo.CantidadEntregada},
                        {"@UsuarioModificacionID", programacionMateriaPrimaInfo.UsuarioModificacion.UsuarioID}
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
        /// Obtiene parametros para el metodo que actualiza la justificacion
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarJustificacion(ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProgramacionMateriaPrimaID", programacionMateriaPrimaInfo.ProgramacionMateriaPrimaId},
                        {"@Justificacion", programacionMateriaPrimaInfo.Justificacion},
                        {"@UsuarioModificacionID", programacionMateriaPrimaInfo.UsuarioModificacion.UsuarioID}
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
        /// Obtiene una programacion de materia prima
        /// </summary>
        /// <param name="pesajeMateriaPrima"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorPesajeMateriaPrima(PesajeMateriaPrimaInfo pesajeMateriaPrima)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProgramacionMateriaPrimaID", pesajeMateriaPrima.ProgramacionMateriaPrimaID}
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
        /// Genera los parametros necesarios para la ejecucion
        /// del procedimiento almacenado ProgramacionMateriaPrima_ActualizarAlmacenMovimiento
        /// </summary>
        /// <param name="programacionMateriaPrimaID"></param>
        /// <param name="almacenMovimientoID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarAlmacenMovimiento(int programacionMateriaPrimaID, long almacenMovimientoID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProgramacionMateriaPrimaID", programacionMateriaPrimaID},
                        {"@AlmacenMovimientoID", almacenMovimientoID},
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
        /// Obtiene los parametros necesarios para la ejecucion del
        /// procedimiento almacenado ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaTicket
        /// </summary>
        /// <param name="programacionMateriaPrimaId"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorProgramacionMateriaPrimaTicket(int programacionMateriaPrimaId, int ticket)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProgramacionMateriaPrimaID", programacionMateriaPrimaId},
                        {"@Ticket", ticket},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerPorCancelar(ProgramacionMateriaPrimaInfo programacion)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProgramacionMateriaPrimaID", programacion.ProgramacionMateriaPrimaId},
                        {"@UsuarioID", programacion.UsuarioModificacion.UsuarioID},
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
