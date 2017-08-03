namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaGanadoAnimal
    {
        /// <summary>
        /// Identificador del animal
        /// </summary>
        public long AnimalID { get; set; }
        /// <summary>
        /// Arete del animal
        /// </summary>
        public string Arete { get; set; }
        /// <summary>
        /// Folio de entrada del animal
        /// </summary>
        public long FolioEntrada { get; set; }
        /// <summary>
        /// Organizacion de la cual llegó el animal
        /// </summary>
        public int OrganizacionIDEntrada { get; set; }
        /// <summary>
        /// Descripción del tipo de ganado
        /// </summary>
        public string TipoGanado { get; set; }
        /// <summary>
        /// Identificador del tipo de ganado
        /// </summary>
        public int TipoGanadoID { get; set; }
        /// <summary>
        /// Sexo del animal
        /// </summary>
        public string Sexo { get; set; }
        /// <summary>
        /// Causa por la cual se le dio salida
        /// </summary>
        public string CausaSalida { get; set; }

        /// <summary>
        /// Peso del animal al momento de la venta
        /// </summary>
        public int Peso { get; set; }

        /// <summary>
        /// Folio del Ticket de la venta
        /// </summary>
        public int FolioTicket { get; set; }
    }
}
