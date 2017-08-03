using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoObservacion
    {
        [TestMethod]
        public void ObtenerTipoObservacionPorId()
        {
            var tipoObservacionPL = new TipoObservacionPL();
            TipoObservacionInfo tipoObservacion = tipoObservacionPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoObservacion);
            Assert.IsTrue(tipoObservacion.Descripcion.Length > 0);
        }
    }
}
