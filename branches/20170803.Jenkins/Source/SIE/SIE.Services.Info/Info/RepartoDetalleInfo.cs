using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("RepartoDetalle")]
    public class RepartoDetalleInfo
    {
        /// <summary>
        /// Identificador del reparto detalle
        /// </summary>
        public long RepartoDetalleID { get; set; }
        /// <summary>
        /// Identificador del reparto
        /// </summary>
        public long RepartoID { get; set; }
        /// <summary>
        /// Identificador del tipo de servicio
        /// </summary>
        public int TipoServicioID { get; set; }
        /// <summary>
        /// Identificador de la formula programada
        /// </summary>
        public int FormulaIDProgramada { get; set; }
        /// <summary>
        /// Identificador de la formula servida
        /// </summary>
        public int FormulaIDServida { get; set; }
        /// <summary>
        /// Tipo de formula
        /// </summary>
        public int TipoFormula { get; set; }
        /// <summary>
        /// Cantidad programada
        /// </summary>
        public int CantidadProgramada { get; set; }
        /// <summary>
        /// Cantidad servida
        /// </summary>
        public int CantidadServida { get; set; }
        /// <summary>
        /// Hora reparto
        /// </summary>
        public string HoraReparto { get; set; }
        /// <summary>
        /// Costo promedio
        /// </summary>
        public decimal CostoPromedio { get; set; }
        /// <summary>
        /// Importe
        /// </summary>
        public decimal Importe { get; set; }
        /// <summary>
        /// Servido
        /// </summary>
        public bool Servido { get; set; }
        /// <summary>
        /// Cabezas
        /// </summary>
        public int Cabezas { get; set; }
        /// <summary>
        /// Identificador del estado del comedor
        /// </summary>
        public int EstadoComederoID { get; set; }
        /// <summary>
        /// Identificador del camion de reparto
        /// </summary>
        public int CamionRepartoID { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Lector registro del lote
        /// </summary>
        public LectorRegistroInfo LectorRegistro { get; set; }

        /// <summary>
        /// Indentifica si se aplico un ajuste al servicio
        /// </summary>
        public bool Ajuste { get; set; }

        /// <summary>
        /// Indentifica si se aplico el servicio
        /// </summary>
        public bool Aplicado { get; set; }

        /// <summary>
        /// ID del usuario de Creacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// ID del almacen movimiento de salida del alimento
        /// </summary>
        public long AlmacenMovimientoID { get; set; }

        /// <summary>
        /// Estatus del Detalle
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Indentifica si se Se prorrateo el costo del consumo
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public bool Prorrateo { get; set; }

        /// <summary>
        /// Indentifica la fecha del Reparto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public DateTime FechaReparto { get; set; }

        /// <summary>
        /// Division
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string Division { get; set; }

        /// <summary>
        /// Clave de la Organizacion
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Lote del reparto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int LoteID { get; set; }

        /// <summary>
        /// PrecioPromedio 
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public decimal PrecioPromedio { get; set; }
    }
}
