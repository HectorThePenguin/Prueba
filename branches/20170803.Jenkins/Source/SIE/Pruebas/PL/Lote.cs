using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class Lote
    {
        [TestMethod]
        public void LoteObtenerPorId()
        {
            var pl = new LotePL();
            LoteInfo info;

            try
            {
                info = pl.ObtenerPorId(1);
            }
            catch (Exception)
            {
                info = null;
            }
            Assert.AreNotEqual(info, null);
        }
    }
}
