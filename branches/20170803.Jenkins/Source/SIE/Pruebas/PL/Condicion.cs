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
    public class Condicion
    {
        /// <summary>
        /// Probar que regrese una lista paginada.
        /// </summary>
        [TestMethod]
        public void CondicionObtenerPorPagina()
        {
            var pl = new CondicionPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new CondicionInfo { Descripcion = string.Empty };

            ResultadoInfo<CondicionInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreNotEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que regrese una lista vacia.
        /// </summary>
        [TestMethod]
        public void CondicionObtenerPorPaginaSinDatos()
        {
            var pl = new CondicionPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new CondicionInfo { Descripcion = "1331312313khgdjkfghjkd" };

            ResultadoInfo<CondicionInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que cree una entidad
        /// </summary>
        [TestMethod]
        public void CondicionCrear()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);

            var pl = new CondicionPL();
            var condicion = new CondicionInfo
            {
                CondicionID = 0,
                Descripcion = string.Format("Prueba Unitaria Crear {0}", randomNumber),
                UsuarioCreacionID = 1,
                Activo = EstatusEnum.Activo
            };

            condicion.CondicionID = pl.Guardar(condicion);
            Assert.AreNotEqual(condicion.CondicionID, 0);
        }

        /// <summary>
        /// Probar que actualice una entidad
        /// </summary>
        [TestMethod]
        public void CondicionActualizar()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);
            var pl = new CondicionPL();
            CondicionInfo condicion = pl.ObtenerPorID(1);
            if (condicion != null)
            {
                string descripcion;
                try
                {
                    descripcion = string.Format("Prueba Unitaria Actualizar {0}", randomNumber);
                    condicion.Descripcion = descripcion;
                    condicion.UsuarioModificacionID = 4;
                    pl.Guardar(condicion);
                }
                catch (Exception)
                {
                    descripcion = string.Empty;
                }
                Assert.AreEqual(condicion.Descripcion, descripcion);
            }
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void CondicionGuardarDescripcionExistente()
        {
            var pl = new CondicionPL();
            var condicion = new CondicionInfo
            {
                CondicionID = 0,
                Descripcion = "Condicion",
                Activo = EstatusEnum.Activo
            };

            try
            {
                pl.Guardar(condicion);
            }
            catch (Exception)
            {
                Assert.AreEqual(condicion.CondicionID, 0);
            }
        }

        /// <summary>
        /// Probar que traiga una entidad por su Id
        /// </summary>
        [TestMethod]
        public void CondicionObtenerPorId()
        {
            var pl = new CondicionPL();
            CondicionInfo condicion = pl.ObtenerPorID(1);
            Assert.AreNotEqual(condicion, null);
        }

        /// <summary>
        /// Probar que traiga una entidad por su Id
        /// </summary>
        [TestMethod]
        public void CondicionObtenerPorIdSinDatos()
        {
            var pl = new CondicionPL();
            CondicionInfo condicion = pl.ObtenerPorID(0);
            Assert.AreEqual(condicion, null);
        }


        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void CondicionObtenerTodos()
        {
            var pl = new CondicionPL();
            IList<CondicionInfo> lista = pl.ObtenerTodos();
            Assert.AreNotEqual(lista, null);
        }

        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void CondicionObtenerTodosActivos()
        {
            var pl = new CondicionPL();
            IList<CondicionInfo> lista = pl.ObtenerTodos(EstatusEnum.Activo);
            Assert.AreNotEqual(lista, null);
        }
    }
}
