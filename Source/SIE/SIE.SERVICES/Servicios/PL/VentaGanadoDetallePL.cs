using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class VentaGanadoDetallePL
    {
        /// <summary>
        /// Obtiene los datos del ganado asignado al ticket
        /// </summary>
        /// <param name="ventaGanadoID"></param>
        /// <returns></returns>
        public List<VentaGanadoDetalleInfo> ObtenerVentaGanadoPorTicket(int ventaGanadoID)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoDetalleBL();
                List<VentaGanadoDetalleInfo> result = venta.ObtenerVentaGanadoPorTicket(ventaGanadoID);
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
        /// 
        /// </summary>
        /// <param name="venta"></param>
        /// <returns></returns>
        public int GuardarDetalle(GrupoVentaGanadoInfo venta)
        {
            try
            {
                Logger.Info();
                var ventaGanadoDetalleBL = new VentaGanadoDetalleBL();
                int result = ventaGanadoDetalleBL.GuardarDetalle(venta);
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
