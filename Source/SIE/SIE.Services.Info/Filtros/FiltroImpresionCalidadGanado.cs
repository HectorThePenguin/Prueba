using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Info.Filtros
{
    public class FiltroImpresionCalidadGanado
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
        public ImpresionCalidadGanadoModel EntradaGanado { get; set; }
    }
}
