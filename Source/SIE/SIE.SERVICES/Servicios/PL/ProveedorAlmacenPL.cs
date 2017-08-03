using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ProveedorAlmacenPL
    {
        /// <summary>
        /// Obtiene un proveedor por id
        /// </summary>
        /// <param name="proveedorInfo"></param>
        /// <returns></returns>
        public ProveedorAlmacenInfo ObtenerPorProveedorId(ProveedorInfo proveedorInfo)
        {
            ProveedorAlmacenInfo proveedorAlmacen;
            try
            {
                Logger.Info();
                var proveedorAlmacenBl = new ProveedorAlmacenBL();
                proveedorAlmacen = proveedorAlmacenBl.ObtenerPorProveedorId(proveedorInfo);
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
            return proveedorAlmacen;
        }

        /// <summary>
        /// Obtiene un proveedor por id y tipo de almacen
        /// </summary>
        /// <param name="proveedorInfo"></param>
        /// <returns></returns>
        public ProveedorAlmacenInfo ObtenerPorProveedorTipoAlmacen(ProveedorAlmacenInfo proveedorInfo)
        {
            ProveedorAlmacenInfo proveedorAlmacen;
            try
            {
                Logger.Info();
                var proveedorAlmacenBl = new ProveedorAlmacenBL();
                proveedorAlmacen = proveedorAlmacenBl.ObtenerPorProveedorTipoAlmacen(proveedorInfo);
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
            return proveedorAlmacen;
        }
    }
}
