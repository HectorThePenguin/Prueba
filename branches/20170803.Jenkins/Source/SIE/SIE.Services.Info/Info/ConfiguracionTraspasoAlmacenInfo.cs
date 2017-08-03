namespace SIE.Services.Info.Info
{
	public class ConfiguracionTraspasoAlmacenInfo : BitacoraInfo
	{
		/// <summary> 
		///	ConfiguracionTraspasoAlmacenID  
		/// </summary> 
		public int ConfiguracionTraspasoAlmacenID { get; set; }

		/// <summary> 
		///	TipoAlmacenOrigenID  
		/// </summary> 
		public TipoAlmacenInfo TipoAlmacenOrigen { get; set; }

		/// <summary> 
		///	TipoAlmacenDestinoID  
		/// </summary> 
		public TipoAlmacenInfo TipoAlmacenDestino { get; set; }

        /// <summary> 
        ///	Indica si el traspaso aplica entre organizaciones
        /// </summary> 
        public bool DiferenteOrganizacion { get; set; }
	}
}
