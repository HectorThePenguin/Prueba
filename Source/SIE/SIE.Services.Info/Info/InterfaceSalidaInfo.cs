using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class InterfaceSalidaInfo : BitacoraInfo
    {
        /// <summary>
        /// Organizacion Origen de la Salida
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// Clave de la Salida
        /// </summary>
        public int SalidaID { get; set; }
        /// <summary>
        /// Organizacion Destino de la Salida
        /// </summary>
        public OrganizacionInfo OrganizacionDestino { get; set; }
        /// <summary>
        /// Fecha de Salida
        /// </summary>
        public DateTime FechaSalida { get; set; }
        /// <summary>
        /// Indica si es por Ruteo
        /// </summary>
        public bool EsRuteo { get; set; }
        /// <summary>
        /// Cantidad de Cabezas en la Salida
        /// </summary>
        public int Cabezas { get; set; }
        /// <summary>
        /// Fecha de Registro de la Salida
        /// </summary>
        public DateTime FechaRegistro { get; set; }
        /// <summary>
        /// OrganizacionID
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Arete codigo arete
        /// </summary>
        public string Arete { get; set; }

        /// <summary>
        /// Detalle de la Interface de Salida
        /// </summary>
        public List<InterfaceSalidaDetalleInfo> ListaInterfaceDetalle { get; set; }
    }
}
