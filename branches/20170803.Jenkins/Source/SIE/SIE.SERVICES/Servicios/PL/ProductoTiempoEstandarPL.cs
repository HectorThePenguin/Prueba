using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Servicios.PL
{
    public class ProductoTiempoEstandarPL
    {
        public ResultadoInfo<ProductoTiempoEstandarInfo> ObtenerPorPagina (PaginacionInfo pagina, ProductoTiempoEstandarInfo filtro)
        {
            ResultadoInfo<ProductoTiempoEstandarInfo> resultado = new ResultadoInfo<ProductoTiempoEstandarInfo>();
            ProductoTiempoEstandarBL productoTiempoEstandarBL = new ProductoTiempoEstandarBL();

            resultado = productoTiempoEstandarBL.ObtenerPorPagina(pagina, filtro);

            return resultado;
        }

        public ResultadoInfo<ProductoTiempoEstandarInfo> ObtenerPorID(PaginacionInfo pagina, ProductoTiempoEstandarInfo filtro)
        {
            ResultadoInfo<ProductoTiempoEstandarInfo> resultado = new ResultadoInfo<ProductoTiempoEstandarInfo>();

            return resultado;
        }

        public bool GuardarProductoTiempoEstandar(ProductoTiempoEstandarInfo productoTiempoEstandar)
        {
            bool resultado = false;
            ProductoTiempoEstandarBL productoTiempoEstandarBl = new ProductoTiempoEstandarBL();

            resultado = productoTiempoEstandarBl.GuardarProductoTiempoEstandar(productoTiempoEstandar);
            return resultado;
        }

        public bool ActualizarProductoTiempoEstandar(ProductoTiempoEstandarInfo productoTiempoEstandar)
        {
            bool resultado = false;
            ProductoTiempoEstandarBL productoTiempoEstandarBl = new ProductoTiempoEstandarBL();
            resultado = productoTiempoEstandarBl.ActualizarProductoTiempoEstandar(productoTiempoEstandar);
            return resultado;
        }

        public ProductoTiempoEstandarInfo ObtenerPorProductoID(ProductoTiempoEstandarInfo productoTiempoEstandar)
        {
            ProductoTiempoEstandarInfo resultado = new ProductoTiempoEstandarInfo();
            ProductoTiempoEstandarBL productoTiempoEstandarBl = new ProductoTiempoEstandarBL();
            resultado = productoTiempoEstandarBl.ObtenerPorProductoID(productoTiempoEstandar);
            return resultado;
        }
        
    }
}
