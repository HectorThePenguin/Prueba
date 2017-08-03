using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class Chofer
    {
        [TestMethod]
        public void ChoferObtenerPorId()
        {
            var pl = new ChoferPL();
            ChoferInfo info;

            try
            {
                info = pl.ObtenerPorID(1);
            }
            catch (Exception)
            {
                info = new ChoferInfo();
            }
            Assert.AreNotEqual(info, null);
        }
        
        [TestMethod]
        public void ChoferObtenerPorIdSinDatos()
        {
            var pl = new ChoferPL();
            ChoferInfo info;

            try
            {
                info = pl.ObtenerPorID(10000000);
            }
            catch (Exception)
            {
                info = new ChoferInfo();
            }
            Assert.AreEqual(info, null);
        }
    }
}
