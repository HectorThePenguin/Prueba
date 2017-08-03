using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SIE.Services.Info.Info;


namespace Pruebas.BL
{
    [TestClass]
    public class ProductoBL
    {
        [TestMethod]
        public void TestObtenerExistencia()
        {
            var pl = new SIE.Services.Servicios.PL.ProductoPL();
            var productos = new List<ProductoInfo>
                                {
                                    new ProductoInfo {ProductoId = 1},
                                    new ProductoInfo {ProductoId = 2},
                                    new ProductoInfo {ProductoId = 3},
                                    new ProductoInfo {ProductoId = 4}
                                };


            var cantidad = pl.ObtenerExistencia(1, productos);
            Assert.IsNull(cantidad);
        }
    }
}
