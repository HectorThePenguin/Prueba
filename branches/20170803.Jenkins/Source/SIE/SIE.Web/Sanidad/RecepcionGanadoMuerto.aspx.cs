using System;
using System.Collections.Generic;
using System.Web;
using Entidad;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using utilerias;
using System.Web.Services;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.Base.Log;

namespace SIE.Web.Sanidad
{
    public partial class RecepcionGanadoMuerto : PageBase
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

                        lblOrganizacion.Text = seguridad.Usuario.Organizacion.Descripcion;
                        lblNombre.Text += " " + seguridad.Usuario.Nombre;
                        lblFecha.Text += " " + DateTime.Now.ToShortDateString();
                        lblHora.Text += " " + DateTime.Now.ToShortTimeString();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
                    }

                    /* Se elimina esta validacion para dejarla por permisos de sistema 
                    if (rolIdUsuario == (int)Roles.EncargadoNecropsia)
                    {
                        lblOrganizacion.Text = seguridad.Usuario.Organizacion.Descripcion;
                        lblNombre.Text += " " + seguridad.Usuario.Nombre;
                        lblFecha.Text += " " + DateTime.Now.ToShortDateString();
                        lblHora.Text += " " + DateTime.Now.ToShortTimeString();

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
        public static IList<MuerteInfo> ObtenerAretesMuertosRecepcion()
        {
            IList<MuerteInfo> retValue = null;

            retValue = null;
            var pl = new MuertePL();

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    retValue = pl.ObtenerGanadoMuertoParaRecepcion(seguridad.Usuario.Organizacion.OrganizacionID);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return retValue;
        }

        /// <summary>
        /// Metodo guardar los aretes recibidos en necropsia
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Response<MuerteInfo> GuardarRecepcion(string value)
        {
            Response<MuerteInfo> retValue = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                //informacion del la organzacion y usuario
                if (seguridad != null)
                {
                    var values = Utilerias.Deserializar<List<MuerteInfo>>(value);
                    var guardarPl = new MuertePL();

                    var resultado = guardarPl.GuardarRecepcionGanadoMuerto(values, seguridad.Usuario.Operador.OperadorID);
                    
                    if (resultado == 1)
                    {
                        retValue = Response<MuerteInfo>.CrearResponseVacio<MuerteInfo>(true, "OK");
                    }

                }
                else
                {
                    retValue = Response<MuerteInfo>.CrearResponseVacio<MuerteInfo>(false, "Fallo al guardar recepcion. Su sesión a expirado, por favor ingrese de nuevo");
                }

            }
            catch (Exception ex)
            {
                retValue = Response<MuerteInfo>.CrearResponseVacio<MuerteInfo>(false, "Error inesperado: " + ex.InnerException.Message);
            }

            return retValue;
        }

    }
}