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
    public class Formula
    {

        /// <summary>
        /// Probar que regrese una lista paginada.
        /// </summary>
        [TestMethod]
        public void FormulaObtenerPorPagina()
        {
            var pl = new FormulaPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new FormulaInfo { Descripcion = string.Empty };

            ResultadoInfo<FormulaInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreNotEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que regrese una lista vacia.
        /// </summary>
        [TestMethod]
        public void FormulaObtenerPorPaginaSinDatos()
        {
            var pl = new FormulaPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new FormulaInfo { Descripcion = "." };

            ResultadoInfo<FormulaInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que cree una entidad
        /// </summary>
        [TestMethod]
        public void FormulaCrear()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);

            var pl = new FormulaPL();
            var formula = new FormulaInfo
            {
                FormulaId = 0,
                Descripcion = string.Format("Prueba Unitaria Crear {0:D10}", randomNumber),
                UsuarioCreacionID = 1,
                TipoFormula = new TipoFormulaInfo { TipoFormulaID = 1},
                Producto = new ProductoInfo { ProductoId = 1},
                Activo = EstatusEnum.Activo
            };

            try
            {
                pl.Guardar(formula);
            }
            catch (Exception)
            {
                formula = null;
            }
            Assert.AreNotEqual(formula, null);
        }

        /// <summary>
        /// Probar que actualice una entidad
        /// </summary>
        [TestMethod]
        public void FormulaActualizar()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);
            var pl = new FormulaPL();
            FormulaInfo formula = pl.ObtenerPorID(9);
            if (formula != null)
            {
                string descripcion;
                try
                {
                    descripcion = string.Format("Prueba Unitaria Actualizar {0:D10}", randomNumber);
                    formula.Descripcion = descripcion;
                    formula.UsuarioModificacionID = 2;
                    pl.Guardar(formula);
                }
                catch (Exception)
                {
                    descripcion = string.Empty;
                }
                Assert.AreEqual(formula.Descripcion, descripcion);
            }
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void FormulaGuardarDescripcionExistente()
        {
            var pl = new FormulaPL();
            var formula = new FormulaInfo
            {
                FormulaId = 0,
                Descripcion = "Formula FC",
                TipoFormula = new TipoFormulaInfo { TipoFormulaID = 1 },
                Producto = new ProductoInfo { ProductoId = 1 },
                UsuarioCreacionID = 1,
                Activo = EstatusEnum.Activo
            };

            try
            {
                formula.FormulaId = pl.Guardar(formula);
            }
            catch (Exception)
            {
                formula.FormulaId = 0;
            }
            Assert.AreEqual(formula.FormulaId, 0);

        }

        [TestMethod]
        public void FormulaObtenerPorId()
        {
            var pl = new FormulaPL();
            FormulaInfo formula = pl.ObtenerPorID(0);
            Assert.AreEqual(formula, null);
            Assert.IsNotNull(formula.TipoFormula);
            Assert.IsNotNull(formula.Producto);
        }

        [TestMethod]
        public void FormulaObtenerPorIdExistente()
        {
            var pl = new FormulaPL();
            FormulaInfo formula = pl.ObtenerPorID(1);
            Assert.AreNotEqual(formula, null);
        }

        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void FormulaObtenerTodosActivos()
        {
            var pl = new FormulaPL();
            IList<FormulaInfo> lista = pl.ObtenerTodos(EstatusEnum.Activo);
            Assert.AreNotEqual(lista, null);
        }
    }
}
