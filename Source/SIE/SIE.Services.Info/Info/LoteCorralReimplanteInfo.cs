using System;

namespace SIE.Services.Info.Info
{
    public class LoteCorralReimplanteInfo : BitacoraInfo
    {
        /// <summary>
        /// Informacion del lote
        /// </summary>
        public LoteInfo Lote { get; set; }
        /// <summary>
        /// Informacion del corral
        /// </summary>
        public CorralInfo Corral { get; set; }
        /// <summary>
        /// Peso origen del corral
        /// </summary>
        public int PesoOrigen { get; set; }
        /// <summary>
        /// Peso reimplante del corral
        /// </summary>
        public int PesoReimplante { get; set; }
        /// <summary>
        /// Total de cabezas reimplantadas del corral
        /// </summary>
        public int TotalCabezas { get; set; }
    }
}
