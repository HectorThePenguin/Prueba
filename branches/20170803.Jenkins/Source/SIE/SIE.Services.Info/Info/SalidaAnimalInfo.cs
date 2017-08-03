using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class SalidaAnimalInfo
    {
        /// <summary>
        /// Identificador del registro .
        /// </summary>
        public int SalidaAnimalID { get; set; }
        /// <summary>
        /// Informacion de la salida del ganado
        /// </summary>
        public SalidaGanadoInfo SalidaGanado { get; set; }
        /// <summary>
        /// Informacion del animal
        /// </summary>
        public AnimalInfo Animal { get; set; }
        /// <summary>
        /// INformacion del lote
        /// </summary>
        public LoteInfo Lote { get; set; }
        /// <summary>
        /// Estatus del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }
        /// <summary>
        /// Usuario Creaciojn del registro
        /// </summary>
        public int UsuarioCreacionID { get; set; }
        /// <summary>
        /// Fecha creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario modificacion del registro
        /// </summary>
        public DateTime UsuarioModificacionID { get; set; }
        /// <summary>
        /// Fecha modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
    }
}
