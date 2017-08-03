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
    public class ConceptoDeteccion
    {
        /// <summary>
        /// Probar que regrese una lista paginada.
        /// </summary>
        [TestMethod]
        public void ConceptoDeteccionObtenerPorPagina()
        {
            var pl = new ConceptoDeteccionPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new ConceptoDeteccionInfo { Descripcion = string.Empty };

            ResultadoInfo<ConceptoDeteccionInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreNotEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que regrese una lista vacia.
        /// </summary>
        [TestMethod]
        public void ConceptoDeteccionObtenerPorPaginaSinDatos()
        {
            var pl = new ConceptoDeteccionPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new ConceptoDeteccionInfo { Descripcion = "1331312313khgdjkfghjkd" };

            ResultadoInfo<ConceptoDeteccionInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que cree una entidad
        /// </summary>
        [TestMethod]
        public void ConceptoDeteccionCrear()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);

            var pl = new ConceptoDeteccionPL();
            var conceptoDeteccion = new ConceptoDeteccionInfo
            {
                ConceptoDeteccionID = 0,
                Descripcion = string.Format("Prueba Unitaria Crear {0}", randomNumber),
                UsuarioCreacionID = 1,
                Activo = EstatusEnum.Activo
            };

            conceptoDeteccion.ConceptoDeteccionID = pl.Guardar(conceptoDeteccion);
            Assert.AreNotEqual(conceptoDeteccion.ConceptoDeteccionID, 0);
        }

        /// <summary>
        /// Probar que actualice una entidad
        /// </summary>
        [TestMethod]
        public void ConceptoDeteccionActualizar()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);
            var pl = new ConceptoDeteccionPL();
            ConceptoDeteccionInfo conceptoDeteccion = pl.ObtenerPorID(1);
            if (conceptoDeteccion != null)
            {
                string descripcion;
                try
                {
                    descripcion = string.Format("Prueba Unitaria Actualizar {0}", randomNumber);
                    conceptoDeteccion.Descripcion = descripcion;
                    conceptoDeteccion.UsuarioModificacionID = 4;
                    pl.Guardar(conceptoDeteccion);
                }
                catch (Exception)
                {
                    descripcion = string.Empty;
                }
                Assert.AreEqual(conceptoDeteccion.Descripcion, descripcion);
            }   
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void ConceptoDeteccionGuardarDescripcionExistente()
        {
            var pl = new ConceptoDeteccionPL();
            var conceptoDeteccion = new ConceptoDeteccionInfo
            {
                ConceptoDeteccionID = 0,
                Descripcion = "ConceptoDeteccion",
                Activo = EstatusEnum.Activo
            };

            try
            {
                pl.Guardar(conceptoDeteccion);
            }
            catch (Exception)
            {
                Assert.AreEqual(conceptoDeteccion.ConceptoDeteccionID, 0);
            }
        }

        /// <summary>
        /// Probar que traiga una entidad por su Id
        /// </summary>
        [TestMethod]
        public void ConceptoDeteccionObtenerPorId()
        {
            var pl = new ConceptoDeteccionPL();
            ConceptoDeteccionInfo conceptoDeteccion = pl.ObtenerPorID(1);
            Assert.AreNotEqual(conceptoDeteccion, null);
        }

        /// <summary>
        /// Probar que traiga una entidad por su Id
        /// </summary>
        [TestMethod]
        public void ConceptoDeteccionObtenerPorIdSinDatos()
        {
            var pl = new ConceptoDeteccionPL();
            ConceptoDeteccionInfo conceptoDeteccion = pl.ObtenerPorID(0);
            Assert.AreEqual(conceptoDeteccion, null);
        }


        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void ConceptoDeteccionObtenerTodos()
        {
            var pl = new ConceptoDeteccionPL();
            IList<ConceptoDeteccionInfo> lista = pl.ObtenerTodos();
            Assert.AreNotEqual(lista, null);
        }

        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void ConceptoDeteccionObtenerTodosActivos()
        {
            var pl = new ConceptoDeteccionPL();
            IList<ConceptoDeteccionInfo> lista = pl.ObtenerTodos(EstatusEnum.Activo);
            Assert.AreNotEqual(lista, null);
        }
    }
}
