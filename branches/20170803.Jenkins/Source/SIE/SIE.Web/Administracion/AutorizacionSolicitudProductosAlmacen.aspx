<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutorizacionSolicitudProductosAlmacen.aspx.cs" Inherits="SIE.Web.Administracion.AutorizacionSolicitudProductosAlmacen" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
        <%: Styles.Render("~/bundles/AutorizacionSolicitudProductosAlmacenEstilo") %>
    </asp:PlaceHolder>



    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</head>

<body class="page-header-fixed" ondragstart="return false;" ondrop="return false;">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div id="skm_LockPane" class="LockOff">
                Por favor espere...
            </div>
            <div class="pantallaCompleta">
                <div class="portlet box SuKarne2">
                    <div class="portlet-title">
                        <div class="caption">
                            <asp:Image ID="skLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                            <span>
                                <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1">
                                Autorización de solicitud de productos a almacén 
                                </asp:Label></span>
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
                                <a href="../Administracion/AutorizacionSolicitudProductosAlmacen.aspx">Autorización de solicitud de productos a almacén</a>
                            </li>
                        </ul>
                        <div class="row-fluid">
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Búsqueda </legend>
                                <div class="span12">
                                    <span class="span3">
                                        <asp:Label ID="Label1" runat="server" CssClass="campoRequerido">*</asp:Label>
                                        <asp:Label runat="server" ID="lblFolio" Text="Folio:" meta:resourcekey="lblFolioResource1"></asp:Label>
                                        <input type="tel" id="txtFolio" class="span4 textoDerecha" />
                                        <img id="btnAyudaFolio" src="../Images/find.png" class="imagen" alt="" />
                                    </span>
                                    <span class="span5">
                                        <asp:Label runat="server" ID="lblAutoriza" Text="Autoriza:" meta:resourcekey="lblAutorizaResource1"></asp:Label>
                                        <input type="text" id="txtAutoriza" class="span8" />
                                    </span>
                                    <span class="span4">
                                        <asp:Label runat="server" ID="lblFechaSolicitud" Text="Fecha Solicitud:" meta:resourcekey="lblFechaSolicitudResource1"></asp:Label>
                                        <input id="txtFechaSolicitud" type="date" class="span4" />
                                    </span>
                                </div>
                            </fieldset>
                            <div id="divGridProductos">
                            </div>
                            <div>
                                <div class="span4">
                                    <asp:Label ID="Label2" runat="server" CssClass="campoRequerido">*</asp:Label>
                                    <asp:Label ID="lblObservacion" runat="server" meta:resourcekey="lblObservacionResource1"></asp:Label>
                                </div>
                                <textarea id="txtObservaciones" class="span12"></textarea>
                            </div>
                        </div>

                        <div class="textoDerecha margenTopdiv">
                            <span class="espacioCortoDerecha">
                                <button type="button" id="btnGuardar" class="btn SuKarne">
                                    <asp:Label ID="lblGuardar" runat="server" Text="Guardar" meta:resourcekey="lblGuardarResource1"></asp:Label></button>
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
            <asp:HiddenField ID="hfSolicitudProductoId" runat="server" />
            <asp:HiddenField ID="hfAutorizaId" runat="server" />
        </form>
    </div>


    <div id="modalBusquedaFolio" class="modal hide fade large" data-backdrop="static" data-keyboard="false" tabindex="-1">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="modal-header">
                    <button id="btnAyudaFolioCerrar" type="button" class="close cerrarBusquedaFolio" aria-hidden="true">
                        <img src="../Images/close.png" />
                    </button>
                    <h3 class="caption">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <asp:Label ID="lblBusquedaFolio" runat="server" Text="Búsqueda de Folio" meta:resourcekey="lblBusquedaProductos"></asp:Label>
                    </h3>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="modal-body">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblFiltrosFolio" runat="server" Text="Filtros" meta:resourcekey="lblFiltrosProducto"></asp:Label></legend>
                        <div class="row-fluid">
                            <span class="span6 sinMargen">
                                <asp:Label ID="lblProductoBusqueda" CssClass="span4" runat="server" Text="Folio:" meta:resourcekey="lblProductoBusqueda"></asp:Label>
                                <input id="txtFolioBusqueda" class="span8 textoDerecha" oninput="maxLengthCheck(this)" maxlength="50" type="tel" />
                            </span>
                            <span class="span6 textoDerecha">
                                <button type="button" id="btnBuscarFolio" class="btn span4 SuKarne">
                                    <asp:Label ID="lblBuscarFolio" runat="server" Text="Buscar" meta:resourcekey="lblBuscarFolio"></asp:Label>
                                </button>
                                <button id="btnAgregarFolio" type="button" class="btn span4 SuKarne">
                                    <asp:Label ID="lblAgregarFolio" runat="server" Text="Agregar" meta:resourcekey="lblAgregarFolio"></asp:Label>
                                </button>
                                <button id="btnCancelarFolio" type="button" class="btn span4 SuKarne">
                                    <asp:Label ID="lblCancelarFolio" runat="server" Text="Cancelar" meta:resourcekey="lblCancelarFolio"></asp:Label>
                                </button>
                            </span>
                        </div>
                    </fieldset>
                    <div id="divGridAyudaSolicitudProducto">
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        //[Mensajes]
        var MensajeSalirSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSalirSinGuardar %>"/>';
        var MensajeSeleccionarFolio = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarFolio %>"/>';
        var MensajeSalirSinSeleccionarFolio = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSalirSinSeleccionarFolio %>"/>';
        var ErrorConsultarProductos = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorConsultarProductos %>"/>';
        var MensajeConsultarSolicitudesPorPagina = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeConsultarSolicitudesPorPagina %>"/>';
        var MensajeConsultarSolicitudesPorFolio = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeConsultarSolicitudesPorFolio %>"/>';
        var MensajeCapturarObservaciones = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCapturarObservaciones %>"/>';
        var MensajeGuardoExito = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeGuardoExito %>"/>';
        var MensajeDatosGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeDatosGuardar %>"/>';
        var MensajeErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorGuardar %>"/>';
        var MensajeCancelarPantalla = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCancelarPantalla %>"/>';
        var MensajeErrorObtenerUsuario = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeErrorObtenerUsuario %>"/>';
        var MensajeSelecionarAlMenosUno = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSelecionarAlMenosUno %>"/>';


        //[columnas Grid AyudaProductos]
        var ColumnaGridAyudaClave = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaGridAyudaClave %>"/>';
        var ColumnaGridayudaDescripcion = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaGridayudaDescripcion %>"/>';
        //[Columnas Grid productos]
        var ColumnaGridCodigo = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaGridCodigo%>"/>';
        var ColumnaGridArtiulo = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaGridArtiulo%>"/>';
        var ColumnaGridCantidad = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaGridCantidad%>"/>';
        var ColumnaGridUnidadMedicion = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaGridUnidadMedicion%>"/>';
        var ColumnaGridDescripcion = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaGridDescripcion%>"/>';
        var ColumnaGridClaseCosto = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaGridClaseCosto%>"/>';
        var ColumnaGridCentroCosto = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaGridCentroCosto%>"/>';
        var ColumnaGridAutorizar = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaGridAutorizar %>"/>';

        //[Etiquetas de los botones]
        var Seleccione = '<asp:Literal runat="server" Text="<%$ Resources:js.Seleccione %>"/>';
        var Si = '<asp:Literal runat="server" Text="<%$ Resources:js.Si %>"/>';
        var No = '<asp:Literal runat="server" Text="<%$ Resources:js.No %>"/>';
        var Aceptar = '<asp:Literal runat="server" Text="<%$ Resources:js.Aceptar %>"/>';
        var OK = '<asp:Literal runat="server" Text="<%$ Resources:js.OK %>"/>';
        
    </script>
    <script src="../Scripts/AutorizacionSolicitudProductosAlmacen.js"></script>
</body>


<script type="text/javascript">
    var Aceptar = 'Aceptar';
</script>
</html>
