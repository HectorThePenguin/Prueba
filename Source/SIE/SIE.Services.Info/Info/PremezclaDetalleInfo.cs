using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class PremezclaDetalleInfo
    {
        /// <summary>
        /// Id de premezcla detalle
        /// </summary>
        public int PremezclaDetalleID { get; set; }

        /// <summary>
        /// Id de la premezcla correspondiente
        /// </summary>
        public PremezclaInfo Premezcla { get; set; }

        /// <summary>
        /// Id de la premezcla en Producto
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Porcentaje correspondiente al detalle
        /// </summary>
        public decimal Porcentaje { get; set; }

        /// <summary>
        /// Kilogramos del producto que componen la premezcla
        /// </summary>
        public decimal Kilogramos { get; set; }

        /// <summary>
        /// Estatus del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Id de usuario que creo el registro
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Id de usuario que modifico el registro
        /// </summary>
        public int UsuarioModificacionId { get; set; }

        /// <summary>
        /// Indica si el registro fue guardado anteriormente
        /// </summary>
        public bool Guardado { get; set; }

        /// <summary>
        /// Lote seleccionado de la lista de lotes disponibles
        /// </summary>
        public AlmacenInventarioLoteInfo Lote { get; set; }

        /// <summary>
        /// Lotes disponibles
        /// </summary>
        public List<AlmacenInventarioLoteInfo> LotesDisponibles { get; set; }
    }
}
