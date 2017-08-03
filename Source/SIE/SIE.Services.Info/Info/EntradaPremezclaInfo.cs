
namespace SIE.Services.Info.Info
{
    public class EntradaPremezclaInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int EntradaPremezclaID { get; set; }

        /// <summary>
        /// Movimiento de entrada
        /// </summary>
        public long AlmacenMovimientoIDEntrada { get; set; }

        /// <summary>
        /// Movimiento de salida
        /// </summary>
        public long AlmacenMovimientoIDSalida { get; set; }

    }
}

