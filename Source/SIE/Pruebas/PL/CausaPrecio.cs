using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class CausaPrecio
    {
        [TestMethod]
        public void CausaPrecioObtenerPorId()
        {
            var causaPrecioPl = new CausaPrecioPL();
            CausaPrecioInfo causaPrecio;

            try
            {
                causaPrecio = causaPrecioPl.ObtenerPorID(1);

            }
            catch (Exception)
            {
                causaPrecio = null;
            }
            Assert.AreNotEqual(causaPrecio, null);
        }
    }
}
