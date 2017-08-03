namespace SIE.Services.Info.Info
{
    public class EntradaGanadoSobranteInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificador de la Tabla
        /// </summary>
        public int EntradaGanadoSobranteID { set; get; }

        /// <summary>
        /// Identificador de la entrada de ganado
        /// </summary>
        public EntradaGanadoInfo EntradaGanado { set; get; }

        /// <summary>
        /// Identificador del animal insertado
        /// </summary>
        public AnimalInfo Animal { set; get; }

        /// <summary>
        /// Importe
        /// </summary>
        public decimal Importe { set; get; }

        /// <summary>
        /// Flag para identificar si el animal esta costeado
        /// </summary>
        public bool Costeado { set; get; }        
    }
}
