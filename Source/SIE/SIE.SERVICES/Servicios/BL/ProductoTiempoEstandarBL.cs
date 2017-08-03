using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Servicios.BL
{
    public class ProductoTiempoEstandarBL
    {
        public ResultadoInfo<ProductoTiempoEstandarInfo> ObtenerPorPagina(PaginacionInfo pagina, ProductoTiempoEstandarInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoTiempoEstandarDAL = new ProductoTiempoEstandarDAL();
                ResultadoInfo<ProductoTiempoEstandarInfo> result = productoTiempoEstandarDAL.ObtenerPorPagina(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public bool GuardarProductoTiempoEstandar(ProductoTiempoEstandarInfo productoTiempoEstandarInfo)
        {
            try
            {
                Logger.Info();
                var productoTiempoEstandarDAL = new ProductoTiempoEstandarDAL();
                bool result = productoTiempoEstandarDAL.GuardarProductoTiempoEstandar(productoTiempoEstandarInfo);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public bool ActualizarProductoTiempoEstandar(ProductoTiempoEstandarInfo productoTiempoEstandarInfo)
        {
            try
            {
                Logger.Info();
                var productoTiempoEstandarDAL = new ProductoTiempoEstandarDAL();
                bool result = productoTiempoEstandarDAL.ActualizarProductoTiempoEstandar(productoTiempoEstandarInfo);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public ProductoTiempoEstandarInfo ObtenerPorProductoID(ProductoTiempoEstandarInfo productoTiempoEstandarInfo)
        {
            try
            {
                Logger.Info();
                var productoTiempoEstandarDAL = new ProductoTiempoEstandarDAL();
                ProductoTiempoEstandarInfo result = productoTiempoEstandarDAL.ObtenerPorProductoID(productoTiempoEstandarInfo);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
