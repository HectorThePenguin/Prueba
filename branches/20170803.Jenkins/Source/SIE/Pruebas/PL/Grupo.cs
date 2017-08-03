using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info; 
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{

    [TestClass]
    public class Grupo
    {
        /// <summary>
        /// Probar que regrese una lista paginada.
        /// </summary>
        [TestMethod]
        public void GrupoObtenerPorPagina()
        {
            var pl = new GrupoPL();
            var pagina = new PaginacionInfo {Inicio = 1, Limite = 15};
            var filtro = new GrupoInfo {Descripcion = null};

            ResultadoInfo<GrupoInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreNotEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que regrese una lista vacia.
        /// </summary>
        [TestMethod]
        public void GrupoObtenerPorPaginaSinDatos()
        {
            var pl = new GrupoPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new GrupoInfo { Descripcion = "." };

            ResultadoInfo<GrupoInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que cree una entidad
        /// </summary>
        [TestMethod]
        public void GrupoCrear()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);

            var pl = new GrupoPL();
            var grupo = new GrupoInfo
                            {
                                GrupoID = 0,
                                Descripcion = string.Format("Prueba Unitaria Crear {0:D10}", randomNumber),
                                GrupoFormularioInfo =
                                    new GrupoFormularioInfo
                                        {
                                            ListaFormulario = new List<FormularioInfo>(),
                                        },
                                Activo = EstatusEnum.Activo
                            };


            pl.Guardar(grupo);
            Assert.AreNotEqual(grupo.GrupoID, 0);
        }

        /// <summary>
        /// Probar que actualice una entidad
        /// </summary>
        [TestMethod]
        public void GrupoActualizar()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);
            var pl = new GrupoPL();
            GrupoInfo grupo = pl.ObtenerPorID(1);
            if (grupo != null)
            {
                string descripcion;
                try
                {
                    descripcion = string.Format("Prueba Unitaria Actualizar {0:D10}", randomNumber);
                    grupo.Descripcion = descripcion;
                    
                    grupo.GrupoFormularioInfo =
                        new GrupoFormularioInfo
                            {
                                ListaFormulario = new List<FormularioInfo>(),
                                Acceso = new AccesoInfo(),
                                Formulario = new FormularioInfo(),
                                Grupo = new GrupoInfo() 
                            };
                    pl.Guardar(grupo);
                }
                catch (Exception)
                {
                    descripcion = string.Empty;
                }
                Assert.AreEqual(grupo.Descripcion, descripcion);
            }
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void GrupoGuardarDescripcionExistente()
        {
            var pl = new GrupoPL();
            var grupo = new GrupoInfo
                            {
                                GrupoID = 0,
                                Descripcion = "Grupo Administradores",
                                GrupoFormularioInfo =
                                    new GrupoFormularioInfo
                                        {
                                            ListaFormulario = new List<FormularioInfo>(),
                                        },
                                Activo = EstatusEnum.Activo
                            };

            try
            {
                pl.Guardar(grupo);

            }
            catch (Exception)
            {
                Assert.AreEqual(grupo.GrupoID, 0);
            }
        }

        [TestMethod]
        public void GrupoObtenerPorId()
        {
            var pl = new GrupoPL();
            GrupoInfo grupo = pl.ObtenerPorID(100);
            Assert.AreEqual(grupo, null);
        }

        [TestMethod]
        public void GrupoObtenerPorIdExistente()
        {
            var pl = new GrupoPL();
            GrupoInfo grupo = pl.ObtenerPorID(1);
            Assert.AreNotEqual(grupo, null);
        }

        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void GrupoObtenerTodos()
        {
            var pl = new GrupoPL();
            IList<GrupoInfo> lista = pl.ObtenerTodos();
            Assert.AreNotEqual(lista, null);
        }

        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void GrupoObtenerTodosActivos()
        {
            var pl = new GrupoPL();
            IList<GrupoInfo> lista = pl.ObtenerTodos(EstatusEnum.Activo);
            Assert.AreNotEqual(lista, null);
        }
    }
}
