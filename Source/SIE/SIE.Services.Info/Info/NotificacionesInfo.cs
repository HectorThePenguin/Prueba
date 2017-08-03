
namespace SIE.Services.Info.Info
{
    public class NotificacionesInfo
    {
        /// <summary>
        /// Identificador de la notificacion
        /// </summary>
        public int NotificionID { get; set; }
        /// <summary>
        /// Identificadpr de la accion siap
        /// </summary>
        public int AccionesSiapID { get; set; }
        /// <summary>
        /// Usario al cual se notificara
        /// </summary>
        public string UsuarioDestino { get; set; }


    }
}
