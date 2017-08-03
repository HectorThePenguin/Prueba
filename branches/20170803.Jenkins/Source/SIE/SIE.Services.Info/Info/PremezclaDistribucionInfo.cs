using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class PremezclaDistribucionInfo
    {
        public int ProveedorId { get; set; }
        public int Iva { get; set; }
        public int PremezclaDistribucionId { get; set; }
	    public int ProductoId { get; set; }
	    public DateTime FechaEntrada { get; set; }
	    public long CantidadExistente { get; set; }
	    public decimal CostoUnitario { get; set; }
        public EstatusEnum Activo { get; set; }
	    public DateTime FechaCreacion { get; set; }
	    public int UsuarioCreacionId { get; set; }
	    public DateTime FechaModificacion { get; set; }
	    public int UsuarioModificacionId { get; set; }
        public List<PremezclaDistribucionDetalleInfo> ListaPremezclaDistribucionDetalle { get; set; }
        public List<PremezclaDistribucionCostoInfo> ListaPremezclaDistribucionCosto { get; set; }

        /// <summary>
        /// Constructor por Default
        /// </summary>
        public PremezclaDistribucionInfo()
        {
            ListaPremezclaDistribucionDetalle = new List<PremezclaDistribucionDetalleInfo>();
            ListaPremezclaDistribucionCosto = new List<PremezclaDistribucionCostoInfo>();
        }
    }
}
