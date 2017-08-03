using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Log;
using SIE.Base.Vista;
using System.Globalization;
using System.Web;
using System.Web.Services;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;
using SIE.Base.Exepciones;

namespace SIE.Web.Administracion
{
    public partial class AutorizacionMovimientos : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if (seguridad != null && seguridad.Usuario.Operador != null)
            {
                txtPrecioVenta.Text = TipoAutorizacionEnum.PrecioVenta.GetHashCode().ToString();
                txtUsoLote.Text = TipoAutorizacionEnum.UsoLote.GetHashCode().ToString();
                txtAjusteInventario.Text = TipoAutorizacionEnum.AjustedeInventario.GetHashCode().ToString();
                txtAutorizado.Text = Estatus.AMPAutoriz.GetHashCode().ToString();
                txtRechazado.Text = Estatus.AMPRechaza.GetHashCode().ToString();
            }
            ObtenerMovimientos();
        }
        /// <summary>
        /// Valida que se cumplan las precondiciones
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ValidarPreCondiciones()
        {
            int result = 0;
            try
            {
                var precondicionesPL = new TipoAutorizacionPL();
                result = precondicionesPL.ValidarPreCondiciones();
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }
        /// <summary>
        /// Obtiene los Movimientos a Autorizar
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public void ObtenerMovimientos()
        {
            try
            {
                var movimientoPL = new TipoAutorizacionPL();
                List<MovimientosAutorizarModel> listaMovimientos = movimientoPL.ObtenerMovimientosAutorizacion();

                listaMovimientos.Add(new MovimientosAutorizarModel() { TipoAutorizacionID = 0, Descripcion = "Seleccione" });
                cmbMovimiento.DataSource = listaMovimientos;
                cmbMovimiento.DataTextField = "Descripcion";
                cmbMovimiento.DataValueField = "TipoAutorizacionID";
                cmbMovimiento.DataBind();
                cmbMovimiento.SelectedValue = "0";
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Obtiene la lista de Organizaciones de Tipo Ganadera
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        [WebMethod]
        public static List<OrganizacionInfo> ObtenerOrganizacion(int organizacion)
        {
            List<OrganizacionInfo> listaOrganizacionesTipoGanadera = null;

            try
            {
                var organizacionesPl = new OrganizacionPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaOrganizacionesTipoGanadera = (List<OrganizacionInfo>)organizacionesPl.ObtenerTipoGanaderas();

                    if (organizacion > 0 && listaOrganizacionesTipoGanadera != null)
                    {
                        //Filtra todos los folios que contengan el numero de folio capturado
                        listaOrganizacionesTipoGanadera = listaOrganizacionesTipoGanadera.Where(
                                registro => registro.OrganizacionID.ToString(CultureInfo.InvariantCulture)
                                        .Contains(organizacion.ToString(CultureInfo.InvariantCulture))).ToList();
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaOrganizacionesTipoGanadera;
        }
        /// <summary>
        /// Obtiene la lista de Organizaciones de Tipo Ganadera
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        [WebMethod]
        public static List<OrganizacionInfo> ObtenerOrganizacionesTipoGanadera(string organizacion)
        {
            List<OrganizacionInfo> listaOrganizacionesTipoGanadera = null;

            try
            {
                var organizacionesPl = new OrganizacionPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaOrganizacionesTipoGanadera = (List<OrganizacionInfo>)organizacionesPl.ObtenerTipoGanaderas();

                    if (organizacion.Length > 0 && listaOrganizacionesTipoGanadera != null)
                    {
                        //Filtra todos las organizaciones que contengan lo capturado
                        listaOrganizacionesTipoGanadera = listaOrganizacionesTipoGanadera.Where(
                                registro => registro.Descripcion.ToString(CultureInfo.InvariantCulture).ToUpper()
                                        .Contains(organizacion.ToString(CultureInfo.InvariantCulture).ToUpper())).ToList();
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaOrganizacionesTipoGanadera;
        }
        /// <summary>
        /// Obtiene las solicitudes de precio de venta con estatus pendiente
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="tipoAutorizacionID"></param>
        /// <returns>Lista de SolicitudAutorizacionPendientesInfo</returns>
        [WebMethod]
        public static List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesPrecioVenta(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;

            try
            {
                var solicitudesPl = new SolicitudAutorizacionPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaSolicitudesPendientes = solicitudesPl.ObtenerSolicitudesPendientesPrecioVenta(organizacionID, tipoAutorizacionID);
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaSolicitudesPendientes;
        }
        /// <summary>
        /// Obtiene las solicitudes de uso de lote con estatus pendiente
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="tipoAutorizacionID"></param>
        /// <returns>Lista de SolicitudAutorizacionPendientesInfo</returns>
        [WebMethod]
        public static List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesUsoLote(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;

            try
            {
                var solicitudesPl = new SolicitudAutorizacionPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaSolicitudesPendientes = solicitudesPl.ObtenerSolicitudesPendientesUsoLote(organizacionID, tipoAutorizacionID);
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaSolicitudesPendientes;
        }
        /// <summary>
        /// Obtiene las solicitudes de ajuste de inventario con estatus pendiente
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns>Lista de SolicitudAutorizacionPendientesInfo</returns>
        [WebMethod]
        public static List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesAjusteInventario(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;

            try
            {
                var solicitudesPl = new SolicitudAutorizacionPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaSolicitudesPendientes = solicitudesPl.ObtenerSolicitudesPendientesAjusteInventario(organizacionID, tipoAutorizacionID);
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaSolicitudesPendientes;
        }
        /// <summary>
        /// Guarda la respuesta a las solicitudes
        /// </summary>
        /// <param name="respuestaSolicitudes"></param>
        /// <param name="organizacionID"></param>
        /// <param name="tipoAutorizacionID"></param>
        /// <returns></returns>
        [WebMethod]
        public static ResultadoValidacion GuardarRespuestasSolicitudes(List<AutorizacionMovimientosInfo> respuestaSolicitudes, int organizacionID, int tipoAutorizacionID)
        {
            var result = new ResultadoValidacion();
            try
            {
                var respuestaPl = new SolicitudAutorizacionPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    int usuarioID = seguridad.Usuario.UsuarioID;
                    result = respuestaPl.GuardarRespuestasSolicitudes(respuestaSolicitudes, organizacionID, tipoAutorizacionID, usuarioID);
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return result;
        }
    }
}