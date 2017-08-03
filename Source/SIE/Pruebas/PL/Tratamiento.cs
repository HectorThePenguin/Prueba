using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTratamiento
    {
        [TestMethod]
        public void ObtenerTratamientoPorId()
        {
            var tratamientoPL = new TratamientoPL();
            TratamientoInfo tratamiento = tratamientoPL.ObtenerPorID(3);
            Assert.IsNotNull(tratamiento);
            Assert.IsNotNull(tratamiento.Organizacion);
            Assert.IsNotNull(tratamiento.TipoTratamientoInfo);
            Assert.IsTrue(tratamiento.Organizacion.Descripcion.Length > 0);
            Assert.IsTrue(tratamiento.TipoTratamientoInfo.Descripcion.Length > 0);
        }
    }
}
