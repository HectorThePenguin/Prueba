using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    /// <summary>
    /// Info para los tipos de cuentas que se pueden consultar en la conciliacion de polizas SIAP vs SAP
    /// </summary>
    public class TiposCuentaConciliacionInfo
    {
        /// <summary>
        /// Descripcion del tipo de cuenta
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Prefijo a no mas de 10 posiciones con el que se consultaran las cuentas de cada poliza
        /// </summary>
        public string Prefijo { get; set; }
    }
}
