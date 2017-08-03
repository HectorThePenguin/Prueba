using System.Collections.Generic;
namespace SIE.Services.Info.Info
{
    public class RespuestaServicioPI
    {
        /// <summary>
        /// Indentificador de Poliza
        /// </summary>
        public int PolizaID { get; set; }
        /// <summary>
        /// Polizas que fue enviada a servicio de PI
        /// </summary>
        public IList<PolizaInfo> Polizas { get; set; }
        /// <summary>
        /// Mensaje que regresa servicio de PI
        /// </summary>
        public string Mensaje { get; set; }
    }
}
