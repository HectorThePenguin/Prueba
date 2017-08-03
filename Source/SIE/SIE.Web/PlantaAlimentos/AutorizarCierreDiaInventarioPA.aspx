<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutorizarCierreDiaInventarioPA.aspx.cs" Inherits="SIE.Web.PlantaAlimentos.AutorizarCierreDiaInventarioPA" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
    <script src="../Scripts/AutorizarCierreDiaInventarioPA.js"></script>
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
        <div id="skm_LockPane" class="LockOff">
        </div>
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                    <span>
                        <asp:Label ID="lblTitulo" runat="server" Text="Autorizar Cierre Día de Planta de Alimentos" meta:resourcekey="lblTituloResource1"></asp:Label></span>
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
                        <a href="../PlantaAlimentos/AutorizarCierreDiaInventarioPA.aspx">Autorizar Cierre Día</a>
                    </li>
                </ul>

                <div class="row-fluid">
                    <fieldset class="scheduler-border">
                        <legend class="scheduler-border">
                            <asp:Label ID="lblAutorizarAjuste" runat="server" Text="Autorizar Ajuste" meta:resourcekey="lblAutorizarAjusteResource1"></asp:Label></legend>
                        <div class="row-fluid">
                            <span class="span4">
                                <asp:Label ID="lblAlmacen" CssClass="margenLabel span3" runat="server" Text="*Almacén:" meta:resourcekey="lblAlmacenResource1"></asp:Label>
                                <select class="span8" id="ddlAlmacen" tabindex="1">
                                    <option value="0">Seleccione</option>
                                </select>
                            </span>

                            <span class="span3">
                                <button id="btnConsultar" type="button" class="btn SuKarne">
                                    <asp:Label ID="lblConsultar" runat="server" Text="Consultar" meta:resourcekey="lblConsultarResource1"></asp:Label>
                                </button>
                            </span>

                        </div>
                    </fieldset>

                    <fieldset class="scheduler-border">
                        <legend class="scheduler-border">
                            <asp:Label ID="lblDetalleAlmacen" runat="server" Text="DetalleAlmacen" meta:resourcekey="lblDetalleAlmacenResource1"></asp:Label></legend>
                        <div class="row-fluid">
                            <span class="span1">
                                <asp:Label ID="lblFolio" CssClass="margenLabel span6" runat="server" Text="Folio:" meta:resourcekey="lblFolioResource1"></asp:Label>

                            </span>
                            <span class="span2">
                                <input class="span8 textBoxChico textoDerecha" disabled="disabled" id="txtFolio" type="text" />
                            </span>

                            <span class="span3">
                                <asp:Label ID="lblEstatus" CssClass="margenLabel span3" runat="server" Text="Estatus:" meta:resourcekey="lblEstatusResource1"></asp:Label>
                                <input class="span8 textBoxChico" disabled="disabled" id="txtEstatus" type="text" />
                            </span>

                            <span class="span3">
                                <asp:Label ID="lblFecha" CssClass="span3" runat="server" Text="Fecha:" meta:resourcekey="lblFechaResource1"></asp:Label>
                                <input class="span6 textBoxChico" disabled="disabled" id="txtFecha" type="text" />
                            </span>
                        </div>

                        <div class="row-fluid">
                            <span class="span1">
                                <asp:Label ID="lblObservaciones" CssClass="margenLabel span2" runat="server" Text="Observaciones:" meta:resourcekey="lblObservacionesResource1"></asp:Label>

                            </span>
                            <span class="span11">

                                <textarea id="txtObservaciones" disabled="disabled" class="span12"></textarea>
                            </span>
                        </div>

                        <div id="divGridCierreDiaInventario"></div>
                    </fieldset>

                    <div class="textoDerecha">
                        <span class="espacioCortoDerecha">
                            <button id="btnGuardar" type="button" class="btn SuKarne">
                                <asp:Label ID="lblGuardar" runat="server" Text="Guardar" meta:resourcekey="lblGuardarResource1"></asp:Label>
                            </button>
                        </span>
                        <span>
                            <button id="btnCancelar" type="button" class="btn SuKarne">
                                <asp:Label ID="lblCancelar" runat="server" Text="Cancelar" meta:resourcekey="lblCancelarResource1"></asp:Label>
                            </button>
                        </span>
                    </div>
                </div>
            </div>

        </div>


    </form>
    <script type="text/javascript">

        //Mensajes
        var ErrorAlmacen = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorAlmacen %>"/>';
        var MensajeCancelarPantalla = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCancelarPantalla %>"/>';
        var SeleccionarAlmacen = '<asp:Literal runat="server" Text="<%$ Resources:js.SeleccionarAlmacen %>"/>';
        var SinMovimientos = '<asp:Literal runat="server" Text="<%$ Resources:js.SinMovimientos %>"/>';
        var ErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorGuardar %>"/>';
        var SinAutorizados = '<asp:Literal runat="server" Text="<%$ Resources:js.SinAutorizados %>"/>';
        var GuardadoConExito = '<asp:Literal runat="server" Text="<%$ Resources:js.GuardadoConExito %>"/>';
        var SinInformacion = '<asp:Literal runat="server" Text="<%$ Resources:js.SinInformacion %>"/>';
        
        
        

        //Columnas Grid
        var ColumnaProducto = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaProducto %>"/>';
        var ColumnaLote = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaLote %>"/>';
        var ColumnaProducto = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaProducto %>"/>';
        var ColumnaTamanioLote = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaTamanioLote %>"/>';
        var ColumnaInventarioTeorico = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaInventarioTeorico %>"/>';
        var ColumnaInventarioFisico = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaInventarioFisico %>"/>';
        var ColumnaMermaSuperavit = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaMermaSuperavit %>"/>';
        var ColumnaPorcentajeLote = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaPorcentajeLote %>"/>';
        var ColumnaPorcentajePermitido = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaPorcentajePermitido %>"/>';
        var ColumnaAutorizar = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaAutorizar %>"/>';

        var Seleccione = '<asp:Literal runat="server" Text="<%$ Resources:js.Seleccione %>"/>';
        var Si = '<asp:Literal  runat="server" Text="<%$ Resources:js.Si %>"/>';
        var No = '<asp:Literal  runat="server" Text="<%$ Resources:js.No %>"/>';
        var OK = '<asp:Literal  runat="server" Text="<%$ Resources:js.OK %>"/>';
        var PorAutorizar = '<asp:Literal  runat="server" Text="<%$ Resources:js.PorAutorizar %>"/>';

    </script>

</body>
</html>
