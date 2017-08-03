namespace SIE.Services.Info.Info
{
	public class InterfaceSalidaTraspasoCostoInfo : BitacoraInfo
	{
		/// <summary> 
		///	InterfaceSalidaTraspasoCostoID  
		/// </summary> 
		public int InterfaceSalidaTraspasoCostoID { get; set; }

		/// <summary> 
		///	InterfaceSalidaTraspasoDetalleID  
		/// </summary> 
		public InterfaceSalidaTraspasoDetalleInfo InterfaceSalidaTraspasoDetalle { get; set; }

		/// <summary> 
		///	AnimalID  
		/// </summary> 
		public long AnimalID { get; set; }

		/// <summary> 
		///	CostoID  
		/// </summary> 
		public CostoInfo Costo { get; set; }

		/// <summary> 
		///	Importe  
		/// </summary> 
		public decimal Importe { get; set; }

        /// <summary>
        /// Indica si el animal ya ha sido facturado
        /// </summary>
	    public bool Facturado { get; set; }
	}
}
