namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("ProduccionDiariaDetalle")]	public class ProduccionDiariaDetalleInfo : BitacoraInfo
	{
		/// <summary> 
		///	Produccion Diaria Detalle  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int ProduccionDiariaDetalleID { get; set; }

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
		///	Producto  
		/// </summary> 
		public int ProductoID { get; set; }

		/// <summary> 
		///	Entidad Producto  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public ProductoInfo Producto { get; set; }

		/// <summary> 
		///	Pesaje Materia Prima  
		/// </summary> 
		public int PesajeMateriaPrimaID { get; set; }

		/// <summary> 
		///	Entidad Pesaje Materia Prima  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public PesajeMateriaPrimaInfo PesajeMateriaPrima { get; set; }

		/// <summary> 
		///	Especificacion Forraje  
		/// </summary> 
		public int EspecificacionForraje { get; set; }

		/// <summary> 
		///	Hora Inicial  
		/// </summary> 
		public string HoraInicial { get; set; }

		/// <summary> 
		///	Hora Final  
		/// </summary> 
		public string HoraFinal { get; set; }

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
        ///	Entidad Produccion Diaria  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public int KilosNeto { get; set; }

	}
}
