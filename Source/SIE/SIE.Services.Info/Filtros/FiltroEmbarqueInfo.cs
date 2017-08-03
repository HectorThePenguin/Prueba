using System;

namespace SIE.Services.Info.Filtros
{
    public class FiltroEmbarqueInfo
    {
        /// <summary>
        /// Identificador del registro de Embarque
        /// </summary>
        public int EmbarqueID { get; set; }

        /// <summary>
        /// Organizacion del embarque 
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Folio de embarque 
        /// </summary>
        public int FolioEmbarque { get; set; }

        /// <summary>
        /// Organizacion Origen
        /// </summary>
        public int OrganizacionOrigenID { get; set; }

        /// <summary>
        /// Organizacion Destino
        /// </summary>
        public int OrganizacionDestinoID { get; set; }

        /// <summary>
        /// Tipo de Movimiento Origen
        /// </summary>
        public int TipoOrganizacionOrigenID { get; set; }

        /// <summary>
        /// Fecha de salida el embarque
        /// </summary>
        public DateTime? FechaSalida { get; set; }

        /// <summary>
        /// Fecha de llegada del embarque
        /// </summary>
        public DateTime? FechaLlegada { get; set; }

        /// <summary>
        /// Estatus del registro
        /// </summary>
        public int Estatus { get; set; }
    }
}