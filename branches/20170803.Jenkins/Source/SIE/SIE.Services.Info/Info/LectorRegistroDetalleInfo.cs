
namespace SIE.Services.Info.Info
{
    public class LectorRegistroDetalleInfo
    {
        /// <summary>
        /// Identificador del lector registro detalle
        /// </summary>
        public int LectorRegistroDetalleID { get; set; }
        /// <summary>
        /// Identificador lector del registro
        /// </summary>
        public int LectorRegistroID { get; set; }
        /// <summary>
        /// Identificador del tipo de servicio
        /// </summary>
        public int TipoServicioID { get; set; }
        /// <summary>
        /// Identificador de la formula anterior
        /// </summary>
        public int FormulaIDAnterior { get; set; }
        /// <summary>
        /// Identificador de la formula programada
        /// </summary>
        public int FormulaIDProgramada { get; set; }

    }
}
