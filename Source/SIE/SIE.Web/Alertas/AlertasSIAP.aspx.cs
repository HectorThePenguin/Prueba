using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using SIE.Base.Log;
using System.Web.UI.WebControls;
using SIE.Base.Vista;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Alertas
{
    public partial class AlertasSIAP : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var listaIncidencias = ObtenerIncidenciasDeOrganizacion();
                if (listaIncidencias != null)
                {
                    if (listaIncidencias.Any())
                    {
                        //Agrupa las incidencias por modulos a la que pertenece la alerta
                        var grupoModulos = listaIncidencias.GroupBy(o => o.Alerta.Modulo.Descripcion);
                        StringBuilder generatedHtml = new StringBuilder();
                        foreach (var alertas in grupoModulos)
                        {
                            //Agrupa las incidencias por el nombre de la alerta
                            var alertasDistintas = alertas.GroupBy(x => x.Alerta.Descripcion);

                            //GENERA HTML DONDE SE AGRUPARAN LAS ALERTAS DEL MODULO
                            HtmlGenericControl ulAlertas = new HtmlGenericControl();
                            ulAlertas.Attributes["class"] = "alerta-group";
                            ulAlertas.TagName = "ul";
                        
                            foreach (var alerta in alertasDistintas)
                            {
                                //Agrupa las incidencias por los estatus a la que pertenece la incidencia
                                var estatusDistintos = alerta.GroupBy(gb => gb.Estatus.Descripcion);

                                //GENERA HTML DONDE SE AGRUPARAN LOS ESTADOS DE LA ALERTA
                                HtmlGenericControl ulEstados = new HtmlGenericControl();
                                ulEstados.Attributes["class"] = "estados-group";
                                ulEstados.TagName = "ul";

                                foreach (var incidencia in estatusDistintos)
                                {
                                    //GENERA HTML CON LOS DIFERENTES ESTATUS PARA HACER POST AL SERVIDOR
                                    var inci = incidencia.FirstOrDefault();
                                    var valor = String.Format("{0}|{1}|{2}", inci.Alerta.Modulo.ModuloID,
                                        inci.Alerta.AlertaID, inci.Estatus.EstatusId);
                                    var spanTagOpen = "<a href=\"javascript:void(0)\" class=\"post\" id=\"" + valor + "\">";
                                    var text = String.Format("{0} ({1})", inci.Estatus.Descripcion, incidencia.Count());
                                    var spanTagClose = "</a>";
                                    var span = String.Format("{0}{1}{2}", spanTagOpen, text, spanTagClose);
                                    //GENERA HTML DE CADA ESTATUS POR ALERTA
                                    HtmlGenericControl liEstatus = new HtmlGenericControl();
                                    liEstatus.Attributes["class"] = "estado-linea";
                                    liEstatus.TagName = "li";
                                    liEstatus.InnerHtml = span;

                                    var liEstatusToString = "";
                                    generatedHtml = new StringBuilder();
                                    using (var htmlStringWriter = new StringWriter(generatedHtml))
                                    {
                                        using (var htmlTextWriter = new HtmlTextWriter(htmlStringWriter))
                                        {
                                            liEstatus.RenderControl(htmlTextWriter);
                                            liEstatusToString = generatedHtml.ToString();
                                        }
                                    }
                                    //AGREGA CADA REGISTRO A LA LISTA DE ESTADOS
                                    ulEstados.InnerHtml += liEstatusToString;
                                }
                                //GENERA HTML DE LA LISTA DE TODOS LOS ESTADOS
                                var ulEstatusToString = "";
                                generatedHtml = new StringBuilder();
                                using (var htmlStringWriter = new StringWriter(generatedHtml))
                                {
                                    using (var htmlTextWriter = new HtmlTextWriter(htmlStringWriter))
                                    {
                                        ulEstados.RenderControl(htmlTextWriter);
                                        ulEstatusToString = generatedHtml.ToString();
                                    }
                                }
                                //GENERA HTML CON LAS DIFERENTES ALERTAS DEL MODULO
                                HtmlGenericControl liAlerta = new HtmlGenericControl();
                                liAlerta.Attributes["class"] = "alerta-linea";
                                liAlerta.TagName = "li";
                                //PINTA EL NOMBRE DE LA ALERTA Y AGREGA LA LISTA DE ESTADOS DE ESTA ALERTA
                                liAlerta.InnerHtml = "<span>" + estatusDistintos.FirstOrDefault().FirstOrDefault().Alerta.Descripcion + "</span>" + ulEstatusToString;
                                var liAlertaToString = "";
                                generatedHtml = new StringBuilder();
                                using (var htmlStringWriter = new StringWriter(generatedHtml))
                                {
                                    using (var htmlTextWriter = new HtmlTextWriter(htmlStringWriter))
                                    {
                                        liAlerta.RenderControl(htmlTextWriter);
                                        liAlertaToString = generatedHtml.ToString();
                                    }
                                }
                                //AGREGA CADA REGISTRO A LA LISTA DE ALERTAS QUE A SU VEZ CONTIENE LA LISTA DE ESTADOS DE ESTA
                                ulAlertas.InnerHtml += liAlertaToString;
                            }
                            //GENERA HTML DE LA LISTA DE TODOS LAS ALERTAS
                            var ulAlertasToString = "";
                            generatedHtml = new StringBuilder();
                            using (var htmlStringWriter = new StringWriter(generatedHtml))
                            {
                                using (var htmlTextWriter = new HtmlTextWriter(htmlStringWriter))
                                {
                                    ulAlertas.RenderControl(htmlTextWriter);
                                    ulAlertasToString = generatedHtml.ToString();
                                }
                            }
                            //GENERA HTML CON LOS DIFERENTES MODULOS
                            HtmlGenericControl liModulo = new HtmlGenericControl();
                            liModulo.Attributes["class"] = "modulo-linea";
                            liModulo.TagName = "li";
                            //PINTA EL NOMBRE DE LA ALERTA Y AGREGA LA LISTA DE ALERTAS
                            liModulo.InnerHtml = "<span>" + alertas.Select(x => x.Alerta.Modulo.Descripcion).FirstOrDefault() + " (" +
                                                 alertasDistintas.Count() + ")</span>" + ulAlertasToString;
                            var liModuloToString = "";
                            generatedHtml = new StringBuilder();
                            using (var htmlStringWriter = new StringWriter(generatedHtml))
                            {
                                using (var htmlTextWriter = new HtmlTextWriter(htmlStringWriter))
                                {
                                    liModulo.RenderControl(htmlTextWriter);
                                    liModuloToString = generatedHtml.ToString();
                                }
                            }
                            //AGREGA CADA LINEA DE MODULO AL TREEVIEW
                            tree1.InnerHtml += liModuloToString;
                        }
                    }
                }
                else
                {
                    lblNoTieneIncidencias.Text = (string) GetLocalResourceObject("lblNoTieneIncidencias.Text");
                }           
            }
            catch (Exception)
            {
                lblNoTieneIncidencias.Text = (string)GetLocalResourceObject("lblErrorAlObtenerIncidencias.Text");
            }

        }

        /// <summary>
        /// Propiedad para acceder a las Incidencias de la organizacion desde la página de la tabla
        /// </summary>
        public List<IncidenciasInfo> ListaIncidenciasEnModulo
        {
            get {
                List<IncidenciasInfo> listaIncidencias = null;
                try{
                    listaIncidencias = ObtenerIncidenciasDeOrganizacion();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
                return listaIncidencias; 
            }
        }

        /// <summary>
        /// Metodo que obtiene las incidencias de la organizacion del usuario
        /// </summary>
        /// <param></param>
        /// <returns>List IncidenciasInfo</returns>
        public static List<IncidenciasInfo> ObtenerIncidenciasDeOrganizacion()
        {
            List<IncidenciasInfo> incidencias = null;
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            int organizacionId = 0;

            if (seguridad != null)
            {
                organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                var usuarioPL = new UsuarioPL();
                var usuario = usuarioPL.ObtenerNivelAlertaPorUsuarioID(seguridad.Usuario.UsuarioID);
                var nivelAlertaUsuario = usuario.Operador.Rol.NivelAlerta.NivelAlertaId;
                var usuarioCorporativo = usuario.Corporativo;
                var incidenciasPL = new IncidenciasPL();
                incidencias = incidenciasPL.ObtenerIncidenciasPorOrganizacionID(organizacionId, usuarioCorporativo);
                incidencias =
                    incidencias.Where(x => x.NivelAlerta.NivelAlertaId <= nivelAlertaUsuario).ToList();
            }
            return incidencias;
        }
    }
}