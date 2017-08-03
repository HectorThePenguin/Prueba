using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class Almacen
    {

        //protected class Producto
        //{
        //    public int ProductoId { set; get; }
        //    public string Descripcion { set; get; }
        //    public decimal Precio { set; get; }
        //    public bool Activo { set; get; }
        //}

        //protected class Pedido
        //{
        //    public int PedidoId { set; get; }
        //    public IList<Producto> 

        //}

        [TestMethod]
        public void AlmacenObtenerPorId()
        {
            var pl = new AlmacenPL();
            AlmacenInfo info;

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

        [TestMethod]
        public void AlmacenListas()
        {
            var pl = new AlmacenPL();
            AlmacenInfo info;

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
