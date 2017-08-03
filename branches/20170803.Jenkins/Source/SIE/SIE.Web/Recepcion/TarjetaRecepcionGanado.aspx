<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TarjetaRecepcionGanado.aspx.cs" Inherits="SIE.Web.Recepcion.TarjetaRecepcionGanado" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
    <link href="../assets/css/TarjetaRecepcionGanado.css" rel="stylesheet" />
    <script src="../Scripts/TarjetaRecepcionGanado.js"></script>
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
                                    <a href="../Recepcion/TarjetaRecepcionGanado.aspx">Tarjeta de Recepción de Ganado</a>
                                </li>
                            </ul>
                            <div class="row-fluid">
                                <div>
                                    <div>
                                        <asp:Label ID="lblfenviz" runat="server" CssClass="negritas" meta:resourcekey="lblfenvizResource1">FEVIZ-07-105</asp:Label>
                                    </div>
                                    <div class="span12">
                                        <div class="span6 alineacionIzquierda">
                                            <div class="span6">
                                                <asp:Label ID="Label1" runat="server" CssClass="campoRequerido">*</asp:Label>
                                                <asp:Label ID="lblFolioEntrada" runat="server" meta:resourcekey="lblFolioEntradaResource1"></asp:Label>
                                                <asp:TextBox ID="txtEditFolioEntrada" class="span6 textBoxTablas" runat="server" type="tel"></asp:TextBox>
                                            </div>


                                        </div>
                                        <div class="span6 alineacionIzquierda">
                                            <asp:Label ID="lblFecha" runat="server" class="span2" meta:resourcekey="lblFechaResource1"></asp:Label>
                                            <asp:TextBox ID="txtNoEditFecha" class="span3 textBoxTablas" runat="server" ViewStateMode="Enabled" meta:resourcekey="txtNoEditFechaResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row-fluid">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="lblDatosEntrada" runat="server" meta:resourcekey="lblDatosEntradaResource1"></asp:Label>
                                    </legend>
                                    <div class="span12">
                                        <div class="span6">
                                            <span class="span2">
                                                <asp:Label ID="lblOrigen" runat="server" meta:resourcekey="lblOrigenResource1"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtNoEditOrigen" class="span10 textBoxTablas" runat="server" ViewStateMode="Enabled" meta:resourcekey="txtNoEditOrigenResource1"></asp:TextBox>
                                            <div class="margenTopdiv">
                                                <span class="span2">
                                                    <asp:Label ID="lblOperador" runat="server" meta:resourcekey="lblOperadorResource1"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNoEditOperador" class="span10 textBoxTablas" runat="server" meta:resourcekey="txtNoEditOperadorResource1"></asp:TextBox>
                                            </div>
                                            <div class="margenTopdiv">
                                                <span class="span2">
                                                    <asp:Label ID="lblPlacas" runat="server" meta:resourcekey="lblPlacasResource1"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNoEditPlacas" class="span10 textBoxTablas" runat="server" meta:resourcekey="txtNoEditPlacasResource1"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="span6 alineacionIzquierda">
                                            <span class="span3">
                                                <asp:Label ID="lblHoraVigilancia" runat="server" meta:resourcekey="lblHoraVigilanciaResource1"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtNoEditVigilancia" class="span3 textBoxTablas" runat="server" ViewStateMode="Enabled" meta:resourcekey="txtNoEditVigilanciaResource1"></asp:TextBox>

                                            <div class="margenTopdiv">
                                                <span class="span3">
                                                    <asp:Label ID="lblCabezasOrigen" runat="server" meta:resourcekey="lblCabezasOrigenResource1"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNoEditCabezasOrigen" class="span3 textBoxTablas" runat="server" ViewStateMode="Enabled" meta:resourcekey="txtNoEditCabezasRecibidasResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="lblRecepcionGanado" runat="server" meta:resourcekey="lblRecepcionGanadoResource1"></asp:Label>
                                    </legend>
                                    <div class="span12">
                                        <div class="span3">
                                            <span class="campoRequerido">*</span>
                                            <asp:Label ID="lblCabezasDescargadas" runat="server" meta:resourcekey="lblCabezasDescargadasResource1"></asp:Label>
                                            <asp:TextBox ID="txtEditCabezasRecibidas" CssClass="span12 textBoxTablas" runat="server" ViewStateMode="Enabled" meta:resourcekey="txtEditCabezasRecibidasResource1" type="tel"></asp:TextBox>
                                        </div>
                                        <div class="span2">
                                            <span class="campoRequerido">*</span>
                                            <asp:Label ID="lblCorral" runat="server" meta:resourcekey="lblCorralResource1"></asp:Label>
                                            <asp:TextBox ID="txtCorral" CssClass="span12 textBoxTablas" runat="server" meta:resourcekey="txtEditCorralResource1"></asp:TextBox>
                                        </div>
                                        <div class="span2">
                                            <span class="campoRequerido">*</span>
                                            <asp:Label ID="LblCondicionesJaula" runat="server" meta:resourcekey="lblCondicionesJaulaResource1"></asp:Label>
                                            <asp:DropDownList ID="ddlCondicionesJaula" runat="server" CssClass="span12 textBoxTablas" ViewStateMode="Enabled" />
                                        </div>
                                        <div class="span2">
                                            <asp:Label ID="lblCabezasCorral" runat="server" meta:resourcekey="lblCabezasCorralResource1"></asp:Label>
                                            <asp:TextBox ID="txtNoEditCabezasCorral" CssClass="span12 textBoxTablas" runat="server" ViewStateMode="Enabled" meta:resourcekey="txtEditCabezasCorralResource1"></asp:TextBox>
                                        </div>
                                        <div class="span2">
                                            <asp:Label ID="lblFaltante" runat="server" meta:resourcekey="lblFaltanteResource1"></asp:Label>
                                            <asp:TextBox ID="txtNoEditFaltante" CssClass="span12 textBoxTablas" runat="server" ViewStateMode="Enabled" meta:resourcekey="txtEditCabezasCorralResource1"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="span3 margenTopdiv">
                                         <span >
                                            <%--<asp:CheckBox ID="rdnManejosinEstres"  runat="server" meta:resourcekey="rdnManejosinEstresResource1" />--%>
                                              <asp:Label ID="lblManejosinEstres" runat="server" meta:resourcekey="rdnManejosinEstresResource1"></asp:Label>
                                             <asp:CheckBox ID="rdnManejosinEstres"  runat="server" />
                                        </span>
                                    </div>

                                    <div id="divGridCondiciones" class="span11 table-container">
                                    </div>
                                </fieldset>
                            </div>
                            <div class="modal-footer textoDerecha">
                                <button id="btnGuardar" type="button" data-dismiss="modal" class="btn SuKarne">
                                    <asp:Label ID="lblGuardar" runat="server" meta:resourcekey="lblGuardarResource1">Guardar</asp:Label></button>

                                <button id="btnCancelar" type="button" data-dismiss="modal" class="btn SuKarne">
                                    <asp:Label ID="lblCancelar" runat="server" meta:resourcekey="lblCancelarResource1">Cancelar</asp:Label>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfEntradaGanadoID" runat="server" />
            <asp:HiddenField ID="hfEmbarqueID" runat="server" />
            <asp:HiddenField ID="hfCorralID" runat="server" />
            <asp:HiddenField ID="hfCapacidadCorral" runat="server" />
        </form>
    </div>
