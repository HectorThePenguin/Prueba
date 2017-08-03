using System;

namespace SIE.Services.Info.Reportes
{
    public class ReportePaseaProcesoInfo
    {
        /// <summary>
        /// Ticket
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// FechaSurtido
        /// </summary>
        public DateTime FechaSurtido { get; set; }

        public string FechaSurtidoConFormato
        {
            get
            {
                return FechaSurtido.ToString("dd/MM/yyyy");

            }

        }

        /// <summary>
        /// PesoNeto
        /// </summary>
        public int PesoNeto { get; set; }

        /// <summary>
        /// ProductoId
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Titulo usado para el informe
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Organizacion del informe
        /// </summary>
        public string Organizacion { get; set; }

        /// <summary>
        /// FechaInicial para filtro de informe
        /// </summary>
        public DateTime Fecha { get; set; }
        
        /// <summary>
        /// Fecha inicio con formato
        /// </summary>
        public string FechaConFormato
        {
            get
            {
                return Fecha.ToString("dd/MM/yyyy");

            }

        }
        
    }
}
