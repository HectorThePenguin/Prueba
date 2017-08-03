using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("ProblemaSintoma")]
	public class ProblemaSintomaInfo : BitacoraInfo
	{
		/// <summary> 
		///	Problema Sintomal  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int ProblemaSintomaID { get; set; }

		/// <summary> 
		///	Problema  
		/// </summary> 
		public int ProblemaID { get; set; }

		/// <summary> 
		///	Entidad Problema  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public ProblemaInfo Problema { get; set; }

		/// <summary> 
		///	Sintoma  
		/// </summary> 
		public int SintomaID { get; set; }

		/// <summary> 
		///	Entidad Sintoma  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public SintomaInfo Sintoma { get; set; }

		/// <summary> 
		///	Fecha Creación  
		/// </summary> 
		[BLToolkit.DataAccess.NonUpdatable]
		public System.DateTime FechaCreacion { get; set; }

		/// <summary> 
		///	Fecha Modificación  
		/// </summary> 
		public System.DateTime? FechaModificacion { get; set; }

        /// <summary> 
        ///	Lista de Sintomas asociados a un Problema  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public List<SintomaInfo> ListaSintomas { get; set; }
	}
}
