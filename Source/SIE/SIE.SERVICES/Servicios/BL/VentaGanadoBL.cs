using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class VentaGanadoBL
    {
        /// <summary>
        /// Obtiene el listado de los datos generales del ticket.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal VentaGanadoInfo ObtenerVentaGanadoPorTicket(TicketInfo Ticket)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoDAL();
                VentaGanadoInfo result = venta.ObtenerVentaGanadoPorTicket(Ticket);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal List<ContenedorVentaGanado> ObtenerVentaGanadoPorTicketPoliza(int folioTicket, int organizacionID)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoDAL();
                List<ContenedorVentaGanado> result = venta.ObtenerVentaGanadoPorTicketPoliza(folioTicket, organizacionID);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los costos de la venta de ganado intensivo
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        internal List<ContenedorVentaGanado> ObtenerVentaGanadoIntensivoPorTicketPoliza(TicketInfo ticket)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoDAL();
                List<ContenedorVentaGanado> result = venta.ObtenerVentaGanadoIntensivoPorTicketPoliza(ticket);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene el listado de los datos de la venta de ganado paginado.
        /// </summary>
        /// <returns></returns>
        internal ResultadoInfo<VentaGanadoInfo> ObtenerVentaGanadoPorPagina(PaginacionInfo pagina, VentaGanadoInfo ventaGanado)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoDAL();
                ResultadoInfo<VentaGanadoInfo> result = venta.ObtenerVentaGanadoPorPagina(pagina, ventaGanado);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la venta de ganado.
        /// </summary>
        /// <returns></returns>
        internal VentaGanadoInfo ObtenerPorFolioTicket(VentaGanadoInfo ventaGanado)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoDAL();
                VentaGanadoInfo result = venta.ObtenerPorFolioTicket(ventaGanado);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la venta de ganado.
        /// </summary>
        /// <returns></returns>
        internal List<ContenedorVentaGanado> ObtenerPorFechaConciliacion(DateTime fechaInicial, DateTime fechaFinal, int organizacionID)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoDAL();
                List<ContenedorVentaGanado> result = venta.ObtenerPorFechaConciliacion(fechaInicial, fechaFinal, organizacionID);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
