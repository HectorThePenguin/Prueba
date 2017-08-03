using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidad;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Alimentacion
{
    public partial class DescargaArchivoDataLink : PageBase
    {

        #region Variables
        private static int organizacionID;
        private static int usuarioID;
        private static SeguridadInfo usuario;
        private List<TipoServicioInfo> listaServicio;
        private static string rutaArchivoDataLink;
        private static string rutaRespaldoArchivo;
        private static string nombreArchivoDataLink;
        private static List<DataLinkInfo> listaDatalink;
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            var resourceObject = GetLocalResourceObject("msgErrorRolUsuario");
            if (resourceObject != null)
            {
                msgErrorUsuario.Value = resourceObject.ToString();
            }
            resourceObject = GetLocalResourceObject("msgNoExisteTurnos");
            if (resourceObject != null)
            {
                msgNoExisteTurnos.Value = resourceObject.ToString();
            }
            resourceObject = GetLocalResourceObject("OK");
            if (resourceObject != null)
            {
                msgOK.Value = resourceObject.ToString();
            }
            resourceObject = GetLocalResourceObject("msgRutaNoExiste");
            if (resourceObject != null)
            {
                msgRuta.Value = resourceObject.ToString();
            }
            resourceObject = GetLocalResourceObject("msgSeleccionesServicio");
            if (resourceObject != null)
            {
                msgSeleccionesServicio.Value = resourceObject.ToString();
            }
            resourceObject = GetLocalResourceObject("RepartoFechaNoValida");
            if (resourceObject != null)
            {
                RepartoFechaNoValida.Value = resourceObject.ToString();
            }
            resourceObject = GetLocalResourceObject("RepartoArchivoSinDatos");
            if (resourceObject != null)
            {
                RepartoArchivoSinDatos.Value = resourceObject.ToString();
            }
            resourceObject = GetLocalResourceObject("RepartoErrorInesperado");
            if (resourceObject != null)
            {
                RepartoErrorInesperado.Value = resourceObject.ToString();
            }
            resourceObject = GetLocalResourceObject("msgSeleccionesFecha");
            if (resourceObject != null)
            {
                msgSeleccionesFecha.Value = resourceObject.ToString();
            }
            resourceObject = GetLocalResourceObject("msgFechaMayor");
            if (resourceObject != null)
            {
                msgFechaMayor.Value = resourceObject.ToString();
            }
            if (usuario != null)
            {
                organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
                usuarioID = usuario.Usuario.UsuarioID;
                if (ValidarParametros())
                {
                    LlenarComboServicios();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeRolUsuario();", true);
            }

        }

        #endregion

        #region Metodos
        /// <summary>
        /// Llena los datos de la lista de servicios
        /// </summary>
        private void LlenarComboServicios()
        {
            try
            {
                var ordenRepartoPl = new RepartoPL();
                listaServicio = ordenRepartoPl.ObtenerTiposDeServicios();

                if (listaServicio != null)
                {
                    List<TipoServicioInfo> lista = listaServicio.Where(servicio => servicio.TipoServicioId == (int)TipoServicioEnum.Matutino || servicio.TipoServicioId == (int)TipoServicioEnum.Vespertino).ToList();
                    lista.Add(new TipoServicioInfo { Descripcion = ComboValoresDefaultEnum.Seleccione.ToString(), TipoServicioId = 0 });
                    cmbServicios.DataSource = lista;

                    cmbServicios.DataBind();
                    cmbServicios.SelectedIndex = lista.Count - 1;
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeServicios();", true);
            }

        }
        /// <summary>
        /// Valida la obtencion de los parametros necesarios para la ejecucion
        /// </summary>
        /// <returns></returns>
        private bool ValidarParametros()
        {
            try
            {
                var dataLink = new DescargaArchivoDataLinkPL();
                var parametrosOrganizacionPl = new ParametroOrganizacionPL();
                ParametroOrganizacionInfo parametro = parametrosOrganizacionPl.ObtenerPorOrganizacionIDClaveParametro(organizacionID,
                    ParametrosEnum.rutaArchivoDatalink.ToString());
                if (parametro == null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeRolUsuario();", true);
                    return false;
                }
                rutaArchivoDataLink = parametro.Valor;
                if (!dataLink.ValidarRutaArchivo(rutaArchivoDataLink))
                {
                    ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeRuta();", true);
                    return false;
                }
                ParametroOrganizacionInfo parametroGeneral = parametrosOrganizacionPl.ObtenerPorOrganizacionIDClaveParametro(organizacionID,
                    ParametrosEnum.nombreArchivoDatalink.ToString());
                if (parametroGeneral == null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeRuta();", true);
                    return false;
                }
                nombreArchivoDataLink = parametroGeneral.Valor;

                parametro = parametrosOrganizacionPl.ObtenerPorOrganizacionIDClaveParametro(organizacionID,
                    ParametrosEnum.RutaRespaldoDL.ToString());
                if(parametro != null)
                {
                    rutaRespaldoArchivo = parametro.Valor;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region MetodosWeb
        /// <summary>
        /// Obtiene los datos del archivo datalink
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Response<ResultadoValidacion> ObtenerDatosArchivo(string fecha, string tipoServicio)
        {
            var resultado = new ResultadoValidacion { Resultado = true };
            var fechaReparto = DateTime.Parse(fecha);
            try
            {
                var validarDataLink = new ValidacionDataLink
                {
                    Fecha = fechaReparto,
                    RutaArchivo = rutaArchivoDataLink,
                    NombreArchivo = nombreArchivoDataLink,
                    TipoServicio = (TipoServicioEnum)Enum.Parse(typeof(TipoServicioEnum), tipoServicio)
                };

                var descargaArchivoDatalinPl = new DescargaArchivoDataLinkPL();

                resultado = descargaArchivoDatalinPl.ObtenerDatosArchivo(validarDataLink);

                if (resultado.Resultado)
                {
                    resultado = descargaArchivoDatalinPl.ValidarDatosArchivo(validarDataLink);

                    listaDatalink = validarDataLink.ListaDataLink;
                }

                return Response<ResultadoValidacion>.CrearResponse(true, resultado);
            }
            catch (Exception)
            {
                resultado.Resultado = false;
                resultado.TipoResultadoValidacion = TipoResultadoValidacion.RepartoErrorInesperado;
                return Response<ResultadoValidacion>.CrearResponse(false, resultado);
            }
        }
        /// <summary>
        /// Realiza la carga de los datos en las tablas de reparto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Response<ResultadoOperacion> DescargarCargarArchivo()
        {
            var resultado = new ResultadoOperacion();
            try
            {
                var descargaArchivoDatalinPl = new DescargaArchivoDataLinkPL();
                var usuarioActual = new UsuarioInfo
                {
                    UsuarioID = usuarioID,
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID }
                };
                var validarDataLink = new ValidacionDataLink
                {
                    RutaArchivo = rutaArchivoDataLink,
                    NombreArchivo = nombreArchivoDataLink,
                    ListaDataLink = listaDatalink,
                    RutaRespaldo = rutaRespaldoArchivo
                };
                resultado = descargaArchivoDatalinPl.CargarArchivoDataLink(validarDataLink, usuarioActual);

                return Response<ResultadoOperacion>.CrearResponse(true, resultado);
            }
            catch (Exception)
            {
                return Response<ResultadoOperacion>.CrearResponse(false, resultado);
            }
        }
        #endregion
    }
}