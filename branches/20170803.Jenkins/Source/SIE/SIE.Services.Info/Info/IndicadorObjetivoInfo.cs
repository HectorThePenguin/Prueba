namespace SIE.Services.Info.Info
{
	public class IndicadorObjetivoInfo : BitacoraInfo
	{
		/// <summary> 
		///	IndicadorObjetivoID  
		/// </summary> 
		public int IndicadorObjetivoID { get; set; }

        /// <summary> 
        ///	ObjetivoMinimo  
        /// </summary> 
        public OrganizacionInfo Organizacion { get; set; }

		/// <summary> 
		///	IndicadorID  
		/// </summary> 
		public IndicadorInfo Indicador { get; set; }

        /// <summary> 
        ///	IndicadorID  
        /// </summary> 
        public IndicadorProductoCalidadInfo IndicadorProductoCalidad { get; set; }

		/// <summary> 
		///	TipoObjetivoCalidadID  
		/// </summary> 
		public TipoObjetivoCalidadInfo TipoObjetivoCalidad { get; set; }

		/// <summary> 
		///	ObjetivoMinimo  
		/// </summary> 
		public decimal ObjetivoMinimo { get; set; }

		/// <summary> 
		///	ObjetivoMaximo  
		/// </summary> 
		public decimal ObjetivoMaximo { get; set; }

		/// <summary> 
		///	Tolerancia  
		/// </summary> 
		public decimal Tolerancia { get; set; }

		/// <summary> 
		///	Medicion  
		/// </summary> 
		public string Medicion { get; set; }

        /// <summary> 
        ///	Producto para los filtros
        /// </summary> 
        public ProductoInfo Producto { get; set; }
	}
}
