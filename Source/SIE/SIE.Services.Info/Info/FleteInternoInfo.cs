using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class FleteInternoInfo
    {
        /// <summary>
        /// Id correspondiente al registro en bd
        /// </summary>
        public int FleteInternoId { get; set; }

        /// <summary>
        /// Organizacion que corresponde al flete interno
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Organizacion que corresponde al flete interno
        /// </summary>
        public OrganizacionInfo OrganizacionDestino { get; set; }

        /// <summary>
        /// TipoMovimiento que corresponde al flete interno
        /// </summary>
        public TipoMovimientoInfo TipoMovimiento { get; set; }

        /// <summary>
        /// Almacen origen que corresponde al flete interno
        /// </summary>
        public AlmacenInfo AlmacenOrigen { get; set; }

        /// <summary>
        /// Almacen destino que corresponde al flete interno
        /// </summary>
        public AlmacenInfo AlmacenDestino { get; set; }

        public int AlmacenInventarioLoteId  { get; set; }

        /// <summary>
        /// Producto correspondiente al flete interno
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Indica si el registro esta activo
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifico el registro
        /// </summary>
        public int UsuarioModificacionId { get; set; }

        /// <summary>
        /// Listado de flete interno detalle
        /// </summary>
        public List<FleteInternoDetalleInfo>  ListadoFleteInternoDetalle { get; set; }

        /// <summary>
        /// Indica si el registro esta guardado
        /// </summary>
        public bool Guardado { get; set; }
    }
}
