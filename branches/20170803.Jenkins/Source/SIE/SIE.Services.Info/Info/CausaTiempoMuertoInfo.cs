namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("CausaTiempoMuerto")]
	public class CausaTiempoMuertoInfo : BitacoraInfo
	{
		/// <summary> 
		///	Causa Tiempo Muerto  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int CausaTiempoMuertoID { get; set; }

		/// <summary> 
		///	Descripción  
		/// </summary> 
		public string Descripcion { get; set; }

		/// <summary> 
		///	Tipo Causa  
		/// </summary> 
		public int TipoCausa { get; set; }

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
