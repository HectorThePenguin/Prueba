using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidad;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;


namespace SIE.Web.Alimentacion
{
    public partial class OrdenRepartoAlimentacion : PageBase
    {
        #region Variables

        private List<TipoServicioInfo> listaServicio;
        private List<int> listaSeccionesCorral = new List<int> { 1, 2, 7, 8, 10 };
        //private static int organizacionID;
        //private static int usuarioID;
        //private static SeguridadInfo usuario;

        #endregion
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            var ordenRepartoPl = new RepartoPL();

            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            var localResourceObject = GetLocalResourceObject("msgErrorRolUsuario");
            if (localResourceObject != null)
                msgErrorUsuario.Value = localResourceObject.ToString();
            localResourceObject = GetLocalResourceObject("msgErrorParametros");
            if (localResourceObject != null)
                msgErrorParametros.Value = localResourceObject.ToString();
            localResourceObject = GetLocalResourceObject("msgSinCorrales");
            if (localResourceObject != null)
                msgSinCorrales.Value = localResourceObject.ToString();
            localResourceObject = GetLocalResourceObject("OK");
            if (localResourceObject != null) msgOK.Value = localResourceObject.ToString();
            localResourceObject = GetLocalResourceObject("msgSinConsumoTotal");
            if (localResourceObject != null)
                msgSinConsumoTotal.Value = localResourceObject.ToString();
            localResourceObject = GetLocalResourceObject("msgCorralesIncompletos");
            if (localResourceObject != null)
                msgCorralesIncompletos.Value = localResourceObject.ToString();
            localResourceObject = GetLocalResourceObject("msgErrorProceso");
            if (localResourceObject != null)
                msgErrorProceso.Value = localResourceObject.ToString();
            localResourceObject = GetLocalResourceObject("AlimentoNoServidoMatutino");
            if (localResourceObject != null)
                AlimentoNoServidoMatutino.Value = localResourceObject.ToString();
            localResourceObject = GetLocalResourceObject("AlimentoNoServidoVespertino");
            if (localResourceObject != null)
                AlimentoNoServidoVespertino.Value = localResourceObject.ToString();
            localResourceObject = GetLocalResourceObject("msgFaltaPorcentaje");
            if (localResourceObject != null)
                msgFaltaPorcentaje.Value = localResourceObject.ToString();
            btnSi.Value = GetLocalResourceObject("btnSi").ToString();
            btnNo.Value = GetLocalResourceObject("btnNo").ToString();
            idUsuario.Value = usuario.Usuario.UsuarioID.ToString(CultureInfo.InvariantCulture);
            if (usuario != null)
            {
                //organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
                //usuarioID = usuario.Usuario.UsuarioID;
                listaServicio = ordenRepartoPl.ObtenerTiposDeServicios();
                if (listaServicio == null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "myScript", " EnviarMensajeSinParametros();", true);
                }
                else
                {
                    foreach (var servicio in listaServicio)
                    {
                        switch (servicio.TipoServicioId)
                        {
                            case (int)TipoServicioEnum.Matutino:
                                lblrdbMatutino.Text = servicio.Descripcion;
                                break;
                            case (int)TipoServicioEnum.Vespertino:
                                lblrdbVespertino.Text = servicio.Descripcion;
                                break;
                        }
                    }
                }

