namespace SIE.Services.Info.Reportes
{
    public class CostosReporteEjecutivoInfo
    {
        /// <summary>
        /// Clave del Costo
        /// </summary>
        public int CostoId { get; set; }
        /// <summary>
        /// Descripcion del Costo
        /// </summary>
        public string Costo { get; set; }
        /// <summary>
        /// Importe del Costo
        /// </summary>
        public decimal Importe { get; set; }
        /// <summary>
        /// Clave de la Entrada de Ganado Costeo
        /// </summary>
        public int EntradaGanadoCosteoId { get; set; }
    }
}
