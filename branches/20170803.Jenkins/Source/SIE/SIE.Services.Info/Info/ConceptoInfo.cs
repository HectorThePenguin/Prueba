namespace SIE.Services.Info.Info
{
    public class ConceptoInfo
    {
        /// <summary>
        /// ID del concepto
        /// </summary>
        public int ConceptoID { get; set; }

        /// <summary>
        /// Descripción del Concepto
        /// </summary>
        public string ConceptoDescripcion { get; set; }

        /// <summary>
        /// Indica si el Concepto es Bueno
        /// </summary>
        public bool Bueno { get; set; }

        /// <summary>
        /// Indica si el Concepto es Malo
        /// </summary>
        public bool Malo { get; set; }

        /// <summary>
        /// Indica si la leyenda del Radio button mostrará Si o No
        /// </summary>
        public bool AplicaSiNo { get; set; }

        /// <summary>
        /// Observación del Concepto
        /// </summary>
        public string Observaciones { get; set; }
    }
}
