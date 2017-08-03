using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TipoProveedorPL
    {
        /// <summary>
        ///     Obtiene un lista de los tipos de proveedor
        /// </summary>
        /// <returns> </returns>
        public IList<TipoProveedorInfo> ObtenerTodos()
        {
            IList<TipoProveedorInfo> lista;
            try
            {
                Logger.Info();
                var tipoProveedorBL = new TipoProveedorBL();
                lista = tipoProveedorBL.ObtenerTodos();
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
        ///     Obtiene un lista de los tipos de proveedor
        /// </summary>
        /// <returns> </returns>
        public IList<TipoProveedorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoProveedorBL = new TipoProveedorBL();
                IList<TipoProveedorInfo> lista = tipoProveedorBL.ObtenerTodos(estatus);

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
