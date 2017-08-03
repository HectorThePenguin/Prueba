
namespace SIE.Services.Info.Info
{
    public class ParametroSemanaInfo:BitacoraInfo
    {
        /// <summary>
        /// Identificador del ParametroSemana
        /// </summary>
        public int ParametroSemanaID { get; set; }

        /// <summary>
        /// Descripcion del parametro
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Activo para el lunes
        /// </summary>
        public bool Lunes { get; set; }

        /// <summary>
        /// Activo para el martes
        /// </summary>
        public bool Martes { get; set; }

        /// <summary>
        /// Activo para el miercoles
        /// </summary>
        public bool Miercoles { get; set; }

        /// <summary>
        /// Activo para el jueves
        /// </summary>
        public bool Jueves { get; set; }

        /// <summary>
        /// Activo para el viernes
        /// </summary>
        public bool Viernes { get; set; }

        /// <summary>
        /// Activo para el sabado
        /// </summary>
        public bool Sabado { get; set; }

        /// <summary>
        /// Activo para el domingo
        /// </summary>
        public bool Domingo { get; set; }
    }
}
