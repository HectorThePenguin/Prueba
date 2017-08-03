using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoProceso
    {
        [TestMethod]
        public void ObtenerTipoProcesoPorId()
        {
            var tipoProcesoPL = new TipoProcesoPL();
            TipoProcesoInfo tipoProceso = tipoProcesoPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoProceso);
            Assert.IsTrue(tipoProceso.Descripcion.Length > 0);
        }
    }
}
