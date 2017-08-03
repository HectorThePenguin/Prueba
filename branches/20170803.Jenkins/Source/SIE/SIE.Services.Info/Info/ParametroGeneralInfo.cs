namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("ParametroGeneral")]
	public class ParametroGeneralInfo : BitacoraInfo
	{
		/// <summary> 
		///	Parámetro General  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int ParametroGeneralID { get; set; }

		/// <summary> 
		///	Parámetro  
		/// </summary> 
		public int ParametroID { get; set; }

		/// <summary> 
		///	Valor  
		/// </summary> 
		public string Valor { get; set; }

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