</body>
<script type="text/javascript">
    var Aceptar = '<asp:Literal runat="server" Text="<%$ Resources:js.Aceptar %>"/>';
    var MensajeInformacion = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeInformacion %>"/>';
    var MsgFolioEntradaNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgFolioEntradaNoExiste %>"/>';
    var MsgGuardadoConExito = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgGuardadoConExito %>"/>';
    var MsgSalirSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgSalirSinGuardar %>"/>';
    var MsgErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgErrorGuardar %>"/>';
    var MsgErrorConsultarCondiciones = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgErrorConsultarCondiciones %>"/>';
    var MsgCorralNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgCorralNoExiste %>"/>';
    var MsgTipoCorralNoRecepcion = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgTipoCorralNoRecepcion %>"/>';
    var MsgFolioEntradaRequerido = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgFolioEntradaRequerido %>"/>';
    var MsgCabezasRecibidasRequerido = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgCabezasRecibidasRequerido %>"/>';
    var MsgCorralRequerido = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgCorralRequerido %>"/>';
    var MsgCondicionesGanadoRequerido = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgCondicionesGanadoRequerido %>"/>';
    var MsgSinCondicionesGanado = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgSinCondicionesGanado %>"/>';
    var MsgRecepcionGanadoCapturada = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgRecepcionGanadoCapturada %>"/>';
    var MsgCabezasDiferentesCondiciones = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgCabezasDiferentesCondiciones %>"/>';
    var MsgCorralLoteActivo = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgCorralLoteActivo %>"/>';
    var MsgErrorAlConsultarFolioEntrada = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgErrorAlConsultarFolioEntrada %>"/>';
    var MsgDatosObligatorios = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgDatosObligatorios %>"/>';
    var MsgSeleccionarCondicionJaula = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgSeleccionarCondicionJaula %>"/>';
    var MsgCapturarConteo = '<asp:Literal runat="server" Text="<%$ Resources:js.MsgCapturarConteo %>"/>';
</script>
<%--<script src="../Scripts/TarjetaRecepcionGanado.js"></script>--%>
</html>
 