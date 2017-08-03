using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class Corral
    {
        /// <summary>
        /// Probar que regrese una lista paginada.
        /// </summary>
        [TestMethod]
        public void CorralObtenerPorPagina()
        {
            var pl = new CorralPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new CorralInfo { Codigo = string.Empty };

            ResultadoInfo<CorralInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreNotEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que regrese una lista vacia.
        /// </summary>
        [TestMethod]
        public void CorralObtenerPorPaginaSinDatos()
        {
            var pl = new CorralPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new CorralInfo { Codigo = "0000000000" };

            ResultadoInfo<CorralInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que cree una entidad
        /// </summary>
        [TestMethod]
        public void CorralCrear()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);

            var pl = new CorralPL();
            var corral = new CorralInfo
            {
                CorralID = 0,
                Codigo = string.Format("C{0:D9}", randomNumber),
                Organizacion = new OrganizacionInfo{ OrganizacionID = 4},
                TipoCorral = new TipoCorralInfo{ TipoCorralID = 1},
                Operador = new OperadorInfo { OperadorID = 1},
                UsuarioCreacionID = 1,
                Activo = EstatusEnum.Activo
            };

            corral.CorralID = pl.Guardar(corral);
            Assert.AreNotEqual(corral.CorralID, 0);
        }

        /// <summary>
        /// Probar que actualice una entidad
        /// </summary>
        [TestMethod]
        public void CorralActualizar()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);
            var pl = new CorralPL();
            CorralInfo corral = pl.ObtenerPorId(1);
            if (corral != null)
            {
                string descripcion;
                try
                {
                    descripcion = string.Format("A{0:D9}", randomNumber);
                    corral.Codigo = descripcion;
                    corral.UsuarioModificacionID = 4;
                    pl.Guardar(corral);
                }
                catch (Exception)
                {
                    descripcion = string.Empty;
                }
                Assert.AreEqual(corral.Codigo, descripcion);
            }
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void CorralGuardarDescripcionExistente()
        {
            var pl = new CorralPL();
            var corral = new CorralInfo
            {
                CorralID = 0,
                Codigo = "Corral",
                Activo = EstatusEnum.Activo
            };

            try
            {
                pl.Guardar(corral);
            }
            catch (Exception)
            {
                Assert.AreEqual(corral.CorralID, 0);
            }
        }

        /// <summary>
        /// Probar que traiga una entidad por su Id
        /// </summary>
        [TestMethod]
        public void CorralObtenerPorId()
        {
            var pl = new CorralPL();
            CorralInfo corral = pl.ObtenerPorId(1);
            Assert.AreNotEqual(corral, null);
        }

        /// <summary>
        /// Probar que traiga una entidad por su Id
        /// </summary>
        [TestMethod]
        public void CorralObtenerPorIdSinDatos()
        {
            var pl = new CorralPL();
            CorralInfo corral = pl.ObtenerPorId(0);
            Assert.AreEqual(corral, null);
        }
    }
}