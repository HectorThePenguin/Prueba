using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTrampa
    {
        [TestMethod]
        public void ObtenerTrampaPorId()
        {
            var trampaPL = new TrampaPL();
            TrampaInfo trampa = trampaPL.ObtenerPorID(1);
            Assert.IsNotNull(trampa);
            Assert.IsNotNull(trampa.Organizacion);
            Assert.IsTrue(trampa.Organizacion.Descripcion.Length > 0);
        }
    }
}
