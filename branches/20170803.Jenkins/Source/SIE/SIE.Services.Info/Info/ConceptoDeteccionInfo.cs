namespace SIE.Services.Info.Info
{
	public class ConceptoDeteccionInfo : BitacoraInfo
	{
	    private string descripcion;
		/// <summary> 
		///	ConceptoDeteccionID  
		/// </summary> 
		public int ConceptoDeteccionID { get; set; }

		/// <summary> 
		///	Descripcion  
		/// </summary> 
        public string Descripcion
        {
            get { return descripcion != null ? descripcion.Trim() : descripcion; }
            set
            {
                if (value != descripcion)
                {
                    descripcion = value != null ?  value.Trim() : null;
                }
            }
        }
	}
}
