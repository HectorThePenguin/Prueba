using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class RecepcionProductoInfo
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int RecepcionProductoId { get; set; }

        /// <summary>
        /// Almacen que recibe
        /// </summary>
        public AlmacenInfo Almacen { get; set; }

        /// <summary>
        /// Folio de la recepcion
        /// </summary>
        public int FolioRecepcion { get; set; }

        /// <summary>
        /// Folio de la orde de compra
        /// </summary>
        public string FolioOrdenCompra { get; set; }

        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        public DateTime FechaSolicitud { get; set; }

        /// <summary>
        /// Proveedor de la recepcion
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// Fecha de recepcion del registro
        /// </summary>
        public DateTime FechaRecepcion { get; set; }

        /// <summary>
        /// Movimiento que se genero al darle entrada
        /// </summary>
        public long AlmacenMovimientoId { get; set; }

        /// <summary>
        /// Factura a la que se le dio entrada
        /// </summary>
        public string Factura { get; set; }

        /// <summary>
        /// Estado del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha en que se creo el registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public UsuarioInfo UsuarioCreacion { get; set; }

        /// <summary>
        /// Fecha en  que se modifico el registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifico el registro
        /// </summary>
        public UsuarioInfo UsuarioModificacion { get; set; }

        /// <summary>
        /// Lista de la recepcion de producto detalle
        /// </summary>
        public List<RecepcionProductoDetalleInfo> ListaRecepcionProductoDetalle{get; set; } 

        public string Observaciones { get; set; }
    }
}
