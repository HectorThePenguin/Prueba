using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class CalidadGanado
    {
        [TestMethod]
        public void CalidadGanadoObtenerPorId()
        {
            var pl = new CalidadGanadoPL();
            CalidadGanadoInfo info;

            try
            {
                info = pl.ObtenerPorID(1);
            }
            catch (Exception)
            {
                info = new CalidadGanadoInfo();
            }
            Assert.AreNotEqual(info, null);
        }

        [TestMethod]
        public void CalidadGanadoObtenerPorIdSinDatos()
        {
            var pl = new CalidadGanadoPL();
            CalidadGanadoInfo info;

            try
            {
                info = pl.ObtenerPorID(0);
            }
            catch (Exception)
            {
                info = new CalidadGanadoInfo();
            }
            Assert.AreEqual(info, null);
        }
    }
}
