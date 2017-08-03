
namespace SIE.Services.Info.Info
{
    public class TicketInfo
    {
        /// <summary>
        /// Contiene la OrganizacionId
        /// </summary>
        public int Organizacion { get; set; }

        /// <summary>
        /// Contiene el peso de la tara
        /// </summary>
        public decimal PesoTara { get; set; }

        /// <summary>
        /// Contiene el Numero Del Cliente
        /// </summary>
        public string Cliente { get; set; }

        /// <summary>
        /// Contiene el tipo de folio a generar
        /// </summary>
        public int TipoFolio { get; set; }

        /// <summary>
        /// Contiene el IdUsuario
        /// </summary>
        public int Usuario { get; set; }

        /// <summary>
        /// Contiene el tipo de venta
        /// </summary>
        public Enums.TipoVentaEnum TipoVenta { get; set; }

        /// <summary>
        /// Contiene el número de ticket
        /// </summary>
        public int FolioTicket { get; set; }
    }
}