                if (!ValidarParametros())
                {
                    ClientScript.RegisterStartupScript(GetType(), "myScript", " EnviarMensajeSinParametros();", true);
                }
                var parametroOrganizacionPL = new ParametroOrganizacionPL();
                ParametroOrganizacionInfo parametro = parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(usuario.Usuario.Organizacion.OrganizacionID,
                                                                               ParametrosEnum.EJECORDENREP.ToString());
                if (parametro != null)
                {
                    int valor;
                    int.TryParse(parametro.Valor, out valor);
                    if (valor != 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "myScript", " EnviarMensajeEjecutandose();", true);
                    }
                }



            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeRolUsuario();", true);
            }
            CargarComboSeccion();
        }
        #endregion

        #region Metodos

        private void CargarComboSeccion()
        {
            var localResourceObject = GetLocalResourceObject("SeccionTodas");
            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if (usuario != null)
            {
                var corralPL = new CorralPL();
                if (localResourceObject != null)
                {
                    var seccionTodos = new SeccionModel
                                           {
                                               Seccion = -1,
                                               Descripcion = localResourceObject.ToString()
                                           };
                    List<SeccionModel> listaSecciones = corralPL.ObtenerSeccionesCorral(usuario.Usuario.Organizacion.OrganizacionID);
                    listaSecciones.Insert(0, seccionTodos);
                    ddlSeccion.DataSource = listaSecciones;
                    ddlSeccion.DataBind();
                }
            }
        }

        /// <summary>
        /// Metodo para guardar la orden de reparto
        /// </summary>
        [WebMethod]
        public static Response<ResultadoOperacion> GenerarOrdenReparto(int tipoServicio, int seccion, string fechaReparto)
        {
            var respuesta = new ResultadoOperacion();

            try
            {
                var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (usuario != null)
                {
                    var repartoPl = new RepartoPL();
                    var ordenReparto = new OrdenRepartoAlimentacionInfo
                                           {
                                               UsuarioID = usuario.Usuario.UsuarioID,
                                               TipoServicioID = tipoServicio,
                                               OrganizacionID = usuario.Usuario.Organizacion.OrganizacionID,
                                               Seccion = seccion,
                                               FechaReparto = Convert.ToDateTime(fechaReparto)
                                           };
                    if (!ActualizarParametroEjecucion(true))
                    {
                        respuesta.CodigoMensaje = 1;
                        return Response<ResultadoOperacion>.CrearResponse(false, respuesta);
                    }
                    respuesta = repartoPl.GenerarOrdenReparto(ordenReparto);
                    ActualizarParametroEjecucion(false);
                    return Response<ResultadoOperacion>.CrearResponse(true, respuesta);
                }
                return Response<ResultadoOperacion>.CrearResponse(false, respuesta);
            }
            catch (Exception)
            {
                ActualizarParametroEjecucion(false);
                return Response<ResultadoOperacion>.CrearResponse(false, respuesta);
            }
        }

        private static bool ActualizarParametroEjecucion(bool ejecutando)
        {
            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if (usuario != null)
            {
                var parametroOrganizacionPL = new ParametroOrganizacionPL();
                ParametroOrganizacionInfo parametro =
                    parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(usuario.Usuario.Organizacion.OrganizacionID,
                                                                                   ParametrosEnum.EJECORDENREP.ToString());
                if (parametro != null)
                {
                    if (parametro.Valor.Trim().Equals("1") && ejecutando)
                    {
                        return false;
                    }
                    if (ejecutando)
                    {
                        parametro.Valor = "1";
                    }
                    else
                    {
                        parametro.Valor = "0";
                    }
                    parametro.UsuarioModificacionID = usuario.Usuario.UsuarioID;
                    parametroOrganizacionPL.Guardar(parametro);
                }
                return true;
            }
            return false;
        }




        /// <summary>
        /// Realiza las validaciones necesarios para poder realizar el guardado de la orden
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Response<ResultadoValidacion> ValidarGuardar(int tipoServicio)
        {
            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            var respuesta = new ResultadoValidacion();
            if (usuario != null)
            {
                
                try
                {
                    var repartoPl = new RepartoPL();
                    respuesta = repartoPl.ValidarOrdenReparto(tipoServicio, usuario.Usuario.Organizacion.OrganizacionID);
                    return Response<ResultadoValidacion>.CrearResponse(true, respuesta);
                }
                catch (Exception)
                {
                    return Response<ResultadoValidacion>.CrearResponse(false, respuesta);
                }
            }
            return Response<ResultadoValidacion>.CrearResponse(false, respuesta);
        }
        /// <summary>
        /// Obtiene el avance del reparto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Response<RepartoAvanceInfo> ObtenerAvanceReparto(int idUsuario)
        {
            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            var respuesta = new RepartoAvanceInfo();
            if (usuario != null)
            {
                try
                {
                    var repartoPl = new RepartoPL();

                    respuesta = repartoPl.ObtenerAvanceReparto(usuario.Usuario.UsuarioID);

                    return Response<ResultadoOperacion>.CrearResponse(true, respuesta);
                }
                catch (Exception)
                {
                    return Response<ResultadoOperacion>.CrearResponse(false, respuesta);
                }
            }
            return Response<ResultadoOperacion>.CrearResponse(false, respuesta);
        }
        /// <summary>
        /// Valida los parametros para la ejecucion
        /// </summary>
        /// <returns></returns>
        private bool ValidarParametros()
        {
            try
            {
                var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (usuario != null)
                {
                    var repartoPl = new RepartoPL();
                    var respuesta = repartoPl.LeerConfiguracion(usuario.Usuario.Organizacion.OrganizacionID, ParametrosEnum.porcentajeMatutino);

                    if (string.IsNullOrEmpty(respuesta))
                    {
                        return false;
                    }
                    respuesta = repartoPl.LeerConfiguracion(usuario.Usuario.Organizacion.OrganizacionID, ParametrosEnum.porcentajeVespertino);

                    if (string.IsNullOrEmpty(respuesta))
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        #endregion
    }
}