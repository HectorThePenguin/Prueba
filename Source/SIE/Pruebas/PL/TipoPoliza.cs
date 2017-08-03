using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoPoliza
    {
        [TestMethod]
        public void ObtenerTipoPolizaPorId()
        {
            var tipoPolizaPL = new TipoPolizaPL();
            TipoPolizaInfo tipoPoliza = tipoPolizaPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoPoliza);
            Assert.IsTrue(tipoPoliza.Descripcion.Length > 0);
        }
    }
}
