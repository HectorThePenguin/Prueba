using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class PremezclaDistribucionDetalleInfo
    {
        public int PremezclaDistribucionDetalleId{ get; set; }
	    public int PremezclaDistribucionId{ get; set; }
	    public int OrganizacionId{ get; set; }
	    public long CantidadASurtir{ get; set; }
        public long AlmacenMovimientoId { get; set; }
	    public EstatusEnum Activo{ get; set; }
	    public DateTime FechaCreacion{ get; set; }
	    public int UsuarioCreacionId{ get; set; }
	    public DateTime FechaModificacion{ get; set; }
        public int UsuarioModificacionId { get; set; }
    }
}
