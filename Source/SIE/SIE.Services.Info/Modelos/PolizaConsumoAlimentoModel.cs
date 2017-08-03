using SIE.Services.Info.Info;
namespace SIE.Services.Info.Modelos
{
    public class PolizaConsumoAlimentoModel
    {
        /// <summary>
        /// Objeto que identifica un reparto
        /// </summary>
        public RepartoInfo Reparto { get; set; }
        /// <summary>
        /// Objeto que indentifica una produccion formula
        /// </summary>
        public ProduccionFormulaInfo ProduccionFormula { get; set; }
        /// <summary>
        /// Objeto del AlmacenMovimiento generado
        /// </summary>
        public AlmacenMovimientoInfo AlmacenMovimiento { get; set; }
    }
}
