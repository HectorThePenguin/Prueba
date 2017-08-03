using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class CabezasSobrantesPorEntradaInfo
    {
        /// <summary>
        /// Entrada Ganado
        /// </summary>
        public EntradaGanadoInfo EntradaGanado { get; set; }

        /// <summary>
        /// Folio Entrada
        /// </summary>
        public int FolioEntrada { get; set; }

        /// <summary>
        /// cantidad de cabezas cortadas
        /// </summary>
        public int CabezasCortadas { get; set; }

        /// <summary>
        /// cantidad de cabezas sobrantes cortadas
        /// </summary>
        public int CabezasSobrantes { get; set; }

        /// <summary>
        /// cantidad de cabezas sobrantes cortadas
        /// </summary>
        public int CabezasSobrantesCortadas { get; set; }

        /// <summary>
        /// Datos de la organizacion Origen
        /// </summary>
        public OrganizacionInfo OrganizacionOrigen { get; set; }

        
    }
}
