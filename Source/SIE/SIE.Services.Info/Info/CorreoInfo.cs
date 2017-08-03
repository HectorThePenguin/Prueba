
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class CorreoInfo
    {
        public CorreoInfo()
        {
            NombreOrigen = string.Empty;
        }
        /// <summary>
        /// Mensaje del coreo
        /// </summary>
        public string Mensaje { get; set; }
        /// <summary>
        /// Asunto
        /// </summary>
        public string Asunto { get; set; }
        /// <summary>
        /// Destinatarios
        /// </summary>
        public List<string>Correos { get; set; } 
        /// <summary>
        /// Nombre de quien envia
        /// </summary>
        public string NombreOrigen { get; set; }
        /// <summary>
        /// Ruta del Archivo adjunto
        /// </summary>
        public string ArchivoAdjunto { get; set; }
        /// <summary>
        /// Accion a la que pertenece el correo
        /// </summary>
        public AccionesSIAPEnum AccionSiap { get; set; }
    }
}
