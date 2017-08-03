
using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{

    public class ReporteMedicamentosAplicadosModel
    {
        public int Id { get; set; }
        public string TipoTratamiento { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cabezas { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }
        public string Unidad { get; set; }
        public decimal UnidadCabeza { get; set; }
        public decimal ImporteCabeza { get; set; }
        public decimal AnimalID { get; set; }
        public int Contar { get; set; }
        public ReporteEncabezadoInfo Encabezado;
        /// <summary>
        /// Titulo del reporte
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Organizacion del informe
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Fecha Inicio
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha Fin
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Fecha inicio con formato
        /// </summary>
        public string FechaInicioConFormato
        {
            get
            {
                return FechaInicio.ToString("dd/MM/yyyy");

            }

        }
        /// <summary>
        /// Fecha fin con formato
        /// </summary>
        public string FechaFinConFormato
        {
            get
            {
                return FechaFin.ToString("dd/MM/yyyy");

            }

        }

        /// <summary>
        /// Formato para mostrado del periodo en el informe
        /// </summary>
        public string FechaEntreCadenas {
            get
            {
                return string.Format("De {0} al {1}",
                    FechaInicioConFormato,
                    FechaFinConFormato);

            }
        
        }
    }
}
