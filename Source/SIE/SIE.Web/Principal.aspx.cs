using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web
{
    public partial class Principal : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Metodo para cargar el Menu segun el usuario
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<MenuInfo> CargarMenu()
        {
            IList<MenuInfo> menuLista = null;
            try
            {
                var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (usuario != null)
                {
                    var menu = new MenuPL();
                    menuLista = menu.ObtenerPorUsuario(usuario.Usuario.UsuarioActiveDirectory, true);
                    if (menuLista != null && menuLista.Any())
                    {
                        MenuInfo opcionNotificaciones = menuLista.FirstOrDefault(desc => desc.WinForm.Equals("Notificaciones"));
                        if (opcionNotificaciones != null)
                        {
                            var entradaProductoPL = new EntradaProductoPL();
                            int cantidadAutorizacionesPendientes =
                                entradaProductoPL.ObtenerCantidadNotificacionesAutorizadas(
                                    usuario.Usuario.Organizacion.OrganizacionID);

                            opcionNotificaciones.Formulario = string.Format(opcionNotificaciones.Formulario,
                                                                            cantidadAutorizacionesPendientes);
                            if (cantidadAutorizacionesPendientes > 0)
                            {
                                opcionNotificaciones.Clase = "notificacionesPendientesAutorizar";
                            }
                        }
                    }
                }
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
            return menuLista;
        }
    }
}