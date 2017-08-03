using System;

namespace SIE.Services.Info.Info
{
	public class DeteccionAnimalInfo : BitacoraInfo
	{
		/// <summary> 
		///	DeteccionAnimalID  
		/// </summary> 
		public int DeteccionAnimalID { get; set; }

		/// <summary> 
		///	AnimalMovimientoID  
		/// </summary> 
		public long AnimalMovimientoID { get; set; }

		/// <summary> 
		///	Arete  
		/// </summary> 
		public string Arete { get; set; }

		/// <summary> 
		///	AreteMetalico  
		/// </summary> 
		public string AreteMetalico { get; set; }

		/// <summary> 
		///	FotoDeteccion  
		/// </summary> 
		public string FotoDeteccion { get; set; }

		/// <summary> 
		///	LoteID  
		/// </summary> 
		public LoteInfo Lote { get; set; }

		/// <summary> 
		///	OperadorID  
		/// </summary> 
		public OperadorInfo Operador { get; set; }

		/// <summary> 
		///	TipoDeteccionID  
		/// </summary> 
		public TipoDeteccionInfo TipoDeteccion { get; set; }

		/// <summary> 
		///	GradoID  
		/// </summary> 
		public GradoInfo Grado { get; set; }

		/// <summary> 
		///	Observaciones  
		/// </summary> 
		public string Observaciones { get; set; }

		/// <summary> 
		///	NoFierro  
		/// </summary> 
		public string NoFierro { get; set; }

		/// <summary> 
		///	FechaDeteccion  
		/// </summary> 
		public DateTime FechaDeteccion { get; set; }

		/// <summary> 
		///	DeteccionAnalista  
		/// </summary> 
		public bool DeteccionAnalista { get; set; }

        /// <summary> 
        ///	Grupo de Corral al que pertenece el lote de la deteccion
        /// </summary> 
        public int GrupoCorralID { get; set; }
	}
}
