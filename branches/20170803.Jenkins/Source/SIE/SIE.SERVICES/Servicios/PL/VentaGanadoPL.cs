using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Base.Infos;

namespace SIE.Services.Servicios.PL
{
    public class VentaGanadoPL
    {
        /// <summary>
        /// Obtiene el listado de los datos generales del ticket.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        public VentaGanadoInfo ObtenerVentaGanadoPorTicket(TicketInfo Ticket)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoBL();
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

        /// <summary>
        /// Obtiene el listado de los datos de la venta de ganado paginado.
        /// </summary>
        /// <returns></returns>
        public ResultadoInfo<VentaGanadoInfo> ObtenerVentaGanadoPorPagina(PaginacionInfo pagina, VentaGanadoInfo ventaGanado)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoBL();
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
        public VentaGanadoInfo ObtenerPorFolioTicket(VentaGanadoInfo ventaGanado)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoBL();
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
        public List<ContenedorVentaGanado> ObtenerPorFechaConciliacion(DateTime fechaInicial, DateTime fechaFinal, int organizacionID)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoBL();
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
