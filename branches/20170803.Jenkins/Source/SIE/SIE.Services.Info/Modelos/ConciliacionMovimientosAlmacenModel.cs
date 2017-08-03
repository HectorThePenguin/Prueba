using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class ConciliacionMovimientosAlmacenModel
    {
        public List<AlmacenMovimientoInfo> AlmacenesMovimientos { get; set; }
        public List<AlmacenMovimientoDetalle> AlmacenesMovimientosDetalle { get; set; }
    }
}
