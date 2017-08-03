using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Sanidad
{
    public partial class CancelarMovimiento : PageBase
    {

        private static int organizacionID;
        private static int usuarioID;
        private static SeguridadInfo usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Titulo.AgregarTitulo("titulo");
            lblHoraSistema.Text = DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            lblFechaSistema.Text = DateTime.Now.ToShortDateString();

            usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            msgErrorRolJefeSanidad.Value = GetLocalResourceObject("msgErrorRolJefeSanidad").ToString();
            msgErrorNoHayMovimientos.Value = GetLocalResourceObject("msgErrorNoHayMovimientos").ToString();
            msgOK.Value = GetLocalResourceObject("OK").ToString();
            

            if (usuario != null)
            {
                organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
                usuarioID = usuario.Usuario.UsuarioID;
                int rolIdUsuario = 0;

                if (usuario.Usuario.Operador != null)
                {
                    rolIdUsuario = usuario.Usuario.Operador.Rol.RolID;
                    lblJefeSanidadSeleccionado.Text = usuario.Usuario.Nombre;
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeRolUsuario();", true);
                }
                /* Se elimina esta validacion para dejarla por permisos de sistema 
                if (rolIdUsuario != (int)Roles.JefeSanidad)
                {
                    ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeRolUsuario();", true);
                }
                */

            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeRolUsuario();", true);
            }

        }

        #region Metodos

        #endregion

        

        /// <summary>
        /// Obtiene la lista de movimientos(Muertes) a cancelar
        /// </summary>
        [WebMethod]
        public static IList<MuerteInfo> ObtenerInformacionCancelarMovimiento()
        {
            IList<MuerteInfo> muertes = null;
            try
            {
                var muertesPl = new MuertePL();
                muertes = 
                    muertesPl.ObtenerInformacionCancelarMovimiento(organizacionID) 
                    ?? new List<MuerteInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return muertes;
        }

        /// <summary>
        /// Metodo para Guardar el muerteInfo
        /// </summary>
        /// <param name="muerteInfo">contenedor donde se encuentra la información de la muerte</param>
        /// <returns></returns>
        [WebMethod]
        public static string CancelarMovimientoMuerte(MuerteInfo ganadoMuertoInfo)
        {
            try
            {
                ganadoMuertoInfo.Activo = EstatusEnum.Activo;
                ganadoMuertoInfo.UsuarioCreacionID = usuarioID;
                ganadoMuertoInfo.EstatusId = (int)EstatusMuertes.Cancelado;
                
                ganadoMuertoInfo.OperadorCancelacionInfo = new OperadorInfo
                {
                    OperadorID = usuario.Usuario.Operador.OperadorID
                };
                

                var muertePL = new MuertePL();

                muertePL.CancelarMovimientoMuerte(ganadoMuertoInfo);

                return "OK";
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

    }
}