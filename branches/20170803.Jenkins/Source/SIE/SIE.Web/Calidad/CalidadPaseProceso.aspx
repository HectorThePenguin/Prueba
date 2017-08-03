<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalidadPaseProceso.aspx.cs"
    Inherits="SIE.Web.Calidad.CalidadPaseProceso" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/css/Comun.css" rel="stylesheet" />
    <link href="../assets/plugins/datepicker/css/datepicker.css" rel="stylesheet" />
    <script src="../assets/plugins/datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/plugins/datepicker/js/locales/bootstrap-datepicker.es.js"></script>

    <link href="../assets/css/CalidadPaseProceso.css" rel="stylesheet" />
    <link href="../assets/css/Semaforo.ashx" type="text/css" rel="stylesheet" />
    
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
<body onload="changeHashOnLoad();" ondragstart="return false;" ondrop="return false;">
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
                            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource2" />
                            <span class="letra">
                                <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource2"></asp:Label></span>
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
                                    <a href="../Calidad/CalidadPaseProceso.aspx">Calidad Pase a Proceso</a>
                                </li>
                            </ul>
                        <div class="row-fluid">
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">
                                    <asp:Label ID="lblBusqueda" runat="server" Text="Búsqueda" meta:resourcekey="lblBusquedaResource2"></asp:Label>
                                </legend>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span5">
                                            <div class="span5">
                                                <asp:Label ID="Label1" runat="server" CssClass="requerido" meta:resourcekey="Label1Resource2">*</asp:Label>
                                                <asp:Label runat="server" ID="lblTipoMovimiento" meta:resourcekey="lblTipoMovimientoResource2"></asp:Label>
                                            </div>
											<div class="span7">
                                                <select id="ddlTipoMovimiento" class="span12"></select>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="span6">
                                                <asp:Label ID="Label2" runat="server" CssClass="requerido span1" meta:resourcekey="Label2Resource2">*</asp:Label>
                                                <asp:Label runat="server" ID="lblFolioPaseProceso" meta:resourcekey="lblFolioPaseProcesoResource2" CssClass="span11"></asp:Label>
                                            </div>
											<div class="span4">
											    <input type="number" min="0" id="txtFolioPaseProceos" class="span12 soloNumeros textoDerecha" oninput="maxLengthCheck(this)" maxlength="4" />
                                            </div>
											<div class="span2">
											    <a id="btnAyudaFolio" href="#" data-toggle="modal" tabindex="-1">
                                                    <img src="../Images/find.png" width="26" height="26" />
                                                </a>
                                            </div>
                                        </div>
                                        <div class="span3">
                                            <div class="span4">
                                                <asp:Label runat="server" ID="lblFecha" meta:resourcekey="lblFechaResource2" CssClass="span12"></asp:Label>
                                            </div>
											<div class="span8">
                                                <input id="txtFecha" class="soloNumeros span12" type="text" oninput="maxLengthCheck(this)" maxlength="8" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="row-fluid" id="divProductoFormula">
                        </div>

                        <div class="row-fluid" id="divObservaciones">
                        </div>
                        <div class="textoDerecha">
                            <span class="espacioCortoDerecha">
                                <button type="button" id="btnGuardar" class="btn SuKarne">
                                    <asp:Label ID="lblGuardar" runat="server" meta:resourcekey="lblGuardarResource1"></asp:Label></button>
                            </span>

                            <span>
                                <button id="btnCancelar" type="button" class="btn SuKarne">
                                    <asp:Label ID="lblCancelar" runat="server" meta:resourcekey="lblCancelarResource1"></asp:Label>
                                </button>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script type="text/javascript">
            var leyendaProducto = '<asp:Literal runat="server" Text="<%$ Resources:LeyendaProducto %>"/>';
            var leyendaFormula = '<asp:Literal runat="server" Text="<%$ Resources:LeyendaFormula %>"/>';

            var producto = '<asp:Literal runat="server" Text="<%$ Resources:lblProductoResource1.Text %>"/>';
            var formula = '<asp:Literal runat="server" Text="<%$ Resources:Label7Resource1.Text %>"/>';
            var subFamilia = '<asp:Literal runat="server" Text="<%$ Resources:Label8Resource1.Text %>"/>';

            var indicadoresCalidad = '<asp:Literal runat="server" Text="<%$ Resources:LabelResource1.Text %>"/>';
            var resultado = '<asp:Literal runat="server" Text="<%$ Resources:LabelResource2.Text %>"/>';
            var rangoObjetivo = '<asp:Literal runat="server" Text="<%$ Resources:LabelResource3.Text %>"/>';
            var observaciones = '<asp:Literal runat="server" Text="<%$ Resources:Label6Resource2.Text %>"/>';

            var sinDatos = '<asp:Literal runat="server" Text="<%$ Resources:NoSeEncontraronDatos %>"/>';
            var errorConsultarTipoMovimiento = '<asp:Literal runat="server" Text="<%$ Resources:ErrorConsultarTiposMovimiento %>"/>';

            var seleccione = '<asp:Literal runat="server" Text="<%$ Resources:ItemSeleccione %>"/>';

            var folioInvalido = '<asp:Literal runat="server" Text="<%$ Resources:FolioProcesoInvalido %>"/>';
            var errorConsultarFolioProceso = '<asp:Literal runat="server" Text="<%$ Resources:ErrorConsultarFolioProceso %>"/>';

            var productoInvalido = '<asp:Literal runat="server" Text="<%$ Resources:MsgProductoInvalido %>"/>';
            var errorConsultarProducto = '<asp:Literal runat="server" Text="<%$ Resources:MsgErrorConsultarProducto %>"/>';

            var formulaInvalida = '<asp:Literal runat="server" Text="<%$ Resources:MsgFormulaInvalida %>"/>';
            var errorConsultarFormula = '<asp:Literal runat="server" Text="<%$ Resources:MsgErrorConsultarFormula %>"/>';

            var errorConsultarSemaforo = '<asp:Literal runat="server" Text="<%$ Resources:MsgErrorConsultarSemaforo %>"/>';
            var camposFaltantes = '<asp:Literal runat="server" Text="<%$ Resources:MsgCamposFaltantes %>"/>';
            var observacionesFaltantes = '<asp:Literal runat="server" Text="<%$ Resources:MsgObservacionesFaltantes %>"/>';

            var MsgGuardadoExitoso = '<asp:Literal runat="server" Text="<%$ Resources:MsgGuardadoExitoso %>"/>';

            var paseProceso = '<asp:Literal runat="server" Text="<%$ Resources:ItemPaseProceso %>"/>';
            var produccionFormulas = '<asp:Literal runat="server" Text="<%$ Resources:ItemProduccionFormula %>"/>';

            var Cancelar = '<asp:Literal runat="server" Text="<%$ Resources:js.Cancelar %>"/>';
            var Si = '<asp:Literal runat="server" Text="<%$ Resources:js.Si %>"/>';
            var No = '<asp:Literal runat="server" Text="<%$ Resources:js.No %>"/>';
        </script>
        <script src="../Scripts/CalidadPaseProceso.js"></script>
        <!-- END PAGE LEVEL SCRIPTS -->
        <!-- END JAVASCRIPTS -->
        <input type="hidden" id="hdnPedidoID" value="0" />
        <input type="hidden" id="hdnProductoID" value="0" />
        <input type="hidden" id="hdnIndicadores" value="0" />

        <div id="modalBusquedaProducto" class="modal hide fade large" data-backdrop="static" data-keyboard="false" tabindex="-1">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button type="button" class="cerrarBusquedaProductos close" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/skLogo.png" />
                            <asp:Label ID="lblBusquedaProductos" runat="server" Text="Búsqueda de Producto"></asp:Label>
                        </h3>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="modal-body">
                        <div class="row-fluid">
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border"><asp:Label ID="lblFiltrosProducto" runat="server" Text="Filtros"></asp:Label></legend>
                                <div class="span12">
                                    <div class="span5 sinMargen">
                                        <asp:Label ID="lblProductoBusqueda" CssClass="span3" runat="server" Text="Producto:"></asp:Label>
                                        <input class="span5" oninput="maxLengthCheck(this)" maxlength="50" id="txtProductoBusqueda" type="text" />
                                    </div>
                                    <div class="span5 textoDerecha">
                                        <button type="button" id="btnBuscarProducto" class="btn SuKarne">
                                            <asp:Label ID="lblBuscarProducto" runat="server" Text="Buscar"></asp:Label></button>
                                        <button id="btnAgregarProducto" type="button" class="btn SuKarne">
                                            <asp:Label ID="lblAgregarProducto" runat="server" Text="Agregar"></asp:Label>
                                        </button>
                                        <button id="btnCancelarProducto" type="button" class="btn SuKarne">
                                            <asp:Label ID="lblCancelarProducto" runat="server" Text="Cancelar"></asp:Label>
                                        </button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div id="divGridProductos">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalBusquedaFolio" class="modal hide fade large" data-backdrop="static" data-keyboard="false" tabindex="-1">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button type="button" class="cerrarBusquedaFolio close" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/skLogo.png" />
                            <asp:Label ID="Label3" runat="server" Text="Búsqueda de folio en pase a proceso"></asp:Label>
                        </h3>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="modal-body">
                        <div class="row-fluid">
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border"><asp:Label ID="Label4" runat="server" Text="Filtros"></asp:Label></legend>
                                <div class="span12">
                                    <div class="span5 sinMargen">
                                        <asp:Label ID="Label5" CssClass="span3" runat="server" Text="Almacén:"></asp:Label>
                                        <input class="span5" oninput="maxLengthCheck(this)" maxlength="50" id="txtFolioBusqueda" type="text" />
                                    </div>
                                    <div class="span5 textoDerecha">
                                        <button type="button" id="btnBuscarFolio" class="btn SuKarne">
                                            <asp:Label ID="lblBuscarFolio" runat="server" Text="Buscar"></asp:Label></button>
                                        <button id="btnAgregarFolio" type="button" class="btn SuKarne">
                                            <asp:Label ID="lblAgregarFolio" runat="server" Text="Agregar"></asp:Label>
                                        </button>
                                        <button id="btnCancelarFolio" type="button" class="btn SuKarne">
                                            <asp:Label ID="lblCancelarFolio" runat="server" Text="Cancelar"></asp:Label>
                                        </button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div id="divGridFolio">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalBusquedaFormula" class="modal hide fade large" data-backdrop="static" data-keyboard="false" tabindex="-1">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button type="button" class="cerrarBusquedaFormula close" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/skLogo.png" />
                            <asp:Label ID="lblBusquedaFormula" runat="server" Text="Búsqueda de fórmulas"></asp:Label>
                        </h3>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="modal-body">
                        <div class="row-fluid">
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border"><asp:Label ID="lblFiltrosFormula" runat="server" Text="Filtros"></asp:Label></legend>
                                <div class="span12">
                                    <div class="span5 sinMargen">
                                        <asp:Label ID="lblFormulaBusqueda" CssClass="span3" runat="server" Text="Fórmula:"></asp:Label>
                                        <input class="span5 textBoxChico" oninput="maxLengthCheck(this)" maxlength="50" id="txtFormulaBusqueda" type="text" />
                                    </div>
                                    <div class="span5 textoDerecha">
                                        <button type="button" id="btnBuscarFormula" class="btn SuKarne">
                                            <asp:Label ID="lblBuscarFormula" runat="server" Text="Buscar"></asp:Label></button>
                                        <button id="btnAgregarFormula" type="button" class="btn SuKarne">
                                            <asp:Label ID="lblAgregarFormula" runat="server" Text="Agregar"></asp:Label>
                                        </button>
                                        <button id="btnCancelarFormula" type="button" class="btn SuKarne">
                                            <asp:Label ID="lblCancelarFormula" runat="server" Text="Cancelar"></asp:Label>
                                        </button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div id="divGridFormula">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
