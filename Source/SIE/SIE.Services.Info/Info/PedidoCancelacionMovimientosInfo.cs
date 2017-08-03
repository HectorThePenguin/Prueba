using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class PedidoCancelacionMovimientosInfo
    {
        public int OrganizacionId { get; set; }

        public int PedidoId { get; set; }

        public int FolioPedido { get; set; }

        public ProductoInfo Producto { get; set; }

        public decimal CantidadSolicitada {get;set;}

        public decimal CantidadProgramada { get; set; }

        public AlmacenInventarioLoteInfo AlmacenInventarioLoteOrigen { get; set; }

        public int Ticket { get; set; }

        public decimal CantidadEntregada { get; set; }

        public AlmacenInventarioLoteInfo AlmacenInventarioLoteDestino { get; set; }

        public decimal CantidadPendiente { get; set; }

        public int ProgramacionMateriaPrimaId { get; set; }

        public int PesajeMateriaPrimaId { get; set; }

        public AlmacenMovimientoInfo AlmacenMovimientoOrigen { get; set; }

        public AlmacenMovimientoInfo AlmacenMovimientoDestino { get; set; }

        public bool CancelarProgramacion { get; set; }

        public int UsuarioID { get; set; }

        public bool Seleccionado { get; set; }
    }
}
