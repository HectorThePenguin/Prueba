using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ContratoHumedadInfo
    {
        /// <summary>
        /// Id del registro
        /// </summary>
        public int ContratoHumedadID { set; get; }

        /// <summary>
        /// Contrato id al que pertenece
        /// </summary>
        public int ContratoID { set; get; }

        /// <summary>
        /// Fecha inicio del contrato humedad
        /// </summary>
        public DateTime FechaInicio { set; get; }

        /// <summary>
        /// Porcentaje de humedad capturado
        /// </summary>
        public decimal PorcentajeHumedad { set; get; }

        /// <summary>
        /// Estatus del registro
        /// </summary>
        public EstatusEnum Activo { set; get; }

        /// <summary>
        /// Fecha creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Indica si la humedad ya fue guardada
        /// </summary>
        public bool Guardado { get; set; }
    }
}
