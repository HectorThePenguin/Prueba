using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using SIE.Base.Log;
using SIE.Base.Vista;
using System.Globalization;
using System.Web;
using System.Web.Services;
using SIE.Services.Facturas;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.Base.Exepciones;


namespace SIE.Web.Manejo
{
    public partial class CapturaPedido : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    listaOrganizacionesTipoGanadera =
                        (List<OrganizacionInfo>) organizacionesPl.ObtenerTipoGanaderas();

                    if (organizacion > 0 && listaOrganizacionesTipoGanadera != null)
                    {
                        //Filtra todos los folios que contengan el numero de folio capturado
                        listaOrganizacionesTipoGanadera = listaOrganizacionesTipoGanadera.Where(
                                registro => registro.OrganizacionID.Equals(organizacion) && registro.Activo.Equals(EstatusEnum.Activo)).ToList();
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
                    listaOrganizacionesTipoGanadera =
                        (List<OrganizacionInfo>) organizacionesPl.ObtenerTodos(EstatusEnum.Activo);

                    if ( listaOrganizacionesTipoGanadera != null)
                    {
                        //Filtra todos las organizaciones que contengan lo capturado
                        listaOrganizacionesTipoGanadera = listaOrganizacionesTipoGanadera.Where(
                                registro => registro.Descripcion.ToString(CultureInfo.InvariantCulture).ToUpper()
                                        .Contains(organizacion.ToString(CultureInfo.InvariantCulture).ToUpper()) &&
                                        registro.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Ganadera.GetHashCode()).ToList();
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
        /// Metodo para Consultar el PedidoGanado Semanal
        /// </summary>
        /// <param name="pedidoGanadoInfo">filtros donde viene la OrganizacionID y la FechaInicio</param>
        /// <returns></returns>
        [WebMethod]
        public static PedidoGanadoInfo ObtenerPedidoGanadoSemanal(PedidoGanadoInfo pedidoGanadoInfo)
        {
            try
            {
                var pedidoGanadoPL = new PedidoGanadoPL();
                var pedidoGanado = pedidoGanadoPL.ObtenerPedidoSemanal(pedidoGanadoInfo)?? new PedidoGanadoInfo();
                if (pedidoGanado.ListaSolicitudes == null)
                {
                    pedidoGanado.ListaSolicitudes = new List<PedidoGanadoEspejoInfo>();
                }
                return pedidoGanado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Guardar en la tabla PedidoGanado
        /// </summary>
        /// <param name="pedidoGanadoInfo">Objeto a guardar en la tabla PedidoGanado</param>
        /// <returns></returns>
        [WebMethod]
        public static PedidoGanadoInfo GuardarPedidoGanado(PedidoGanadoInfo pedidoGanadoInfo)
        {
            try
            {
                var pedidoGanadoPL = new PedidoGanadoPL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                pedidoGanadoInfo.UsuarioCreacionID = seguridad.Usuario.UsuarioID;
                var pedidoGanado = pedidoGanadoPL.GuardarPedidoGanado(pedidoGanadoInfo)??new PedidoGanadoInfo();
                return pedidoGanado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para guardar en la tabla PedidoGanadoEspejo
        /// </summary>
        /// <param name="pedidoGanadoEspejoInfo">Objeto a guardar en la tabla PedidoGanadoEspejo</param>
        /// <returns></returns>
        [WebMethod]
        public static PedidoGanadoEspejoInfo GuardarPedidoGanadoEspejo(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                var pedidoGanadoPL = new PedidoGanadoPL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                pedidoGanadoEspejoInfo.UsuarioCreacionID = seguridad.Usuario.UsuarioID;
                pedidoGanadoEspejoInfo.UsuarioSolicitanteID = seguridad.Usuario.UsuarioID;
                var pedidoGanado = pedidoGanadoPL.GuardarPedidoGanadoEspejo(pedidoGanadoEspejoInfo)??new PedidoGanadoEspejoInfo();
                return pedidoGanado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Actualizar Estatus PedidoGanadoEspejo
        /// </summary>
        /// <param name="pedidoGanadoEspejoInfo">Objeto a actualizar en la tabla PedidoGanadoEspejo</param>
        /// <returns></returns>
        [WebMethod]
        public static bool ActualizarPedidoGanadoEspejoEstatus(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                var pedidoGanadoPL = new PedidoGanadoPL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                pedidoGanadoEspejoInfo.Activo = EstatusEnum.Inactivo;
                pedidoGanadoEspejoInfo.UsuarioModificacionID = seguridad.Usuario.UsuarioID;
                pedidoGanadoEspejoInfo.UsuarioCreacionID = seguridad.Usuario.UsuarioID;
                pedidoGanadoEspejoInfo.UsuarioAproboID = seguridad.Usuario.UsuarioID;
                pedidoGanadoPL.ActualizarPedidoGanadoEspejoEstatus(pedidoGanadoEspejoInfo);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Metodo para Actualizar la tabla PedidoGanado
        /// </summary>
        /// <param name="pedidoGanadoEspejoInfo">Objeto a actualizar en la tabla PedidoGanado</param>
        /// <returns></returns>
        [WebMethod]
        public static bool ActualizarPedidoGanado(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                var pedidoGanadoPL = new PedidoGanadoPL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                pedidoGanadoEspejoInfo.Activo = EstatusEnum.Inactivo;
                pedidoGanadoEspejoInfo.UsuarioModificacionID = seguridad.Usuario.UsuarioID;
                pedidoGanadoEspejoInfo.UsuarioCreacionID = seguridad.Usuario.UsuarioID;
                pedidoGanadoEspejoInfo.UsuarioAproboID = seguridad.Usuario.UsuarioID;
                pedidoGanadoPL.ActualizarPedidoGanadoEspejoEstatus(pedidoGanadoEspejoInfo);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Método para enviar correos
        /// </summary>
        /// <param name="tipoSolicitud">Texto con el tipo de solicitud, se utiliza para el formato del correo</param>
        /// <param name="semanaCambio">Semana en la que se realiza la solicitud</param>
        /// <param name="organizacionID">Identificador de la organizacion a la que se realiza la solicitud</param>
        /// <param name="clave">Parametro que especifica a quienes se mandará el correo</param>
        [WebMethod]
        public static void EnviarCorreo(string tipoSolicitud, string semanaCambio, int organizacionID, string clave,int usuarioID)
        {
            var seguridad = (SeguridadInfo)ObtenerSeguridad();

            var correo = new CorreoPL();
            var correoenviar = new CorreoInfo();
            var usuariosCorreo = new List<UsuarioInfo>();
            var organizacionPL = new OrganizacionPL();
            var usuarioInfo = new UsuarioInfo();
            var usuarioPL = new UsuarioPL();
            var organizacionInfo = new OrganizacionInfo();
            var parametroPL = new ParametroGeneralPL();
            var terminacionCorreo = new ParametroGeneralInfo();

            try
            {
                OrganizacionInfo organizacion = seguridad.Usuario.Organizacion;
                terminacionCorreo = parametroPL.ObtenerPorClaveParametro("CorreoTerminacion");

                string asuntoCorreo = HttpContext.GetLocalResourceObject("~/Manejo/CapturaPedido.aspx", "msgAsuntoCorreo.Text").ToString();
                string mensajeCorreo = HttpContext.GetLocalResourceObject("~/Manejo/CapturaPedido.aspx", "msgMensajeCorreo.Text").ToString();
                string parametroManejo = HttpContext.GetLocalResourceObject("~/Manejo/CapturaPedido.aspx", "clvCorreoJefeManejo.Text").ToString();

                organizacionInfo = organizacionPL.ObtenerPorID(organizacionID);
                if (string.IsNullOrEmpty(clave))
                {
                    usuarioInfo = usuarioPL.ObtenerPorID(usuarioID);
                    correoenviar.Asunto = String.Format(asuntoCorreo, tipoSolicitud, semanaCambio, organizacionInfo.Descripcion);
                    correoenviar.Correos = new List<string>();
                    correoenviar.Mensaje = String.Format(mensajeCorreo, tipoSolicitud, semanaCambio);
                    correoenviar.Correos.Add(usuarioInfo.UsuarioActiveDirectory + terminacionCorreo.Valor);

                    correo.EnviarCorreo(organizacion, correoenviar);
                }
                else
                {
                    usuariosCorreo = ObtenerCorreos(clave);
                    if (clave.Equals(parametroManejo))
                    {
                        usuariosCorreo.RemoveAll(x => x.OrganizacionID != organizacionID);
                    }
                    foreach (var usuario in usuariosCorreo)
                    {
                        correoenviar.Asunto = String.Format(asuntoCorreo, tipoSolicitud, semanaCambio, organizacionInfo.Descripcion);
                        correoenviar.Correos = new List<string>();
                        correoenviar.Mensaje = String.Format(mensajeCorreo, tipoSolicitud, semanaCambio);
                        correoenviar.Correos.Add(usuario.UsuarioActiveDirectory + terminacionCorreo.Valor);

                        correo.EnviarCorreo(organizacion, correoenviar);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Este método obtiene el listado de usuarios a los que se enviarán correos
        /// </summary>
        /// <param name="clave">Roles concatenados, obtenidos desde la tabla ParametrosGenerales</param>
        /// <returns>Listado de usuarios</returns>
        public static List<UsuarioInfo> ObtenerCorreos(string clave)
        {
            var correos = new List<UsuarioInfo>();
            var parametroPL = new ParametroGeneralPL();
            var parametroGeneral = new ParametroGeneralInfo();
            var usuarioPL = new UsuarioPL();
            var roles = new List<int>();

            try
            {
                parametroGeneral = parametroPL.ObtenerPorClaveParametro(clave);
                roles = parametroGeneral.Valor.Split(',').Select(int.Parse).ToList();

                correos = usuarioPL.ObtenerCorreos(roles);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return correos;
        }

        /// <summary>
        /// Metodo para Consultar dias habiles
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ParametroSemanaInfo ObtenerDiasHabiles()
        {
            try
            {
                string parametroSemanaClave = HttpContext.GetLocalResourceObject("~/Manejo/CapturaPedido.aspx", "clvPedido.Text").ToString();
                var parametroSemanaInfo = new ParametroSemanaInfo();
                parametroSemanaInfo.Descripcion = parametroSemanaClave;
                var parametroSemanaPL = new ParametroSemanaPL();
                var parametroSemana = parametroSemanaPL.ObtenerParametroSemanaPorDescripcion(parametroSemanaInfo)?? new ParametroSemanaInfo();
                return parametroSemana;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar el Numero de semana
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerNumeroSemana(DateTime fechaCalcular)
        {
            try
            {
                var parametroSemanaPL = new ParametroSemanaPL();
                var numeroSemana = parametroSemanaPL.ObtenerNumeroSemana(fechaCalcular);
                return numeroSemana;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para determinar si el usuario tiene organizacion ganadera.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerOrganizacionUsuario()
        {
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                var organizacion = seguridad.Usuario.Organizacion;
                var tipoOrganizacion = TipoOrganizacion.Ganadera.GetHashCode();
                if (seguridad.Usuario.Organizacion.TipoOrganizacion.TipoOrganizacionID != tipoOrganizacion)
                {
                    organizacion = new OrganizacionInfo();
                }
                return organizacion.OrganizacionID;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para obtener usuario.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerUsuarioID()
        {
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                return seguridad.Usuario.UsuarioID;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para obtener usuario.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerRolManejo()
        {
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                int rol = seguridad.Usuario.Operador.Rol.RolID;
                return rol;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para validar que existan organizaciones activas
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static bool ValidarOrganizaciones()
        {
            bool existen = true;
            try
            {
                var organizacionPL = new OrganizacionPL();
                IList<OrganizacionInfo> organizaciones = organizacionPL.ObtenerTodos(EstatusEnum.Activo);

                if (organizaciones == null)
                {
                    existen = false;
                }
                else if (organizaciones.Count == 0)
                {
                    existen = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return existen;
        }
    }
}