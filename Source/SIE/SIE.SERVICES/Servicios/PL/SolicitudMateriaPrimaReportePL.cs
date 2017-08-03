using System;
using BLToolkit.Data.Linq;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class SolicitudMateriaPrimaReportePL
    {
        /// <summary>
        /// Imprime un reporte de solicitud de materia prima
        /// </summary>
        /// <param name="pedidoInfo"></param>
        /// <param name="nombreArchivo"></param>
        public bool ImprimirPedidoMateriaPrima(PedidoInfo pedidoInfo,string nombreArchivo)
        {
            try
            {
                var solicitudMateriaPrimaReporteBl = new SolicitudMateriaPrimaReporteBL();
                return solicitudMateriaPrimaReporteBl.ImprimirPedidoMateriaPrima(pedidoInfo, nombreArchivo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }
    }
}
