using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class FleteInternoCostoInfo
    {
        /// <summary>
        /// Identificador del costo
        /// </summary>
        public int FleteInternoCostoId { get; set; }

        /// <summary>
        /// Flete interno detalle id 
        /// </summary>
        public int FleteInternoDetalleId { get; set; }

        /// <summary>
        /// Costo id del flete interno costo
        /// </summary>
        public CostoInfo Costo { get; set; }

        /// <summary>
        /// Tarifa del costo
        /// </summary>
        public decimal Tarifa { get; set; }

        /// <summary>
        /// Tarifa convertida
        /// </summary>
        public decimal TarifaKilos { get; set; }

        /// <summary>
        /// Indica si el registro esta activo
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha creacion del registro
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
        /// Usuario que modifica el registro
        /// </summary>
        public int UsuarioModificacionId { get; set; }

        /// <summary>
        /// Indica si el registro esta guardado
        /// </summary>
        public bool Guardado { get; set; }

        /// <summary>
        /// Indica si el registro se elimino en pantalla
        /// </summary>
        public bool Eliminado { get; set; }

        /// <summary>
        /// Indica si el registro fue modificado
        /// </summary>
        public bool Modificado { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public int TipoTarifaID { get; set; }
    }
}
