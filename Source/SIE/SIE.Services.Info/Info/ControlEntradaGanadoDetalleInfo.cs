using SIE.Services.Info.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ControlEntradaGanadoDetalleInfo
    {
        /// <summary>
        /// Identificador de Control Entrda Ganado Detallae
        /// </summary>
        public long ControlEntradaGanadoDetalleID { get; set; }

        /// <summary>
        /// Identificador control entrada ganado 
        /// </summary>
        public long ControlEntradaGanadoID { get; set; }

        /// <summary>
        /// Costo
        /// </summary>
        public CostoInfo Costo { get; set; }
        
        /// <summary>
        /// Importe
        /// </summary>
        public decimal Importe { get; set; }
        
        /// <summary>
        /// Estatus
        /// </summary>
        public EstatusEnum Activo { get; set; }
        
        /// <summary>
        /// Usuario creacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Fecha creacion del control
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario creacion
        /// </summary>
        public int UsuarioModificaID { get; set; }

        /// <summary>
        /// Fecha modificacion del control
        /// </summary>
        public DateTime FechaModificacion { get; set; }
    }
}
