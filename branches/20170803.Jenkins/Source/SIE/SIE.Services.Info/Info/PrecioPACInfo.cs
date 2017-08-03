using System;

namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("PrecioPAC")]	public class PrecioPACInfo : BitacoraInfo
	{
		/// <summary> 
		///	Precio  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int PrecioPACID { get; set; }

		/// <summary> 
		///	Organización  
		/// </summary> 
        [BLToolkit.Mapping.MapField("OrganizacionID")]
		public int OrganizacionID { get; set; }

		/// <summary> 
		///	Entidad Organización  
		/// </summary> 
        [BLToolkit.Mapping.Association(CanBeNull = false, ThisKey = "OrganizacionID", OtherKey = "OrganizacionID")]
		public OrganizacionInfo Organizacion { get; set; }

		/// <summary> 
		///	Tipo  
		/// </summary> 
        [BLToolkit.Mapping.MapField("TipoPACID")]
		public int TipoPACID { get; set; }

		/// <summary> 
		///	Entidad Tipo  
		/// </summary> 
        [BLToolkit.Mapping.Association(CanBeNull = false, ThisKey = "TipoPACID", OtherKey = "TipoPACID")]
		public TipoPACInfo TipoPAC { get; set; }

		/// <summary> 
		///	Precio  
		/// </summary> 
        [BLToolkit.Mapping.MapField("Precio")]
		public decimal Precio { get; set; }

        /// <summary> 
        ///	Precio  
        /// </summary> 
        [BLToolkit.Mapping.MapField("PrecioViscera")]
        public decimal PrecioViscera { get; set; }

		/// <summary> 
		///	Fecha Inicio  
		/// </summary> 
        [BLToolkit.Mapping.MapField("FechaInicio")]
		public DateTime FechaInicio { get; set; }

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
