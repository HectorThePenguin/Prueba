using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestLoteReimplante
    {
        [TestMethod]
        public void ObtenerLoteReimplantePorId()
        {
            var loteReimplantePL = new LoteReimplantePL();
            LoteReimplanteInfo loteReimplante = loteReimplantePL.ObtenerPorID(1);
            Assert.IsNotNull(loteReimplante);
        }
    }
}
