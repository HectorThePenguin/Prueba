using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestParametroOrganizacion
    {
        [TestMethod]
        public void ObtenerParametroOrganizacionPorId()
        {
            var parametroOrganizacionPL = new ParametroOrganizacionPL();
            ParametroOrganizacionInfo parametroOrganizacion = parametroOrganizacionPL.ObtenerPorID(1);
            Assert.IsNotNull(parametroOrganizacion);
            Assert.IsNotNull(parametroOrganizacion.Organizacion);
            Assert.IsNotNull(parametroOrganizacion.Parametro);
            Assert.IsTrue(parametroOrganizacion.Organizacion.Descripcion.Length > 0);
            Assert.IsTrue(parametroOrganizacion.Parametro.Descripcion.Length > 0);
        }
    }
}
