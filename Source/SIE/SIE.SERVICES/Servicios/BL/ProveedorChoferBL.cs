using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ProveedorChoferBL
    {
        /// <summary>
        /// Método que obtiene el chofer por su identificador
        /// </summary>
        /// <param name="proveedorChoferId"></param>
        /// <returns></returns>
        internal ProveedorChoferInfo ObtenerProveedorChoferPorId(int proveedorChoferId)
        {
            ProveedorChoferInfo resultado;
            try
            {
                Logger.Info();
                var proveedorChofer = new ProveedorChoferDAL();
                resultado = proveedorChofer.ObtenerProveedorChoferPorId(proveedorChoferId);

                if (resultado != null)
                {
                    var choferBl = new ChoferBL();
                    resultado.Chofer = choferBl.ObtenerPorID(resultado.Chofer);

                    var proveedorBl = new ProveedorBL();
                    resultado.Proveedor = proveedorBl.ObtenerPorID(resultado.Proveedor.ProveedorID);
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        internal List<ProveedorChoferInfo> ObtenerProveedorChoferPorProveedorId(int proveedorId)
        {
            List<ProveedorChoferInfo> listaProveedorChofer;
            try
            {
                Logger.Info();
                var proveedorChofer = new ProveedorChoferDAL();
                listaProveedorChofer = proveedorChofer.ObtenerProveedorChoferPorProveedorId(proveedorId);

                if (listaProveedorChofer != null)
                {
                    foreach (var registroProveedorChofer in listaProveedorChofer)
                    {
                        var choferBl = new ChoferBL();
                        registroProveedorChofer.Chofer = choferBl.ObtenerPorID(registroProveedorChofer.Chofer);

                        var proveedorBl = new ProveedorBL();
                        registroProveedorChofer.Proveedor = proveedorBl.ObtenerPorID(registroProveedorChofer.Proveedor.ProveedorID);
                    }
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
        internal ProveedorChoferInfo ObtenerProveedorChoferPorChoferID(int proveedorId,int choferId)
        {
            try
            {
                Logger.Info();
                var proveedorChoferDal = new ProveedorChoferDAL();
                ProveedorChoferInfo result = proveedorChoferDal.ObtenerProveedorChoferPorChoferID(proveedorId, choferId);
                return result;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene el proveedor chofer por el Chofer y Proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <param name="choferId"></param>
        /// <returns></returns>
        internal ProveedorChoferInfo ObtenerProveedorChoferPorProveedorIdChoferId(int proveedorId, int choferId)
        {
            ProveedorChoferInfo proveedorChofer = null;

            try
            {
                Logger.Info();
                var proveedorChoferDal = new ProveedorChoferDAL();
                proveedorChofer = proveedorChoferDal.ObtenerProveedorChoferPorProveedorIdChoferId(proveedorId, choferId);
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
