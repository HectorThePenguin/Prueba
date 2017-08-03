using System.Collections.Generic;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Info.Info
{
	public class CorralDetectorInfo : BitacoraInfo
	{
		/// <summary> 
		///	CorralDetectorID  
		/// </summary> 
		public int CorralDetectorID { get; set; }

		/// <summary> 
		///	OperadorID  
		/// </summary> 
		public OperadorInfo Operador { get; set; }

		/// <summary> 
		///	CorralID  
		/// </summary> 
        public List<SeleccionInfoModelo<CorralInfo>> Corrales { get; set; }

        /// <summary> 
        ///	OrganizacionInfo
        /// </summary> 
        public OrganizacionInfo Organizacion { get; set; }

	}
}
