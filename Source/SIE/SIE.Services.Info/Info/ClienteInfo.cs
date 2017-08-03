using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public class ClienteInfo : BitacoraInfo
    {
        /// <summary> 
        ///	ClienteID  
        /// </summary> 
        public int ClienteID { get; set; }

        /// <summary> 
        ///	CodigoSAP  
        /// </summary> 
        [AtributoAyuda(Nombre = "PropiedadCodigoCliente", EncabezadoGrid = "Código", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadCodigoCliente", EncabezadoGrid = "Código", MetodoInvocacion = "ObtenerClientePorCliente", PopUp = false)]
        public string CodigoSAP { get; set; }

        /// <summary> 
        ///	Descripcion  
        /// </summary> 
        [AtributoAyuda(Nombre = "PropiedadDescripcionCliente", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCliente", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerClientePorCliente", PopUp = false)]
        public string Descripcion { get; set; }

        public string Poblacion { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Calle { get; set; }
        public string CodigoPostal { get; set; }
        public string RFC { get; set; }
        public MetodoPagoInfo MetodoPago { get; set; }
        public int CondicionPago { get; set; }
        public string DiasPago { get; set; }
        public string Sociedad { get; set; }
        public bool Bloqueado { get; set; }
    }
}
