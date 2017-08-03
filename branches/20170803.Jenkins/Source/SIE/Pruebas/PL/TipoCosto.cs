using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoCosto
    {
        [TestMethod]
        public void ObtenerTipoCostoPorId()
        {
            var tipoCostoPL = new TipoCostoPL();
            TipoCostoInfo tipoCosto = tipoCostoPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoCosto);
            Assert.IsTrue(tipoCosto.Descripcion.Length > 0);
        }
    }
}
