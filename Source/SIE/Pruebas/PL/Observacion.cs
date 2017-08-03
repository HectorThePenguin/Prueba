using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestObservacion
    {
        [TestMethod]
        public void ObtenerObservacionPorId()
        {
            var observacionPL = new ObservacionPL();
            ObservacionInfo observacion = observacionPL.ObtenerPorID(1);
            Assert.IsNotNull(observacion);
            Assert.IsNotNull(observacion.TipoObservacion);
            Assert.IsTrue(observacion.TipoObservacion.Descripcion.Length > 0);
        }
    }
}
