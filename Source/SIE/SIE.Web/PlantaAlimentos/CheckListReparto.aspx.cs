using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;

namespace SIE.Web.PlantaAlimentos
{
    public partial class CheckListReparto : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfErrorImprimir.Value = Session["ErrorCheckListReparto"] != null ? Session["ErrorCheckListReparto"].ToString() : string.Empty;
            Session["ErrorCheckListReparto"] = null;
            var seguridad = (SeguridadInfo)ObtenerSeguridad();
            if(seguridad.Usuario.Operador == null)
            {
                hfOperador.Value = "0";
                return;
            }
            hfOperadorID.Value = seguridad.Usuario.UsuarioID.ToString(CultureInfo.InvariantCulture);
            hfOperador.Value = seguridad.Usuario.Nombre;
        }

        private static int _turnoManual = 3;

        /// <summary>
        /// Obtiene las Causas de Tiempo Muerto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<CausaTiempoMuertoInfo> ObtenerCausasTiempoMuerto()
        {
            try
            {
                using (var causaTiempoMuertoBL = new CausaTiempoMuertoBL())
                {
                    List<CausaTiempoMuertoInfo> listaCausas = causaTiempoMuertoBL.ObtenerPorTipoCausa(TipoCausa.CheckListReparto.GetHashCode()).ToList();
                    return listaCausas;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las Causas de Tiempo Muerto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<TipoServicioInfo> ObtenerTiposServicio()
        {
            try
            {
                using (var tipoServicioBL = new TipoServicioBL())
                {
                    var listaTipoServicio = tipoServicioBL.ObtenerTodos(EstatusEnum.Activo);
                    return listaTipoServicio.Where(tipo => tipo.TipoServicioId != _turnoManual).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las Causas de Tiempo Muerto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static CamionRepartoInfo ObtenerPorNumeroEconomico(string numeroEconomico)
        {
            try
            {
                using (var camionRepartoBL = new CamionRepartoBL())
                {
                    var seguridad = (SeguridadInfo)ObtenerSeguridad();
                    CamionRepartoInfo camionReparto = camionRepartoBL.ObtenerPorNumeroEconomico(numeroEconomico,
                                                                                                seguridad.Usuario.Organizacion.OrganizacionID);
                    return camionReparto;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las Causas de Tiempo Muerto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<CamionRepartoInfo> ObtenerPorNumeroEconomicoBusqueda(string numeroEconomico)
        {
            try
            {
                using (var camionRepartoBL = new CamionRepartoBL())
                {
                    var seguridad = (SeguridadInfo)ObtenerSeguridad();
                    IList<CamionRepartoInfo> camionReparto = camionRepartoBL.ObtenerPorNumeroEconomicoBusqueda(numeroEconomico,
                                                                                                seguridad.Usuario.Organizacion.OrganizacionID);
                    return camionReparto;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las Causas de Tiempo Muerto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static FiltroGenerarArchivoDataLink GenerarRepartos(FiltroCheckListReparto filtro)
        {
            try
            {
                var repartoAlimentoBL = new RepartoAlimentoBL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtro.OperadorID = seguridad.Usuario.Operador.OperadorID;
                filtro.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                FiltroGenerarArchivoDataLink repartos = repartoAlimentoBL.GenerarRepartos(filtro);
                return repartos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las Causas de Tiempo Muerto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static void GuardarReparto(RepartoAlimentoInfo reparto, List<GridRepartosModel> repartoDetalle)
        {
            try
            {
                var repartoAlimentoBL = new RepartoAlimentoBL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                reparto.UsuarioCreacionID = seguridad.Usuario.UsuarioID;
                if(reparto.RepartoAlimentoID > 0)
                {
                    reparto.UsuarioModificacionID = seguridad.Usuario.UsuarioID;
                }
                reparto.UsuarioIDReparto = seguridad.Usuario.UsuarioID;
                int organizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                repartoAlimentoBL.Guardar(reparto, repartoDetalle, organizacionID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las Causas de Tiempo Muerto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static DateTime ObtenerFechaServidor()
        {
            try
            {
                return DateTime.Now;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las Causas de Tiempo Muerto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static RepartoAlimentoInfo ConsultarRepartos(FiltroCheckListReparto filtro)
        {
            try
            {
                var repartoAlimentoBL = new RepartoAlimentoBL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtro.OperadorID = seguridad.Usuario.UsuarioID;
                RepartoAlimentoInfo repartos = repartoAlimentoBL.ConsultarRepartos(filtro);
                return repartos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }


    }
}