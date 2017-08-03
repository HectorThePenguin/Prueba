using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class ConciliacionPolizasMovimientosModel
    {
        public List<PolizaInfo> PolizasFaltantes { get; set; }
        public List<PolizaInfo> PolizasMovimientosDiferentes { get; set; }
        public List<PolizaInfo> PolizasMovimientosDuplicados { get; set; }
    }
}
