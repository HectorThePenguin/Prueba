using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
using SIE.Base.Log;
using System.Web.UI.WebControls;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using ListItem = System.Web.UI.WebControls.ListItem;
using TableCell = System.Web.UI.WebControls.TableCell;
using TableRow = System.Web.UI.WebControls.TableRow;
using SIE.Base.Vista;

namespace SIE.Web.Alertas
{
    public partial class TablaIncidencias : PageBase
    {

        private static AlertasSIAP lista = new AlertasSIAP();
        private static IEnumerable<IncidenciasInfo> listaFiltrada;
        private static int nivelAlertaUsuario;
        private static SeguridadInfo usuarioSeguridad;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Se obtiene el moduloID, alertaID y estatusID mediante POST
            var moduloID = Convert.ToInt32(Request["moduloID"]);
            var alertaID = Convert.ToInt32(Request["alertaID"]);
            var estatusID = Convert.ToInt32(Request["estatusID"]);
            //Obtenemos info del usuario por el logueado
            usuarioSeguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            //Se filtran las incidencias de la organizacion por estos parametros para mostrar en tabla
            listaFiltrada = ListaFiltrada(moduloID, alertaID, estatusID);
            var lista = new List<AlertaAccionInfo>();
            //Se obtienen las acciones configuradas para la alerta y las agrega al Dropdown de acciones.
            lista = ObtenerAccionesporAlertaID(alertaID);
            if (lista != null)
            {
                if (lista.Any())
                {
                    foreach (var objeto in lista)
                    {
                        ListItem item = new ListItem();
                        item.Text = objeto.Descripcion;
                        item.Value = objeto.AccionId.ToString();
                        DropDownAcciones.Items.Add(item);
                    }
                }
            }
            if (usuarioSeguridad != null)
            {
                var usuarioPL = new UsuarioPL();
                //Obtiene el nivel del usuario para visualizar alertas.
                var usuario = usuarioPL.ObtenerNivelAlertaPorUsuarioID(usuarioSeguridad.Usuario.UsuarioID);
                var estatusPosteado = Convert.ToInt32(Request["estatusID"]);
                if (listaFiltrada != null)
                {
                    var firstOrDefault = listaFiltrada.FirstOrDefault();
                    var listafiltradaNivel =
                        listaFiltrada.Where(
                            n => n.NivelAlerta.NivelAlertaId == usuario.Operador.Rol.NivelAlerta.NivelAlertaId);
                    if (firstOrDefault != null)
                    {
                        //Obtiene los niveles de alerta configurados, actuales y de usuario para mostrar controles dependiendo
                        var nivelAlertaConfigurado = firstOrDefault.Alerta.ConfiguracionAlerta.NivelAlerta.NivelAlertaId;
                        nivelAlertaUsuario = usuario.Operador.Rol.NivelAlerta.NivelAlertaId;
                        var nivelAlertaActual = firstOrDefault.NivelAlerta.NivelAlertaId;
                        hiddenNivelAlertaID.Value = nivelAlertaActual.ToString();
                        hiddenNivelAlertaUsuario.Value = nivelAlertaUsuario.ToString();
                        hiddenModuloID.Value = firstOrDefault.Alerta.Modulo.ModuloID.ToString();
                        hiddenAlertaID.Value = firstOrDefault.Alerta.AlertaID.ToString();
                        HiddenUsuarioID.Value = usuarioSeguridad.Usuario.UsuarioID.ToString();
                        if (Estatus.RechaAlert.GetHashCode() == estatusID) hiddenEsRechazado.Value = "1";
                        if (Estatus.NuevaAlert.GetHashCode() == estatusID) hiddenEsNuevo.Value = "1";
                        if (Estatus.RegisAlert.GetHashCode() == estatusID) hiddenEsRegistrado.Value = "1";
                        if (Estatus.VenciAlert.GetHashCode() == estatusID) hiddenEsVencida.Value = "1";
                        hiddenEstatusAnteriorID.Value = firstOrDefault.Estatus.EstatusId.ToString();
                        HiddenNivelAlertaConfigurado.Value = nivelAlertaConfigurado.ToString();

                        LimpiarCampos();
 
                        btnGuardar.Visible = true;
                        btnCancelar.Visible = true;
                        btnGuardar.Enabled = false;
                        btnCancelar.Enabled = false;
                        if(nivelAlertaActual < nivelAlertaUsuario)
                        {
                            btnGuardar.Text = GetLocalResourceObject("Codebehind.btnGuardarText").ToString();
                            btnCancelar.Text = GetLocalResourceObject("Codebehind.btnCancelarText").ToString();
                            DropDownAcciones.Enabled = false;
                            TextAreaComentarios.Enabled = false;
                            fechaRequerido.Visible = true;
                            accionRequerido.Visible = true;
                            horaRequerido.Visible = true;
                            textHistorico.Visible = true;
                            PanelControlesAcciones.Visible = true;
                        }
                        else if (nivelAlertaConfigurado == nivelAlertaUsuario && Estatus.RechaAlert.GetHashCode() == estatusPosteado)
                        {
                            btnGuardar.Text = GetLocalResourceObject("Codebehind.btnGuardarText").ToString();
                            btnCancelar.Text = GetLocalResourceObject("Codebehind.btnCancelarText").ToString();
                            PanelControlesAcciones.Visible = true;
                            Label5.Visible = true;
                            DropDownAcciones.Enabled = false;
                            TextAreaComentarios.Enabled = false;
                            textHistorico.Visible = true;
                            fechaRequerido.Visible = true;
                            accionRequerido.Visible = true;
                            horaRequerido.Visible = true;
                        }
                        //Si el nivel actual es menor al nivel de usuario y se realizó POST con estatus Registrado o Vencido
                        //Muestra los controles para visualizar historico
                        else if (nivelAlertaActual < nivelAlertaUsuario && Estatus.NuevaAlert.GetHashCode() == estatusPosteado)
                        {
                            PanelControlesAcciones.Visible = true;
                            DropDownAcciones.Enabled = false;
                            panelComentarios.Visible = false;
                            textHistorico.Visible = true;
                            TextAreaComentarios.Enabled = false;
                            btnGuardar.Enabled = false;
                            btnGuardar.Text = GetLocalResourceObject("Codebehind.btnGuardarText").ToString();
                            btnCancelar.Enabled = false;
                            btnCancelar.Text = GetLocalResourceObject("Codebehind.btnCancelarText").ToString();
                        }
                        //Si el nivel actual es mayor al nivel de usuario y se realizó POST con estatus Registrado o Vencido
                        //Muestra los controles para visualizar historico
                        else if (nivelAlertaActual > nivelAlertaUsuario && Estatus.RegisAlert.GetHashCode() == estatusPosteado)
                        {
                            PanelControlesAcciones.Visible = true;
                            DropDownAcciones.Enabled = false;
                            panelComentarios.Visible = false;
                            textHistorico.Visible = true;
                            TextAreaComentarios.Enabled = false;
                            btnGuardar.Enabled = false;
                            btnGuardar.Text = GetLocalResourceObject("Codebehind.btnGuardarText").ToString();
                            btnCancelar.Enabled = false;
                            btnCancelar.Text = GetLocalResourceObject("Codebehind.btnCancelarText").ToString();
                        }
                        //Si el nivel configurado es menor al nivel de alerta de usuario y se realizó POST con estatus Registrado o Vencido
                        //Muestra los controles para autorizar o rechazar
                        else if (nivelAlertaConfigurado < nivelAlertaUsuario && Estatus.RegisAlert.GetHashCode() == estatusPosteado)
                        {
                            btnGuardar.Text = GetLocalResourceObject("Codebehind.btnAutorizarText").ToString();
                            btnGuardar.ID = "btnAutorizar";
                            btnCancelar.ID = "btnRechazar";
                            btnGuardar.Text = GetLocalResourceObject("Codebehind.btnAutorizarText").ToString();
                            btnCancelar.Text = GetLocalResourceObject("Codebehind.btnRechazarText").ToString();
                        }
                        else if(nivelAlertaActual == nivelAlertaUsuario)
                        {
                            if(nivelAlertaConfigurado == nivelAlertaUsuario)
                            {
                                btnGuardar.Text = GetLocalResourceObject("Codebehind.btnGuardarText").ToString();
                                btnCancelar.Text = GetLocalResourceObject("Codebehind.btnCancelarText").ToString();
                            }
                            else
                            {
                                btnGuardar.ID = "btnAutorizar";
                                btnCancelar.ID = "btnRechazar";
                                btnGuardar.Text = GetLocalResourceObject("Codebehind.btnAutorizarText").ToString();
                                btnCancelar.Text = GetLocalResourceObject("Codebehind.btnRechazarText").ToString();
                            }

                        }
                    }
                    //Si no hay mas objetos en la lista de incidencias muestra el texto de no hay mas registros
                    else
                    {
                        PanelNoRegistros.Visible = true;
                    }
                }
            }
        }

