
using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteCronicosRecuperacionFolio
    {
        /// <summary>
        /// Folio ó Partida de animal
        /// </summary>
        public long FolioEntrada { get; set; }
        /// <summary>
        /// Identificador de la Organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Descripcion de la Organizacion
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Dias que paso en engorda
        /// </summary>
        public int DiasEngorda { get; set; }
        /// <summary>
        /// Identificador del animal
        /// </summary>
        public long AnimalID { get; set; }
        /// <summary>
        /// Fecha de entrada del Animal
        /// </summary>
        public DateTime FechaLlegada { get; set; }
    }
}
