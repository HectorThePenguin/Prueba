
using System;

namespace SIE.Services.Info.Info
{
    public class ReporteSolicitudPaseProcesoEncabezadoInfo
    {
        /// <summary>
        /// Titulo del reporte
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Organizacion del informe
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Fecha 
        /// </summary>
        public DateTime Fecha { get; set; }
        

        /// <summary>
        /// Fecha con formato
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
