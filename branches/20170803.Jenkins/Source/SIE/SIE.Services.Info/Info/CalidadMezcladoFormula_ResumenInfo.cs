namespace SIE.Services.Info.Info
{
    public class CalidadMezcladoFormula_ResumenInfo
    {
        /// <summary>
        /// Es el campo Descripcion de la tabla TipoMuestra. El cual, describe los Tipos de Análisis diosponibles
        /// </summary>
        public string TipoAnalisis { get; set; }

        /// <summary>
        /// Es el ID del TipoAnalisis
        /// </summary>
        public int TipoMuestraID { get; set; }

        /// <summary>
        /// Se refiere al campo PesoBaseHuemda de la tabla CalidadMezcladoFactor
        /// </summary>
        public int PesoBH { get; set; }

        /// <summary>
        /// Se refiere al campo PesoBaseSeca de la tabla CalidadMezcladoFactor
        /// </summary>
        public int PesoBS { get; set; }

        /// <summary>
        /// Se refiere al campo Factor de la tabla CalidadMezcladoFactor
        /// </summary>
        public decimal Factor { get; set; }

        /// <summary>
        /// Se refiere al campo Particulas de la tabla CalidadMezcladoDetalle
        /// </summary>
        public int Particulas { get; set; }
        /// <summary>
        /// Se refiere al campo peso de la tabla "CalidadMezcladoDetalle"
        /// </summary>
        public int Peso { get; set; }
        /// <summary>
        /// Muestra el número de partículas para las Muestras inicial, media y final. Se tiene que calcular.
        /// </summary>
        public decimal ParticulasEsperadas { get; set; }
        /// <summary>
        /// Indica el promedio de partículas encontradas para cada una de las muestra (M Inicial, M media, M final).
        /// </summary>
        public decimal PromParticulasEsperadas { get; set; }
        /// <summary>
        /// Muestra  para cada una de las muestras (M inicial, M media, M final) el % de eficiencia de recuperación. Se tiene que calcular
        /// </summary>
        public decimal PorEficiencia { get; set; }
    }
}
