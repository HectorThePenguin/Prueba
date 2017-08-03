namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("AlmacenProductoCuenta")]
	public class AlmacenProductoCuentaInfo : BitacoraInfo
	{
		/// <summary> 
		///	Almacén Producto Cuenta  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int AlmacenProductoCuentaID { get; set; }

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
		///	Producto  
		/// </summary> 
		public int ProductoID { get; set; }

		/// <summary> 
		///	Entidad Producto  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public ProductoInfo Producto { get; set; }

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
