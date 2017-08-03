using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class EntradaGanadoCosteoInfo : BitacoraInfo
    {
        /// <summary>
        ///     Identificador del costeo de la entrada de ganado
        /// </summary>
        public int EntradaGanadoCosteoID { get; set; }

        /// <summary>
        ///     Id de la organización
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        ///     Id de la entrada de ganado
        /// </summary>
        public int EntradaGanadoID { get; set; }

        /// <summary>
        ///     Observaciones
        /// </summary>
        public string Observacion { get; set; }

        /// <summary>
        /// Indica los dias de Estancia promedio que duro el ganado en el Centro
        /// </summary>
        public int DiasEstancia { set; get; }

        /// <summary>
        ///     Detalle de la calidad del ganado
        /// </summary>
        public List<EntradaGanadoCalidadInfo> ListaCalidadGanado { set; get; }

        /// <summary>
        ///     Detalle de los tipos de ganado
        /// </summary>
        public List<EntradaDetalleInfo> ListaEntradaDetalle { set; get; }

        /// <summary>
        ///     Detalle de los costos de la entrada
        /// </summary>
        public List<EntradaGanadoCostoInfo> ListaCostoEntrada { set; get; }
    }
}
