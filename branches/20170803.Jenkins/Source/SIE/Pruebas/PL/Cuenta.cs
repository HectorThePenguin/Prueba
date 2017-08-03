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
    public class Cuenta
    {
        /// <summary>
        /// Probar que regrese una lista paginada.
        /// </summary>
        [TestMethod]
        public void CuentaObtenerPorPagina()
        {
            var pl = new CuentaPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new CuentaInfo { Descripcion = string.Empty };

            ResultadoInfo<CuentaInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreNotEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que regrese una lista vacia.
        /// </summary>
        [TestMethod]
        public void CuentaObtenerPorPaginaSinDatos()
        {
            var pl = new CuentaPL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new CuentaInfo { Descripcion = "." };

            ResultadoInfo<CuentaInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que cree una entidad
        /// </summary>
        [TestMethod]
        public void CuentaCrear()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);

            var pl = new CuentaPL();
            var cuenta = new CuentaInfo
                             {
                                 CuentaID = 0,
                                 Descripcion = string.Format("Prueba Unitaria Crear {0:D10}", randomNumber),
                                 TipoCuenta = new TipoCuentaInfo { TipoCuentaID = 2},
                                 ClaveCuenta = string.Format("{0:D10}", randomNumber),
                                 UsuarioCreacionID = 1,
                                 Activo = EstatusEnum.Activo
                             };

            try
            {
                pl.Guardar(cuenta);
            }
            catch (Exception)
            {
                cuenta = null;
            }
            Assert.AreNotEqual(cuenta, null);
        }

        /// <summary>
        /// Probar que actualice una entidad
        /// </summary>
        [TestMethod]
        public void CuentaActualizar()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);
            var pl = new CuentaPL();
            CuentaInfo cuenta = pl.ObtenerPorID(9);
            if (cuenta != null)
            {
                string descripcion;
                try
                {
                    descripcion = string.Format("Prueba Unitaria Actualizar {0:D10}", randomNumber);
                    cuenta.Descripcion = descripcion;
                    cuenta.UsuarioModificacionID = 2;
                    pl.Guardar(cuenta);
                }
                catch (Exception)
                {
                    descripcion = string.Empty;
                }
                Assert.AreEqual(cuenta.Descripcion, descripcion);
            }
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void CuentaGuardarDescripcionExistente()
        {
            var pl = new CuentaPL();
            var cuenta = new CuentaInfo
            {
                CuentaID = 0,
                Descripcion = "Cuenta Administradores",
                Activo = EstatusEnum.Activo
            };

            try
            {
                pl.Guardar(cuenta);
            }
            catch (Exception)
            {
                Assert.AreEqual(cuenta.CuentaID, 0);
            }
        }

        [TestMethod]
        public void CuentaObtenerPorId()
        {
            var pl = new CuentaPL();
            CuentaInfo cuenta = pl.ObtenerPorID(100);
            Assert.AreEqual(cuenta, null);
        }

        [TestMethod]
        public void CuentaObtenerPorIdExistente()
        {
            var pl = new CuentaPL();
            CuentaInfo cuenta = pl.ObtenerPorID(1);
            Assert.AreNotEqual(cuenta, null);
        }

        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void CuentaObtenerTodos()
        {
            var pl = new CuentaPL();
            IList<CuentaInfo> lista = pl.ObtenerTodos();
            Assert.AreNotEqual(lista, null);
        }

        /// <summary>
        /// Probar que traiga solo los activos
        /// </summary>
        [TestMethod]
        public void CuentaObtenerTodosActivos()
        {
            var pl = new CuentaPL();
            IList<CuentaInfo> lista = pl.ObtenerTodos(EstatusEnum.Activo);
            Assert.AreNotEqual(lista, null);
        }
    }
}
