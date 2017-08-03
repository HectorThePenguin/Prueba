using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class FleteInternoPL
    {
        /// <summary>
        /// Metodo para obtener un listado de tipos contrato por estatus
        /// </summary>
        public List<FleteInternoInfo> ObtenerFletesPorEstado(EstatusEnum estatus)
        {
            List<FleteInternoInfo> result;
            try
            {
                Logger.Info();
                var fleteInternoBL = new FleteInternoBL();
                result = fleteInternoBL.ObtenerFletesPorEstado(estatus);
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
            return result;
        }

        /// <summary>
        /// Obtiene un listado de fletes por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<FleteInternoInfo> ObtenerPorPaginaFiltroDescripcionOrganizacion(PaginacionInfo pagina, FleteInternoInfo filtro)
        {
            ResultadoInfo<FleteInternoInfo> fleteInternoLista;
            try
            {
                Logger.Info();
                var fleteInternoBl = new FleteInternoBL();
                fleteInternoLista = fleteInternoBl.ObtenerPorPaginaFiltroDescripcionOrganizacion(pagina, filtro);
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
            return fleteInternoLista;
        }

        /// <summary>
        /// Obtiene un flete interno
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public FleteInternoInfo ObtenerPorConfiguracion(FleteInternoInfo filtro)
        {
            FleteInternoInfo fleteInterno;
            try
            {
                Logger.Info();
                var fleteInternoBl = new FleteInternoBL();
                fleteInterno = fleteInternoBl.ObtenerPorConfiguracion(filtro);
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
            return fleteInterno;
        }

        /// <summary>
        /// Obtiene un listado de costos por fleteconfiguracion
        /// </summary>
        /// <param name="fleteInternoInfo"></param>
        /// <param name="proveedorInfo"></param>
        /// <returns></returns>
        public List<FleteInternoCostoInfo> ObtenerCostosPorFleteConfiguracion(FleteInternoInfo fleteInternoInfo, ProveedorInfo proveedorInfo)
        {
            try
            {
                Logger.Info();
                var fleteInternoBl = new FleteInternoBL();
                List<FleteInternoCostoInfo> resultado = fleteInternoBl.ObtenerCostosPorFleteConfiguracion(fleteInternoInfo, proveedorInfo);
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
