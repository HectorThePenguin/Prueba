using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class EntradaGanadoCalidadInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int EntradaGanadoCalidadID { get; set; }

        /// <summary>
        /// Identificador la Entrada de Ganado
        /// </summary>
        public int EntradaGanadoID { get; set; }

        /// <summary>
        /// Sexo del Tipo de Animal
        /// </summary>
        public CalidadGanadoInfo CalidadGanado { get; set; }

        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int Valor { get; set; }
    }
}
    