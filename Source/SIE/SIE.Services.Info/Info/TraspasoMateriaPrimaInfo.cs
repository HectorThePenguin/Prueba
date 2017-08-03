using System;

namespace SIE.Services.Info.Info
{
	public class TraspasoMateriaPrimaInfo : BitacoraInfo
	{
		/// <summary> 
		///	TraspasoMateriaPrimaID  
		/// </summary> 
		public int TraspasoMateriaPrimaID { get; set; }

        /// <summary> 
        ///	ContratoID  
        /// </summary> 
        public OrganizacionInfo Organizacion { get; set; }

		/// <summary> 
		///	ContratoID  
		/// </summary> 
		public ContratoInfo ContratoOrigen { get; set; }

        /// <summary> 
        ///	ContratoID  
        /// </summary> 
        public ContratoInfo ContratoDestino { get; set; }

		/// <summary> 
		///	FolioTraspaso  
		/// </summary> 
		public long FolioTraspaso { get; set; }

		/// <summary> 
		///	Almacen Origen  
		/// </summary> 
		public AlmacenInfo AlmacenOrigen { get; set; }

        /// <summary> 
        ///	Almacen Destino
        /// </summary> 
        public AlmacenInfo AlmacenDestino { get; set; }

		/// <summary> 
		///	InventarioLoteOrigenID  
		/// </summary> 
		public AlmacenInventarioLoteInfo AlmacenInventarioLoteOrigen { get; set; }

		/// <summary> 
		///	InventarioLoteDestinoID  
		/// </summary> 
		public AlmacenInventarioLoteInfo AlmacenInventarioLoteDestino { get; set; }

		/// <summary> 
		///	CuentaSAPID  
		/// </summary> 
		public CuentaSAPInfo CuentaSAP { get; set; }

		/// <summary> 
		///	Justificacion  
		/// </summary> 
		public string Justificacion { get; set; }

		/// <summary> 
		///	AlmacenMovimientoEntradaID  
		/// </summary> 
		public AlmacenMovimientoInfo AlmacenMovimientoOrigen { get; set; }

		/// <summary> 
		///	AlmacenMovimientoSalidaID  
		/// </summary> 
		public AlmacenMovimientoInfo AlmacenMovimientoDestino { get; set; }

        public DateTime FechaMovimiento { get; set; }

	}
}
