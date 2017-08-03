
using System;

namespace SIE.Services.Info.Enums
{
    public class BitacoraErroresInfo
    {
        /// <summary>
        /// Identificador del error
        /// </summary>
        public int BitacoraErroresID { get; set; }
        /// <summary>
        /// Identificador de la accion Siap
        /// </summary>
        public AccionesSIAPEnum AccionesSiapID { get; set; }
        /// <summary>
        /// Mensaje
        /// </summary>
        public string Mensaje { get; set; }
        /// <summary>
        /// Fecha del error
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Identificador del usuario que creo el registro
        /// </summary>
        public int UsuarioCreacionID { get; set; }
    }
}
