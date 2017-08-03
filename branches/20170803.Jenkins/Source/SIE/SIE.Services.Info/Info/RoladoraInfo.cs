namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("Roladora")]	public class RoladoraInfo : BitacoraInfo
	{
		/// <summary> 
		///	Roladora  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int RoladoraID { get; set; }

		/// <summary> 
		///	Organización  
		/// </summary> 
		public int OrganizacionID { get; set; }

		/// <summary> 
		///	Entidad Organización  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public OrganizacionInfo Organizacion { get; set; }

		/// <summary> 
		///	Descripción  
		/// </summary> 
		public string Descripcion { get; set; }

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
