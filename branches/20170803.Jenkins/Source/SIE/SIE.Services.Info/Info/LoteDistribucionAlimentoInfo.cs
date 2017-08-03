using System;

namespace SIE.Services.Info.Info
{
	public class LoteDistribucionAlimentoInfo : BitacoraInfo
	{
		/// <summary> 
		///	LoteDistribucionAlimentoID  
		/// </summary> 
		public int LoteDistribucionAlimentoID { get; set; }

		/// <summary> 
		///	LoteID  
		/// </summary> 
		public LoteInfo Lote { get; set; }

		/// <summary> 
		///	TipoServicioID  
		/// </summary> 
		public TipoServicioInfo TipoServicio { get; set; }

		/// <summary> 
		///	EstatusDistribucionID  
		/// </summary> 
		public EstatusInfo EstatusDistribucion { get; set; }

		/// <summary> 
		///	Fecha  
		/// </summary> 
		public DateTime Fecha { get; set; }
	}
}
