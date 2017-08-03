namespace SIE.Services.Info.Info
{
	public class FormularioInfo : BitacoraInfo
	{
		/// <summary> 
		///	FormularioID  
		/// </summary> 
		public int FormularioID { get; set; }

		/// <summary> 
		///	Descripcion  
		/// </summary> 
		public string Descripcion { get; set; }

		/// <summary> 
		///	WinForm  
		/// </summary> 
		public string WinForm { get; set; }

		/// <summary> 
		///	Orden  
		/// </summary> 
		public int Orden { get; set; }

		/// <summary> 
		///	PadreID  
		/// </summary> 
		public int PadreID { get; set; }

		/// <summary> 
		///	ModuloID  
		/// </summary> 
		public ModuloInfo Modulo { get; set; }

		/// <summary> 
		///	Imagen  
		/// </summary> 
		public string Imagen { get; set; }
		/// <summary> 
		///	Web  
		/// </summary> 
		public bool Web { get; set; }

        /// <summary> 
        ///	AccesoID  
        /// </summary> 
        public AccesoInfo Acceso { get; set; }
	}
}
