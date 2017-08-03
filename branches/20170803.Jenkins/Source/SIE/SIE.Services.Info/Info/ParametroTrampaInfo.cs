namespace SIE.Services.Info.Info
{
	public class ParametroTrampaInfo : BitacoraInfo
	{
		/// <summary> 
		///	ParametroTrampaID  
		/// </summary> 
		public int ParametroTrampaID { get; set; }

		/// <summary> 
		///	ParametroID  
		/// </summary> 
		public ParametroInfo Parametro { get; set; }

		/// <summary> 
		///	TrampaID  
		/// </summary> 
		public TrampaInfo Trampa { get; set; }

		/// <summary> 
		///	Valor  
		/// </summary> 
		public string Valor { get; set; }
	}
}
