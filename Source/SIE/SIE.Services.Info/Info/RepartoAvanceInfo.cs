
namespace SIE.Services.Info.Info
{
    public class RepartoAvanceInfo
    {
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public int UsuarioID { get; set; }
        /// <summary>
        /// Seccion actual del proceso
        /// </summary>
        public string Seccion { get; set; }
        /// <summary>
        /// Total de corrales
        /// </summary>
        public int TotalCorrales { get; set; }
        /// <summary>
        /// Total de corrales de la seccion
        /// </summary>
        public int TotalCorralesSeccion { get; set; }
        /// <summary>
        /// Total de corrales procesados
        /// </summary>
        public int TotalCorralesProcesados { get; set; }
        /// <summary>
        /// Total de corrales procesados por seccion
        /// </summary>
        public int TotalCorralesProcesadosSeccion { get; set; }
        /// <summary>
        /// Porcentaje por seccion
        /// </summary>
        public int PorcentajeSeccion { get; set; }
        /// <summary>
        /// Porcentaje total
        /// </summary>
        public int PorcentajeTotal { get; set; }
        /// <summary>
        /// Indica si ocurrio un error
        /// </summary>
        public int EstatusError { get; set; }
    }
}
