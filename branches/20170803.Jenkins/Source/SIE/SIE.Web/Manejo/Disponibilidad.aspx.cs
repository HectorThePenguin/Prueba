using System;
using System.Collections.Generic;
using System.Web.Services;
using SIE.Base.Vista;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Manejo
{
    public partial class Disponibilidad : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Método para consultar las semanas
        /// </summary>
        /// <returns>Regresa una lista de semanas</returns>
        [WebMethod]
        public static List<SemanaInfo> TraerSemanas()
        {
            try
            {
                var semanas =
                    new
                        List<SemanaInfo>
                        {
                            new SemanaInfo {SemanaID = 6, Descripcion = "6"},
                            new SemanaInfo {SemanaID = 7, Descripcion = "7"},
                            new SemanaInfo {SemanaID = 8, Descripcion = "8"},
                            new SemanaInfo {SemanaID = 9, Descripcion = "9"},
                            new SemanaInfo {SemanaID = 10, Descripcion = "10"},
                            new SemanaInfo {SemanaID = 11, Descripcion = "11"},
                            new SemanaInfo {SemanaID = 0, Descripcion = "REVISIÓN"},
                        };
                return semanas;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para consultar las semanas
        /// </summary>
        /// <returns>Regresa una lista de semanas</returns>
        [WebMethod]
        public static DateTime TraerFechaInicioSemana6()
        {
            try
            {
                return DateTime.Now.Date.AddDays(28);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para consultar las disponibilidades
        /// </summary>
        /// <returns>Regresa una lista de disponinilidades</returns>
        [WebMethod]
        public static List<DisponibilidadLoteInfo> TraerDisponibilidad(FiltroDisponilidadInfo filtroDisponilidadInfo)
        {
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtroDisponilidadInfo.OrganizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                filtroDisponilidadInfo.Fecha = DateTime.Now;
                if (filtroDisponilidadInfo.Semanas != 0)
                {
                    filtroDisponilidadInfo.Semanas = filtroDisponilidadInfo.Semanas - 1;
                }
                var lotePL = new LotePL();
                var disponibilidadLote = lotePL.ObtenerLotesPorDisponibilidad(filtroDisponilidadInfo);

                return disponibilidadLote;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para Guardar
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static void Guardar(FiltroDisponilidadInfo filtroDisponilidadInfo)
        {
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtroDisponilidadInfo.UsuarioId = seguridad.Usuario.UsuarioID;
                var lotePL = new LotePL();
                lotePL.ActualizarLoteDisponibilidad(filtroDisponilidadInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}