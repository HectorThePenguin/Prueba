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
    public class Costo
    {
        /// <summary>
        /// Probar que regrese una lista paginada.
        /// </summary>
        [TestMethod]
        public void CostoObtenerPorPagina()
        {
            var pl = new CostoPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new CostoInfo { Descripcion = null };

            ResultadoInfo<CostoInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreNotEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que regrese una lista vacia.
        /// </summary>
        [TestMethod]
        public void CostoObtenerPorPaginaSinDatos()
        {
            var pl = new CostoPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new CostoInfo { Descripcion = "." };

            ResultadoInfo<CostoInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que cree una entidad
        /// </summary>
        [TestMethod]
        public void CostoCrear()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);

            string descripcion = string.Format("Prueba Unitaria Crear {0:D10}", randomNumber);

            var pl = new CostoPL();
            var costo = new CostoInfo
            {
                CostoID = 0,
                Descripcion = descripcion,
                ClaveContable = string.Format("{0:D3}", randomNumber),
                UsuarioCreacionID = 1,
                TipoCosto = new TipoCostoInfo { TipoCostoID = 1},
                TipoProrrateo = new TipoProrrateoInfo { TipoProrrateoID = 1},
                Retencion = new RetencionInfo {RetencionID = 1},
                Activo = EstatusEnum.Activo
            };

            pl.Crear(costo);
            costo = pl.ObtenerPorDescripcion(descripcion);
            Assert.AreNotEqual(costo.CostoID, 0);
        }

        /// <summary>
        /// Probar que actualice una entidad
        /// </summary>
        [TestMethod]
        public void CostoActualizar()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);
            var pl = new CostoPL();
            CostoInfo costo = pl.ObtenerPorID(1);
            if (costo != null)
            {
                string descripcion;
                try
                {
                    descripcion = string.Format("Prueba Unitaria Actualizar {0:D10}", randomNumber);
                    costo.Descripcion = descripcion;
                    costo.UsuarioModificacionID = 2;
                    pl.Actualizar(costo);
                }
                catch (Exception)
                {
                    descripcion = string.Empty;
                }
                Assert.AreEqual(costo.Descripcion, descripcion);
            }
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void CostoGuardarDescripcionExistente()
        {
            var pl = new CostoPL();
            var costo = new CostoInfo
            {
                CostoID = 0,
                Descripcion = "Costo",
                Activo = EstatusEnum.Activo
            };

            try
            {
                pl.Actualizar(costo);
            }
            catch (Exception)
            {
                Assert.AreEqual(costo.CostoID, 0);
            }
        }

        [TestMethod]
        public void CostoObtenerPorId()
        {
            var pl = new CostoPL();
            CostoInfo costo = pl.ObtenerPorID(0);
            Assert.AreEqual(costo, null);
        }

        [TestMethod]
        public void CostoObtenerPorIdExistente()
        {
            var pl = new CostoPL();
            CostoInfo costo = pl.ObtenerPorID(1);
            Assert.AreNotEqual(costo, null);
        }

        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void CostoObtenerTodosActivos()
        {
            var pl = new CostoPL();
            IList<CostoInfo> lista = pl.ObtenerTodos(EstatusEnum.Activo);
            Assert.AreNotEqual(lista, null);
        }
    }
}
