using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaGanadoFolio
    {
        /// <summary>
        /// Identificador de la entrada de
        /// ganado
        /// </summary>
        public int EntradaGanadoID { get; set; }
        /// <summary>
        /// Fecha de entrada del ganado
        /// </summary>
        public DateTime FechaEntrada { get; set; }
        /// <summary>
        /// Folio de entrada del ganado
        /// </summary>
        public int FolioEntrada { get; set; }
        /// <summary>
        /// Organizacion origen del animal
        /// </summary>
        public int OrganizacionOrigenID { get; set; }
        /// <summary>
        /// Tipo de organizacion
        /// </summary>
        public int TipoOrganizacionID { get; set; }
        /// <summary>
        /// Descripcion de la organizacion
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Proveedor al que se le compro el animal
        /// </summary>
        public string Proveedor { get; set; }

        /// <summary>
        /// Identificador del animal
        /// </summary>
        public int AnimalID { get; set; }
    }
}
