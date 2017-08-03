using System;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Info.Filtros
{
    public class FiltroImpresionTarjetaRecepcion
    {
        /// <summary>
        /// Fecha de busqueda de tarjetas
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Organizacion de la recepcion
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// Entrada de ganado 
        /// </summary>
        public ImpresionTarjetaRecepcionModel EntradaGanado { get; set; }
    }
}
