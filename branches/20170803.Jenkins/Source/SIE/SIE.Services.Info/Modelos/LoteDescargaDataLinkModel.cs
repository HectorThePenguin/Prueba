using System;

namespace SIE.Services.Info.Modelos
{
    public class LoteDescargaDataLinkModel
    {
        /// <summary>
        /// Peso de Inicio del Lote
        /// </summary>
        public int PesoInicio { get; set; }
        /// <summary>
        /// Id del Lote
        /// </summary>
        public int LoteID { get; set; }
        /// <summary>
        /// Fecha de Inicio del Lote
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Cabezas del Lote
        /// </summary>
        public int Cabezas { get; set; }
    }
}
