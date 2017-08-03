using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestOrganizacion
    {
        [TestMethod]
        public void ObtenerOrganizacionPorId()
        {
            var organizacionPL = new OrganizacionPL();
            OrganizacionInfo organizacion = organizacionPL.ObtenerPorID(1);
            Assert.IsNotNull(organizacion);
            Assert.IsNotNull(organizacion.TipoOrganizacion);
            Assert.IsTrue(organizacion.TipoOrganizacion.Descripcion.Length > 0);
        }

        [TestMethod]
        public void ObtenerOrganizacionPorIdConIva()
        {
            var organizacionPL = new OrganizacionPL();
            OrganizacionInfo organizacion = organizacionPL.ObtenerPorIdConIva(4);
            Assert.IsNotNull(organizacion);
            Assert.IsNotNull(organizacion.TipoOrganizacion);
            Assert.IsNotNull(organizacion.Iva);
            Assert.IsNotNull(organizacion.Iva.CuentaPagar);
            Assert.IsNotNull(organizacion.Iva.CuentaRecuperar);
            Assert.IsNotNull(organizacion.TipoOrganizacion);
            Assert.IsTrue(organizacion.TipoOrganizacion.Descripcion.Length > 0);
            Assert.IsTrue(organizacion.Iva.CuentaPagar.ClaveCuenta.Length > 0);
            Assert.IsTrue(organizacion.Iva.CuentaRecuperar.ClaveCuenta.Length > 0);
            Assert.IsTrue(organizacion.TipoOrganizacion.Descripcion.Length > 0);
        }
    }
}
