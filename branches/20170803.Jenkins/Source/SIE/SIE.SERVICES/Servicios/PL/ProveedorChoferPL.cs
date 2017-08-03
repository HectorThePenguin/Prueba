using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ProveedorChoferPL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        public List<ProveedorChoferInfo> ObtenerProveedorChoferPorProveedorId(int proveedorId)
        {
            List<ProveedorChoferInfo> listaProveedorChofer = null;

            try
            {
                Logger.Info();
                var proveedorChoferBL = new ProveedorChoferBL();
                listaProveedorChofer = proveedorChoferBL.ObtenerProveedorChoferPorProveedorId(proveedorId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaProveedorChofer;
        }
        /// <summary>
        ///  Obtiene los campos de la tabla ProveedorChofer consultando por ChoferID
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <param name="choferId"></param>
        /// <returns></returns>
        public ProveedorChoferInfo ObtenerProveedorChoferPorChoferID (int proveedorId,int choferId)
        {
            try
            {
                Logger.Info();
                var proveedorChoferBL = new ProveedorChoferBL();
                ProveedorChoferInfo result = proveedorChoferBL.ObtenerProveedorChoferPorChoferID(proveedorId, choferId);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proveedorChoferId"></param>
        /// <returns></returns>
        public ProveedorChoferInfo ObtenerProveedorChoferPorProveedorChoferId(int proveedorChoferId)
        {
            ProveedorChoferInfo proveedorChofer = null;

            try
            {
                Logger.Info();
                var proveedorChoferBL = new ProveedorChoferBL();
                proveedorChofer = proveedorChoferBL.ObtenerProveedorChoferPorId(proveedorChoferId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return proveedorChofer;
        }
        /// <summary>
        /// Obtiene el proveedor chofer por el Chofer y Proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <param name="choferId"></param>
        /// <returns></returns>
        public ProveedorChoferInfo ObtenerProveedorChoferPorProveedorIdChoferId(int proveedorId, int choferId)
        {
            ProveedorChoferInfo proveedorChofer = null;

            try
            {
                Logger.Info();
                var proveedorChoferBL = new ProveedorChoferBL();
                proveedorChofer = proveedorChoferBL.ObtenerProveedorChoferPorProveedorIdChoferId(proveedorId, choferId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return proveedorChofer;
        }
    }
}
