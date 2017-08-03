using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class AlmacenCierreDiaInventarioInfo
    {
        /// <summary>
        /// Identificador del almacen
        /// </summary>
        public AlmacenInfo Almacen { get; set; }
        /// <summary>
        /// Folio del almacen
        /// </summary>
        public long FolioAlmacen { get; set; }

        /// <summary>
        /// Id del producto
        /// </summary>
        public int ProductoID { get; set; }

        /// <summary>
        /// Descripcion del producto
        /// </summary>
        public string ProductoDescripcion { get; set; }

        /// <summary>
        /// Unidad de medicion del producto
        /// </summary>
        public string ClaveUnidad { get; set; }

        /// <summary>
        /// Precio del producto
        /// </summary>
        public decimal PrecioPromedio { get; set; }

        /// <summary>
        /// Cantidad
        /// </summary>
        public decimal CantidadReal { get; set; }

        /// <summary>
        /// Importe real del producto
        /// </summary>
        public decimal ImporteReal { get; set; }

        /// <summary>
        /// Observaciones 
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Tipo de movimiento almacen
        /// </summary>
        public int TipoMovimiento { get; set; }
       
        /// <summary>
        /// estatus de almacen movimiento
        /// </summary>
        public int Estatus { get; set; }

        /// <summary>
        /// indentificador del almacen movimiento
        /// </summary>
        public long AlmacenMovimientoID { get; set; }

        /// <summary>
        /// Id usuario
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Organizacion id
        /// </summary>
        public int OrganizacionId { get; set; }

        /// <summary>
        /// Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// fecha movimiento
        /// </summary>
        public DateTime FechaMovimiento { get; set; }

    }
}
