using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestRetencion
    {
        [TestMethod]
        public void ObtenerRetencionPorId()
        {
            var retencionPL = new RetencionPL();
            RetencionInfo retencion = retencionPL.ObtenerPorID(1);
            Assert.IsNotNull(retencion);
            Assert.IsTrue(retencion.Descripcion.Length > 0);
        }
    }
}
