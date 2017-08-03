using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    /// <summary>
    /// Clase para el manejo de los parametros que se utilizaran en la validacion
    /// </summary>
    public class ParametrosValidacionLimiteCredito
    {
        /// <summary>
        /// Codigo SAP del cliente
        /// </summary>
        public string CodigoSAP { get; set; }

        /// <summary>
        /// Importe
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Abreviatura del tipo de moneda usada
        /// </summary>
        public string Moneda { get; set; }

        /// <summary>
        /// Sociedad a la que pertenece el cliente
        /// </summary>
        public string Sociedad { get; set; }
    }
}
