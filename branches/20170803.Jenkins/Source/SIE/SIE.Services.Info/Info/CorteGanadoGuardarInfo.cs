using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class CorteGanadoGuardarInfo
    {
        /// <summary>
        /// Info de la entrada de ganado
        /// </summary>
        public EntradaGanadoInfo EntradaGanado { get; set; }

        /// <summary>
        /// Lote origen 
        /// </summary>
        public LoteInfo LoteOrigen { get; set; }

        /// <summary>
        /// Tipo Formula info
        /// </summary>
        public TipoFormulaInfo TipoFormula { get; set; }

        /// <summary>
        /// Tipo Servicio info 
        /// </summary>
        public TipoServicioInfo TipoServicioInfo { get; set; }

        /// <summary>
        /// Tipo Servicio info 
        /// </summary>
        public int UsuarioCreacionID { get; set; }

    }
}
