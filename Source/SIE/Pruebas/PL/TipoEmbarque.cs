using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoEmbarque
    {
        [TestMethod]
        public void ObtenerTipoEmbarquePorId()
        {
            var tipoEmbarquePL = new TipoEmbarquePL();
            TipoEmbarqueInfo tipoEmbarque = tipoEmbarquePL.ObtenerPorID(1);
            Assert.IsNotNull(tipoEmbarque);
            Assert.IsTrue(tipoEmbarque.Descripcion.Length > 0);
        }
    }
}
