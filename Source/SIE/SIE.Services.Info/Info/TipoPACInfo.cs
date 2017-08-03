namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("TipoPAC")]	public class TipoPACInfo : BitacoraInfo
	{
		/// <summary> 
		///	Tipo  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int TipoPACID { get; set; }

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
