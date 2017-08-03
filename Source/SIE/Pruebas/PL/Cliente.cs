using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class Cliente
    {
        /// <summary>
        /// Probar que regrese una lista paginada.
        /// </summary>
        [TestMethod]
        public void ClienteObtenerPorPagina()
        {
            var pl = new ClientePL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new ClienteInfo { Descripcion = null };

            ResultadoInfo<ClienteInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreNotEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que regrese una lista vacia.
        /// </summary>
        [TestMethod]
        public void ClienteObtenerPorPaginaSinDatos()
        {
            var pl = new ClientePL();
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
            var filtro = new ClienteInfo { Descripcion = "1331312313khgdjkfghjkd" };

            ResultadoInfo<ClienteInfo> listaPaginada = pl.ObtenerPorPagina(pagina, filtro);
            Assert.AreEqual(listaPaginada, null);
        }

        /// <summary>
        /// Probar que cree una entidad
        /// </summary>
        [TestMethod]
        public void ClienteCrear()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);

            var pl = new ClientePL();
            var cliente = new ClienteInfo
            {
                ClienteID = 0,
                Descripcion = string.Format("Prueba Unitaria {0}", randomNumber),
                CodigoSAP = string.Format("{0:D10}", randomNumber),
                UsuarioCreacionID =  1,
                Activo = EstatusEnum.Activo
            };
            
            cliente.ClienteID = pl.Guardar(cliente);
            Assert.AreNotEqual(cliente.ClienteID, 0);
        }

        /// <summary>
        /// Probar que actualice una entidad
        /// </summary>
        [TestMethod]
        public void ClienteActualizar()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 100);
            var pl = new ClientePL();
            ClienteInfo cliente = pl.ObtenerPorID(3);
            if (cliente != null)
            {
                string descripcion;
                try
                {
                    descripcion = string.Format("Cliente actualizado {0}", randomNumber);
                    cliente.Descripcion = descripcion;
                    cliente.UsuarioModificacionID = 2;
                    pl.Guardar(cliente);
                }
                catch (Exception)
                {
                    descripcion = string.Empty;
                }
                Assert.AreEqual(cliente.Descripcion, descripcion);
            }
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void ClienteGuardarDescripcionExistente()
        {
            var pl = new ClientePL();
            var cliente = new ClienteInfo
            {
                ClienteID = 0,
                Descripcion = "Cliente Administradores",
                Activo = EstatusEnum.Activo
            };

            try
            {
                pl.Guardar(cliente);
            }
            catch (Exception)
            {
                Assert.AreEqual(cliente.ClienteID, 0);
            }
        }

        [TestMethod]
        public void ClienteObtenerPorId()
        {
            var pl = new ClientePL();
            ClienteInfo cliente = pl.ObtenerPorID(100);
            Assert.AreEqual(cliente, null);
        }

        [TestMethod]
        public void ClienteObtenerPorIdExistente()
        {
            var pl = new ClientePL();
            ClienteInfo cliente = pl.ObtenerPorID(3);
            Assert.AreNotEqual(cliente, null);
        }
    }
}
