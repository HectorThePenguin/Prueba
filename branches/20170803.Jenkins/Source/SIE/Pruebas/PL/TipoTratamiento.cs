using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoTratamiento
    {
        [TestMethod]
        public void ObtenerTipoTratamientoPorId()
        {
            var tipoTratamientoPL = new TipoTratamientoPL();
            TipoTratamientoInfo tipoTratamiento = tipoTratamientoPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoTratamiento);
            Assert.IsTrue(tipoTratamiento.Descripcion.Length > 0);
        }
    }
}
