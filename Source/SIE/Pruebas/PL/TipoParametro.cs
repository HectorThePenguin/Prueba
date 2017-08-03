using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoParametro
    {
        [TestMethod]
        public void ObtenerTipoParametroPorId()
        {
            var tipoParametroPL = new TipoParametroPL();
            TipoParametroInfo tipoParametro = tipoParametroPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoParametro);
            Assert.IsTrue(tipoParametro.Descripcion.Length > 0);
        }
    }
}
