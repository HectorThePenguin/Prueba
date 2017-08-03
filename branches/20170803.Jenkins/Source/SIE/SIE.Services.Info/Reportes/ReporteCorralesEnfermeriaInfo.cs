using System;

namespace SIE.Services.Info.Reportes
{
	public class ReporteCorralesEnfermeriaInfo
	{
		/// <summary> 
		///	CorralEnfermeria  
		/// </summary> 
		public string CorralEnfermeria { get; set; }

		/// <summary> 
		///	Arete  
		/// </summary> 
		public string Arete { get; set; }

		/// <summary> 
		///	FechaEnfermeria  
		/// </summary> 
		public string FechaEnfermeria { get; set; }

		/// <summary> 
		///	PesoOrigen  
		/// </summary> 
		public int PesoOrigen { get; set; }

		/// <summary> 
		///	TipoGanado  
		/// </summary> 
		public string TipoGanado { get; set; }

		/// <summary> 
		///	Problema  
		/// </summary> 
		public string Problema { get; set; }

		/// <summary> 
		///	CorralOrigen  
		/// </summary> 
		public string CorralOrigen { get; set; }

		/// <summary> 
		///	DiasEngorda  
		/// </summary> 
		public int DiasEngorda { get; set; }

		/// <summary> 
		///	DiasEnfermeria  
		/// </summary> 
		public int DiasEnfermeria { get; set; }

		/// <summary> 
		///	Partida  
		/// </summary> 
		public int Partida { get; set; }

		/// <summary> 
		///	FechaLlegada  
		/// </summary> 
		public string FechaLlegada { get; set; }

		/// <summary> 
		///	Origen  
		/// </summary> 
		public string Origen { get; set; }

        /// <summary> 
        ///	Corralid  
        /// </summary> 
        [SIE.Services.Info.Atributos.AtributoIgnorarColumnaExcel]
        public int CorralID { get; set; }

        /// <summary> 
        ///	Origen  
        /// </summary> 
        [SIE.Services.Info.Atributos.AtributoIgnorarColumnaExcel]
        public bool EsTotal { get; set; }

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
        public DateTime Fecha { get; set; }

	    public string EncabezadoFecha {
	        get { return string.Format("{0}", Fecha.ToString("dd/MM/yyyy")); }
	    }
	}
}
