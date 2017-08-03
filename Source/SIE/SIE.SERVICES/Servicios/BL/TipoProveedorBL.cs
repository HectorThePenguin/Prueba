
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class TipoProveedorBL
    {
        /// <summary>
        ///     Obtiene una lista de los tipo de proveedores
        /// </summary>
        /// <returns></returns>
        internal IList<TipoProveedorInfo> ObtenerTodos()
        {
            IList<TipoProveedorInfo> lista;
            try
            {
                Logger.Info();
                var tipoProveedorDAL = new TipoProveedorDAL();
                lista = tipoProveedorDAL.ObtenerTodos();
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
            return lista;
        }

        /// <summary>
        ///     Obtiene una lista de TipoProveedor filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<TipoProveedorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoProveedorDAL = new TipoProveedorDAL();
                IList<TipoProveedorInfo> lista = tipoProveedorDAL.ObtenerTodos(estatus);

                return lista;
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

