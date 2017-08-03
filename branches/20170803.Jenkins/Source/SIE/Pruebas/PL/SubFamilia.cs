using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestSubFamilia
    {
        [TestMethod]
        public void ObtenerSubFamiliaPorId()
        {
            var subFamiliaPL = new SubFamiliaPL();
            SubFamiliaInfo subFamilia = subFamiliaPL.ObtenerPorID(1);
            Assert.IsNotNull(subFamilia);
            Assert.IsNotNull(subFamilia.Familia);
            Assert.IsTrue(subFamilia.Descripcion.Length > 0);
            Assert.IsTrue(subFamilia.Familia.Descripcion.Length > 0);
        }
    }
}
