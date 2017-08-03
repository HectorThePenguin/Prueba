using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class CorralRangoInfo
    {
        /// <summary>
        /// Organizacion a la que pertenece el Corral
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Identificador de corral
        /// </summary>
        public int CorralID { get; set; }

        /// <summary>
        /// Identificador del corral anterior.
        /// </summary>
        public int CorralAnteriorID { get; set; }

        /// <summary>
        /// Tipo de sexo
        /// </summary>
        public string Sexo { get; set; }

        /// <summary>
        /// Rango inicial 
        /// </summary>
        public Decimal RangoInicial { get; set; }

        /// <summary>
        /// Rango final
        /// </summary>
        public int RangoFinal { get; set; }

        /// <summary>
        /// Identificador para el tipo de ganado
        /// </summary>
        /// 
        public int TipoGanadoID { get; set; }

        /// <summary>
        /// Descripcion del tipo de ganado
        /// </summary>
        public string DescripcionTipoGanado { get; set; }

        /// <summary>
        /// Estatus del animal
        /// </summary>
        public int Activo { get; set; }

        /// <summary>
        /// Fecha de la creacion 
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Identificador del usuario que modifica
        /// </summary>
        public int UsuarioModificacionId { get; set; }

        /// <summary>
        /// Codigo del animal
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// identificador del corral
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Bandera si ha sido modificado el corral
        /// </summary>
        public bool Modificado { get; set; }

        /// <summary>
        /// Accion que se le realizo al grid (Agregar o actualizar)
        /// </summary>
        public AccionConfigurarCorrales Accion { get; set; }

        /// <summary>
        /// Descripcion del sexo del animal
        /// </summary>
        public string SexoDescripcion
        {
            get { return (Sexo == "H") ? "Hembra" : "Macho"; }

        }
    }
}
