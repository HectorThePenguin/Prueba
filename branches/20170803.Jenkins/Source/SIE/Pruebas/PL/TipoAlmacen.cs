using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoAlmacen
    {
        [TestMethod]
        public void ObtenerTipoAlmacenPorId()
        {
            var tipoAlmacenPL = new TipoAlmacenPL();
            TipoAlmacenInfo tipoAlmacen = tipoAlmacenPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoAlmacen);
            Assert.IsTrue(tipoAlmacen.Descripcion.Length > 0);
        }
    }
}
