using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxVentaGanado
    {
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorFolioTicket(TicketInfo Ticket)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioTicket", Ticket.FolioTicket},
                            {"@OrganizacionID", Ticket.Organizacion},
                            {"@TipoVenta", Ticket.TipoVenta.GetHashCode()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosPorFolioTicketPoliza(int folioTicket, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioTicket", folioTicket},
                            {"@OrganizacionID", organizacionID}
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
        /// del procedimiento almacenado VentaGanado_ObtenerPorFolioTicket
        /// </summary>
        /// <param name="ventaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolioTicket(VentaGanadoInfo ventaGanado)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioTicket", ventaGanado.FolioTicket},
                            {"@OrganizacionID", ventaGanado.Lote.OrganizacionID},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina,
                                                                              VentaGanadoInfo ventaGanado)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", ventaGanado.NombreCliente},
                            {"@OrganizacionID", ventaGanado.Lote.OrganizacionID},
                            {"@Activo", ventaGanado.Activo},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite},
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
        /// ejecucion del procedimiento almacenado
        /// VentaGanado_ObtenerSalidasPolizaConciliacion
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFechaConciliacion(DateTime fechaInicial, DateTime fechaFinal, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FechaInicial", fechaInicial},
                            {"@FechaFinal", fechaFinal},
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
    }
}
