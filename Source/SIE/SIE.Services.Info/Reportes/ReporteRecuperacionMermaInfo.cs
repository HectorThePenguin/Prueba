namespace SIE.Services.Info.Reportes
{
    public class ReporteRecuperacionMermaInfo
    {
        /// <summary>
        /// Fecha de Entrada del Ganado
        /// </summary>
        public string FechaEntrada { get; set; }
        /// <summary>
        /// Numero de Partida
        /// </summary>
        public long FolioEntrada { get; set; }
        /// <summary>
        /// Cantidad de Cabezas Origen
        /// </summary>
        public int CabezasOrigen { get; set; }
        /// <summary>
        /// Cantidad de Cabezas en Produccion
        /// </summary>
        public int CabezasProduccion { get; set; }
        /// <summary>
        /// Cantidad de Cabezas en Enfermeria
        /// </summary>
        public int CabezasEnfermeria { get; set; }
        /// <summary>
        /// Cantidad de Cabezas en Venta
        /// </summary>
        public int CabezasVenta { get; set; }
        /// <summary>
        /// Cantidad de Cabezas Muertas
        /// </summary>
        public int CabezasMuertas { get; set; }
        /// <summary>
        /// Peso Origen del Ganado
        /// </summary>
        public decimal PesoOrigen { get; set; }
        /// <summary>
        /// Peso de Llegada del Ganado
        /// </summary>
        public decimal PesoLlegada { get; set; }
        /// <summary>
        /// Peso al Corte del Ganado
        /// </summary>
        public decimal PesoCorte { get; set; }
        /// <summary>
        /// Merma de Transito
        /// </summary>
        public decimal MermaTransito { get; set; }
        /// <summary>
        /// Recuperacion de Merma
        /// </summary>
        public decimal RecuperacionMerma { get; set; }

        public string Titulo { get; set; }

        public string RangoFechas { get; set; }

        public string Organizacon { get; set; }
    }
}
