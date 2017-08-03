using System;

namespace SIE.Services.Info.Info
{
    public class ClasificacionGanadoInfo
    {
        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int ClasificacionGanadoID { get; set; }

        /// <summary>
        /// Descripción de la tabla
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Saber si esta activo
        /// </summary>
        public int Activo { get; set; }

        /// <summary>
        /// Fecha de Creacion Clasificacion De Ganado
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que Creo Clasificacion De Ganado
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Fecha de Modificacion Clasificacion De Ganado
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que Clasificacion el Corral
        /// </summary>
        public int UsuarioModificacionID { get; set; }
    }
}
