using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProveedorChoferDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener chofer proveedor
        /// </summary>
        /// <param name="proveedorChoferId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerProveedorChoferPorId(int proveedorChoferId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProveedorChoferID", proveedorChoferId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        /// Parametros para consultar los choferes del proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosProveedorChoferPorProveedorId(int ProveedorId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProveedorID", ProveedorId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        /// Parametros para consultar los choferes del proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosProveedorChoferPorContratoId(int folio, int TipoFlete)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Folio", folio},
                        {"@TipoFlete", TipoFlete}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        ///  Obtiene los campos de la tabla ProveedorChofer consultando por ChoferID
        /// </summary>
        /// <param name="choferId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerProveedorChoferPorChoferID(int proveedorId,int choferId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProveedorID",proveedorId},
                        {"@ChoferID", choferId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        /// Obtiene los parametros para Obtener el proveedor chofer por el Chofer y Proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <param name="choferId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerProveedorChoferPorProveedorIdChoferId(int proveedorId, int choferId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProveedorId", proveedorId},
                        {"@ChoferId", choferId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

    }
}
