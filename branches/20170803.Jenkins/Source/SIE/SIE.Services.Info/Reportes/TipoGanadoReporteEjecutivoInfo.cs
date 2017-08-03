using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Reportes
{
    public class TipoGanadoReporteEjecutivoInfo
    {
        /// <summary>
        /// Clave del Tipo de Ganado
        /// </summary>
        public int TipoGanadoId { get; set; }
        /// <summary>
        /// Descripcion del Tipo de Ganado
        /// </summary>
        public string TipoGanado { get; set; }
        /// <summary>
        /// Sexo del Ganado
        /// </summary>
        public Sexo Sexo { get; set; }
        /// <summary>
        /// Cantidad de Cabezas
        /// </summary>
        public int Cabezas { get; set; }
        /// <summary>
        /// Peso de Origen
        /// </summary>
        public decimal PesoOrigen { get; set; }
        /// <summary>
        /// Peso de Llegada
        /// </summary>
        public decimal PesoLlegada { get; set; }
        /// <summary>
        /// Peso Bruto
        /// </summary>
        public decimal PesoBruto { get; set; }
        /// <summary>
        /// Peso Tara
        /// </summary>
        public decimal PesoTara { get; set; }
        /// <summary>
        /// Precio por Kilo
        /// </summary>
        public decimal PrecioKilo { get; set; }
        /// <summary>
        /// Clave de la Entrada de Ganado Costeo
        /// </summary>
        public int EntradaGanadoCosteoId { get; set; }
        /// <summary>
        /// Merma para la entrada
        /// </summary>
        public decimal Merma { get; set; }
        /// <summary>
        /// Costo integrado
        /// </summary>
        public decimal CostoIntegrado { get; set; }

        public decimal KilosLlegadaPorCabeza { get; set; }

        public decimal KiliLlegadaEntrada { get; set; }

    }
}
