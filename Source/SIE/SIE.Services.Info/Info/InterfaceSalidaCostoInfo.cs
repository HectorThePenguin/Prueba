using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class InterfaceSalidaCostoInfo
    {
        /// <summary>
        /// Organizacion de la Salida
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Clave de la Salida
        /// </summary>
        public int SalidaID { get; set; }

        /// <summary>
        /// Arete que indentifica el Animal
        /// </summary>
        public String Arete { get; set; }

        /// <summary>
        /// Fecha de Compra
        /// </summary>
        public DateTime FechaCompra { get; set; }

        /// <summary>
        /// Costo
        /// </summary>
        public CostoInfo Costo { get; set; }

        /// <summary>
        /// Importe del Costo
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Fecha de Registro
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Usuario de Registro
        /// </summary>
        public string UsuarioRegistro { get; set; }

        /// <summary>
        /// Número de Documento
        /// </summary>
        public string NumeroDocumento { get; set; }
    }
}
