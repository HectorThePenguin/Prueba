using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class Embarque
    {
        [TestMethod]
        public void EmbarqueObtenerPorId()
        {
            var pl = new EmbarquePL();
            EmbarqueInfo info;

            try
            {
                info = pl.ObtenerPorID(1);
            }
            catch (Exception)
            {
                info = null;
            }
            Assert.AreNotEqual(info, null);
        }
    }
}
