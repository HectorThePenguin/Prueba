namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("ClaseCostoProducto")]	public class ClaseCostoProductoInfo : BitacoraInfo
	{
        /// <summary>
        /// Constructor
        /// </summary>
        public ClaseCostoProductoInfo()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="cuentaSAP"></param>
        public ClaseCostoProductoInfo(ClaseCostoProductoInfo info, CuentaSAPInfo cuentaSAP)
        {
            ClaseCostoProductoID = info.ClaseCostoProductoID;
            AlmacenID = info.AlmacenID;
            ProductoID = info.ProductoID;
            CuentaSAPID = info.CuentaSAPID;
            Activo = info.Activo;
            FechaCreacion = info.FechaCreacion;
            UsuarioCreacionID = info.UsuarioCreacionID;
            FechaModificacion = info.FechaModificacion;
            UsuarioModificacionID = info.UsuarioModificacionID;
            CuentaSAP = cuentaSAP;
        }

		/// <summary> 
		///	Clase Costo Producto  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int ClaseCostoProductoID { get; set; }

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
