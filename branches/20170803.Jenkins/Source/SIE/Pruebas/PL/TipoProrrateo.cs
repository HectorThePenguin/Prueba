using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoProrrateo
    {
        [TestMethod]
        public void ObtenerTipoProrrateoPorId()
        {
            var tipoProrrateoPL = new TipoProrrateoPL();
            TipoProrrateoInfo tipoProrrateo = tipoProrrateoPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoProrrateo);
            Assert.IsTrue(tipoProrrateo.Descripcion.Length > 0);
        }
    }
}
