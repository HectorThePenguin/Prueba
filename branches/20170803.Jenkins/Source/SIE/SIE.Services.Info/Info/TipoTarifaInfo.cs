using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class TipoTarifaInfo
    {
        /// <summary>
        /// Identificador de la tarifa
        /// </summary>
        public int TipoTarifaId { get; set; }
        /// <summary>
        /// Descripcion de la tarifa
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }
        /// <summary>
        /// Fecha en que se creo el registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario que crea el registro
        /// </summary>
        public UsuarioInfo UsuarioCreacion { get; set; }
        /// <summary>
        /// Fecha en que se modifico el registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario que modifica el registro
        /// </summary>
        public UsuarioInfo UsuarioModificacion { get; set; }
    }
}
