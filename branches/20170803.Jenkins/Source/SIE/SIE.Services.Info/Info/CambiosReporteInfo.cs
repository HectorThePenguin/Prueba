using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class CambiosReporteInfo
    {
        public int OrganizacionID { get; set; }

        public int UsuarioModificacionID { get; set; }

        public string Lote { get; set; }

        public int TipoServicioID { get; set; }

        public int FormulaIDProgramada { get; set; }

        public int EstadoComederoID { get; set; }

        public int CantidadProgramada { get; set; }

        public string Observaciones { get; set; }

        public DateTime FechaReparto { get; set; }

        public CorralInfo CorralInfo { get; set; }

        public long RepartoID { get; set; }

        public long RepartoDetalleIdManiana { get; set; }

        public long RepartoDetalleIdTarde { get; set; }

        public int CantidadServida { get; set; }

        public int Servido { get; set; }

        public int ValidaPorcentaje { get; set; }

        public bool InactivarDetalle { get; set; }

        public bool CambiarLote { get; set; }

        public int Cabezas { get; set; }

        public int CantidadProgramadaOriginal { get; set; }

        public bool NoModificar { get; set; }
    }
}
