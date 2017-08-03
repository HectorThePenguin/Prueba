using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ConfiguracionEmbarqueDetalleInfo : BitacoraInfo
    {
        /// <summary>
        /// Campo que almacena el id del detalle de la configuración del embarque.
        /// </summary>
        public int ConfiguracionEmbarqueDetalleID { get; set; }

        /// <summary>
        /// Campo que almacena el id de la configuración del embarque
        /// </summary>
        public int? ConfiguracionEmbarqueID { get; set; }

        /// <summary>
        /// Indice del detalle.
        /// </summary>
        public int Indice { get; set; }

        /// <summary>
        /// Descripción del detalle
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Kilometros que recorrerá el embarque de acuerdo a la configuración.
        /// </summary>
        public decimal? Kilometros { get; set; }

        /// <summary>
        /// Horas que durarña en viaje el embarque de acuerdo a la configuración seleccionada.
        /// </summary>
        public TimeSpan Horas { get; set; }

        /// <summary>
        /// Fecha de creación del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de modificación del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
    }
}
