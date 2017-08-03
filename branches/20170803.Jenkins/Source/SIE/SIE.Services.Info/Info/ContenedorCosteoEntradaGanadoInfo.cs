using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class ContenedorCosteoEntradaGanadoInfo
    {
        /// <summary>
        /// Propiedad de Entrada de Ganado, para consultar los Datos de Entrada
        /// </summary>
        public EntradaGanadoInfo EntradaGanado { set; get; }

        /// <summary>
        /// Propiedad de Entrada de Ganado Costeo, para guardar y/o consultar la entrada de Ganado
        /// </summary>
        public EntradaGanadoCosteoInfo EntradaGanadoCosteo { set; get; }

        /// <summary>
        /// Propiedad de Entrada de Ganado en Transito, para consultar los Datos en Transito
        /// </summary>
        public EntradaGanadoTransitoInfo EntradaGanadoTransito { get; set; }

        /// <summary>
        /// Propiedad de Entrada Ganado Muerte, para consultar los datos de
        /// las muertes en transito
        /// </summary>
        public List<EntradaGanadoMuerteInfo> EntradasGanadoMuertes { get; set; }

        /// <summary>
        /// Constructor por Default
        /// </summary>
        public ContenedorCosteoEntradaGanadoInfo()
        {
            EntradaGanadoCosteo = new EntradaGanadoCosteoInfo
            {
                ListaCostoEntrada = new List<EntradaGanadoCostoInfo>(),
                ListaEntradaDetalle = new List<EntradaDetalleInfo>(),
                ListaCalidadGanado = new List<EntradaGanadoCalidadInfo>()
            };
        }
    }
}
