using System;

namespace SIE.Services.Info.Info
{
    public class EmbarqueTarifaInfo : BitacoraInfo
    {
        /// <summary>
        ///   Identificador de la tarifa de Embarque.
        /// </summary>
        public int EmbarqueTarifaID { set; get; }

        /// <summary>
        ///     Importe de la tarifa del Embarque
        /// </summary>
        public decimal Importe { set; get; }

        /// <summary>
        ///     Proveedor de la tarifa del Embarque
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        ///     Detalle del Embarque
        /// </summary>
        public ConfiguracionEmbarqueDetalleInfo ConfiguracionEmbarqueDetalle { get; set; }

    }
}
