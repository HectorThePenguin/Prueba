using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class FleteInternoBL
    {
        /// <summary>
        /// Crea un flete interno
        /// </summary>
        /// <returns></returns>
        internal int Crear(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                var fleteInternoDal = new FleteInternoDAL();
                int result = fleteInternoDal.Crear(fleteInternoInfo);
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
        /// Obtiene el total de fletes (activos e inactivos)
        /// </summary>
        /// <returns></returns>
        internal List<FleteInternoInfo> ObtenerFletesPorEstado(EstatusEnum estatus)
        {
            List<FleteInternoInfo> result;
            try
            {
                Logger.Info();
                var fleteInternoDal = new FleteInternoDAL();
                result = fleteInternoDal.ObtenerFletesPorEstado(estatus);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un listado de fletes internos por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<FleteInternoInfo> ObtenerPorPaginaFiltroDescripcionOrganizacion(PaginacionInfo pagina, FleteInternoInfo filtro)
        {

            try
            {
                Logger.Info();
                var fleteInternoDal = new FleteInternoDAL();
                ResultadoInfo<FleteInternoInfo> fleteInternoLista = fleteInternoDal.ObtenerPorPaginaFiltroDescripcionOrganizacion(pagina, filtro);
                return fleteInternoLista;
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
        /// Actualiza el campo activo de un flete interno
        /// </summary>
        internal void ActualizarEstado(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                var fleteInternDal = new FleteInternoDAL();
                fleteInternDal.ActualizarEstado(fleteInternoInfo);
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
        /// Obtiene un flete interno por configuracion
        /// </summary>
        /// <returns></returns>
        internal FleteInternoInfo ObtenerPorConfiguracion(FleteInternoInfo fleteInternoInfo)
        {
            FleteInternoInfo result;
            try
            {
                Logger.Info();
                var fleteInternoDal = new FleteInternoDAL();
                result = fleteInternoDal.ObtenerPorConfiguracion(fleteInternoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un listado de costos por configuracion flete
        /// </summary>
        /// <returns></returns>
        internal List<FleteInternoCostoInfo> ObtenerCostosPorFleteConfiguracion(FleteInternoInfo fleteInternoInfo, ProveedorInfo proveedorInfo)
        {
            try
            {
                Logger.Info();
                var fleteInternoDal = new FleteInternoDAL();
                List<FleteInternoCostoInfo> resultado = fleteInternoDal.ObtenerCostosPorFleteConfiguracion(fleteInternoInfo, proveedorInfo);
                return resultado;
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
