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
    internal class PremezclaBL
    {
        /// <summary>
        /// Obtiene una lista de premezclas por organizacion
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        internal List<PremezclaInfo> ObtenerPorOrganizacion(OrganizacionInfo organizacion)
        {
            try
            {
                var premezclaDal = new PremezclaDAL();
                return premezclaDal.ObtenerPorOrganizacion(organizacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtiene una premezcla por producto y organizacion id
        /// </summary>
        /// <param name="premezclaInfo"></param>
        /// <returns></returns>
        internal PremezclaInfo ObtenerPorProductoIdOrganizacionId(PremezclaInfo premezclaInfo)
        {
            try
            {
                var productoBl = new ProductoBL();
                var premezclaDal = new PremezclaDAL();
                var premezclaDetalleBl = new PremezclaDetalleBL();
                premezclaInfo = premezclaDal.ObtenerPorProductoIdOrganizacionId(premezclaInfo);
                //Obtener detalle de premezcla
                if (premezclaInfo != null)
                {
                    premezclaInfo.ListaPremezclaDetalleInfos =
                        premezclaDetalleBl.ObtenerPremezclaDetallePorPremezclaId(premezclaInfo);
                    if (premezclaInfo.ListaPremezclaDetalleInfos != null)
                    {
                        foreach (var premezclaInfoP in premezclaInfo.ListaPremezclaDetalleInfos)
                        {
                            premezclaInfo.Producto.Activo = EstatusEnum.Activo;
                            premezclaInfoP.Producto = productoBl.ObtenerPorID(premezclaInfoP.Producto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return premezclaInfo;
        }

        /// <summary>
        /// Crea una premezcla
        /// </summary>
        /// <returns></returns>
        internal int Crear(PremezclaInfo premezclaInfo)
        {
            try
            {
                Logger.Info();
                var premezclaDal = new PremezclaDAL();
                int result = premezclaDal.Crear(premezclaInfo);
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

        /// <summary>
        /// Obtiene una lista de premezclas con sus subproductos
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        internal List<PremezclaInfo> ObtenerPorOrganizacionDetalle(OrganizacionInfo organizacion)
        {
            List<PremezclaInfo> premezclas = null;
            try
            {
                var premezclaDal = new PremezclaDAL();
                premezclas = premezclaDal.ObtenerPorOrganizacionDetalle(organizacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return premezclas;
        }
    }
}
