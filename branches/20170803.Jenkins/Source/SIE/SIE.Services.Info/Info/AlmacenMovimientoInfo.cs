using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class AlmacenMovimientoInfo 
    {
        /// <summary>
        ///     Identificador AnimalID .
        /// </summary>
        public long AnimalID { get; set; }
        /// <summary>
        /// ID del registro
        /// </summary>
        public long AlmacenMovimientoID { get; set; }
        /// <summary>
        /// ID del almacen
        /// </summary>
        public int AlmacenID { get; set; }

        /// <summary>
        /// entidad del Almacen
        /// </summary>
        public AlmacenInfo Almacen { get; set; }

        /// <summary>
        /// Tipo de movimiento
        /// </summary>
        public int TipoMovimientoID { get; set; }
        /// <summary>
        /// Proveedor
        /// </summary>
        public int ProveedorId { get; set; }
        /// <summary>
        /// Folio del movimiento
        /// </summary>
        public long FolioMovimiento { get; set; }
        /// <summary>
        /// Fecha movimiento
        /// </summary>
        public DateTime FechaMovimiento { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Animal movimiento
        /// </summary>
        public long AnimalMovimientoID { get; set; }
        /// <summary>
        /// FechaCreacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        ///     Identificador AnimalID .
        /// </summary>
        public int CostoID { get; set; }
        /// <summary>
        /// FechaModificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Nombre del usuario creacion
        /// </summary>
        public string NombreUsuarioCreacion { get; set; }
        /// <summary>
        /// Nombre del usuario creacion
        /// </summary>
        public string NombreTipoMovimiento { get; set; }

        /// <summary>
        /// Id del Usuario de Creacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Id del Usuario de Modificación
        /// </summary>
        public int UsuarioModificacionID { get; set; }

        /// <summary>
        /// Nombre del usuario creacion
        /// </summary>
        public List<AlmacenMovimientoDetalle> ListaAlmacenMovimientoDetalle { get; set; }
        
        /// <summary>
        /// Entidad de almacen movimiento costo
        /// </summary>
        public AlmacenMovimientoCostoInfo AlmacenMovimientoCosto { get; set; }

        /// <summary>
        /// Entidad que muestra toda la lista de los costos movimientos
        /// </summary>
        public List<AlmacenMovimientoCostoInfo> ListaAlmacenMovimientoCosto { get; set; }

        public int OrganizacionID { get; set; }

        /// <summary>
        /// Identifica si la poliza de Salida de Consumo se cargara a la cuenta de Diesel o a la de Costos
        /// </summary>
        public bool EsCuentaDiesel { get; set; }

        public TipoMovimientoInfo TipoMovimiento { get; set; }

        /// <summary>
        /// Especifica si el movimiento es una salida por envio de producto
        /// </summary>
        public bool EsEnvioAlimento { get; set; }
    }
}
