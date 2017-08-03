<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalificacionCalidad.aspx.cs" Inherits="SIE.Web.Recepcion.CalificacionCalidad" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
    
    <script src="../Scripts/CalificacionCalidad.js"></script>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />

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
    <form id="form1" runat="server">

        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <div id="skm_LockPane" class="LockOff">
            </div>
            <div>
                <div class="pantallaCompleta">
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
                                    <a href="CalificacionCalidad.aspx">Formato para la calificación de calidad</a>
                                </li>
                            </ul>
                            <div class="row-fluid">
                                <div>
                                    <asp:Label ID="lblfenviz" runat="server" CssClass="negritas" meta:resourcekey="lblfenvizResource1"></asp:Label>
                                </div>
                                <div class="span12">
                                    <div>
                                        <div class="span2">
                                            <span>
                                                <asp:Label ID="lblEntrada" runat="server" meta:resourcekey="lblEntradaResource1"></asp:Label>
                                            </span>
                                            <input class="span12 textBoxChico soloNumeros" oninput="maxLengthCheck(this)" maxlength="7" id="txtEntrada" min="0" type="number" />
                                        </div>
                                        <div class="span4">
                                            <span>
                                                <asp:Label ID="lblProveedor" runat="server" meta:resourcekey="lblProveedorResource1"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtProveedor" class="span12 textBoxChico" ReadOnly="True" runat="server" meta:resourcekey="txtProveedorResource1"></asp:TextBox>
                                        </div>
                                        <div class="span1">
                                            <span>
                                                <asp:Label ID="lblCorral" runat="server" meta:resourcekey="lblCorralResource1"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtCorral" class="span12 textBoxChico" ReadOnly="True" runat="server" meta:resourcekey="txtCorralResource1"></asp:TextBox>
                                        </div>
                                        <div class="span1">
                                            <span>
                                                <asp:Label ID="lblCabezas" runat="server" meta:resourcekey="lblCabezasResource1"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtCabezas" class="span12 textBoxChico" ReadOnly="True" runat="server" meta:resourcekey="txtCabezasResource1"></asp:TextBox>
                                        </div>
                                        <div class="span1">
                                            <span>
                                                <asp:Label ID="lblCabezasMuertas" runat="server" meta:resourcekey="lblCabezasMuertasResource1"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtCabezasMuertas" class="span12 textBoxChico" ReadOnly="True" runat="server" meta:resourcekey="txtCabezasMuertasResource1"></asp:TextBox>
                                        </div>
                                        <div class="span2">
                                            <span>
                                                <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFechaResource1"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtFecha" class="span12 textBoxChico" ReadOnly="True" runat="server" meta:resourcekey="txtFechaResource1"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1"></asp:Label></legend>

                                    <div class="row-fluid">
                                        <div class="span12">
                                            <div id="GridCalidad" class="span7">
                                            </div>

                                            <div class="span5">
                                                <table class="table table-bordered table-striped alineacionDerecha">
                                                    <caption class="cabecero">
                                                        <asp:Label ID="lblResumen" runat="server" meta:resourcekey="lblResumenResource1"></asp:Label></caption>
                                                    <thead>
                                                        <tr>
                                                            <th class="alineacionCentro" scope="col">
                                                                <asp:Label ID="lblEnLinea" runat="server" meta:resourcekey="lblEnLineaResource1"></asp:Label>
                                                            </th>
                                                            <th class="alineacionCentro" scope="col">
                                                                <asp:Label ID="lblProduccionCabecero" runat="server" meta:resourcekey="lblProduccionCabeceroResource1"></asp:Label>
                                                            </th>
                                                            <th class="alineacionCentro" scope="col">
                                                                <asp:Label ID="lblVentaCabecero" runat="server" meta:resourcekey="lblVentaCabeceroResource1"></asp:Label>
                                                            </th>
                                                            <th class="alineacionCentro" scope="col">
                                                                <asp:Label ID="lblMuertasCabecero" runat="server" meta:resourcekey="lblMuertasCabeceroResource1"></asp:Label>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td class="alineacionCentro">
                                                                <asp:Label ID="lblLinea" CssClass="totales" runat="server" meta:resourcekey="lblLineaResource1"></asp:Label>
                                                            </td>
                                                            <td class="alineacionCentro">
                                                                <asp:Label ID="lblProduccion" CssClass="totales" runat="server" meta:resourcekey="lblProduccionResource1"></asp:Label>
                                                            </td>
                                                            <td class="alineacionCentro">
                                                                <asp:Label ID="lblVenta" CssClass="totales" runat="server" meta:resourcekey="lblVentaResource1"></asp:Label>
                                                            </td>
                                                            <td class="alineacionCentro span1">
                                                                <input class="span12 textBoxTablas totales alineacionCentro soloNumeros" disabled="disabled" oninput="maxLengthCheck(this)" maxlength="3" id="txtMuertas" type="number" />
                                                                <%--<asp:TextBox ID="txtMuertas" class="span12 textBoxTablas totales alineacionCentro soloNumeros" MaxLength="3" runat="server" meta:resourcekey="txtMuertasResource1"></asp:TextBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" class="cabecero alineacionCentro">
                                                                <asp:Label ID="lblTotalesTitulo" runat="server" meta:resourcekey="lblTotalesTituloResource1"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" class="alineacionCentro">
                                                                <asp:Label ID="lblTotales" CssClass="totales" runat="server" meta:resourcekey="lblTotalesResource1"></asp:Label></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <div class="textoDerecha">
                                  <%--  <span class="espacioCortoDerecha">
                                        <button type="button" id="btnGuardar" class="btn SuKarne">
                                            <asp:Label ID="lblGuardar" runat="server" meta:resourcekey="lblGuardarResource1"></asp:Label></button>
                                    </span>--%>

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
            </div>
        </div>
        <asp:HiddenField ID="hfEntradaGanadoID" runat="server" />
    </form>
    <script type="text/javascript">

        //MENSAJES
        var MensajeInformacion = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeInformacion %>"/>';
        var FolioVacio = '<asp:Literal runat="server" Text="<%$ Resources:js.SinFolioEntrada %>"/>';
        var Aceptar = '<asp:Literal runat="server" Text="<%$ Resources:js.Aceptar %>"/>';
        var CabezasMayor = '<asp:Literal runat="server" Text="<%$ Resources:js.CabezasMayor %>"/>';
        var SinDatos = '<asp:Literal runat="server" Text="<%$ Resources:js.SinDatos %>"/>';
        var ErrorConsultarCalidad = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorConsultarCalidad %>"/>';
        var SinEntrada = '<asp:Literal runat="server" Text="<%$ Resources:js.SinEntrada %>"/>';
        var ErrorConsultarEntrada = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorConsultarEntrada %>"/>';
        var GuardoExito = '<asp:Literal runat="server" Text="<%$ Resources:js.GuardoExito %>"/>';
        var ErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorGuardar %>"/>';
        var FaltaDatos = '<asp:Literal runat="server" Text="<%$ Resources:js.FaltaDatos %>"/>';

        var EmbarqueNoRecibido = '<asp:Literal runat="server" Text="<%$ Resources:js.EmbarqueNoRecibido %>"/>';
        var EntradaCosteada = '<asp:Literal runat="server" Text="<%$ Resources:js.EntradaCosteada %>"/>';
        var EntradaConCalidad = '<asp:Literal runat="server" Text="<%$ Resources:js.EntradaConCalidad %>"/>';
        var EntradaSinCondicion = '<asp:Literal runat="server" Text="<%$ Resources:js.EntradaSinCondicion %>"/>';
        var Cancelar = '<asp:Literal runat="server" Text="<%$ Resources:js.Cancelar %>"/>';
        var Si = '<asp:Literal runat="server" Text="<%$ Resources:js.Si %>"/>';
        var No = '<asp:Literal runat="server" Text="<%$ Resources:js.No %>"/>';
        //MENSAJES

        //CABECEROS COLUMNAS
        var ColumnaCalidad = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaCalidad %>"/>';
        var ColumnaHembras = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaHembras %>"/>';
        var ColumnaMachos = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaMachos %>"/>';
        //CABECEROS COLUMNAS

    </script>
</body>


<%--<script src="../assets/plugins/jquery-ui-1.10.1.custom.min.js"></script>--%>
<%--<script src="../assets/plugins/bootstrap-modal/js/bootstrap-modal.js"></script>--%>
<%--<script src="../assets/scripts/jquery-jtemplates.js"></script>--%>
<%--<script src="../assets/plugins/data-tables/DT_bootstrap.js"></script>--%>
<%--<script src="../assets/plugins/bootstrap-modal/js/bootstrap-modalmanager.js"></script>--%>
<%--<script src="../assets/scripts/ui-modals.js"></script>--%>
<%--<script src="../assets/scripts/app.js"></script>--%>
<%--<script src="../assets/plugins/spin.js"></script>--%>
<%--<script src="../assets/plugins/jquery.spin.js"></script>--%>

<%--<script src="../assets/plugins/numericInput/jquery-numericInput.min.js"></script>--%>
<%--<script src="../Scripts/CalificacionCalidad.js"></script>--%>
<%--<script src="../assets/plugins/jquery.mobile-1.4.0/jquery.mobile-1.4.0.min.js"></script>--%>
</html>
