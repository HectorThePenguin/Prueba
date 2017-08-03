using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestUsuario
    {
        [TestMethod]
        public void ObtenerUsuarioPorId()
        {
            var usuarioPL = new UsuarioPL();
            UsuarioInfo usuario = usuarioPL.ObtenerPorID(3);
            Assert.IsNotNull(usuario);
            Assert.IsNotNull(usuario.Organizacion);
            Assert.IsTrue(usuario.Organizacion.Descripcion.Length > 0);
        }
    }
}
