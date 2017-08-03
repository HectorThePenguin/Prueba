using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestOperador
    {
        [TestMethod]
        public void ObtenerOperadorPorId()
        {
            var operadorPL = new OperadorPL();
            OperadorInfo operador = operadorPL.ObtenerPorID(1);
            Assert.IsNotNull(operador);
            Assert.IsNotNull(operador.Organizacion);
            Assert.IsNotNull(operador.Rol);
            Assert.IsNotNull(operador.Usuario);
            Assert.IsNotNull(operador.Usuario.Organizacion);
            Assert.IsTrue(operador.Organizacion.Descripcion.Length > 0);
            Assert.IsTrue(operador.Rol.Descripcion.Length > 0);
            Assert.IsTrue(operador.Usuario.Nombre.Length > 0);
            Assert.IsTrue(operador.Usuario.Organizacion.Descripcion.Length > 0);
        }

        [TestMethod]
        public void ObtenerOperadorPorRolId()
        {
            var operadorPL = new OperadorPL();
            IList<OperadorInfo> operador = operadorPL.ObtenerPorIDRol(4, 10);
            Assert.IsNotNull(operador);            
            Assert.IsTrue(operador.Count > 0);
        }

        [TestMethod]
        public void ObtenerSoloRolSanidadPorPagina()
        {
            var pl = new OperadorPL();
            var pagina = new PaginacionInfo
                             {
                                 Inicio = 1,
                                 Limite = 15
                             };

            var operador = new OperadorInfo {Organizacion = new OrganizacionInfo {OrganizacionID = 4}};
            ResultadoInfo<OperadorInfo> listaPaginada = pl.ObtenerSoloRolSanidadPorPagina(pagina, operador);
            Assert.AreNotEqual(listaPaginada, null);
        }

        [TestMethod]
        public void ObtenerSoloRolSanidadPorID()
        {
            var operadorPL = new OperadorPL();
            var filtro = new OperadorInfo
                               {
                                   OperadorID = 12,
                                   Organizacion = new OrganizacionInfo { OrganizacionID = 4 }
                               };

            OperadorInfo operador = operadorPL.ObtenerSoloRolSanidadPorID(filtro);
            Assert.IsNotNull(operador);

            filtro.OperadorID = 1;
            operador = operadorPL.ObtenerSoloRolSanidadPorID(filtro);
            Assert.IsNull(operador);
           
        }
    }
}
