namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("SupervisorEnfermeria")]
	public class SupervisorEnfermeriaInfo : BitacoraInfo
	{
		/// <summary> 
		///	SupervisorEnfermeriaID  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int SupervisorEnfermeriaID { get; set; }

		/// <summary> 
		///	Operador  
		/// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public OperadorInfo Operador { get; set; }

        /// <summary> 
        ///	OperadorID  
        /// </summary> 
        public int OperadorID { get; set; }

		/// <summary> 
		///	Enfermeria
		/// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public EnfermeriaInfo Enfermeria { get; set; }

        /// <summary> 
        ///	EnfermeriaID  
        /// </summary> 
        public int EnfermeriaID { get; set; }

        /// <summary> 
        ///	Fecha Creaci�n  
        /// </summary> 
        [BLToolkit.DataAccess.NonUpdatable]
        public System.DateTime FechaCreacion { get; set; }

        /// <summary> 
        ///	Fecha Modificaci�n  
        /// </summary> 
        public System.DateTime? FechaModificacion { get; set; }

        /// <summary> 
        ///	Organizacion
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public OrganizacionInfo Organizacion { get; set; }
	}
}
