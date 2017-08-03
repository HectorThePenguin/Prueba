
namespace SIE.Services.Info.Info
{
    public class ServidorSmtpInfo
    {
        /// <summary>
        /// Servidor de correo
        /// </summary>
        public string Servidor { get; set; }
        /// <summary>
        /// Puerto del servidor
        /// </summary>
        public string Puerto { get; set; }
        /// <summary>
        /// Usuario del servicio smpt
        /// </summary>
        public string Cuenta { get; set; }
        /// <summary>
        /// Contraseña para autenticarse en el servidor smtp
        /// </summary>
        public string Autentificacion { get; set; }
        /// <summary>
        /// Indica si el servidor requiere conexiones segura
        /// </summary>
        public bool RequiereSsl { get; set; }
    }
}
