namespace SIE.Services.Info.Info
{
	public class ProveedorRetencionInfo : BitacoraInfo
	{
		/// <summary> 
		///	ProveedorRetencionID  
		/// </summary> 
		public int ProveedorRetencionID { get; set; }

		/// <summary> 
		///	ProveedorID  
		/// </summary> 
		public ProveedorInfo Proveedor { get; set; }

		/// <summary> 
		///	RetencionID  
		/// </summary> 
		public RetencionInfo Retencion { get; set; }

		/// <summary> 
		///	IvaID  
		/// </summary> 
		public IvaInfo Iva { get; set; }
	}
}
