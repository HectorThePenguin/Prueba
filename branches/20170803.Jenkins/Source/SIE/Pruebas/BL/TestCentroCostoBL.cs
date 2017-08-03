using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace Pruebas.BL
{
    [TestClass]
    public class TestCentroCostoBL
    {
        [TestMethod]
        public void ObtenerPorId()
        {
            var centroCosto = new CentroCostoBL().ObtenerPorID(1);
            Assert.AreNotEqual(centroCosto, null);

            centroCosto = new CentroCostoBL().ObtenerPorID(1000);
            Assert.AreEqual(centroCosto, null);
        }

        [TestMethod]
        public void ObtenerDescripcion()
        {
            var centroCosto = new CentroCostoBL().ObtenerPorDescripcion("Camion");
            Assert.AreNotEqual(centroCosto, null);

            centroCosto = new CentroCostoBL().ObtenerPorCentroCostoSAP(new CentroCostoInfo{CentroCostoSAP = "000003"});
            Assert.AreEqual(centroCosto, null);
        }

        [TestMethod]
        public void ObtenerPorAutorizador()
        {
            var filtro = new CentroCostoInfo();

            var centrosCosto = new CentroCostoBL().ObtenerPorPagina(new PaginacionInfo{Inicio = 1, Limite = 15}, filtro);
            Assert.AreNotEqual(centrosCosto, null);

            filtro.AutorizadorID = 2;

            var centroCostoConAutorizador = new CentroCostoBL().ObtenerPorPagina(new PaginacionInfo { Inicio = 1, Limite = 15 }, filtro);
            Assert.AreNotEqual(centroCostoConAutorizador, null);
        }

        [TestMethod]
        public void ObtenerPorCentroCostoSAP()
        {
            var filtro = new CentroCostoInfo { CentroCostoSAP = "000005", AutorizadorID = 2 };

            var centroCosto = new CentroCostoBL().ObtenerPorCentroCostoSAP(filtro);
            Assert.AreNotEqual(centroCosto, null);

            filtro = new CentroCostoInfo { CentroCostoSAP = "000005", AutorizadorID = 4 };
            var centroCostoSinDatos = new CentroCostoBL().ObtenerPorCentroCostoSAP(filtro);
            Assert.IsNull(centroCostoSinDatos, null);
        }
    }
}
