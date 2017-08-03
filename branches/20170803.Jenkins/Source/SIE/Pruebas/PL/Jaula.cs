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
    public class Jaula
    {
        /// <summary>
        /// Probar que regrese una lista paginada.
        /// </summary>
        [TestMethod]
        public void JaulaObtenerPorPagina()
        {
            var pl = new JaulaPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new JaulaInfo { PlacaJaula = string.Empty };

            ResultadoInfo<JaulaInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreNotEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que regrese una lista vacia.
        /// </summary>
        [TestMethod]
        public void JaulaObtenerPorPaginaSinDatos()
        {
            var pl = new JaulaPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new JaulaInfo { PlacaJaula = "." };

            ResultadoInfo<JaulaInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que cree una entidad
        /// </summary>
        [TestMethod]
        public void JaulaCrear()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);

            var pl = new JaulaPL();
            var jaula = new JaulaInfo
            {
                JaulaID = 0,
                PlacaJaula = string.Format("C{0:D9}", randomNumber),
                UsuarioCreacionID = 1,
                Proveedor = new ProveedorInfo { ProveedorID = 1 },
                Activo = EstatusEnum.Activo
            };

            try
            {
                pl.Guardar(jaula);
            }
            catch (Exception)
            {
                jaula = null;
            }
            Assert.AreNotEqual(jaula, null);
        }

        /// <summary>
        /// Probar que actualice una entidad
        /// </summary>
        [TestMethod]
        public void JaulaActualizar()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);
            var pl = new JaulaPL();
            JaulaInfo jaula = pl.ObtenerPorID(1);
            if (jaula != null)
            {
                string descripcion;
                try
                {
                    descripcion = string.Format("A{0:D9}", randomNumber);
                    jaula.PlacaJaula = descripcion;
                    jaula.UsuarioModificacionID = 2;
                    pl.Guardar(jaula);
                }
                catch (Exception)
                {
                    descripcion = string.Empty;
                }
                Assert.AreEqual(jaula.PlacaJaula, descripcion);
            }
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void JaulaGuardarDescripcionExistente()
        {
            var pl = new JaulaPL();
            var jaula = new JaulaInfo
                            {
                                JaulaID = 0,
                                PlacaJaula = "215-UM4",
                                Proveedor = new ProveedorInfo{ProveedorID = 1},
                                UsuarioCreacionID = 1,
                                Activo = EstatusEnum.Activo
                            };

            try
            {
                pl.Guardar(jaula);
                jaula = pl.ObtenerPorDescripcion("215-UM4");
            }
            catch (Exception)
            {
                jaula.JaulaID = 0;
            }
            Assert.AreEqual(jaula.JaulaID, 0);

        }

        [TestMethod]
        public void JaulaObtenerPorId()
        {
            var pl = new JaulaPL();
            JaulaInfo jaula = pl.ObtenerPorID(0);
            Assert.AreEqual(jaula, null);
        }

        [TestMethod]
        public void JaulaObtenerPorIdExistente()
        {
            var pl = new JaulaPL();
            JaulaInfo jaula = pl.ObtenerPorID(1);
            Assert.AreNotEqual(jaula, null);
        }

        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void JaulaObtenerTodosActivos()
        {
            var pl = new JaulaPL();
            IList<JaulaInfo> lista = pl.ObtenerTodos(EstatusEnum.Activo);
            Assert.AreNotEqual(lista, null);
        }
    }
}
