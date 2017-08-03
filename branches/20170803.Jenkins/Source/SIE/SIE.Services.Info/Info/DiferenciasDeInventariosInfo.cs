using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class DiferenciasDeInventariosInfo
    {
        /// <summary>
        /// Id de almacenmovimientodetalle
        /// </summary>
        public AlmacenMovimientoDetalle AlmacenMovimientoDetalle { get; set; }
        /// <summary>
        /// AlmacenMovimientoCorrespondiente
        /// </summary>
        public AlmacenMovimientoInfo AlmacenMovimiento { get; set; }
        /// <summary>
        /// Almacen correspondiente al movimiento
        /// </summary>
        public AlmacenInfo Almacen { get; set; }
        /// <summary>
        /// Almacen correspondiente al movimiento
        /// </summary>
        public string DescripcionAjuste { get; set; }
        /// <summary>
        /// Producto correspondiente al detalle
        /// </summary>
        public ProductoInfo Producto { get; set; }
        /// <summary>
        /// Registro correspondiente al lote
        /// </summary>
        public AlmacenInventarioLoteInfo AlmacenInventarioLote { get; set; }
        /// <summary>
        /// Estatos del movimiento
        /// </summary>
        public EstatusInfo Estatus { get; set; }
        /// <summary>
        /// Kilogramos totales del lote
        /// </summary>
        public decimal KilogramosTotales { get; set; }
        /// <summary>
        /// Kilogramos fisicos (capturar)
        /// </summary>
        public decimal KilogramosFisicos { get; set; }
        /// <summary>
        /// Kilogramos teoricos
        /// </summary>
        public decimal KilogramosTeoricos { get; set; }
        /// <summary>
        /// Porcentaje correspondiente al ajuste
        /// </summary>
        public decimal PorcentajeAjuste { get; set; }
        /// <summary>
        /// Indica si el ajuste puede ser editado
        /// </summary>
        public bool Editable { get; set; }
        /// <summary>
        /// Indica si el ajuste fue guardado
        /// </summary>
        public bool Guardado { get; set; }
        /// <summary>
        /// Indica si el registro se puede eliminar
        /// </summary>
        public bool Eliminable { get; set; }
        /// <summary>
        /// Indica si es permitido guardar el ajuste
        /// </summary>
        public bool PorcentajePermitido { get; set; }
        /// <summary>
        /// Requiere autorizacion
        /// </summary>
        public bool RequiereAutorizacion { get; set; }
        /// <summary>
        /// Usuario que crea el registro
        /// </summary>
        public int UsuarioCreacionId { get; set; }
        /// <summary>
        /// Indica si el objeto esta seleccionado
        /// </summary>
        public bool Seleccionado { get; set; }
        /// <summary>
        /// Indica si producto tiene configuracion
        /// </summary>
        public bool TieneConfiguracion { get; set; }
        /// <summary>
        /// Porcentaje de ajuste permitido para merma
        /// </summary>
        public decimal PorcentajeAjustePermitidoMerma { get; set; }
        /// <summary>
        /// Porcentaje de ajuste permitido para superavit
        /// </summary>
        public decimal PorcentajeAjustePermitidoSuperavit { get; set; }
        /// <summary>
        /// Porcentaje de ajuste permitido para superavit
        /// </summary>
        public decimal PorcentajeAjustePermitido { get; set; }

        /// <summary>
        /// Cantidad de la diferencia de inventario
        /// </summary>
        public decimal DiferenciaInventario { get; set; }
    }
}
