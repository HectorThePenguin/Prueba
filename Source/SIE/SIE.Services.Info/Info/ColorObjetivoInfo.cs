namespace SIE.Services.Info.Info
{
	public class ColorObjetivoInfo : BitacoraInfo
	{
		/// <summary> 
		///	ColorObjetivoID  
		/// </summary> 
		public int ColorObjetivoID { get; set; }

		/// <summary> 
		///	TipoObjetivoCalidadID  
		/// </summary> 
		public TipoObjetivoCalidadInfo TipoObjetivoCalidad { get; set; }

		/// <summary> 
		///	Descripcion  
		/// </summary> 
		public string Descripcion { get; set; }

		/// <summary> 
		///	Tendencia  
		/// </summary> 
		public string Tendencia { get; set; }

		/// <summary> 
		///	CodigoColor  
		/// </summary> 
		public string CodigoColor { get; set; }
	}
}
