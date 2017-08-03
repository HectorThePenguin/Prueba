using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTipoOrganizacion
    {
        [TestMethod]
        public void ObtenerTipoOrganizacionPorId()
        {
            var tipoOrganizacionPL = new TipoOrganizacionPL();
            TipoOrganizacionInfo tipoOrganizacion = tipoOrganizacionPL.ObtenerPorID(1);
            Assert.IsNotNull(tipoOrganizacion);
            Assert.IsNotNull(tipoOrganizacion.TipoProceso);
            Assert.IsTrue(tipoOrganizacion.TipoProceso.Descripcion.Length > 0);
        }
    }
}
