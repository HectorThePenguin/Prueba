using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("ProduccionDiaria")]
	public class ProduccionDiariaInfo : BitacoraInfo
	{
		/// <summary> 
		///	Produccion Diaria  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int ProduccionDiariaID { get; set; }

		/// <summary> 
		///	Turno  
		/// </summary> 
		public int Turno { get; set; }

		/// <summary> 
		///	Litros Inicial  
		/// </summary> 
		public decimal LitrosInicial { get; set; }

		/// <summary> 
		///	Litros Final  
		/// </summary> 
		public decimal LitrosFinal { get; set; }

		/// <summary> 
		///	Horometro Inicial  
		/// </summary> 
		public int HorometroInicial { get; set; }

		/// <summary> 
		///	Horometro Final  
		/// </summary> 
		public int HorometroFinal { get; set; }

		/// <summary> 
		///	Fecha Produccion  
		/// </summary> 
		public DateTime FechaProduccion { get; set; }

		/// <summary> 
		///	Usuario Iutorizo  
		/// </summary> 
		public int? UsuarioIDAutorizo { get; set; }

		/// <summary> 
		///	Entidad Usuario Iutorizo  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public UsuarioInfo Usuario { get; set; }

		/// <summary> 
		///	Observaciones  
		/// </summary> 
		public string Observaciones { get; set; }

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
        ///	Lista con el detalle de la Produccion Diaria
        /// </summary> 
        public List<ProduccionDiariaDetalleInfo> ListaProduccionDiariaDetalle { get; set; }

        /// <summary> 
        ///	Lista con los Tiempos Muertos que hubo en la producción
        /// </summary> 
        public List<TiempoMuertoInfo> ListaTiempoMuerto { get; set; }
	}
}
