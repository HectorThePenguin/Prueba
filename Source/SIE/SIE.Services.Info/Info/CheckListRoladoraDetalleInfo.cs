namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("CheckListRoladoraDetalle")]	public class CheckListRoladoraDetalleInfo : BitacoraInfo
	{
		/// <summary> 
		///	Check List Roladora Detalle  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int CheckListRoladoraDetalleID { get; set; }

		/// <summary> 
		///	Check List Roladora  
		/// </summary> 
		public int CheckListRoladoraID { get; set; }

		/// <summary> 
		///	Entidad Check List Roladora  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public CheckListRoladoraInfo CheckListRoladora { get; set; }

		/// <summary> 
		///	Check List Roladora Rango  
		/// </summary> 
		public int CheckListRoladoraRangoID { get; set; }

		/// <summary> 
		///	Entidad Check List Roladora Rango  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public CheckListRoladoraRangoInfo CheckListRoladoraRango { get; set; }

		/// <summary> 
		///	Check List Roladora Accion  
		/// </summary> 
		public int? CheckListRoladoraAccionID { get; set; }

		/// <summary> 
		///	Entidad Check List Roladora Accion  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public CheckListRoladoraAccionInfo CheckListRoladoraAccion { get; set; }

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
