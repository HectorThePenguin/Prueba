namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("TiempoMuerto")]
	public class TiempoMuertoInfo : BitacoraInfo
	{
		/// <summary> 
		///	Tiempo Muerto  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int TiempoMuertoID { get; set; }

		/// <summary> 
		///	Produccion Diaria  
		/// </summary> 
		public int ProduccionDiariaID { get; set; }

		/// <summary> 
		///	Entidad Produccion Diaria  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public ProduccionDiariaInfo ProduccionDiaria { get; set; }

		/// <summary> 
		///	Reparto Alimento  
		/// </summary> 
		public int RepartoAlimentoID { get; set; }

		/// <summary> 
		///	Entidad Reparto Alimento  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public RepartoAlimentoInfo RepartoAlimento { get; set; }

		/// <summary> 
		///	Hora Inicio  
		/// </summary> 
		public string HoraInicio { get; set; }

		/// <summary> 
		///	Hora Fin  
		/// </summary> 
		public string HoraFin { get; set; }

		/// <summary> 
		///	Causa Tiempo Muerto  
		/// </summary> 
		public int CausaTiempoMuertoID { get; set; }

		/// <summary> 
		///	Entidad Causa Tiempo Muerto  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public CausaTiempoMuertoInfo CausaTiempoMuerto { get; set; }

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
