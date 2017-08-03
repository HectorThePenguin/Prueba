using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ProgramacionMateriaPrimaInfo
    {
        /// <summary>
        /// Identificador unico de la programacion de materia prima.
        /// </summary>
        public int ProgramacionMateriaPrimaId { get; set; }

        /// <summary>
        /// Detalle del Pedido a Programar
        /// </summary>
        public int PedidoDetalleId { get; set; }

        /// <summary>
        /// Almacen donde se localiza el producto.
        /// </summary>
        public AlmacenInfo Almacen { get; set; }

        /// <summary>
        /// Organizacion del producto que se programara la cantidad.
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Lote de la materia prima
        /// </summary>
        public AlmacenInventarioLoteInfo InventarioLoteOrigen { get; set; }

        /// <summary>
        /// Cantidad de producto que se programo para ser entregado.
        /// </summary>
        public decimal CantidadProgramada { get; set; }

        /// <summary>
        /// Cantidad de producto que se ha entregado.
        /// </summary>
        public decimal CantidadEntregada { get; set; }

        /// <summary>
        /// Observaciones que se realizaron cuando se realizo la programación.
        /// </summary>
        public String Observaciones { get; set; }

        /// <summary>
        /// Justificación de la programacion del producto.
        /// </summary>
        public String Justificacion { get; set; }

        /// <summary>
        /// Fecha en la que se programo la solicitud.
        /// </summary>
        public DateTime FechaProgramacion { get; set; }

        /// <summary>
        /// Estatus
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha de Creación
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario de Creación
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
        /// Lista de los ticket generados en bascula.
        /// </summary>
        public List<PesajeMateriaPrimaInfo> PesajeMateriaPrima { get; set; } 
    }
}
