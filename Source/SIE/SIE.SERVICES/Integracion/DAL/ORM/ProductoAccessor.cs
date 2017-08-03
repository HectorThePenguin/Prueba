using System.Collections.Generic;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class ProductoAccessor : BLToolkit.DataAccess.DataAccessor<ProductoInfo>
    {
        [BLToolkit.DataAccess.SprocName("Producto_ObtenerPorFamilia")]
        public abstract List<ProductoInfo> ObtenerPorFamilia(int productoID, string descripcion, int familiaID);

        [BLToolkit.DataAccess.SprocName("Producto_ObtenerPorSubFamiliaAyuda")]
        public abstract List<ProductoInfo> ObtenerPorSubFmilia(int productoID, string descripcion, int subFamiliaID);

        [BLToolkit.DataAccess.SprocName("Producto_ObtenerValoresProduccionMolino")]
        public abstract List<FiltroProductoProduccionMolino> ObtenerValoresProduccionMolino(int productoID);

        /// <summary>
        /// Obtiene la existencia de un almacén de un producto especifico.
        /// </summary>
        /// <param name="xmlProductos"></param>
        /// <returns></returns>
        [BLToolkit.DataAccess.SprocName("Producto_ObtenerExistencia")]
        public abstract IList<AlmacenInventarioInfo> ObtenerExistencia(string xmlProductos);
    }
}
