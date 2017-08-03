using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoCorral
    {
        [TestMethod]
        public void ObtenerTipoCorralPorId()
        {
            var tipoCorralPL = new TipoCorralPL();
            TipoCorralInfo tipoCorral = tipoCorralPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoCorral);
            Assert.IsTrue(tipoCorral.Descripcion.Length > 0);
        }
    }
}
