using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System;

namespace SIE.Services.Info.Info
{
    public class CancelacionMovimientoInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int CancelacionMovimientoId{get;set;}

        /// <summary>
        /// Tipo de cancelacion del movimiento
        /// </summary>
        public TipoCancelacionInfo TipoCancelacion{get;set;}

        /// <summary>
        /// Pedido cancelado
        /// </summary>
        public PedidoInfo Pedido {get;set;}

        /// <summary>
        /// Ticket cancelado
        /// </summary>
        public int Ticket{get;set;}

        /// <summary>
        /// Movimiento origen cancelado
        /// </summary>
        public AlmacenMovimientoInfo AlmacenMovimientoOrigen {get;set;}

        /// <summary>
        /// Movimiento de cancelacion
        /// </summary>
        public AlmacenMovimientoInfo AlmacenMovimientoCancelado {get;set;}

        /// <summary>
        /// Fecha de la cancelacion
        /// </summary>
        public DateTime FechaCancelacion {get;set;}

        /// <summary>
        /// Justificacion para la cancelacion
        /// </summary>
        public String Justificacion	{get;set;}
    }
}
