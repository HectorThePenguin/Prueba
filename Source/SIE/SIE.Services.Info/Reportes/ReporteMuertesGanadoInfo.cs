using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
	public class ReporteMuertesGanadoInfo
	{
	    public ReporteMuertesGanadoInfo()
	    {
	        Enfermeria = string.Empty;
	    }

        /// <summary> 
        ///	AnimalID  
        /// </summary> 
        [Atributos.AtributoIgnorarColumnaExcel]
        public long AnimalID { get; set; }
        
        /// <summary> 
		///	Enfermeria  
		/// </summary> 
		public string Enfermeria { get; set; }

		/// <summary> 
		///	CorralID  
		/// </summary> 
        [Atributos.AtributoIgnorarColumnaExcel]
		public int CorralID { get; set; }

		/// <summary> 
		///	Codigo  
		/// </summary> 
		public string Codigo { get; set; }

		/// <summary> 
		///	TipoGanado  
		/// </summary> 
		public string TipoGanado { get; set; }

		/// <summary> 
		///	FechaLlegada  
		/// </summary> 
		public DateTime FechaLlegada { get; set; }

		/// <summary> 
		///	Arete  
		/// </summary> 
		public string Arete { get; set; }

		/// <summary> 
		///	Origen  
		/// </summary> 
		public string Origen { get; set; }

		/// <summary> 
		///	Partida  
		/// </summary> 
		public int Partida { get; set; }

		/// <summary> 
		///	Sexo  
		/// </summary> 
		public string Sexo { get; set; }

		/// <summary> 
		///	Peso  
		/// </summary> 
		public int Peso { get; set; }

		/// <summary> 
		///	DiasEngorda  
		/// </summary> 
		public int DiasEngorda { get; set; }

		/// <summary> 
		///	Causa  
		/// </summary> 
		public string Causa { get; set; }

		/// <summary> 
		///	Detector  
		/// </summary> 
		public string Detector { get; set; }

		/// <summary> 
		///	FechaTratamiento1  
		/// </summary> 
		public DateTime FechaTratamiento1 { get; set; }

        /// <summary>
        /// Fecha de tratamiento 1 formateada
        /// </summary>
	    public string FechaTratamiento1ConFormato {
	        get { return FechaTratamiento1.Year != 1900 ? FechaTratamiento1.ToString("dd/MM/yyy") : string.Empty; }
	    }

	    /// <summary> 
		///	MedicamentoAplicado1  
		/// </summary> 
		public string MedicamentoAplicado1 { get; set; }

		/// <summary> 
		///	FechaTratamiento2  
		/// </summary> 
		public DateTime FechaTratamiento2 { get; set; }

        /// <summary>
        /// Fecha de tratamiento 2 formateada
        /// </summary>
        public string FechaTratamiento2ConFormato
        {
            get { return FechaTratamiento2.Year != 1900 ? FechaTratamiento2.ToString("dd/MM/yyy") : string.Empty; }
        }
		/// <summary> 
		///	MedicamentoAplicado2  
		/// </summary> 
		public string MedicamentoAplicado2 { get; set; }

		/// <summary> 
		///	FechaTratamiento3  
		/// </summary> 
		public DateTime FechaTratamiento3 { get; set; }
        /// <summary>
        /// Fecha de tratamiento 2 formateada
        /// </summary>
        public string FechaTratamiento3ConFormato
        {
            get { return FechaTratamiento3.Year != 1900 ? FechaTratamiento3.ToString("dd/MM/yyy") : string.Empty; }
        }

		/// <summary> 
		///	MedicamentoAplicado3  
		/// </summary> 
		public string MedicamentoAplicado3 { get; set; }

		/// <summary> 
		///	TipoDeteccion  
		/// </summary> 
		public string TipoDeteccion { get; set; }

		/// <summary> 
		///	Tabla  
		/// </summary> 
        [Atributos.AtributoIgnorarColumnaExcel]
        public string Tabla { get; set; }

        /// <summary>
        /// Datos usados en el reporte
        /// </summary>
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
        /// Cadena con formato para mostrado en el reporte de entre fechas
        /// </summary>
        public string CadenaEntreFechas
        {
            get { return "Del " + FechaInicioConFormato + " al " + FechaFinConFormato; }
        }

        /// <summary>
        /// Temperatura del animal antes de morir
        /// </summary>
	    public decimal Temperatura { get; set; }

        /// <summary>
        /// Grado de la deteccion
        /// </summary>
	    public int Grado { get; set; }
    }
}
