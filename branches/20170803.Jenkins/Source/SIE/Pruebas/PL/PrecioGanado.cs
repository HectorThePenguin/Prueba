using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestPrecioGanado
    {
        [TestMethod]
        public void ObtenerPrecioGanadoPorId()
        {
            var precioGanadoPL = new PrecioGanadoPL();
            PrecioGanadoInfo precioGanado = precioGanadoPL.ObtenerPorID(1);
            Assert.IsNotNull(precioGanado);
            Assert.IsNotNull(precioGanado.Organizacion);
            Assert.IsNotNull(precioGanado.TipoGanado);
            Assert.IsTrue(precioGanado.Organizacion.Descripcion.Length > 0);
            Assert.IsTrue(precioGanado.TipoGanado.Descripcion.Length > 0);
        }
    }
}
