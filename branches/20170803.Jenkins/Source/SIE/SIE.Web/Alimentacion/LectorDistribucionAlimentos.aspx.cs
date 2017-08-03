using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Alimentacion
{
    public partial class LectorDistribucionAlimentos : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            if (seguridad == null)
            {
                var localResourceObject = GetLocalResourceObject("mensajeErrorSession");
                if (localResourceObject != null)
                    EnviarMensajeUsuarioErrorSession(localResourceObject.ToString());
            }
            else
            {
                txtOrganizacion.Text = seguridad.Usuario.Organizacion.Descripcion;
                txtHora.Text = DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                txtFecha.Text = DateTime.Now.ToShortDateString();

                txtNombreLector.Text = seguridad.Usuario.Operador == null ? seguridad.Usuario.Nombre : seguridad.Usuario.Operador.NombreCompleto;
            }
        }
        /// <summary>
        /// Envia un mensaje al usuario ejecutando una instruccion javascript.
        /// </summary>
        /// <param name="mensaje"></param>
        private void EnviarMensajeUsuarioErrorSession(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuarioErrorSession('" + mensaje + "');", true);
        }

        #region Metodos Web

        /// <summary>
        /// Método que obtiene el siguiente corral en la lista segun se registro.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static RepartoInfo ObtenerSiguienteCorral()
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            RepartoInfo reparto = null;
            var repartoPl = new RepartoPL();

            try
            {
                if (seguridad != null)
                {
                    if (seguridad.Usuario.Operador == null)
                    {
                        var operadorPL = new OperadorPL();
                        seguridad.Usuario.Operador = operadorPL.ObtenerPorUsuarioId(seguridad.Usuario.UsuarioID, seguridad.Usuario.OrganizacionID);
                    }
                    reparto = repartoPl.ObtenerRepartoPorOperadorId(seguridad.Usuario.Operador, new CorralInfo());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                reparto = null;
            }

            return reparto;
        }

        /// <summary>
        /// Método que obtiene el listado de estatus de distribución de alimentos.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<EstatusInfo> ObtenerEstatusDistribucion()
        {
            var estatusPl = new EstatusPL();

            try
            {
                return estatusPl.ObtenerEstatusTipoEstatus((int) TipoEstatus.Distribucióndealimentos);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Método que valida el corral ingresado por el usuario.
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public static CorralInfo ValidarCorral(CorralInfo corralInfo)
        {
            var corral = new CorralInfo();

            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                    var operadorId = seguridad.Usuario.Operador.OperadorID;

                    var corralPL = new CorralPL();
                    var operadorPL = new OperadorPL();

                    corral = corralPL.ObtenerCorralPorCodigo(organizacionId, corralInfo.Codigo);

                    if (corral != null)
                    {
                        var operador = operadorPL.ObtenerLectorPorCodigoCorral(corral.Codigo, organizacionId);

                        if (operador != null)
                        {
                            if (operador.OperadorID == operadorId)
                            {
                                corral.Operador = operador;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                corral = null;
            }

            return corral;
        }

        /// <summary>
        /// Método que obtiene el reparto del corral tecleado por el usuario.
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        [WebMethod]
        public static RepartoInfo ObtenerRepartoPorCorral(CorralInfo corral)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            RepartoInfo reparto = null;
            var repartoPl = new RepartoPL();

            try
            {
                if (seguridad != null)
                {
                    reparto = repartoPl.ObtenerRepartoPorOperadorId(seguridad.Usuario.Operador, corral);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                reparto = null;
            }

            return reparto;
        }

        /// <summary>
        /// Método que guarda el estatus en que se encontraba el corral.
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="estatusDistribucion"></param>
        /// <returns></returns>
        [WebMethod]
        public static int GuardarEstatusDistribucion(string codigoCorral, int estatusDistribucion)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            int resultado = 0;
            var repartoPl = new RepartoPL();
            var corral = new CorralInfo();

            try
            {
                if (seguridad != null)
                {
                    corral.Codigo = codigoCorral;
                    corral.Organizacion = seguridad.Usuario.Organizacion;
                    corral.UsuarioCreacionID = seguridad.Usuario.UsuarioID;

                    resultado = repartoPl.GuardarEstatusDistribucion(corral, estatusDistribucion);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                resultado = 0;
            }
            return resultado;
        }
        
        #endregion
    }
}