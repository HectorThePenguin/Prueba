using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class DatosOrigen
    {
        /// <summary>
        /// Folio origen
        /// </summary>
        public int folioOrigen { get; set; }
        /// <summary>
        /// Fecha origen
        /// </summary>
        public DateTime fechaOrigen { get; set; }
        /// <summary>
        /// Humedad origen
        /// </summary>
        public decimal humedadOrigen { get; set; }
        /// <summary>
        /// fecha formateada
        /// </summary>
        public string fechaFormateada { get; set; }
        /// <summary>
        /// promedio muestras
        /// </summary>
        public decimal promedioMuestras { get; set; }
    }
}


