using System.Collections.Generic;

namespace SIE.Services.Polizas.Modelos
{
    internal class PolizaModel
    {
        /// <summary>
        /// Encabezados en poliza
        /// </summary>
        public List<PolizaEncabezadoModel> Encabezados { get; set; }
        /// <summary>
        /// Detalle de la poliza
        /// </summary>
        public List<PolizaDetalleModel> Detalle { get; set; }
        /// <summary>
        /// Costos de la poliza
        /// </summary>
        public List<PolizaCostoModel> Costos { get; set; }
        /// <summary>
        /// Registro contable de la poliza
        /// </summary>
        public List<PolizaRegistroContableModel> RegistroContable { get; set; }
    }
}
