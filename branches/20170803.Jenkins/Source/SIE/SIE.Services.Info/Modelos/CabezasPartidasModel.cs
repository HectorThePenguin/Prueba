using System;

namespace SIE.Services.Info.Modelos
{
    public class CabezasPartidasModel
    {
        /// <summary>
        /// id de la Organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Folio de la entrada de ganado
        /// </summary>
        public int FolioEntrada { get; set; }
        /// <summary>
        /// Fecha de la entrada de ganado
        /// </summary>
        public DateTime FechaEntrada { get; set; }
        /// <summary>
        /// Total de cabezas Recibidas
        /// </summary>
        public int CabezasRecibidas { get; set; }
        /// <summary>
        /// Total de Cabezas cortadas
        /// </summary>
        public int CabezasCortadas { get; set; }
        /// <summary>
        /// Toal de cabezas en el historico
        /// </summary>
        public int CabezasHistorico { get; set; }
        /// <summary>
        /// total de cabezas cortadas y en el historico
        /// </summary>
        public int CabezasTotal { get; set; }
        /// <summary>
        /// Cabezas pendientes por procesar
        /// </summary>
        public int CabezasPendientes { get; set; }
        /// <summary>
        /// Cabezas actuales del Lote
        /// </summary>
        public int CabezasLote { get; set; }
        /// <summary>
        /// id del embarque de la Entrada
        /// </summary>
        public int EmbarqueID { get; set; }
    }
}
