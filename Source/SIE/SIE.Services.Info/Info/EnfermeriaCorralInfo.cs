namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("EnfermeriaCorral")]
	public class EnfermeriaCorralInfo : BitacoraInfo
	{
		/// <summary> 
		///	Enfermería Corral  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int EnfermeriaCorralID { get; set; }

		/// <summary> 
		///	Enfermería  
		/// </summary> 
		public int EnfermeriaID { get; set; }

		/// <summary> 
		///	Entidad Enfermería  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public EnfermeriaInfo Enfermeria { get; set; }

		/// <summary> 
		///	Corral  
		/// </summary> 
		public int CorralID { get; set; }

		/// <summary> 
		///	Entidad Corral  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public CorralInfo Corral { get; set; }

		/// <summary> 
		///	Fecha Creación  
		/// </summary> 
		[BLToolkit.DataAccess.NonUpdatable]
		public System.DateTime FechaCreacion { get; set; }

		/// <summary> 
		///	Fecha Modificación  
		/// </summary> 
		public System.DateTime? FechaModificacion { get; set; }
	}
}
