namespace SIE.Services.Info.Modelos
{
    public class ImpresionCalidadMezcladoDetalleModel
    {
        /// <summary>
        /// id de la calidad de mezclado
        /// </summary>
        public int CalidadMezcladoID { get; set; }
        /// <summary>
        /// Indica el tipo de muestra
        /// </summary>
        public string TipoMuestra { get; set; }
        /// <summary>
        /// Particulas esperadas
        /// </summary>
        public double ParticulasEsperadas { get; set; }
        /// <summary>
        /// Peso de humedad
        /// </summary>
        public int PesoBaseHumeda { get; set; }
        /// <summary>
        /// Peso seco
        /// </summary>
        public int PesoBaseSeca { get; set; }
        /// <summary>
        /// numero de la muestra
        /// </summary>
        public string NumeroMuestra { get; set; }
        /// <summary>
        /// Peso de la muestra
        /// </summary>
        public int Peso { get; set; }
        /// <summary>
        /// particulas encontradas en la muestra
        /// </summary>
        public double Particulas { get; set; }
    }
}
