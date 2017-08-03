using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ReporteSalidasConCostoParametrosInfo
    {
        public int OrganizacionID {get; set;}
        public DateTime FechaInicial  {get; set;}
        public DateTime FechaFinal {get; set;}
        public int TipoSalida {get; set;}
        public int TipoProceso {get; set;}
        public Boolean EsDetallado { get; set; }
    }
}
