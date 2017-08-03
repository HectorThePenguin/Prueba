using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Filtros
{
    public class FiltroProductoInfo
    {
        /// <summary>
        /// Identificador del producto
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Descripción del producto
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Identificador de la unidad
        /// </summary>
        public int UnidadID { get; set; }

        /// <summary>
        /// Descripción de la unidad de medida
        /// </summary>
        public string Unidad { get; set; }

        /// <summary>
        /// Identificador de la subfamilia
        /// </summary>
        public int SubFamiliaID { get; set; }

        /// <summary>
        /// Descripción de la Subfamilia
        /// </summary>
        public string SubFamilia { get; set; }

        /// <summary>
        /// Identificador de la Familia
        /// </summary>
        public int FamiliaID { get; set; }

        /// <summary>
        /// Descripción de la Familia
        /// </summary>
        public string Familia { get; set; }

        /// <summary>
        /// Lista de familias a filtrar
        /// </summary>
        public string FiltroFamilia { get; set; }
        
        /// <summary>
        ///  Indica si el registro  se encuentra Activo
        /// </summary>
        public EstatusEnum Activo { get; set; }
    }
}
