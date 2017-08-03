<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProduccionDiariaMolino.aspx.cs" Inherits="SIE.Web.PlantaAlimentos.ProduccionDiariaMolino" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
    <link href="../assets/css/ProduccionDiariaMolino.css" rel="stylesheet" />
    <script src="../Scripts/ProduccionDiariaMolino.js"></script>
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
            <div id="divContenedor" class="row-fluid">
                <div class="span12">
                    <div class="portlet box SuKarne2">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                <span>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Producción Diaria de Molino" meta:resourcekey="lblTituloResource1"></asp:Label></span>
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
                                    <a href="../PlantaAlimentos/ProduccionDiariaMolino.aspx">Producción Diaria de Molino</a>
                                </li>
                            </ul>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border-sinPadding">
                                    <legend class="scheduler-border-sinPadding">
                                        <asp:Label ID="lblDatosProduccion" runat="server" Text="Datos de Producción Diaria " meta:resourcekey="lblDatosProduccionResource1"></asp:Label></legend>

                                    <div class="row-fluid">
                                        <span class="span1">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblTurno" runat="server" Text="Turno:" meta:resourcekey="lblTurnoResource1"></asp:Label>
                                        </span>
                                        <span class="span3">
                                            <select class="span12" id="ddlTurno" tabindex="1">
                                                <option value="|">Seleccione</option>
                                            </select>
                                        </span>
                                        <span class="span2">
                                            <asp:Label ID="lblHumedad" CssClass="margenLabel" runat="server" Text="% Humedad Forraje Molido:" meta:resourcekey="lblHumedadResource1"></asp:Label>
                                        </span>
                                        <span class="span2">
                                            <input class="span12 textBoxChico textBoxDerecha" disabled="disabled" id="txtHumedad" type="text" />
                                        </span>
                                        <span class="span2">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblLitrosInicial" runat="server" Text="Litros Agua / Grasa Inicial:" meta:resourcekey="lblLitrosInicialResource1"></asp:Label>
                                        </span>
                                        <span class="span2">
                                            <input class="span10 textBoxChico soloNumeros" tabindex="5" oninput="maxLengthCheck(this)" maxlength="7" id="txtLitrosInicial" min="0" type="number" />
                                        </span>
                                    </div>

                                    <div class="row-fluid">
                                        <span class="span1">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblTicket" runat="server" Text="Ticket:" meta:resourcekey="lblTicketResource1"></asp:Label>
                                        </span>
                                        <span class="span3">
                                            <input class="span4 textBoxChico txtAyudaTicket textoAyuda" tabindex="3" oninput="maxLengthCheck(this)" maxlength="7" id="txtTicket" min="0" type="text" />
                                            <img src="../Images/find.png" id="btnAyudaTickets" class="imagen" alt="" />
                                        </span>
                                        <span class="span2">
                                            <asp:Label ID="lblKilosNeto" runat="server" Text="Kilos Neto:" meta:resourcekey="lblKilosNetoResource1"></asp:Label>
                                        </span>
                                        <span class="span2">
                                            <input class="span12 textBoxChico textBoxDerecha" disabled="disabled" id="txtKilosNeto" type="text" />
                                        </span>
                                        <span class="span2">
                                            <asp:Label ID="lblLitroFinal" runat="server" Text="Litros Agua / Grasa Final:" meta:resourcekey="lblLitroFinalResource1"></asp:Label>
                                        </span>
                                        <span class="span2 ajuste-text">
                                            <input class="span10 textBoxChico soloNumeros noRequerido" disabled="disabled" tabindex="6" oninput="maxLengthCheck(this)" maxlength="7" id="txtLitrosFinal" min="0" type="number" />
                                        </span>
                                    </div>

                                    <div class="row-fluid">
                                        <span class="span1">
                                            <span class="requerido" style="margin-right: -1px;">*</span>
                                            <asp:Label ID="lblProducto" Style="margin-right: -20px" runat="server" Text="Producto:" meta:resourcekey="lblProductoResource1"></asp:Label>
                                        </span>

                                        <span class="span3">
                                            <input class="span4 textBoxChico soloNumeros textoAyuda" disabled="disabled" tabindex="2" oninput="maxLengthCheck(this)" maxlength="7" id="txtProducto" min="0" type="number" />
                                            <input class="span6 textBoxChico" disabled="disabled" id="txtDescripcionProducto" type="text" />
                                            <%--<img src="../Images/find.png" id="btnAyudaProductos" class="imagen" alt="" />--%>
                                        </span>

                                        <span class="span2">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblForraje" runat="server" Text="Especificación Forraje:" meta:resourcekey="lblForrajeResource1"></asp:Label>
                                        </span>
                                        <span class="span2">
                                            <select class="span12" id="ddlForraje" tabindex="4">
                                                <option value="0">Seleccione</option>
                                            </select>
                                        </span>
                                        <span class="span2">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblHorometroInicial" runat="server" Text="Horómetro Inicial:" meta:resourcekey="lblHorometroInicialResource1"></asp:Label>
                                        </span>
                                        <span class="span2">
                                            <input class="span10 textBoxChico soloNumeros" tabindex="7" oninput="maxLengthCheck(this)" maxlength="7" id="txtHorometroInicial" min="0" type="number" />
                                        </span>
                                    </div>

                                    <div class="row-fluid">
                                        <span class="span1">
                                            <asp:Label ID="lblLote" runat="server" Text="Lote:" meta:resourcekey="lblLoteResource1"></asp:Label>
                                        </span>
                                        <span class="span3">
                                            <input class="span12 textBoxChico textBoxDerecha" disabled="disabled" id="txtLote" type="text" />
                                        </span>
                                        <span class="span2">
                                            <asp:Label ID="lblPacas" runat="server" Text="Conteo Pacas:" meta:resourcekey="lblPacasResource1"></asp:Label>
                                        </span>
                                        <span class="span2">
                                            <input class="span12 textBoxChico textBoxDerecha" disabled="disabled" id="txtConteoPacas" type="text" />
                                        </span>
                                        <span class="span2">
                                            <asp:Label ID="lblHorometroFinal" runat="server" Text="Horómetro Final:" meta:resourcekey="lblHorometroFinalResource1"></asp:Label>
                                        </span>
                                        <span class="span2">
                                            <input class="span10 textBoxChico soloNumeros noRequerido" disabled="disabled" tabindex="8" oninput="maxLengthCheck(this)" maxlength="7" id="txtHorometroFinal" min="0" type="number" />
                                        </span>
                                    </div>

                                    <div class="row-fluid">
                                        <span class="span1">
                                            <asp:Label ID="lblFecha" runat="server" Text="Fecha:" meta:resourcekey="lblFechaResource1"></asp:Label>
                                        </span>
                                        <span class="span3">
                                            <input class="span12 textBoxChico" disabled="True" runat="server" id="txtFecha" type="text" />
                                        </span>
                                        <span class="span2">
                                            <asp:Label ID="lblHoraTicketInicial" runat="server" Text="Hora Ticket Inicial:" meta:resourcekey="lblHoraTicketInicialResource1"></asp:Label>
                                        </span>
                                        <span class="span2">
                                            <input class="span12 textBoxChico horas" tabindex="9" disabled="disabled" oninput="maxLengthCheck(this)" maxlength="7" id="txtHoraTicketInicial" type="time" />
                                        </span>
                                        <span class="span2">
                                            <asp:Label ID="lblHoraTicketFinal" runat="server" Text="Hora Ticket Final:" meta:resourcekey="lblHoraTicketFinalResource1"></asp:Label>
                                        </span>
                                        <span class="span2">
                                            <input class="span10 textBoxChico horas noRequerido" disabled="disabled" tabindex="10" oninput="maxLengthCheck(this)" maxlength="7" id="txtHoraTicketFinal" type="time" />
                                        </span>
                                    </div>
                                    <div class="row-fluid">
                                        <span class="span1">
                                            <asp:Label ID="lblTipoEstatus" runat="server" Text="Tipo Estatus:" meta:resourcekey="lblTipoEstatusResource1"></asp:Label>
                                        </span>
                                        <span class="span3">
                                            <asp:Label ID="lblEstatus" CssClass="span12" runat="server" Text="Sin Supervisar" meta:resourcekey="lblEstatus"></asp:Label>
                                        </span>

                                    </div>

                                    <div class="textoDerecha">
                                        <span class="espacioCortoDerecha">
                                            <button type="button" id="btnAgregar" class="btn SuKarne">
                                                <asp:Label ID="lblAgregar" runat="server" Text="Agregar" meta:resourcekey="lblAgregarResource1"></asp:Label></button>
                                        </span>

                                        <span>
                                            <button id="btnLimpiar" type="button" class="btn SuKarne">
                                                <asp:Label ID="lblLimpiar" runat="server" Text="Limpiar" meta:resourcekey="lblLimpiarResource1"></asp:Label>
                                            </button>
                                        </span>
                                    </div>
                                </fieldset>

                                <div id="divGridMolino">
                                </div>

                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones" meta:resourcekey="lblObservacionesResource1"></asp:Label></legend>

                                    <div class="row-fluid">
                                        <textarea id="txtObservacion" oninput="maxLengthCheck(this)" maxlength="255" class="span12"></textarea>
                                    </div>
                                </fieldset>
                                <div class="textoDerecha">
                                    <span class="espacioCortoDerecha">
                                        <button type="button" id="btnHoraMuertas" class="btn SuKarne">
                                            <asp:Label ID="lblHoraMuertas" runat="server" Text="Horas Muertas" meta:resourcekey="lblHoraMuertasResource1"></asp:Label></button>
                                    </span>

                                    <span class="espacioCortoDerecha">
                                        <button id="btnSupervisor" type="button" class="btn SuKarne">
                                            <asp:Label ID="lblSupervisor" runat="server" Text="Supervisor" meta:resourcekey="lblSupervisorResource1"></asp:Label>
                                        </button>
                                    </span>
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
                </div>
            </div>



        </form>

        <div id="modalSupervisor" class="modal hide fade" data-backdrop="static" data-keyboard="false" tabindex="-1">
            <div id="divModal" class="LockOff">
            </div>
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button type="button" class="close cerrarSupervisor" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                            <asp:Label ID="lblSupervisorTitulo" runat="server" Text="Supervisor" meta:resourcekey="lblSupervisorTitulo"></asp:Label>
                        </h3>
                    </div>

                </div>
                <div class="portlet-body form">
                    <div class="modal-body">

                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblIniciarSesion" runat="server" Text="Iniciar Sesión" meta:resourcekey="lblIniciarSesion"></asp:Label></legend>

                            <div class="row-fluid sinMargen">
                                <asp:Label ID="lblUsuario" CssClass="margenLabel span2" runat="server" Text="*Usuario:" meta:resourcekey="lblUsuario"></asp:Label>
                                <input class="span6" oninput="maxLengthCheck(this)" maxlength="50" id="txtUsuario" type="text" />
                            </div>
                            <div class="row-fluid sinMargen">
                                <asp:Label ID="lblContrasenia" CssClass="margenLabel span2" runat="server" Text="*Contraseña:" meta:resourcekey="lblContrasenia"></asp:Label>
                                <input class="span6" oninput="maxLengthCheck(this)" maxlength="50" id="txtContrasenia" type="password" />
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" id="btnAceptarSupervisor" class="btn SuKarne">
                    <asp:Label ID="lblAceptar" runat="server" Text="Aceptar" meta:resourcekey="lblAceptar"></asp:Label></button>

                <button id="btnCancelarSupervisor" type="button" class="btn SuKarne">

                    <asp:Label ID="lblCancelarSupervisor" runat="server" Text="Cancelar" meta:resourcekey="lblCancelarSupervisor"></asp:Label>
                </button>

            </div>
        </div>

        <div id="modalHorasMuertas" class="modal hide fade large" data-width="900" data-backdrop="static" data-keyboard="false" tabindex="-1">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button type="button" class="close cerrarHorasMuertas" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                            <asp:Label ID="lblTituloHorasMuertas" runat="server" Text="Horas Muertas" meta:resourcekey="lblTituloHorasMuertas"></asp:Label>
                        </h3>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="modal-body">

                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblDatosHorasMuertes" runat="server" Text="Horas Muertas" meta:resourcekey="lblDatosHorasMuertes"></asp:Label></legend>
                            <div class="row-fluid">
                                <span class="span2">
                                    <asp:Label ID="lblHoraInicial" runat="server" Text="Hora Inicial:" meta:resourcekey="lblHoraInicial"></asp:Label>
                                </span>
                                <span class="span2">
                                    <input class="span12 horas" id="txtHoraInicial" type="time" />
                                </span>
                                <span class="span2">
                                    <asp:Label ID="lblHoraFinal" runat="server" Text="Hora Final:" meta:resourcekey="lblHoraFinal"></asp:Label>
                                </span>
                                <span class="span2">
                                    <input class="span12 horas" id="txtHoraFinal" type="time" />
                                </span>

                            </div>
                            <div class="row-fluid">
                                <span class="span2">
                                    <asp:Label ID="lblCausa" runat="server" Text="Causa:" meta:resourcekey="lblCausa"></asp:Label>
                                </span>
                                <span class="span6">
                                    <select id="ddlCausa" class="span10">
                                        <option value="|">Seleccione
                                        </option>
                                    </select>
                                </span>
                            </div>
                            <div class="row-fluid">
                                <span class="span12 textoDerecha">
                                    <button type="button" id="btnAgregarMuertas" class="btn SuKarne">
                                        <asp:Label ID="lblAgregarMuertas" runat="server" Text="Agregar" meta:resourcekey="lblAgregarMuertas"></asp:Label></button>
                                    <button id="btnLimpiarMuertas" type="button" class="btn SuKarne">
                                        <asp:Label ID="lblLimpiarMuertas" runat="server" Text="Limpiar" meta:resourcekey="lblLimpiarMuertas"></asp:Label>
                                    </button>
                                </span>
                            </div>

                        </fieldset>
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblCausasRegistradas" runat="server" Text="Causas Registradas" meta:resourcekey="lblCausasRegistradas"></asp:Label></legend>
                            <div id="contenedorCausasRegistradas"></div>

                            <table id="GridCausasRegistradas" class="table table-hover table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th class="alineacionCentro" scope="col">Horas Muertas</th>
                                        <th class="alineacionCentro" scope="col">Causa</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>

                        </fieldset>
                    </div>
                </div>

                <div class="modal-footer">
                    <button id="btnCerrarHorasMuertas" type="button" class="btn SuKarne">
                        <asp:Label ID="lblCerrarHorasMuertas" runat="server" Text="Cerrar" meta:resourcekey="lblCerrarHorasMuertas"></asp:Label>
                    </button>

                </div>
            </div>
        </div>

        <div id="modalBusquedaProducto" class="modal hide fade large" data-backdrop="static" data-keyboard="false" tabindex="-1">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button type="button" class="close cerrarBusquedaProductos" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                            <asp:Label ID="lblBusquedaProductos" runat="server" Text="Búsqueda de Productos" meta:resourcekey="lblBusquedaProductos"></asp:Label>
                        </h3>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="modal-body">
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblFiltrosProducto" runat="server" Text="Filtros" meta:resourcekey="lblFiltrosProducto"></asp:Label></legend>
                            <div class="row-fluid no-left-margin">
                                <span class="span6 ">
                                    <asp:Label ID="lblProductoBusqueda" runat="server" Text="Producto:" meta:resourcekey="lblProductoBusqueda"></asp:Label>
                                    <input class="span8" oninput="maxLengthCheck(this)" maxlength="50" id="txtProductoBusqueda" type="text" />
                                </span>
                                <span class="span4 textoDerecha">
                                    <button type="button" id="btnBuscarProducto" class="btn SuKarne">
                                        <asp:Label ID="lblBuscarProducto" runat="server" Text="Buscar" meta:resourcekey="lblBuscarProducto"></asp:Label></button>
                                    <button id="btnAgregarProducto" type="button" class="btn SuKarne">
                                        <asp:Label ID="lblAgregarProducto" runat="server" Text="Agregar" meta:resourcekey="lblAgregarProducto"></asp:Label>
                                    </button>
                                    <button id="btnCancelarProducto" type="button" class="btn SuKarne">
                                        <asp:Label ID="lblCancelarProducto" runat="server" Text="Cancelar" meta:resourcekey="lblCancelarProducto"></asp:Label>
                                    </button>
                                </span>
                            </div>
                        </fieldset>
                        <div id="divGridProductos">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalBusquedaTicket" class="modal hide fade large" data-backdrop="static" data-keyboard="false" tabindex="-1">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button type="button" class="close cerrarBusquedaTicket" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                            <asp:Label ID="lblBusquedaTicket" runat="server" Text="Búsqueda de Ticket" meta:resourcekey="lblBusquedaTicket"></asp:Label>
                        </h3>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="modal-body">
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblFiltrosTicket" runat="server" Text="Filtros" meta:resourcekey="lblFiltrosTicket"></asp:Label></legend>
                            <div class="row-fluid">
                                <span class="span6 no-left-margin">
                                    <asp:Label ID="lblTicketBusqueda" runat="server" Text="Ticket:" meta:resourcekey="lblTicketBusqueda"></asp:Label>
                                    <input class="span8 txtAyudaTicket" oninput="maxLengthCheck(this)" maxlength="50" id="txtTicketBusqueda" type="text" />
                                </span>
                                <span class="span4 textoDerecha">
                                    <button type="button" id="btnBuscarTicket" class="btn SuKarne">
                                        <asp:Label ID="lblBuscarTicket" runat="server" Text="Buscar" meta:resourcekey="lblBuscarTicket"></asp:Label></button>
                                    <button id="btnAgregarTicket" type="button" class="btn SuKarne">
                                        <asp:Label ID="lblAgregarTicket" runat="server" Text="Agregar" meta:resourcekey="lblAgregarTicket"></asp:Label>
                                    </button>
                                    <button id="btnCancelarTicket" type="button" class="btn SuKarne">
                                        <asp:Label ID="lblCancelarTicket" runat="server" Text="Cancelar" meta:resourcekey="lblCancelarTicket"></asp:Label>
                                    </button>
                                </span>
                            </div>
                        </fieldset>
                        <div id="divGridTickets">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalSupervisorCompleto" class="modal hide fade extraLarge" data-backdrop="static" data-keyboard="false" tabindex="-1">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button type="button" class="close cerrarSupervisorCompleto" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                            <asp:Label ID="lblTituloSupervisor" runat="server" Text="Supervisor" meta:resourcekey="lblTituloSupervisor"></asp:Label>
                        </h3>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="modal-body">
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblSupervision" runat="server" Text="Supervisión" meta:resourcekey="lblSupervision"></asp:Label></legend>
                            <span class="row-fluid sinMargen">
                                <asp:Label ID="lblSupervisorUsuario" CssClass="span1" runat="server" Text="Usuario:" meta:resourcekey="lblSupervisorUsuario"></asp:Label>
                                <input class="span3" maxlength="50" disabled="disabled" id="txtUsuarioSupervisor" type="text" />
                            </span>
                        </fieldset>
                        <div id="divGridProduccionSupervisor">
                        </div>

                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblObservacionesSupervisor" runat="server" Text="Observaciones" meta:resourcekey="lblObservacionesSupervisor"></asp:Label></legend>

                            <div class="row-fluid">
                                <textarea id="txtObservacionesSupervisor" oninput="maxLengthCheck(this)" maxlength="255" class="span12"></textarea>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnGuardarSupervision" class="btn SuKarne">
                        <asp:Label ID="lblGuardarSupervision" runat="server" Text="Guardar" meta:resourcekey="lblGuardarSupervision"></asp:Label></button>

                    <button id="btnCancelarSupervision" type="button" class="btn SuKarne">
                        <asp:Label ID="lblCancelarSupervision" runat="server" Text="Cancelar" meta:resourcekey="lblCancelarSupervision"></asp:Label>
                    </button>

                </div>
            </div>
        </div>

    </div>
    <script type="text/javascript">
        //MENSAJES
        var MensajeCamposRequeridos = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCamposRequeridos %>"/>';
        var MensajeLitrosIniciales = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeLitrosIniciales %>"/>';
        var MensajeFormatoHorasInicial = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeFormatoHoraInicial %>"/>';
        var MensajeFormatoHorasFinal = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeFormatoHoraFinal %>"/>';
        var MensajeHoraFinalMenor = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeHoraFinalMenor %>"/>';
        var ErrorValidarUsuario = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorValidarUsuario %>"/>';
        var ErrorObtenerForraje = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorObtenerForraje %>"/>';
        var ErrorCausasTiempoMuerto = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorCausasTiempoMuerto %>"/>';
        var ErrorTurnos = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorTurnos %>"/>';
        var MensajeHoraInicialRequerida = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeHoraInicialRequerida %>"/>';
        var MensajeHoraFinalRequerida = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeHoraFinalRequerida %>"/>';
        var MensajeRegistroCompleto = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeRegistroCompleto %>"/>';
        var MensajeCancelarTicket = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCancelarTicket %>"/>';
        var MensajeSalirSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSalirSinGuardar %>"/>';
        var MensajeSeleccionarRegistroHorasMuertas = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarRegistroHorasMuertas %>"/>';
        var MensajeModificarHorasMuertas = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeModificarHorasMuertas %>"/>';
        var MensajeSeleccionarProducto = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarProducto %>"/>';
        var MensajeSalirSinSeleccionarTicket = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSalirSinSeleccionarTicket %>"/>';
        var MensajeSalirSinSeleccionarProducto = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSalirSinSeleccionarProducto %>"/>';
        var MensajeSeleccionarTicket = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarTicket %>"/>';
        var ErrorConsultarTickets = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorConsultarTickets %>"/>';
        var MensajeTicketNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeTicketNoExiste %>"/>';
        var ErrorConsultarProductos = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorConsultarProductos %>"/>';
        var MensajeProductoNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeProductoNoExiste %>"/>';
        var MensajeConsultarProductosTodos = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeConsultarProductosTodos %>"/>';
        var ErrorInformacionAdicional = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorInformacionAdicional %>"/>';
        var MensajeSinPesajeMateriaPrima = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSinPesajeMateriaPrima %>"/>';
        var ErrorInformacionAdicionalTicket = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorInformacionAdicionalTicket %>"/>';
        var MensajeCapturarObservaciones = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCapturarObservaciones %>"/>';
        var MensajeGuardoExito = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeGuardoExito %>"/>';
        var MensajeDatosGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeDatosGuardar %>"/>';
        var MensajesRegistrosIncompletos = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajesRegistrosIncompletos %>"/>';
        var ErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorGuardar %>"/>';
        var MensajeCancelarPantalla = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCancelarPantalla %>"/>';
        var MensajeCausaRequerida = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCausaRequerida %>"/>';
        var MensajeSinDatosSupervisar = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSinDatosSupervisar %>"/>';
        var MensajeProductoRequerido = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeProductoRequerido %>"/>';
        var MensajeLitrosFinalMenor = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeLitrosFinalMenor %>"/>';
        var MensajeHorometroFinal = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeHorometroFinal %>"/>';
        var MensajeSesionRequerido = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSesionRequerido %>"/>';
        var MensajeCapturarLitrosFinal = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCapturarLitrosFinal %>"/>';
        var MensajeCapturarHorometroFinal = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCapturarHorometroFinal %>"/>';
        var MensajeCapturarHoraTicketFinal = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCapturarHoraTicketFinal %>"/>';







        //Columnas Grid
        var ColumnaTicket = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaTicket %>"/>';
        var ColumnaHoraTicket = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaHoraTicket %>"/>';
        var ColumnaProducto = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaProducto %>"/>';
        var ColumnaHorometroInicial = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaHorometroInicial %>"/>';
        var ColumnaHorometroFinal = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaHorometroFinal %>"/>';
        var ColumnaEspecificacionForraje = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaEspecificacionForraje %>"/>';
        var ColumnaLote = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaLote %>"/>';
        var ColumnaKilosNetos = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaKilosNetos %>"/>';
        var ColumnaConsumo = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaConsumo %>"/>';
        var ColumnaHumedad = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaHumedad %>"/>';
        var ColumnaHorasMuertas = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaHorasMuertas %>"/>';
        var ColumnaConteoPacas = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaConteoPacas %>"/>';
        var ColumnaCancelar = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaCancelar %>"/>';
        var ColumnaId = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaId %>"/>';
        var ColumnaDescripcion = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaDescripcion %>"/>';



        //
        var Seleccione = '<asp:Literal runat="server" Text="<%$ Resources:js.Seleccione %>"/>';
        var Si = '<asp:Literal runat="server" Text="<%$ Resources:js.Si %>"/>';
        var No = '<asp:Literal runat="server" Text="<%$ Resources:js.No %>"/>';
        var Aceptar = '<asp:Literal runat="server" Text="<%$ Resources:js.Aceptar %>"/>';
        var Supervisado = '<asp:Literal runat="server" Text="<%$ Resources:js.Supervisado %>"/>';
        var NoSupervisado = '<asp:Literal runat="server" Text="<%$ Resources:js.NoSupervisado %>"/>';
        var OK = '<asp:Literal runat="server" Text="<%$ Resources:js.OK %>"/>';
        
        
        
        
        
        
    </script>
</body>
</html>
