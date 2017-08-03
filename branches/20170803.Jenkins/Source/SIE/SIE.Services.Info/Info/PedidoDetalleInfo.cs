using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Annotations;

namespace SIE.Services.Info.Info
{
    public class PedidoDetalleInfo 
    {
        private decimal totalCantidadProgramada = 0 ;

        /// <summary>
        /// Identificador del detalle pedido.
        /// </summary>
        public int PedidoDetalleId { get; set; }

        /// <summary>
        /// Identificador del pedido.
        /// </summary>
        public int PedidoId { get; set; }

        /// <summary>
        /// Producto
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Lote donde se almacenara el producto
        /// </summary>
        public AlmacenInventarioLoteInfo InventarioLoteDestino { get; set; }

        /// <summary>
        /// Cantidad del producto solicitada en el pedido
        /// </summary>
        public decimal CantidadSolicitada { get; set; }

        /// <summary>
        /// Observaciones del detalle de pedido.
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Estatus
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha Creación
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario Creación
        /// </summary>
        public UsuarioInfo UsuarioCreacion { get; set; }

        /// <summary>
        /// Fecha de Modificación
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario de Modificación
        /// </summary>
        public UsuarioInfo UsuarioModificacion { get; set; }

        /// <summary>
        /// Programacion de la materia prima que se solicita en el pedido
        /// </summary>
        public List<ProgramacionMateriaPrimaInfo> ProgramacionMateriaPrima { get; set; }

        /// <summary>
        /// Devuelve la cantidad programada del detalle
        /// </summary>
        /// <returns></returns>
        public decimal TotalCantidadProgramada
        {
            get
            {
                if (ProgramacionMateriaPrima != null)
                {
                    totalCantidadProgramada = (from programacion in ProgramacionMateriaPrima
                             select programacion.CantidadProgramada).Sum();
                }

                return totalCantidadProgramada;

            }
            set { totalCantidadProgramada = value; }
        }
        /// <summary>
        /// Lote 
        /// </summary>
        public int LoteSelecionado { get; set; }
        /// <summary>
        /// Lote en uso
        /// </summary>
        public int LoteUso { get; set; }

    }
}
