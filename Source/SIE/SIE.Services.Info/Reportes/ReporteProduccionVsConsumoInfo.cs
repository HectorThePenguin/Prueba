using System;
using SIE.Services.Info.Info;


namespace SIE.Services.Info.Reportes
{
   public class ReporteProduccionVsConsumoInfo
    {
        /// <summary> 
        ///	Descripcion 
        /// </summary> 
        public string Descripcion { get; set; }

        /// <summary> 
        ///	OrganizacionReporte nombre para la organizacion en el reporte 
        /// </summary> 
        public string OrganizacionReporte { get; set; }

        /// <summary> 
        ///	Titulo del reporte 
        /// </summary> 
        public string Titulo { get; set; }

        /// <summary> 
        ///	Fecha desde para el reporte 
        /// </summary> 
        public DateTime FechaDesde { get; set; }

        /// <summary> 
        ///	Fecha hasta para el reporte 
        /// </summary> 
        public DateTime FechaHasta { get; set; }

        /// <summary> 
        ///	CantidadEnPlanta  
        /// </summary> 
        public decimal CantidadEnPlanta { get; set; }

        /// <summary> 
        ///	CantidadDelReparto  
        /// </summary> 
        public int CantidadDelReparto { get; set; }

        /// <summary> 
        ///	OrganizacionID
        /// </summary> 
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary> 
        ///	OrganizacionDescripcion
        /// </summary> 
        public string OrganizacionDescripcion { get; set; }

        /// <summary> 
        ///	Diferencial
        /// </summary> 
        public decimal Diferencial { get; set; }

        /// <summary> 
        ///	Fisico
        /// </summary> 
        public decimal Fisico { get; set; }

        /// <summary> 
        ///	Aplicar a diario Diario
        /// </summary> 
        public decimal AplicarADiario { get; set; }

        /// <summary> 
        ///	Aplicados Semana y/o Ajustes
        /// </summary> 
        public decimal AplicadosSemana { get; set; }

        /// <summary> 
        ///	Diferencial Semanal
        /// </summary> 
        public decimal DiferencialSemanal { get; set; }

        public string FechainicioConFormato
        {
            get
            {
                return FechaDesde.ToString("dd/MM/yyyy");

            }

        }

        public string FechaFinConFormato
        {
            get
            {
                return FechaHasta.ToString("dd/MM/yyyy");

            }

        }
    }


}
