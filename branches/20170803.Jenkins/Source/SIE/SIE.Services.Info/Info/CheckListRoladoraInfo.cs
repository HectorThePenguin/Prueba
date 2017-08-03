using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("CheckListRoladora")]
	public class CheckListRoladoraInfo : BitacoraInfo
	{
		/// <summary> 
		///	Check List Roladora  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int CheckListRoladoraID { get; set; }

		/// <summary> 
		///	Check List Roladora General  
		/// </summary> 
		public int CheckListRoladoraGeneralID { get; set; }

		/// <summary> 
		///	Entidad Check List Roladora General  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public CheckListRoladoraGeneralInfo CheckListRoladoraGeneral { get; set; }

		/// <summary> 
		///	Roladora  
		/// </summary> 
		public int RoladoraID { get; set; }

		/// <summary> 
		///	Entidad Roladora  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public RoladoraInfo Roladora { get; set; }

        /// <summary> 
        ///	Entidad Roladora  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public List<RoladoraInfo> Roladoras { get; set; }

		/// <summary> 
		///	Usuario Iesponsable  
		/// </summary> 
		public int? UsuarioIDResponsable { get; set; }

		/// <summary> 
		///	Entidad Usuario Iesponsable  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public UsuarioInfo Usuario { get; set; }

		/// <summary> 
		///	Fecha Check List  
		/// </summary> 
		public DateTime FechaCheckList { get; set; }

        /// <summary>
        /// Hora de la Notificacion
        /// </summary>
        [BLToolkit.Mapping.MapIgnore, BLToolkit.DataAccess.SqlIgnore]
	    public string Hora { get; set; }

		/// <summary> 
		///	Fecha Creación  
		/// </summary> 
		[BLToolkit.DataAccess.NonUpdatable]
		public System.DateTime FechaCreacion { get; set; }

		/// <summary> 
		///	Fecha Modificación  
		/// </summary> 
		public System.DateTime? FechaModificacion { get; set; }

        /// <summary>
        /// Hora de la Notificacion
        /// </summary>
        [BLToolkit.Mapping.MapIgnore, BLToolkit.DataAccess.SqlIgnore]
        public CheckListRoladoraHorometroInfo CheckListRoladoraHorometro { get; set; }
	}
}
