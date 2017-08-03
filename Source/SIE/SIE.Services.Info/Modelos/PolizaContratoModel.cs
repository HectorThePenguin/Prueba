using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class PolizaContratoModel
    {
        /// <summary>
        /// Entidad que representa un contrato
        /// </summary>
        public ContratoInfo Contrato { get; set; }
        /// <summary>
        /// Lista que representa los costos ligados al contrato
        /// </summary>
        public List<CostoInfo> OtrosCostos { get; set; }

        /// <summary>
        /// Objeto para generar la Polzia de Parcialidades
        /// </summary>
        public ContratoParcialInfo ContratoParcial { get; set; }

        /// <summary>
        /// Entidad que representa un Almacen Movimiento
        /// </summary>
        public AlmacenMovimientoInfo AlmacenMovimiento { get; set; }
    }
}
