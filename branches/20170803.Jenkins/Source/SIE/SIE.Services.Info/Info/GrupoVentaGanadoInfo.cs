using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SIE.Services.Info.Enums;    

namespace SIE.Services.Info.Info
{
    /// <summary>
    /// Contiene toda la informacion a registrar sobre la salida de ganado individual
    /// </summary>
    public class GrupoVentaGanadoInfo
    {
        /// <summary>
        /// Codigo del Corral
        /// </summary>
        public String CodigoCorral { get; set; }

        /// <summary>
        /// Id de la organizacion
        /// </summary>
        public int OrganizacionId { get; set; }

        /// <summary>
        /// Id del Usuario que genera la venta
        /// </summary>
        public int UsuarioID { get; set; }

        /// <summary>
        /// Id de la Tarifa
        /// </summary>
        public int CausaPrecioID { get; set; }

        /// <summary>
        /// Tota de cabezas vendidas
        /// </summary>
        public int TotalCabezas { get; set; }

        /// <summary>
        /// Cabezero de la venta
        /// </summary>
        public VentaGanadoInfo VentaGanado { get; set; }

        /// <summary>
        /// Detalle de la venta
        /// </summary>
        public IList<VentaGanadoDetalleInfo> VentaGandadoDetalle { get; set; }

        /// <summary>
        /// Tipo de venta
        /// </summary>
        public TipoVentaEnum TipoVenta { get; set; }
    }
}
