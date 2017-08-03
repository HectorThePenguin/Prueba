using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Xml.Linq;
using System.Linq;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ProveedorChoferDAL : DALBase
    {
        /// <summary>
        /// Obtiene el proveedor chofer por su identificador.
        /// </summary>
        /// <param name="proveedorChoferId"></param>
        /// <returns></returns>
        internal ProveedorChoferInfo ObtenerProveedorChoferPorId(int proveedorChoferId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxProveedorChoferDAL.ObtenerProveedorChoferPorId(proveedorChoferId);
                var ds = Retrieve("ProveedorChofer_ObtenerPorId", parameters);
                ProveedorChoferInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorChoferDAL.ObtenerProveedorChoferPorId(ds);
                }
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
        /// Obtiene una lista de proveedor chofer por proveedorid
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        internal List<ProveedorChoferInfo> ObtenerProveedorChoferPorProveedorId(int proveedorId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxProveedorChoferDAL.ObtenerParametrosProveedorChoferPorProveedorId(proveedorId);
                var ds = Retrieve("ProveedorChofer_ObtenerPorProveedorID", parameters);
                List<ProveedorChoferInfo> listaProveedorChofer = null;
                if (ValidateDataSet(ds))
                {
                    listaProveedorChofer = MapProveedorChoferDAL.ObtenerProveedorChoferPorProveedorId(ds);
                }
                return listaProveedorChofer;
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
                var parameters = AuxProveedorChoferDAL.ObtenerProveedorChoferPorChoferID(proveedorId,choferId);
                var ds = Retrieve("ProveedorChofer_ObtenerPorChoferID", parameters);
                ProveedorChoferInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorChoferDAL.ObtenerProveedorChoferPorChoferID(ds);
                }
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
            try
            {
                Logger.Info();
                var parameters = AuxProveedorChoferDAL.ObtenerParametrosObtenerProveedorChoferPorProveedorIdChoferId(proveedorId, choferId);
                var ds = Retrieve("ProveedorChofer_ObtenerPorProveedorIdChoferId", parameters);
                ProveedorChoferInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorChoferDAL.ObtenerProveedorChoferPorId(ds);
                }
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
        /// Guarda a los proveedores y sus choferes
        /// </summary>
        /// <param name="proveedorChoferesInfo">Lista que contiene al proveedor y al chofer</param>
        /// <returns></returns>
        internal List<ProveedorChoferInfo> Guardar(List<ProveedorChoferInfo> proveedorChoferesInfo)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("Root",
                                from p in proveedorChoferesInfo
                                select
                                    new XElement("ProveedorChofer",
                                        new XAttribute("ProveedorId", p.Proveedor.ProveedorID),
                                        new XAttribute("ChoferId", p.Chofer.ChoferID),
                                        new XAttribute("UsuarioId", p.UsuarioCreacionID)
                                    )
                         );

                
                var parameters = new Dictionary<string, object>();
                parameters.Add("@XML", xml.ToString());

                var ds = Retrieve("ProveedorChofer_Guardar", parameters);
                List<ProveedorChoferInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorChoferDAL.ObtenerProveedorChoferPorProveedorId(ds);
                }
                return new List<ProveedorChoferInfo>();
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
    }
}
