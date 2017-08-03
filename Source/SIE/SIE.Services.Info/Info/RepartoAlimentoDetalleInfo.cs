namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("RepartoAlimentoDetalle")]
	public class RepartoAlimentoDetalleInfo : BitacoraInfo
	{
		/// <summary> 
		///	Reparto Alimento Detalle  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int RepartoAlimentoDetalleID { get; set; }

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
		///	Folio Reparto  
		/// </summary> 
		public int FolioReparto { get; set; }

		/// <summary> 
		///	Formula Iacion  
		/// </summary> 
		public int FormulaIDRacion { get; set; }

		/// <summary> 
		///	Entidad Formula Iacion  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public FormulaInfo Formula { get; set; }

		/// <summary> 
		///	Tolva  
		/// </summary> 
		public string Tolva { get; set; }

		/// <summary> 
		///	Kilos Embarcados  
		/// </summary> 
		public int KilosEmbarcados { get; set; }

		/// <summary> 
		///	Kilos Repartidos  
		/// </summary> 
		public int KilosRepartidos { get; set; }

		/// <summary> 
		///	Sobrante  
		/// </summary> 
		public int Sobrante { get; set; }

		/// <summary> 
		///	Corral Inicio  
		/// </summary> 
		public int CorralIDInicio { get; set; }

		/// <summary> 
		///	Entidad Corral Inicio  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public CorralInfo CorralInicio { get; set; }

		/// <summary> 
		///	Corral Iinal  
		/// </summary> 
		public int CorralIDFinal { get; set; }

		/// <summary> 
		///	Entidad Corral Iinal  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public CorralInfo CorralFinal { get; set; }

		/// <summary> 
		///	Hora Reparto Inicio  
		/// </summary> 
		public string HoraRepartoInicio { get; set; }

		/// <summary> 
		///	Hora Reparto Final  
		/// </summary> 
		public string HoraRepartoFinal { get; set; }

		/// <summary> 
		///	Observaciones  
		/// </summary> 
		public string Observaciones { get; set; }

        /// <summary> 
        ///	Peso Final del reparto
        /// </summary> 
        public int PesoFinal { get; set; }

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
