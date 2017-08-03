using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class IncidenciasPL
    {
        /// <summary>
        /// Proceso para obtener las incidencias del SIAP
        /// </summary>
        public void GenerarIncidenciasSIAP()
        {
            try
            {
                Logger.Info();
                var incidenciaBl = new IncidenciasBL();
                incidenciaBl.GenerarIncidenciasSIAP();
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
        /// Proceso para obtener las configuraciones de alertas
        /// </summary>
        public List<AlertaInfo> ObtenerConfiguracionAlertas(EstatusEnum activo)
        {
            List<AlertaInfo> listaAlertaConfiguracion = null;
            try
            {
                var incidenciasBL = new IncidenciasBL();
                listaAlertaConfiguracion = incidenciasBL.ObtenerConfiguracionAlertas(activo);
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
            return listaAlertaConfiguracion;
        }

        /// <summary>
        /// Proceso para obtener las incidencias de la organización
        /// </summary>
        public List<IncidenciasInfo> ObtenerIncidenciasPorOrganizacionID(int organizacionID, bool usuarioCorporativo)
        {
            try
            {
                Logger.Info();
                var incidenciaBl = new IncidenciasBL();
                return incidenciaBl.ObtenerIncidenciasPorOrganizacionID(organizacionID, usuarioCorporativo);
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
        /// Proceso para actualizar la incidencia
        /// <param name="incidencia"></param>
        /// </summary>
        public IncidenciasInfo ActualizarIncidencia(IncidenciasInfo incidencia)
        {
            try
            {
                Logger.Info();
                var incidenciaBl = new IncidenciasBL();
                return incidenciaBl.ActualizarIncidencia(incidencia);
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
        /// Proceso para rechazar la incidencia
        /// <param name="incidencia"></param>
        /// </summary>
        public IncidenciasInfo RechazarIncidencia(IncidenciasInfo incidencia)
        {
            try
            {
                Logger.Info();
                var incidenciaBl = new IncidenciasBL();
                return incidenciaBl.RechazarIncidencia(incidencia);
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
        /// Metodo para autorizar la incidencia
        /// </summary>
        /// <param name="incidenciaInfo"></param>
        public void AutorizarIncidencia(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                var incidenciasBL = new IncidenciasBL();
                incidenciasBL.AutorizarIncidencia(incidenciaInfo);
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
        /// Proceso para obtener el seguimiento de una incidencia
        /// <param name="incidenciaID"></param>
        /// </summary>
        public List<IncidenciaSeguimientoInfo> ObtenerSeguimientoPorIncidenciaID(int incidenciaID)
        {
            try
            {
                Logger.Info();
                var incidenciaBl = new IncidenciasBL();
                return incidenciaBl.ObtenerSeguimientoPorIncidenciaID(incidenciaID);
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
