
namespace SIE.Services.Info.Info
{
    public class SalidaIndividualInfo
    {
        /// <summary>
        /// Contiene el folio del ticket ingresado
        /// </summary>
        public int FolioTicket { get; set; }

        /// <summary>
        /// Contiene el peso bruto de la carga
        /// </summary>
        public decimal PesoBruto { get; set; }

        /// <summary>
        /// Contiene el Peso Por Cabeza
        /// </summary>
        public decimal Peso { get; set; }

        /// <summary>
        /// Contiene el numero de Cabezas del ticket
        /// </summary>
        public int NumeroDeCabezas { get; set; }

        /// <summary>
        /// Contiene el OrganizacionId
        /// </summary>
        public int Organizacion { get; set; }

        /// <summary>
        /// Contiene el UsuarioId
        /// </summary>
        public int Usuario { get; set; }

        /// <summary>
        /// Contiene el tipo de venta
        /// </summary>
        public Enums.TipoVentaEnum TipoVenta { get; set; }

        /// <summary>
        /// Contiene el codigo del corral
        /// </summary>
        public string Corral { get; set; }
    }
}
