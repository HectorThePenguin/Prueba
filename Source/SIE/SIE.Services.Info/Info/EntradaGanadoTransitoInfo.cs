using System.Collections.Generic;
namespace SIE.Services.Info.Info
{
	public class EntradaGanadoTransitoInfo : BitacoraInfo
	{
		/// <summary> 
		///	EntradaGanadoTransitoID  
		/// </summary> 
		public int EntradaGanadoTransitoID { get; set; }

	    /// <summary> 
	    ///	Lote
	    /// </summary> 
	    public LoteInfo Lote { get; set; }

	    /// <summary> 
		///	Cabezas  
		/// </summary> 
		public int Cabezas { get; set; }

        /// <summary>
        /// Peso
        /// </summary>
	    public int Peso { get; set; }

        /// <summary>
        /// Coleccion de EntradaGanadoTransitoDetalle
        /// </summary>
	    public List<EntradaGanadoTransitoDetalleInfo> EntradasGanadoTransitoDetalles { get; set; }

        /// <summary>
        /// Indica si es sobrante
        /// </summary>
	    public bool Sobrante { get; set; }
	}
}
