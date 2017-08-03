
//--*********** Info *************
using System;

namespace SIE.Services.Info.Reportes
{
    public class AlimentacionConsumoDetalleInfo
    {
        public DateTime fecha{ get; set; }

        public int formulaId{ get; set; }

        public string formula{ get; set; }

        public int cabezas{ get; set; }

        public int kilosDia{ get; set; }

        public int servidosAcomulados{ get; set; }

        public int diasAnimal{ get; set; }

        public decimal consumoDia{ get; set; }

        public decimal promedioAcomulado{ get; set; }

        public decimal precio{ get; set; }

        public decimal importe{ get; set; }

        /// <summary>
        /// Organizacion
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Titulo
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Fecha
        /// </summary>
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Corral { get; set; }
        public string TipoGanado { get; set; }
        public string Proceso { get; set; }
    }
}
