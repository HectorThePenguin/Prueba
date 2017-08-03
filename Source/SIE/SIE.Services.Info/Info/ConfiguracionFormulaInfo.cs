using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ConfiguracionFormulaInfo
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int ConfiguracionFormulaID { get; set; }
        /// <summary>
        /// Identificador de la organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Formula
        /// </summary>
        public FormulaInfo Formula { get; set; }
        /// <summary>
        /// Peso inicio minimo
        /// </summary>
        public int PesoInicioMinimo { get; set; }
        /// <summary>
        /// Peso inicio Maximo
        /// </summary>
        public int PesoInicioMaximo { get; set; }
        /// <summary>
        /// Tipo de ganado separados por 
        /// </summary>
        public string TipoGanado { get; set; }
        /// <summary>
        /// Peso Salida
        /// </summary>
        public int PesoSalida { get; set; }
        /// <summary>
        /// Siguiente Formula 
        /// </summary>
        public FormulaInfo FormulaSiguiente { get; set; }
        /// <summary>
        /// Dias estancia minimos
        /// </summary>
        public int DiasEstanciaMinimo { get; set; }
        /// <summary>
        /// Dias estancia Maximos
        /// </summary>
        public int DiasEstanciaMaximo { get; set; }
        /// <summary>
        /// Dias transicion Minimos
        /// </summary>
        public int DiasTransicionMinimo { get; set; }
        /// <summary>
        /// Dias Transicion maximos
        /// </summary>
        public int DiasTransicionMaximo { get; set; }
        /// <summary>
        /// Disponibilidad
        /// </summary>
        public Disponibilidad Disponibilidad { get; set; }
        /// <summary>
        /// Activo el registro
        /// </summary>
        public EstatusEnum Activo { get; set; }
        /// <summary>
        /// Fecha Creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario Creacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }
        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario Modificacion
        /// </summary>
        public int UsuarioModificacionID { get; set; }
        /// <summary>
        /// Nombre de archivo a importar
        /// </summary>
        public string NombreArchivo { get; set; }
        
    }

}