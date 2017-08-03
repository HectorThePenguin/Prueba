using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ProveedorAlmacenBL
    {
        /// <summary>
        /// Obtiene un proveedor almacen por proveedor id
        /// </summary>
        /// <param name="proveedorInfo"></param>
        /// <returns></returns>
        internal ProveedorAlmacenInfo ObtenerPorProveedorId(ProveedorInfo proveedorInfo)
        {
            ProveedorAlmacenInfo proveedorAlmacen;
            try
            {
                Logger.Info();
                var proveedorAlmacenDAL = new ProveedorAlmacenDAL();
                proveedorAlmacen = proveedorAlmacenDAL.ObtenerPorProveedorId(proveedorInfo);
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
        /// Obtiene un proveedor almacen por proveedor id
        /// </summary>
        /// <param name="proveedorInfo"></param>
        /// <returns></returns>
        internal ProveedorAlmacenInfo ObtenerPorProveedorTipoAlmacen(ProveedorAlmacenInfo proveedorInfo)
        {
            ProveedorAlmacenInfo proveedorAlmacen;
            try
            {
                Logger.Info();
                var proveedorAlmacenDAL = new ProveedorAlmacenDAL();
                proveedorAlmacen = proveedorAlmacenDAL.ObtenerPorProveedorTipoAlmacen(proveedorInfo);
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
