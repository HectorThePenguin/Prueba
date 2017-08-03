namespace SIE.Services.Info.Info
{
	public class IndicadorProductoCalidadInfo : BitacoraInfo
	{
		/// <summary> 
		///	IndicadorProductoCalidadID  
		/// </summary> 
		public int IndicadorProductoCalidadID { get; set; }

		/// <summary> 
		///	IndicadorID  
		/// </summary> 
		public IndicadorInfo Indicador { get; set; }

		/// <summary> 
		///	ProductoID  
		/// </summary> 
		public ProductoInfo Producto { get; set; }
	}
}
