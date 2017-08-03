using System;

namespace SIE.Services.Info.Modelos
{
    public class PolizaSacrificioModel
    {
        /// <summary>
        /// Clave del lote
        /// </summary>
        public int LoteID { get; set; }
        /// <summary>
        /// Fecha
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Importe de canal
        /// </summary>
        public decimal ImporteCanal { get; set; }
        /// <summary>
        /// Importe de piel
        /// </summary>
        public decimal ImportePiel { get; set; }
        /// <summary>
        /// Importe de viscera
        /// </summary>
        public decimal ImporteViscera { get; set; }
        /// <summary>
        /// Serie de la factura
        /// </summary>
        public string Serie { get; set; }
        /// <summary>
        /// Folio de la factura
        /// </summary>
        public string Folio { get; set; }
        /// <summary>
        /// Lote
        /// </summary>
        public string Lote { get; set; }
        /// <summary>
        /// Codigo de corral
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Clave de la organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Cantidad de canales sacrificados
        /// </summary>
        public int Canales { get; set; }
        /// <summary>
        /// Peso de las canales sacrificadas
        /// </summary>
        public decimal Peso { get; set; }
        /// <summary>
        /// Codigo del proveedor
        /// </summary>
        public string ParametroProveedor { get; set; }
        /// <summary>
        /// Identificador del detalle de la salida
        /// </summary>
        public long InterfaceSalidaTraspasoDetalleID { get; set; }
        /// <summary>
        /// Peso de la piel
        /// </summary>
        public int PesoPiel { get; set; }

        /// <summary>
        /// Indica si se genero poliza ó no
        /// </summary>
        public bool PolizaGenerada { get; set; }

        /// <summary>
        /// Corral que se sacrifico
        /// </summary>
        public string Corral { get; set; }
    }
}
