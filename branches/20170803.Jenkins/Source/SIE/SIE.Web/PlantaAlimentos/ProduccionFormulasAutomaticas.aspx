<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProduccionFormulasAutomaticas.aspx.cs" Inherits="SIE.Web.PlantaAlimentos.ProduccionFormulasAutomaticas" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Import Namespace="System.Web.Optimization" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    <script src="../Scripts/ProduccionFormulasAutomaticas.js"></script>
    <%--<link href="../assets/css/ProduccionFormulasAutomaticas.css" rel="stylesheet" />--%>

    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        function changeHashOnLoad() {
            window.location.href += "#";
            setTimeout("changeHashAgain()", "50");
        }

        function changeHashAgain() {
            window.location.href += "1";
        }

        var storedHash = window.location.hash;
        window.setInterval(function () {
            if (window.location.hash != storedHash) {
                window.location.hash = storedHash;
            }
        }, 50);

    </script>
</head>
<body class="page-header-fixed" onload="changeHashOnLoad();" ondragstart="return false;" ondrop="return false;">
    <div id="Principal">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div id="skm_LockPane" class="LockOff">
            </div>
            <div class="container-fluid" />
            <div class="row-fluid">
                <div class="span12">
                    <div class="portlet box SuKarne2">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                <span>
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1"></asp:Label></span>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <ul class="breadcrumb">
                                <li>
                                    <i class="icon-home"></i>
                                    <a href="../Principal.aspx">Home</a>
                                    <i class="icon-angle-right"></i>
                                </li>
                                <li>
                                    <a href="ProduccionFormulasAutomaticas.aspx">Producción de Fórmulas Automática</a>
                                </li>
                            </ul>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="lblMonitor" runat="server" meta:resourcekey="lblMonitorResource1"></asp:Label></legend>
                                    <div class="portlet-body form">
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span2"><asp:Label ID="lblArchivoProduccion" runat="server" CssClass="control-label" meta:resourcekey="lblArchivoProduccionResource1"></asp:Label></div>
                                                <div class="span4"><asp:TextBox runat="server" ID="txtArchivoProduccion" CssClass="span12" Enabled="False" meta:resourcekey="txtArchivoProduccionResource1"></asp:TextBox></div>
                                                <div class="span2">
                                                    <asp:FileUpload id="FileUploadControl" runat="server" style="display:none" onchange="checkfile(this);" meta:resourcekey="FileUploadControlResource1"/>
                                                    <button class="btn SuKarne" id="btnImportar2" runat="server" data-toggle="modal"><i class="icon-search"> </i> Importar</button> 
                                                    <asp:Button ID="btnCargarImagen" runat="server" OnClick="btnCargarImagen_Click"  style="display:none" meta:resourcekey="btnCargarImagenResource1"/>
                                                </div>
                                                <div class="span1"><asp:Label ID="lblFecha" runat="server" CssClass="control-label" meta:resourcekey="lblFechaResource1"></asp:Label></div>
                                                <div class="span3"><asp:TextBox runat="server" ID="txtFecha" AutoPostBack="true" CssClass="m-wrap medium span12" meta:resourcekey="txtFechaResource1" OnTextChanged="txtFecha_TextChanged" MaxLength="10"></asp:TextBox></div>
                                            </div>
                                        </div>
                                        <div id="Div1"></div>
                                        <div id="divGridFormulasAgrupadas">
                                        </div>
                                        <div id="Div2"></div>

                                        <div class="row-fluid">
                                            <div class="span4 pull-right">
                                                <div style="text-align:right;">
                                                    <button class="btn SuKarne" id="btnGuardar" runat="server" data-toggle="modal">Guardar</button> 
                                                    <button class="btn SuKarne" id="btnCancelar" runat="server" data-toggle="modal">Cancelar</button> 
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>

        <div id="modalBachadas" class="modal hide fade" style="margin-top: -200px;height: 300px;width:600px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button id="Cerrarventana" type="button" class="close" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="Image3Resource1" />
                            <asp:Label ID="lblDetalleporbachada" runat="server" Text="Detalle por Batchada" meta:resourcekey="lblDetalleporbachadaResource1" ></asp:Label>
                        </h3>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="modal-body">
                        <div id="divFormulasDetalladas">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="dlgResumenProduccionFormulas" class="modal hide fade" style="margin-top: 1px;"  tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="portlet box SuKarne2">
				<div class="portlet-title">
                    <div class="caption">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <span class="letra">
                            <asp:Label ID="Label9" runat="server" meta:resourcekey="lblTituloDialogoResumen"></asp:Label></span>
                    </div>
                </div>
                <div class="portlet-body form">
					<div class="modal-body" style="text-align: right">
							<fieldset class="scheduler-border">
							<div class="no-left-margin">
                                <table id="tbResumenFormulas" class="table table-striped table-advance table-hover">
                                    <thead>
                                        <tr>
                                            <th class="span2 alineacionCentro" scope="col"><asp:Label ID="lblResumenDescripcion" runat="server" meta:resourcekey="HeaderResumenDescripcion"></asp:Label></th>
                                            <th class="span2 alineacionCentro" scope="col"><asp:Label ID="lblResumenCantidad" runat="server" meta:resourcekey="HeaderResumenCantidad"></asp:Label></th>
                                            <th class="span2 alineacionCentro" scope="col"><asp:Label ID="lblResumenCantidadReparto" runat="server" meta:resourcekey="HeaderResumenCantidadReparto"></asp:Label></th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                        </fieldset>
						<button runat="server" id="btnAceptarResumen" class="btn SuKarne" data-dismiss="modal">Aceptar</button>
					</div>
                </div>
            </div>
		</div>

        <!--Dialogo Cancelar-->
        <div id="dlgCancelar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
			<div class="modal-body">
				<label id="lblMensajeDialogo"></label>
			</div>
			<div class="modal-footer">
				<button id="btnDialogoSi" class="btn SuKarne" data-dismiss="modal">Si</button>
                <button id="btnDialogoNo" class="btn SuKarne" data-dismiss="modal">No</button>
			</div>
		</div>
    </div>
    <script type="text/javascript">

        //MENSAJES
        var msjArchivoVacio = '<asp:Literal runat="server" Text="<%$ Resources:msjArchivoVacio %>"/>';
        var msjInformacionRequerida = '<asp:Literal runat="server" Text="<%$ Resources:msjInformacionRequerida %>"/>';
        var msnInicioFin = '<asp:Literal runat="server" Text="<%$ Resources:msnInicioFin %>"/>';
        var msjSumaTotal = '<asp:Literal runat="server" Text="<%$ Resources:msjSumaTotal %>"/>';
        var msjProductos = '<asp:Literal runat="server" Text="<%$ Resources:msjProductos %>"/>';
        var msjRotomix = '<asp:Literal runat="server" Text="<%$ Resources:msjRotomix %>"/>';
        var msjBatch = '<asp:Literal runat="server" Text="<%$ Resources:msjBatch %>"/>';
        var msjFormulas = '<asp:Literal runat="server" Text="<%$ Resources:msjFormulas %>"/>';
        var msjArchivoTXT = '<asp:Literal runat="server" Text="<%$ Resources:msjArchivoTXT %>"/>';
        var msjSeleccioneArchivo = '<asp:Literal runat="server" Text="<%$ Resources:msjSeleccioneArchivo %>"/>';
        var msjFechaYHora = '<asp:Literal runat="server" Text="<%$ Resources:msjFechaYHora %>"/>';
        var msjIngrediente = '<asp:Literal runat="server" Text="<%$ Resources:msjIngrediente %>"/>';
        var msjGuardadoExitoso = '<asp:Literal runat="server" Text="<%$ Resources:msjGuardadoExitoso %>"/>';
        var msjConfirmacion = '<asp:Literal runat="server" Text="<%$ Resources:msjConfirmacion %>"/>';
        var msjSeleccionArchivo = '<asp:Literal runat="server" Text="<%$ Resources:msjSeleccionArchivo %>"/>';

        var ingrediente = '<asp:Literal runat="server" Text="<%$ Resources:ingrediente %>"/>';
        var Formula = '<asp:Literal runat="server" Text="<%$ Resources:Formula %>"/>';
        var CantidadProgramada = '<asp:Literal runat="server" Text="<%$ Resources:CantidadProgramada %>"/>';
        var CantidadReal = '<asp:Literal runat="server" Text="<%$ Resources:CantidadReal %>"/>';
        var VerDetalle = '<asp:Literal runat="server" Text="<%$ Resources:VerDetalle %>"/>';
        var vacio = '<asp:Literal runat="server" Text="<%$ Resources:vacio %>"/>';
        var columnas = '<asp:Literal runat="server" Text="<%$ Resources:columnas %>"/>';
        var ColumnaCodigo = '<asp:Literal runat="server" Text="<%$ Resources:ColumnaCodigo %>"/>';
        var sumatorias = '<asp:Literal runat="server" Text="<%$ Resources:sumatorias %>"/>';
        var producto = '<asp:Literal runat="server" Text="<%$ Resources:producto %>"/>';
        var rotomix = '<asp:Literal runat="server" Text="<%$ Resources:rotomix %>"/>';
        var batch = '<asp:Literal runat="server" Text="<%$ Resources:batch %>"/>';
        var formulas = '<asp:Literal runat="server" Text="<%$ Resources:formulas %>"/>';
        var OK = '<asp:Literal runat="server" Text="<%$ Resources:OK %>"/>';
        var fecha = '<asp:Literal runat="server" Text="<%$ Resources:fecha %>"/>';
        var msgFechaInvalida = '<asp:Literal runat="server" Text="<%$ Resources:msgFechaInvalida.Text %>"/>';

        var msjSeis = '<asp:Literal runat="server" Text="<%$ Resources:msjSeis %>"/>';
        var msjNueve = '<asp:Literal runat="server" Text="<%$ Resources:msjNueve %>"/>';

        var KilosProducidos = '<asp:Literal runat="server" Text="<%$ Resources:KilosProducidos %>"/>';
        var NumeroBachada = '<asp:Literal runat="server" Text="<%$ Resources:NumeroBachada %>"/>';
        var rotomix2 = '<asp:Literal runat="server" Text="<%$ Resources:rotomix2 %>"/>';

        var columnaBatch = '<asp:Literal runat="server" Text="<%$ Resources:columnaBatch %>"/>';
        var columnaFormula = '<asp:Literal runat="server" Text="<%$ Resources:columnaFormula %>"/>';
        var columnaCodigo2 = '<asp:Literal runat="server" Text="<%$ Resources:columnaCodigo2 %>"/>';
        var columnaMeta = '<asp:Literal runat="server" Text="<%$ Resources:columnaMeta %>"/>';
        var columnaReal = '<asp:Literal runat="server" Text="<%$ Resources:columnaReal %>"/>';
        var columnaFecha = '<asp:Literal runat="server" Text="<%$ Resources:columnaFecha %>"/>';
        var columnaHora = '<asp:Literal runat="server" Text="<%$ Resources:columnaHora %>"/>';
        var columnaMarca = '<asp:Literal runat="server" Text="<%$ Resources:columnaMarca %>"/>';
        var columnaNombre = '<asp:Literal runat="server" Text="<%$ Resources:columnaNombre %>"/>';

        $("#lblMensajeDialogo").text(msjConfirmacion);
    </script>
</body>
</html>
