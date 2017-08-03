using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class AplicacionConsumoModel
    {
        /// <summary>
        /// Fecha del Consumo
        /// </summary>
        public DateTime FechaConsumo { get; set; }
        /// <summary>
        /// Organizacion del Consumo
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// Detalle de los Repartos
        /// </summary>
        public List<AplicacionConsumoDetalleModel> ListaFormulas { get; set; }
    }
}
