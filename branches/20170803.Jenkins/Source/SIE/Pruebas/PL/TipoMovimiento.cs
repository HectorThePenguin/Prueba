using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoMovimiento
    {
        [TestMethod]
        public void ObtenerTipoMovimientoPorId()
        {
            var tipoMovimientoPL = new TipoMovimientoPL();
            TipoMovimientoInfo tipoMovimiento = tipoMovimientoPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoMovimiento);
            Assert.IsNotNull(tipoMovimiento.TipoPoliza);
            Assert.IsTrue(tipoMovimiento.Descripcion.Length > 0);
            Assert.IsTrue(tipoMovimiento.TipoPoliza.Descripcion.Length > 0);
        }
    }
}
