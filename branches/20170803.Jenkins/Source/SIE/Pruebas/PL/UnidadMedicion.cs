using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestUnidadMedicion
    {
        [TestMethod]
        public void ObtenerUnidadMedicionPorId()
        {
            var unidadMedicionPL = new UnidadMedicionPL();
            UnidadMedicionInfo unidadMedicion = unidadMedicionPL.ObtenerPorID(1);
            Assert.IsNotNull(unidadMedicion);
            Assert.IsTrue(unidadMedicion.Descripcion.Length > 0);
        }
    }
}
