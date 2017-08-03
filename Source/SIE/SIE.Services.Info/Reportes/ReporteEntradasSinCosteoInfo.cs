using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
   public class ReporteEntradasSinCosteoInfo
    {
        /// <summary> 
        ///	Folio del reporte 
        /// </summary> 
        public int Folio { get; set; }

        /// <summary> 
        ///	Fecha del reporte 
        /// </summary> 
        public DateTime Fecha { get; set; }

        /// <summary> 
        ///	Producto  
        /// </summary> 
        public string Producto { get; set; }

        /// <summary> 
        ///	Peso  
        /// </summary> 
        public int Peso { get; set; }

        /// <summary> 
        ///	Observaciones 
        /// </summary> 
        public string Observaciones { get; set; }

        /// <summary> 
        ///	Almacen destino 
        /// </summary> 
        public string AlmacenDestino { get; set; }

        /// <summary> 
        ///	LoteDestino 
        /// </summary> 
        public int LoteDestino { get; set; }

        /// <summary> 
        ///	Proveedor 
        /// </summary> 
        public string Proveedor { get; set; }

        /// <summary> 
        ///	Organizacion 
        /// </summary> 
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary> 
        ///	Organizacion para el reporte 
        /// </summary> 
        public string OrganizacionDescripcion { get; set; }

        /// <summary> 
        ///	Organizacion para el reporte 
        /// </summary> 
        public string OrganizacionReporte { get; set; }

        /// <summary> 
        ///	Titulo del reporte 
        /// </summary> 
        public string Titulo { get; set; }

        /// <summary> 
        ///	fecha desde para el reporte 
        /// </summary> 
        public DateTime FechaDesde { get; set; }

        /// <summary> 
        ///	Fecha hasta para el reporte 
        /// </summary> 
        public DateTime FechaHasta { get; set; }

        /// <summary> 
        ///	Metodo para el formato de la fecha Desde
        /// </summary> 
        public string FechainicioConFormato
        {
            get
            {
                return FechaDesde.ToString("dd/MM/yyyy");

            }

        }

        /// <summary> 
        ///	Metodo para el formato de la fecha Desde
        /// </summary> 
        public string FechaConFormato
        {
            get
            {
                return Fecha.ToString("dd/MM/yyyy");

            }

        }




        /// <summary> 
        ///	Metodo para el formato de la fecha Hasta
        /// </summary> 
        public string FechaFinConFormato
        {
            get
            {
                return FechaHasta.ToString("dd/MM/yyyy");

            }

        }
    }
}
