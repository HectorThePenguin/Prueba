using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoGanado
    {
        [TestMethod]
        public void ObtenerTipoGanadoPorId()
        {
            var tipoGanadoPL = new TipoGanadoPL();
            TipoGanadoInfo tipoGanado = tipoGanadoPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoGanado);
            Assert.IsTrue(tipoGanado.Descripcion.Length > 0);
        }
    }
}
