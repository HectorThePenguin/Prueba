namespace SIE.Services.Info.Filtros
{
    public class FiltroTicketProduccionMolino
    {
        /// <summary>
        /// Identificador de la tabla Pesaje Materia Prima
        /// </summary>
        public int PesajeMateriaPrimaID { get; set; }
        /// <summary>
        /// Identificador de los Kilos netos
        /// </summary>
        public int KilosNetos { get; set; }

        /// <summary>
        /// Identificador del Conteo de Pacas
        /// </summary>
        public int ConteoPacas { get; set; }

        /// <summary>
        /// Identificador de la Hora Inicial del Ticket
        /// </summary>
        public string HoraTicketInicial { get; set; }

        /// <summary>
        /// Numero de Lote donde se encuentra el Producto
        /// </summary>
        public int Lote { get; set; }

        /// <summary>
        /// Humedad del Forraje en la Calidad de Materia Prima
        /// </summary>
        public decimal HumedadForraje { get; set; }

        /// <summary>
        /// Identificador del Producto
        /// </summary>
        public int ProductoID { get; set; }

        /// <summary>
        /// Descripcion del Producto
        /// </summary>
        public string Descripcion { get; set; }

    }
}
