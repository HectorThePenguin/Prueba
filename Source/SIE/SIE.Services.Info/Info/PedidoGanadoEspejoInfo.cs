using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class PedidoGanadoEspejoInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificador del pedido ganado
        /// </summary>
        public int PedidoGanadoEspejoID { get; set; }

        /// <summary>
        /// Pedido ganado
        /// </summary>
        public PedidoGanadoInfo PedidoGanado { get; set; }

        /// <summary>
        /// Organizacion del pedido ganado
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Cabezas promedio esperadas
        /// </summary>
        public int CabezasPromedio { get; set; }

        /// <summary>
        /// Fecha de inicio
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Jaulas para el lunes
        /// </summary>
        public int Lunes { get; set; }

        /// <summary>
        /// Jaulas para el martes
        /// </summary>
        public int Martes { get; set; }

        /// <summary>
        /// Jaulas para el miercoles
        /// </summary>
        public int Miercoles { get; set; }

        /// <summary>
        /// Jaulas para el jueves
        /// </summary>
        public int Jueves { get; set; }

        /// <summary>
        /// Jaulas para el viernes
        /// </summary>
        public int Viernes { get; set; }

        /// <summary>
        /// Jaulas para el sabado
        /// </summary>
        public int Sabado { get; set; }

        /// <summary>
        /// Jaulas para el domingo
        /// </summary>
        public int Domingo { get; set; }

        /// <summary>
        /// Justificacion
        /// </summary>
        public string Justificacion { get; set; }

        /// <summary>
        /// Estado del pedido
        /// </summary>
        public bool? Estatus { get; set; }

        /// <summary>
        /// Id del usuario solicitante
        /// </summary>
        public int UsuarioSolicitanteID { get; set; }

        /// <summary>
        /// Id del usuario aprobo
        /// </summary>
        public int UsuarioAproboID { get; set; }
    }
}
