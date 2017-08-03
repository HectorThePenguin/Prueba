namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("CheckListRoladoraRango")]
	public class CheckListRoladoraRangoInfo : BitacoraInfo
	{
		/// <summary> 
		///	Check List Roladora Rango  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int CheckListRoladoraRangoID { get; set; }

		/// <summary> 
		///	Pregunta  
		/// </summary> 
		public int PreguntaID { get; set; }

		/// <summary> 
		///	Entidad Pregunta  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public PreguntaInfo Pregunta { get; set; }

		/// <summary> 
		///	Descripción  
		/// </summary> 
		public string Descripcion { get; set; }

		/// <summary> 
		///	Código Color  
		/// </summary> 
		public string CodigoColor { get; set; }

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
