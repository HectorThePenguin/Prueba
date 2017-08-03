
namespace SIE.Services.Info.Info
{
    public class ParametroCapturaManualConsumoInfo
    {
        /// <summary>
        /// FormulaId que se repartira para consumo
        /// </summary>
        public int FormulaId;

        /// <summary>
        /// OrganizacionId del usuario que se logueo
        /// </summary>
        public int OrganizacionId;

        /// <summary>
        /// UsuarioId que se logueo
        /// </summary>
        public int UsuarioId;

        /// <summary>
        /// Kilogramos que se sirvieron
        /// </summary>
        public int KilogramosServidos;

        /// <summary>
        /// Id Del tipo de corral al que se le esta haciendo el reparto.
        /// </summary>
        public int TipoCorralId;

        /// <summary>
        /// Es la hora de la maquina al momento de guardar.
        /// </summary>
        public string HoraSistema;

        /// <summary>
        /// Es el lote al que se le descontara del inventario
        /// </summary>
        public int LoteId { get; set; }
    }
}
