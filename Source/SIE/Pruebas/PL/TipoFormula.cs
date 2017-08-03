using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoFormula
    {
        [TestMethod]
        public void ObtenerTipoFormulaPorId()
        {
            var tipoFormulaPL = new TipoFormulaPL();
            TipoFormulaInfo tipoFormula = tipoFormulaPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoFormula);
            Assert.IsTrue(tipoFormula.Descripcion.Length > 0);
        }
    }
}
