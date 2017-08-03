namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("CheckListRoladoraAccion")]	public class CheckListRoladoraAccionInfo : BitacoraInfo
	{
		/// <summary> 
		///	Check List Roladora Accion  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int? CheckListRoladoraAccionID { get; set; }

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

        [BLToolkit.Mapping.MapIgnore]
	    public long Indice { get; set; }
	}
}
