
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class CriterioSupervisionInfo
    {
        /// <summary>
        /// Identificador del criterio
        /// </summary>
        public int CriterioSupervisionId { get; set; }
        /// <summary>
        /// Descripcion del criterio
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Valor inicial del rango del criterio
        /// </summary>
        public int ValorInicial { get; set; }
        /// <summary>
        /// Valor final del rango del criterio
        /// </summary>
        public int ValorFinal { get; set; }
        /// <summary>
        /// Codigo de color del criterio
        /// </summary>
        public string CodigoColor { get; set; }
        /// <summary>
        /// Estado del criterio
        /// </summary>
        public EstatusEnum Activo { get; set; }

    }
}
