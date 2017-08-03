namespace SIE.Services.Info.Info
{
	public class TratamientoProductoInfo : BitacoraInfo
	{
		/// <summary> 
		///	TratamientoProductoID  
		/// </summary> 
		public int TratamientoProductoID { get; set; }

		/// <summary> 
		///	TratamientoID  
		/// </summary> 
		public TratamientoInfo Tratamiento { get; set; }

		/// <summary> 
		///	ProductoID  
		/// </summary> 
		public ProductoInfo Producto { get; set; }

		/// <summary> 
		///	Dosis  
		/// </summary> 
		public int Dosis { get; set; }

        /// <summary> 
        ///	Aplica Factor  
        /// </summary> 
        public bool Factor { get; set; }

        /// <summary> 
        ///	FactorMacho  
        /// </summary> 
        public decimal FactorMacho { get; set; }

        /// <summary> 
        ///	FactorHembra  
        /// </summary> 
        public decimal FactorHembra { get; set; }

        /// <summary> 
        ///	HabilitaEdicion  
        /// </summary> 
        public bool HabilitaEdicion { get; set; }

        /// <summary> 
        ///	Orden para paginar los Productos  
        /// </summary> 
        public int Orden { get; set; }

        public TratamientoProductoInfo Clone()
        {
            var tratamientoProductoClone = new TratamientoProductoInfo
                {
                    TratamientoProductoID = TratamientoProductoID,
                    Tratamiento = Tratamiento,
                    Producto = Producto,
                    Dosis = Dosis,
                    FactorHembra = FactorHembra,
                    FactorMacho = FactorMacho,
                    HabilitaEdicion = HabilitaEdicion,
                    Activo = Activo,
                    UsuarioCreacionID = UsuarioCreacionID,
                    UsuarioModificacionID = UsuarioModificacionID
                };
            return tratamientoProductoClone;
        }
	}
}
