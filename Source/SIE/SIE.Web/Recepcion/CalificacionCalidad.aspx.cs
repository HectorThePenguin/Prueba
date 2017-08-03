using System;
using System.Collections.Generic;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Recepcion
{
    public partial class CalificacionCalidad : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Metodo para Consultar las calidades del ganado
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<CalidadGanadoInfo> TraerCalidades()
        {
            try
            {
                var calidadGanadoPL = new CalidadGanadoPL();
                var calidades = calidadGanadoPL.ObtenerTodosCapturaCalidad(EstatusEnum.Activo);
                return calidades;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar la información de la entrada de Ganado
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static EntradaGanadoInfo TraerInformacionEntrada(FiltroCalificacionGanadoInfo filtroCalificacionGanado)
        {
            try
            {
                var entradaGanadoPL = new EntradaGanadoPL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtroCalificacionGanado.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                var entradaGanado = entradaGanadoPL.ObtenerEntradaGanadoCapturaCalidad(filtroCalificacionGanado);
                return entradaGanado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar la información de la entrada de Ganado
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<EntradaGanadoCalidadInfo> TraerCalidadesEntradaGanado(FiltroCalificacionGanadoInfo filtroCalificacionGanado)
        {
            try
            {
                var entradaGanadoCalidadPL = new EntradaGanadoCalidadPL();
                var entradaGanadoCalidad =
                    entradaGanadoCalidadPL.ObtenerEntradaGanadoId(filtroCalificacionGanado.EntradaGanadoID);
                return entradaGanadoCalidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar la información de la entrada de Ganado
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static void Guardar(FiltroCalificacionGanadoInfo filtroCalificacionGanadoInfo)
        {
            try
            {
                var entradaGanadoCosteoPL = new EntradaGanadoCosteoPL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtroCalificacionGanadoInfo.UsuarioID = seguridad.Usuario.UsuarioID;
                entradaGanadoCosteoPL.GuardarCalidadGanado(filtroCalificacionGanadoInfo);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}