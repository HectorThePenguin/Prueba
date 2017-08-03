using System;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("CheckListRoladoraGeneral")]
	public class CheckListRoladoraGeneralInfo : BitacoraInfo
	{
		/// <summary> 
		///	Check List Roladora General  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int CheckListRoladoraGeneralID { get; set; }

		/// <summary> 
		///	Turno  
		/// </summary> 
		public int Turno { get; set; }

		/// <summary> 
		///	Fecha Inicio  
		/// </summary> 
		public DateTime FechaInicio { get; set; }

		/// <summary> 
		///	Usuario Iupervisor  
		/// </summary> 
		public int? UsuarioIDSupervisor { get; set; }

		/// <summary> 
		///	Entidad Usuario Iupervisor  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public UsuarioInfo Usuario { get; set; }

		/// <summary> 
		///	Observaciones  
		/// </summary> 
		public string Observaciones { get; set; }

		/// <summary> 
		///	Surfactante Inicio  
		/// </summary> 
		public decimal? SurfactanteInicio { get; set; }

		/// <summary> 
		///	Surfactante Fin  
		/// </summary> 
		public decimal? SurfactanteFin { get; set; }

		/// <summary> 
		///	Contador Agua Inicio  
		/// </summary> 
		public decimal? ContadorAguaInicio { get; set; }

		/// <summary> 
		///	Contador Agua Fin  
		/// </summary> 
		public decimal? ContadorAguaFin { get; set; }

		/// <summary> 
		///	Grano Entero Final  
		/// </summary> 
		public decimal? GranoEnteroFinal { get; set; }

		/// <summary> 
		///	Fecha Creación  
		/// </summary> 
		[BLToolkit.DataAccess.NonUpdatable]
		public DateTime FechaCreacion { get; set; }

		/// <summary> 
		///	Fecha Modificación  
		/// </summary> 
		public DateTime? FechaModificacion { get; set; }

        /// <summary>
        /// Parametros
        /// </summary>
        public ParametrosCheckListRoladoModel ParametrosCheckListRolado { get; set; }

        /// <summary>
        /// Usuario que genero el CheckList
        /// </summary>
        public string NombreUsuario { get; set; }
	}
}
