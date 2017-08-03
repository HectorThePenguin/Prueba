using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    /// <summary>
    /// Clase que representa la configuracion de acciones para ejecutar
    /// </summary>
    public class ConfiguracionAccionesInfo
    {
        /// <summary>
        /// Id del proceso
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del proceso
        /// </summary>
        public string Proceso { get; set; }
        /// <summary>
        /// Descripcion del proceso
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Codigo dle proceso
        /// </summary>
        public string Codigo { get; set; }     
        /// <summary>
        /// Fecha ejecucion
        /// </summary>
        public DateTime FechaEjecucion { get; set; }
        /// <summary>
        /// Fecha ultima ejecucion
        /// </summary>
        public DateTime FechaUltimaEjecucion { get; set; }
        /// <summary>
        /// Lunes
        /// </summary>
        public bool Lunes { get; set; } 
        /// <summary>
        /// Martes
        /// </summary>
        public bool Martes { get; set; } 
        /// <summary>
        /// Miercoles
        /// </summary>
        public bool Miercoles { get; set; }
        /// <summary>
        /// Jueves
        /// </summary>
        public bool Jueves { get; set; } 
        /// <summary>
        /// Viernes
        /// </summary>
        public bool Viernes { get; set; } 
        /// <summary>
        /// Sabado
        /// </summary>
        public bool Sabado { get; set; } 
        /// <summary>
        /// Domingo
        /// </summary>
        public bool Domingo { get; set; } 
        /// <summary>
        /// Repetir
        /// </summary>
        public bool Repetir { get; set; }    
        /// <summary>
        /// Activo
        /// </summary>
        public EstatusEnum Activo { get; set; } 
        /// <summary>
        /// Fecha de Creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario de creacion
        /// </summary>
        public int UsuarioCreacionId { get; set; }
        /// <summary>
        /// Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario de modificacion
        /// </summary>
        public int UsuarioModificacionId { get; set; }
    }
}
