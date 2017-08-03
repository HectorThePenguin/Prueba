using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestParametro
    {
        [TestMethod]
        public void ObtenerParametroPorId()
        {
            var parametroPL = new ParametroPL();
            ParametroInfo parametro = parametroPL.ObtenerPorID(1);
            Assert.IsNotNull(parametro);
            Assert.IsNotNull(parametro.TipoParametro);
            Assert.IsTrue(parametro.Descripcion.Length > 0);
            Assert.IsTrue(parametro.TipoParametro.Descripcion.Length > 0);
        }
    }
}
