using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class Familia
    {
        /// <summary>
        /// Probar que regrese una lista paginada.
        /// </summary>
        [TestMethod]
        public void FamiliaObtenerPorPagina()
        {
            var pl = new FamiliaPL();
            var pagina = new PaginacionInfo {Inicio = 1, Limite = 15};
            var filtro = new FamiliaInfo {Descripcion = string.Empty};

            ResultadoInfo<FamiliaInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreNotEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que regrese una lista vacia.
        /// </summary>
        [TestMethod]
        public void FamiliaObtenerPorPaginaSinDatos()
        {
            var pl = new FamiliaPL();
            var pagina = new PaginacionInfo {Inicio = 1, Limite = 15};
            var filtro = new FamiliaInfo {Descripcion = "."};

            ResultadoInfo<FamiliaInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que cree una entidad
        /// </summary>
        [TestMethod]
        public void FamiliaCrear()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);

            var pl = new FamiliaPL();
            var familia = new FamiliaInfo
                             {
                                 FamiliaID = 0,
                                 Descripcion = string.Format("Prueba Unitaria Crear {0:D10}", randomNumber),
                                 UsuarioCreacionID = 1,
                                 Activo = EstatusEnum.Activo
                             };

            try
            {
                pl.Guardar(familia);
            }
            catch (Exception)
            {
                familia = null;
            }
            Assert.AreNotEqual(familia, null);
        }

        /// <summary>
        /// Probar que actualice una entidad
        /// </summary>
        [TestMethod]
        public void FamiliaActualizar()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);
            var pl = new FamiliaPL();
            FamiliaInfo familia = pl.ObtenerPorID(9);
            if (familia != null)
            {
                string descripcion;
                try
                {
                    descripcion = string.Format("Prueba Unitaria Actualizar {0:D10}", randomNumber);
                    familia.Descripcion = descripcion;
                    familia.UsuarioModificacionID = 2;
                    pl.Guardar(familia);
                }
                catch (Exception)
                {
                    descripcion = string.Empty;
                }
                Assert.AreEqual(familia.Descripcion, descripcion);
            }
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void FamiliaGuardarDescripcionExistente()
        {
            var pl = new FamiliaPL();
            var familia = new FamiliaInfo
                             {
                                 FamiliaID = 0,
                                 Descripcion = "Materia Primas",
                                 UsuarioCreacionID = 1,
                                 Activo = EstatusEnum.Activo
                             };

            try
            {
               familia.FamiliaID = pl.Guardar(familia);
            }
            catch (Exception)
            {
                familia.FamiliaID = 0;
            }
            Assert.AreEqual(familia.FamiliaID, 0);

        }

        [TestMethod]
        public void FamiliaObtenerPorId()
        {
            var pl = new FamiliaPL();
            FamiliaInfo familia = pl.ObtenerPorID(100);
            Assert.AreEqual(familia, null);
        }

        [TestMethod]
        public void FamiliaObtenerPorIdExistente()
        {
            var pl = new FamiliaPL();
            FamiliaInfo familia = pl.ObtenerPorID(1);
            Assert.AreNotEqual(familia, null);
        }

        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void FamiliaObtenerTodosActivos()
        {
            var pl = new FamiliaPL();
            IList<FamiliaInfo> lista = pl.ObtenerTodos(EstatusEnum.Activo);
            Assert.AreNotEqual(lista, null);
        }
    }
}