namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("CuentaAlmacenSubFamilia")]
	public class CuentaAlmacenSubFamiliaInfo : BitacoraInfo
	{
		/// <summary> 
		///	Cuenta Almacén Sub Familia  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int CuentaAlmacenSubFamiliaID { get; set; }

		/// <summary> 
		///	Almacén  
		/// </summary> 
		public int AlmacenID { get; set; }

		/// <summary> 
		///	Entidad Almacén  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public AlmacenInfo Almacen { get; set; }

		/// <summary> 
		///	Sub Familia  
		/// </summary> 
		public int SubFamiliaID { get; set; }

		/// <summary> 
		///	Entidad Sub Familia  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public SubFamiliaInfo SubFamilia { get; set; }

		/// <summary> 
		///	Cuenta  
		/// </summary> 
		public int CuentaSAPID { get; set; }

		/// <summary> 
		///	Entidad Cuenta  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public CuentaSAPInfo CuentaSAP { get; set; }

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
