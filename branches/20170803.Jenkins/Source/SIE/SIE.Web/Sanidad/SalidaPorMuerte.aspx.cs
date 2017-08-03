using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using  SIE.Base.Log;

namespace SIE.Web.Sanidad
{
    public partial class SalidaPorMuerte : PageBase
    {
        /// <summary>
        /// Carga de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitUserData();
        }

        /// <summary>
        /// Inicializacion de datos de usuario
        /// </summary>
        private void InitUserData()
        {
            int rolIdUsuario = 0;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    if (seguridad.Usuario.Operador != null)
                    {
                        rolIdUsuario = seguridad.Usuario.Operador.Rol.RolID;
                        lblNombre.Text = seguridad.Usuario.Nombre;
                        lblOrganizacion.Text = seguridad.Usuario.Organizacion.Descripcion;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
                    }
                    /* Se elimina esta validacion para dejarla por permisos de sistema 
                    if (rolIdUsuario == (int)Roles.EncargadoNecropsia)
                    {
                        lblNombre.Text = seguridad.Usuario.Nombre;
                        lblOrganizacion.Text = seguridad.Usuario.Organizacion.Descripcion;

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
                    }*/
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            
        }

        /// <summary>
        /// Metodo para Consultar los Corrales disponibles para Cerrar y/o Reimprimir
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<MuerteInfo> ObtenerAretesMuertos()
         {
             IList<MuerteInfo> retValue = null;

             retValue = null;
             var pl = new MuertePL();

             try
             {
                 var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                 if(seguridad!= null)
                 {
                    retValue = pl.ObtenerGanadoParaSalidaPorMuerte(seguridad.Usuario.Organizacion.OrganizacionID);
                 }
             }
             catch (Exception ex)
             {
                 Logger.Error(ex);
                 throw;
             }

            return retValue;
        }

    }
}