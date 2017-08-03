using SIE.Services.Info.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ControlEntradaGanadoInfo
    {
        /// <summary>
        /// Identificador de Control entrada Ganado
        /// </summary>
        public long ControlEntradaGandoID { get; set; }

        /// <summary>
        /// Entrada ganado info
        /// </summary>
        public EntradaGanadoInfo EntradaGanado { get; set; }

        /// <summary>
        /// Animal Info
        /// </summary>
        public AnimalInfo Animal { get; set; }

        /// <summary>
        /// Estatus del animal
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

        /// <summary>
        /// Lista de control entrada ganado detalle
        /// </summary>
        public List<ControlEntradaGanadoDetalleInfo> ListaControlEntradaGanadoDetalle { get; set; }

    }
}
