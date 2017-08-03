using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestRol
    {
        [TestMethod]
        public void ObtenerRolPorId()
        {
            var rolPL = new RolPL();
            RolInfo rol = rolPL.ObtenerPorID(1);
            Assert.IsNotNull(rol);
            Assert.IsTrue(rol.Descripcion.Length > 0);
        }
    }
}