        private void LimpiarCampos()
        {
            DropDownAcciones.Enabled = false;
            TextAreaComentarios.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;

            PanelControlesAcciones.Visible = true;
            panelComentarios.Visible = true;
            PanelControlesAcciones.Visible = true;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
            fechaRequerido.Visible = true;
            accionRequerido.Visible = true;
            horaRequerido.Visible = true;
            textHistorico.Visible = true;
            DropDownAcciones.Visible = true;
        }
        /// <summary>
        /// Metodo que manda a actualizar la incidencia nueva o rechazada a registrada.
        /// </summary>
        /// <param>incidencia</param>
        /// <param>fecha</param>
        /// <returns>bool</returns>
        [WebMethod]
        public static bool? ActualizarIncidencia(IncidenciasInfo incidencia, string fecha)
        {
            try
            {
                Logger.Info();
                if (Convert.ToDateTime(fecha) < DateTime.Now)
                {
                    return null;
                }
                var incidenciaPL = new IncidenciasPL();
                incidencia.FechaVencimiento = Convert.ToDateTime(fecha);
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                incidencia.UsuarioResponsable.UsuarioID = seguridad.Usuario.UsuarioID;
                incidenciaPL.ActualizarIncidencia(incidencia);
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return false;
        }

        /// <summary>
        /// Metodo que manda a rechazar la incidencia registrada
        /// </summary>
        /// <param>incidencia</param>
        /// <param>fecha</param>
        /// <returns>bool</returns>
        [WebMethod]
        public static bool RechazarIncidencia(IncidenciasInfo incidencia)
        {
            try
            {
                Logger.Info();
                var incidenciaPL = new IncidenciasPL();
                incidencia.Estatus.EstatusId = Estatus.RechaAlert.GetHashCode();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                incidencia.UsuarioResponsable.UsuarioID = 0;
                incidenciaPL.RechazarIncidencia(incidencia);
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return false;
        }

        /// <summary>
        /// Metodo que manda a autorizar la incidencia registrada
        /// </summary>
        /// <param>incidencia</param>
        /// <returns>bool</returns>
        [WebMethod]
        public static bool AutorizarIncidencia(IncidenciasInfo incidencia)
        {
            try
            {
                Logger.Info();
                var incidenciaPL = new IncidenciasPL();
                incidencia.Estatus.EstatusId = Estatus.CerrarAler.GetHashCode();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                incidencia.UsuarioResponsable.UsuarioID = seguridad.Usuario.UsuarioID;
                incidenciaPL.AutorizarIncidencia(incidencia);
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return false;
        }

        /// <summary>
        /// Metodo que obtiene las acciones configuradas para una alerta
        /// </summary>
        /// <param>alertaID</param>
        /// <returns>List AlertaAccionInfo</returns>
        [WebMethod]
        public static List<AlertaAccionInfo> ObtenerAccionesporAlertaID(int alertaID)
        {
            List<AlertaAccionInfo> acciones = null;

            try
            {
                Logger.Info();
                var listaAcciones = new ConfiguracionAlertasPL();
                acciones = listaAcciones.ObtenerListaAcciones(alertaID);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return acciones;
        }

        /// <summary>
        /// Metodo que obtiene el seguimiento de una incidencia
        /// </summary>
        /// <param>incidenciaID</param>
        /// <returns>List IncidenciaSeguimientoInfo</returns>
        [WebMethod]
        public static string ObtenerSeguimientoPorIncidenciaID(int incidenciaID)
        {
            List<IncidenciaSeguimientoInfo> lista;
            try
            {
                Logger.Info();
                var listaSeguimiento = new IncidenciasPL();
                lista = listaSeguimiento.ObtenerSeguimientoPorIncidenciaID(incidenciaID);
                JavaScriptSerializer theSerializer = new JavaScriptSerializer();
                var json = theSerializer.Serialize(lista);
                return json;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return null;
        }

        /// <summary>
        /// Metodo que manda a actualizar la incidencia nueva o rechazada a registrada.
        /// </summary>
        /// <param>incidencia</param>
        /// <param>fecha</param>
        /// <returns>bool</returns>
        [WebMethod]
        public static string ObtenerIncidencias(int moduloID, int alertaID, int estatusID)
        {
            try
            {
                Logger.Info();
                List<IncidenciasInfo> insidencias = ListaFiltrada(moduloID, alertaID, estatusID).ToList();
                var incidencias = from inci in insidencias
                                  select new
                                  {
                                      id = inci.IncidenciasID,
                                      inci.Accion,
                                      fechavencimiento = inci.FechaVencimiento,
                                      comentarios = inci.Comentarios,
                                      folio = inci.Folio
                                  };
                JavaScriptSerializer theSerializer = new JavaScriptSerializer();
                var json = theSerializer.Serialize(incidencias);
                return json;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return null;
        }

        /// <summary>
        /// Metodo que filtra las incidencias de la organizacion por modulo, alerta y estatus seleccionados.
        /// </summary>
        /// <param>moduloID</param>
        /// <param>alertaID</param>
        /// <param>estatusID</param>
        /// <returns>List ListaFiltrada</returns>
        private static IEnumerable<IncidenciasInfo> ListaFiltrada(int moduloID, int alertaID, int estatusID)
        {
            List<IncidenciasInfo> listaCompleta = lista.ListaIncidenciasEnModulo;
            IEnumerable<IncidenciasInfo> listaFiltrada = null;
            if (listaCompleta != null)
            {
                if (listaCompleta.Any())
                {
                    var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                    if (seguridad != null)
                    {
                        var usuarioPL = new UsuarioPL();
                        var usuario = usuarioPL.ObtenerNivelAlertaPorUsuarioID(seguridad.Usuario.UsuarioID);
                        var nivelAlertaUsuario = usuario.Operador.Rol.NivelAlerta.NivelAlertaId;
                        listaFiltrada = listaCompleta.Where(mod => mod.Alerta.Modulo.ModuloID == moduloID
                                                            && mod.Alerta.AlertaID == alertaID
                                                            && mod.Estatus.EstatusId == estatusID
                                                            && mod.Alerta.ConfiguracionAlerta.NivelAlerta.NivelAlertaId <= nivelAlertaUsuario);
                    }
                }
            }
            return listaFiltrada;
        }

        /// <summary>
        /// Metodo que crea la tabla mediante una lista de incidencias
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void CrearTabla()
        {
            string estilo = string.Empty;
            TablaIncidenciasFiltrada.Rows.Clear();
            if (listaFiltrada != null)
            {
                if (listaFiltrada.Any())
                {
                    var primerDeLista = listaFiltrada.FirstOrDefault();
                    //Obtiene el XML de la primera incidencia de la lista
                    var XMLtoTable = primerDeLista.XmlConsulta;
                    TextoCabecero.Text = primerDeLista.Alerta.Descripcion + " - " + primerDeLista.Estatus.Descripcion;
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(XMLtoTable.ToString());
                    //Obtiene la lista de nodos dentro del xml->table
                    var listaNodosColumnas = xml.FirstChild.ChildNodes;

                    //Crea el cabecero de la tabla en el orden:
                    //Radiobutton - Folio - Organizacion - XML Nodo 1 - XML Nodo 2 - etc
                    TableHeaderRow taRow = new TableHeaderRow();
                    taRow.TableSection = TableRowSection.TableHeader;
                    TableHeaderCell taCellDatosRadioButton = new TableHeaderCell();
                    taRow.Cells.Add(taCellDatosRadioButton);
                    TableHeaderCell taCellDatosFechaCreacion = new TableHeaderCell();
                    taCellDatosFechaCreacion.Text = (string)GetLocalResourceObject("lblFechaCreacionTableHeader");
                    taRow.Cells.Add(taCellDatosFechaCreacion);
                    TableHeaderCell taCellDatosFechaVencimiento = new TableHeaderCell();
                    taCellDatosFechaVencimiento.Text = (string)GetLocalResourceObject("lblFechaVencimientoTableHeader");
                    taRow.Cells.Add(taCellDatosFechaVencimiento);
                    TableHeaderCell taCellDatosFolio = new TableHeaderCell();
                    taCellDatosFolio.Text = (string)GetLocalResourceObject("lblFolioTableHeader");
                    taRow.Cells.Add(taCellDatosFolio);

                    TableHeaderCell taCellDatosOrganizacion = new TableHeaderCell();
                    taCellDatosOrganizacion.Text = (string)GetLocalResourceObject("lblOrganizacionTableHeader");
                    taRow.Cells.Add(taCellDatosOrganizacion);
                    //Crea una celda por cada nodo en el xml para la cabecera.
                    foreach (XmlNode columna in listaNodosColumnas)
                    {
                        TableHeaderCell taCell = new TableHeaderCell();
                        var nombreCabecero = columna.Name.Replace('_', ' ');
                        taCell.Text = nombreCabecero;
                        taRow.Cells.Add(taCell);
                    }
                    //Agrega la fila cabecero creada dinamicamente a la tabla
                    TablaIncidenciasFiltrada.Rows.Add(taRow);

                    //Por cada registro en la listafiltrada llenará los valores en la tabla
                    foreach (var incidencia in listaFiltrada)
                    {
                        TableRow taRowDatos = new TableRow();
                        //Si el usuario responsable es diferente de 0 (La incidencia tiene seguimiento)
                        estilo = string.Empty;
                        if ((incidencia.UsuarioResponsable.UsuarioID != 0 &&
                            incidencia.Alerta.ConfiguracionAlerta.NivelAlerta.NivelAlertaId == nivelAlertaUsuario &&
                            primerDeLista.Estatus.EstatusId == Estatus.VenciAlert.GetHashCode()) ||
                            (primerDeLista.Estatus.EstatusId == Estatus.NuevaAlert.GetHashCode() && incidencia.UsuarioResponsable.UsuarioID == usuarioSeguridad.Usuario.UsuarioID))
                        {
                            estilo = "incidenciasConSeguimiento";
                        }
                        if (primerDeLista.Estatus.EstatusId == Estatus.RechaAlert.GetHashCode() &&
                            incidencia.UsuarioResponsable.UsuarioID == usuarioSeguridad.Usuario.UsuarioID)
                        {
                            estilo = "incidenciasRechazadas";
                        }
                        taRowDatos.CssClass = estilo;
                        //Se crea una celda para el radiobutton el cual contendrá el id de la incidencia
                        TableCell taCellDatosRadioButtonValor = new TableCell();
                        taCellDatosRadioButtonValor.CssClass = estilo;
                        taCellDatosRadioButtonValor.Text = "<input type=\"radio\" class=\"radioIncidencia\" name=\"incidenciaID\" value=\"" + incidencia.IncidenciasID + "\">";
                        taRowDatos.Cells.Add(taCellDatosRadioButtonValor);


                        //Se crea una celda para fecha de la incidencia
                        TableCell taCellDatosFechaValor = new TableCell();
                        taCellDatosFechaValor.CssClass = estilo;
                        taCellDatosFechaValor.Text = incidencia.Fecha.ToString();
                        taRowDatos.Cells.Add(taCellDatosFechaValor);

                        //Se crea una celda para la fecha vigencia de la incidencia
                        TableCell taCellDatosFechaVencimientoValor = new TableCell();
                        taCellDatosFechaVencimientoValor.ID = "fechavencimiento";
                        taCellDatosFechaVencimientoValor.CssClass = estilo;
                        taCellDatosFechaVencimientoValor.Text = incidencia.FechaVencimiento.ToString();
                        taRowDatos.Cells.Add(taCellDatosFechaVencimientoValor);

                        //Se crea una celda para el folio de la incidencia
                        TableCell taCellDatosFolioValor = new TableCell();
                        taCellDatosFolioValor.CssClass = estilo;
                        taCellDatosFolioValor.Text = incidencia.Folio.ToString();
                        taRowDatos.Cells.Add(taCellDatosFolioValor);
                        //Se crea una celda para la organizacion de la incidencia
                        TableCell taCellDatosOrganizacionValor = new TableCell();
                        taCellDatosOrganizacionValor.CssClass = estilo;
                        taCellDatosOrganizacionValor.Text = incidencia.Organizacion.Descripcion;
                        taRowDatos.Cells.Add(taCellDatosOrganizacionValor);

                        //Lee el xml para convertir los valores en celdas
                        XmlDocument xmlIncidencia = new XmlDocument();
                        xmlIncidencia.LoadXml(incidencia.XmlConsulta.ToString());
                        var listaNodosDatos = xmlIncidencia.FirstChild.ChildNodes;

                        foreach (XmlNode valor in listaNodosDatos)
                        {
                            TableCell taCellDatos = new TableCell();
                            taCellDatos.CssClass = estilo;
                            taCellDatos.Text = valor.InnerText;
                            taRowDatos.Cells.Add(taCellDatos);
                        }
                        //Agrega una lista de valores al cuerpo de la tabla
                        TablaIncidenciasFiltrada.Rows.Add(taRowDatos);
                    }
                }
            }
        }

        /// <summary>
        /// Metodo que se llama cuando carga el control tabla
        /// </summary>
        protected void TablaIncidenciasFiltrada_OnLoad(object sender, EventArgs e)
        {
            CrearTabla();
        }
    }
}