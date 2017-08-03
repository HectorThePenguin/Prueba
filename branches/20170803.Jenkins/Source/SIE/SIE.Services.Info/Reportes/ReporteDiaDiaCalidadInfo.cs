
//--*********** Info *************
using System;
using SIE.Services.Info.Info;
namespace SIE.Services.Info.Reportes
{
    public class ReporteDiaDiaCalidadInfo
    {
        /// <summary>
        /// Producto
        /// </summary>
        public string Producto { get; set; }
        /// <summary>
        /// Indicador
        /// </summary>
        public string Indicador { get; set; }
        /// <summary>
        /// Resultado
        /// </summary>
        public Decimal Resultado { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// RangoObjetivo
        /// </summary>
        public string RangoObjetivo { get; set; }

        /// <summary>
        /// Organizacion
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Titulo
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Fecha
        /// </summary>
        public DateTime Fecha { get; set; }
    }
}
