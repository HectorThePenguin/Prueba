using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestParametroTrampa
    {
        [TestMethod]
        public void ObtenerParametroTrampaPorId()
        {
            var parametroTrampaPL = new ParametroTrampaPL();
            ParametroTrampaInfo parametroTrampa = parametroTrampaPL.ObtenerPorID(1);
            Assert.IsNotNull(parametroTrampa);
            Assert.IsNotNull(parametroTrampa.Parametro);
            Assert.IsNotNull(parametroTrampa.Trampa);
            Assert.IsTrue(parametroTrampa.Parametro.Descripcion.Length > 0);
            Assert.IsTrue(parametroTrampa.Trampa.Descripcion.Length > 0);
        }
    }
}
