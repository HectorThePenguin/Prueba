namespace SIE.Services.Info.Info
{
	public class CausaPrecioInfo : BitacoraInfo
	{
		/// <summary> 
		///	CausaPrecioID  
		/// </summary> 
		public int CausaPrecioID { get; set; }

		/// <summary> 
		///	CausaSaldiaID  
		/// </summary> 
		public CausaSalidaInfo CausaSalida { get; set; }

        /// <summary> 
        ///	info de la organizacion  
        /// </summary> 
        public OrganizacionInfo Organizacion { get; set; }

		/// <summary> 
		///	PrecioGanadoID  
		/// </summary> 
		public decimal Precio { get; set; }

        /// <summary> 
        ///	Clase para manejar los filtros en la pantalla Causa Precio
        /// </summary>
        public CausaSalidaInfo CausaSalidaFiltro { get; set; }

        /// <summary> 
        ///	Clase para manejar los filtros en la pantalla Causa Precio
        /// </summary>
        public TipoMovimientoInfo TipoMovimientoFiltro { get; set; }
	}
}
