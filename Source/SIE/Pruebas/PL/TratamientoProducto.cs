using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class TestTratamientoProducto
    {
        [TestMethod]
        public void ObtenerTratamientoProductoPorId()
        {
            var tratamientoProductoPL = new TratamientoProductoPL();
            TratamientoProductoInfo tratamientoProducto = tratamientoProductoPL.ObtenerPorID(3);
            Assert.IsNotNull(tratamientoProducto);
            Assert.IsNotNull(tratamientoProducto.Producto);
            Assert.IsNotNull(tratamientoProducto.Tratamiento);
            Assert.IsTrue(tratamientoProducto.Producto.ProductoDescripcion.Length > 0);
            Assert.IsTrue(tratamientoProducto.Tratamiento.Descripcion.Length > 0);
        }
    }
}
