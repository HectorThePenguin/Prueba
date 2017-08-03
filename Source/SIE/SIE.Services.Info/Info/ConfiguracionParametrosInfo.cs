

namespace SIE.Services.Info.Info
{
    /// <summary>
    /// Entidad para portar los datos de configuracion de la tabla FROM ParametroOrganizacion Parametro TipoParametro
    /// </summary>
    public class ConfiguracionParametrosInfo
    {
        /// <summary>
        /// identificador del parametro
        /// </summary>
        public int ParametroID { get; set; }
        /// <summary>
        /// Valor del parametro
        /// </summary>
        public string Valor { get; set; }
        /// <summary>
        /// Clave del parametro
        /// </summary>
        public string Clave { get; set; }
        /// <summary>
        /// Descripcion del parametros
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Identificador de la organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Tipo del parametro solicitado
        /// </summary>
        public int TipoParametro { get; set; }
    }
}
