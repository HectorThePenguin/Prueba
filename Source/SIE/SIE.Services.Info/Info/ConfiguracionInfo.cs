namespace SIE.Services.Info.Info
{
    public class ConfiguracionInfo
    {
        /// <summary>
        /// Indica el puerto de la bascula
        /// </summary>
        public string PuertoBascula { get; set; }

        /// <summary>
        /// Indica el DataBits de la bascula
        /// </summary>
        public string BasculaDataBits { get; set; }

        /// <summary>
        /// Indica el Paridad de la bascula
        /// </summary>
        public string BasculaParidad { get; set; }

        /// <summary>
        /// Indica el Baudrate de la bascula
        /// </summary>
        public string BasculaBaudrate { get; set; }

        /// <summary>
        /// Indica el BitStop de la bascula
        /// </summary>
        public string BasculaBitStop { get; set; }

        /// <summary>
        /// Indica la impresora de la Recepción de Ganado
        /// </summary>
        public string ImpresoraRecepcionGanado { get; set; }

        /// <summary>
        /// Indica el número maximo de caracteres por linea para la impresión del Ticket
        /// </summary>
        public int MaxCaracteresLinea { get; set; }

        /// <summary>
        /// Indica la impresora de la Poliza
        /// </summary>
        public string ImpresoraPoliza { get; set; }

        /// <summary>
        /// Indica el número maximo de caracteres por linea para la impresión de la Poliza
        /// </summary>
        public int MaxCaracteresLineaPoliza { get; set; }

        /// <summary>
        /// Indica el nombre de la fuente de la letra para la Poliza
        /// </summary>
        public string NombreFuentePoliza { get; set; }

        /// <summary>
        /// Indica el dominio para Login del Usuario
        /// </summary>
        public string Dominio { get; set; }

        /// <summary>
        /// Indica el contenedor para el Login del usuario
        /// </summary>
        public string Contenedor { get; set; }

        /// <summary>
        /// Indica el Grupo de Active Directory para el Login del usuario
        /// </summary>
        public string GrupoAD { get; set; }

        /// <summary>
        /// Servidor Active Directory
        /// </summary>
        public string ServidorActiveDirectory { get; set; }

        /// <summary>
        /// Numero de lineas maximas por pagina 
        /// </summary>
        public int MaxLineasPorPaginaPoliza { get; set; }
        /// <summary>
        /// Nombre proveedor propio
        /// </summary>
        public string ProveedorPropio { get; set; }
        /// <summary>
        /// Indica el puerto de la bascula
        /// </summary>
        public string PuertoDickey { get; set; }

        /// <summary>
        /// Indica el DataBits de la bascula
        /// </summary>
        public string DickeyDataBits { get; set; }

        /// <summary>
        /// Indica el Paridad de la bascula
        /// </summary>
        public string DickeyParidad { get; set; }

        /// <summary>
        /// Indica el Baudrate de la bascula
        /// </summary>
        public string DickeyBaudrate { get; set; }

        /// <summary>
        /// Indica el BitStop de la bascula
        /// </summary>
        public string DickeyBitStop { get; set; }
    }
}
