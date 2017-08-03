namespace SIE.Services.Info.Info 
{
    public class DiferenciasIndicadoresMuestraContrato
    {
        /// <summary>
        /// Identificador del indicador
        /// </summary>
        public int IndicadorID { get; set; }

        /// <summary>
        /// Descripcion del Indicador
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Porcentaje capturado en la muetra de la entrada
        /// </summary>
        public decimal PorcentajeMuestra { get; set; }

        /// <summary>
        /// Porcentaje captura en el contrato de la entrada
        /// </summary>
        public decimal PorcentajeContrato { get; set; }
    }
}
