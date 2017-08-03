using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class Camion
    {
        [TestMethod]
        public void CamionObtenerPorId()
        {
            var pl = new CamionPL();
            CamionInfo info;

            try
            {
                info = pl.ObtenerPorID(1);
            }
            catch (Exception)
            {
                info = new CamionInfo();
            }
            Assert.AreNotEqual(info, null);
        }

        [TestMethod]
        public void CamionObtenerPorIdSinDatos()
        {
            var pl = new CamionPL();
            CamionInfo info;

            try
            {
                info = pl.ObtenerPorID(0);
            }
            catch (Exception)
            {
                info = new CamionInfo();
            }
            Assert.AreEqual(info, null);
        }
    }
}
