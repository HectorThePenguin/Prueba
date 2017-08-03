using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Alimentacion
{
    public partial class GenerarArchivoDataLink : PageBase
    {

        #region Variables
        private List<TipoServicioInfo> listaServicio;
        private static int organizacionID;
        private static SeguridadInfo usuario;
        private static string rutaGenerarArchivoDataLink;
        #endregion

        #region Eventos
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if (usuario != null)
            {
                organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
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

        #region MetodosWeb
        /// <summary>
        /// Metodo para guardar las salidas
        /// </summary>
        /// <param name="fechaReparto"></param>
        /// <param name="tipoServicioID"></param>
        /// <returns></returns>
        [WebMethod]
        public static ResultadoOperacion GenerarArchivo(String fechaReparto, int tipoServicioID)
        {
            var repartoInfo = new RepartoInfo()
            {
                OrganizacionID = organizacionID,
                Fecha = Convert.ToDateTime(fechaReparto)
            };
            var generarArchivoDatalinkPl = new GenerarArchivoDatalinkPL();
            return generarArchivoDatalinkPl.GenerarArchivoDatalink(repartoInfo, tipoServicioID);
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

                if (listaServicio == null) return;
                var lista = listaServicio.Where(servicio => servicio.TipoServicioId == (int)TipoServicioEnum.Matutino || servicio.TipoServicioId == (int)TipoServicioEnum.Vespertino).ToList();
                lista.Add(new TipoServicioInfo { Descripcion = "Seleccione", TipoServicioId = 0 });
                cboServicios.DataSource = lista;

                cboServicios.DataBind();
                cboServicios.SelectedIndex = lista.Count - 1;
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
                var dataLink = new GenerarArchivoDatalinkPL();
                var parametrosOrganizacionPl = new ParametroOrganizacionPL();
                var parametro = parametrosOrganizacionPl.ObtenerPorOrganizacionIDClaveParametro(organizacionID,
                    ParametrosEnum.rutaGenerarArchivoDatalink.ToString());
                if (parametro == null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeRutaNoConfigurada();", true);
                    return false;
                }
                rutaGenerarArchivoDataLink = parametro.Valor;
                if (!dataLink.ValidarRutaArchivo(rutaGenerarArchivoDataLink))
                {
                    ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeRuta();", true);
                    return false;
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