using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestProveedor
    {
        [TestMethod]
        public void ObtenerProveedorPorId()
        {
            var proveedorPL = new ProveedorPL();
            ProveedorInfo proveedor = proveedorPL.ObtenerPorID(1);
            Assert.IsNotNull(proveedor);
            Assert.IsNotNull(proveedor.TipoProveedor);
            Assert.IsTrue(proveedor.TipoProveedor.Descripcion.Length > 0);
        }
    }
}
