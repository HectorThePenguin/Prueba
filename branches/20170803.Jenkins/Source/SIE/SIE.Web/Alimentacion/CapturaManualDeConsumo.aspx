<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapturaManualDeConsumo.aspx.cs" Inherits="SIE.Web.Alimentacion.CapturaManualDeConsumo" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>
    
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/CapturaManualDeConsumo.js"></script>
    <link href="../assets/css/CapturaManualDeConsumo.css" rel="stylesheet" />

    <script type="text/javascript">
        var mensajeErrorSession = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorSession %>"/>';
        var mensajeNoHayFormulas = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoHayFormulas %>"/>';
        var mensajeNoExistenCorrales = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoExistenCorrales %>"/>';
        var mensajeEsNecesarioSeleccionarCorral = '<asp:Literal runat="server" Text="<%$ Resources:mensajeEsNecesarioSeleccionarCorral %>"/>';
        var mensajeNoHayTipoCorral = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoHayTipoCorral %>"/>';
        var mensajeCorralNoExite = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCorralNoExite %>"/>';
        var mensajeCorralNoCorrespondeAProduccion = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCorralNoCorrespondeAProduccion %>"/>';
        var mensajeCancelar = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCancelar %>"/>';
        var mensajeNoHaCapturadoCorrales = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoHaCapturadoCorrales %>"/>';
        var mensajeExito = '<asp:Literal runat="server" Text="<%$ Resources:mensajeExito %>"/>';
        var mensajeDatosEnBlanco = '<asp:Literal runat="server" Text="<%$ Resources:mensajeDatosEnBlanco %>"/>';
        var mensajeCapturarCorral = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCapturarCorral %>"/>';
        var mensajeCorralYaEstaEnLaLista = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCorralYaEstaEnLaLista %>"/>';
        var mensajeErrorAlConsultarCabezas = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarCabezas %>"/>';
        var mensajeErrorGenerarConsumo = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorGenerarConsumo %>"/>';
        var mensajeCorralNoTieneLoteActivo = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCorralNoTieneLoteActivo %>"/>';
        var mensajeNoExistenCorralesDeEnfermeria = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoExistenCorralesDeEnfermeria %>"/>';
        var mensajeErrorAlConsultarInventario = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarInventario %>"/>';
        var mensajeInsuficienteInventario = '<asp:Literal runat="server" Text="<%$ Resources:mensajeInsuficienteInventario %>"/>';
        var mensajeExistestenciaInsuficiente = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoHayExistenciaSuficiente %>"/>';
        var sinCorralSeleccionado = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarCorrales %>"/>';
        var mensajeNoHayLotes = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoHayLotes %>"/>';
        var mensajeErrorAlConsultarLotes = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarLotes %>"/>';
        var mensajeSeleccionarLote = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarLote %>"/>';
        var mensajeSeleccionarFormula = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarFormula %>"/>';
        var mensajeIngresarKilogramosServidos = '<asp:Literal runat="server" Text="<%$ Resources:mensajeIngresarKilogramosServidos %>"/>';
        var mensajeSeleccionarTipoCorral = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarTipoCorral %>"/>';
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row-fluid contenedor-capturamanual">
            <div class="span12">
                <div class="portlet box SuKarne2">
                    <div class="portlet-title">
                        <div class="row-fluid caption">
                            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                            <span>
                                <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTitulo"></asp:Label>
                            </span>
                        </div>
                    </div>

                    <div class="portlet-body form">
                        <div class="row-fluid">
                            <ul class="breadcrumb">
                                <li>
                                    <i class="icon-home"></i>
                                    <a href="../Principal.aspx">Home</a>
                                    <i class="icon-angle-right"></i>
                                </li>
                                <li>
                                    <a href="../Alimentacion/CapturaManualDeConsumo.aspx">
                                        <asp:Label ID="Label1" runat="server" meta:resourcekey="lblTitulo"></asp:Label>
                                    </a>
                                </li>
                            </ul>
                        </div>
                        
                        <div class="row-fluid">
                            <div class="span11 caja">
                                <div class="space10"></div>
                                <div class="span4">
                                    <div class="row-fluid">
                                        <div class="span6"><span class="">&nbsp;&nbsp;</span><asp:Label ID="Label4" runat="server" meta:resourcekey="lblFecha"></asp:Label></div>
                                        <div class="span6"><asp:TextBox ID="txtFecha" runat="server" CssClass="control span12"></asp:TextBox></div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="span6"><span class="requerido">* </span><asp:Label ID="Label5" runat="server" meta:resourcekey="lblKilogramosServidos"></asp:Label></div>
                                        <div class="span6"><asp:TextBox ID="txtKilogramos" runat="server" CssClass="control span12" type="tel"></asp:TextBox></div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="span6"><span class="requerido">* </span><asp:Label ID="Label6" runat="server" meta:resourcekey="lblFormula"></asp:Label></div>
                                        <div class="span6"><asp:DropDownList ID="cmbFormulas" CssClass="control span12" runat="server"></asp:DropDownList></div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="span6"><span class="requerido">* </span><asp:Label ID="Label2" runat="server" meta:resourcekey="lblLote"></asp:Label></div>
                                        <div class="span6"><asp:DropDownList ID="cmbLotes" CssClass="control span12" runat="server"></asp:DropDownList></div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="span6"><span class="requerido">* </span><asp:Label ID="Label7" runat="server" meta:resourcekey="lblTipoCorral"></asp:Label></div>
                                        <div class="span6"><asp:DropDownList ID="cmbTipoCorral" CssClass="control span12" runat="server"></asp:DropDownList></div>
                                    </div>
                                    <div class="row-fluid">
                                        <fieldset id="groupCorral" class="scheduler-border">
                                           <legend class="scheduler-border">
                                               <asp:Label ID="Label9" runat="server" meta:resourcekey="lblGroupBox"></asp:Label>
                                           </legend>
                                            <div class="span12">
                                                <div class="span4"><asp:Label ID="Label8" runat="server" meta:resourcekey="lblCorral"></asp:Label></div>
                                                <div class="span6"><asp:TextBox ID="txtCorralProduccion" runat="server" CssClass="control span12"></asp:TextBox></div>
                                                <div class="span1">
                                                    <button type="button" id="btnAgregarCorral" class="btn letra SuKarne span12">
                                                       <i class="icon-plus"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            
                                <div class="span7">
                                    <div class="span5 unTercioPantalla">
                                        <div class="space10"></div>
                                        <div class="span12"><asp:Label ID="Label11" runat="server" Text="Label" meta:resourcekey="lblCorrales"></asp:Label></div>
                                        <div class="contenedor-lista span12">
                                            <ul id="listaSeleccion" class="lista">
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="span1 botones">
                                        <div class="spance12"></div>
                                        <div class="spance12"></div>
                                        <div class="spance12"></div>
                                        <button type="button" id="btnSoloChecadosADerecha" class="btn letra SuKarne span12">
                                           <i class="icon-chevron-right"></i>
                                        </button>
                                        <button type="button" id="btnTodosADerecha" class="btn letra SuKarne span12">
                                            <i class="icon-fast-forward"></i>
                                        </button>
                                        <div class="spance12"></div>
                                        <button type="button" id="btnSoloChecadosAIzquierda" class="btn letra SuKarne span12">
                                            <i class="icon-chevron-left"></i>
                                        </button>
                                        <button type="button" id="btnTodosAIzquierda" class="btn letra SuKarne span12">
                                            <i class="icon-fast-backward"></i>
                                        </button>
                                    </div>
                                    <div class="span5 unTercioPantalla">
                                        <div class="space10"></div>
                                        <div class="span12"><asp:Label ID="Label10" runat="server" Text="Label" meta:resourcekey="lblCorralesParaCalculo"></asp:Label></div>
                                        <div class="contenedor-lista span12">
                                            <ul id="listaSeleccionados" class="lista">
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="seccionbotones span10">
                                <div class="pull-right">
                                    <button type="button" id="btnGuardar" data-toggle="modal" class="btn letra SuKarne">
                                        <asp:Label ID="Label12" runat="server" meta:resourcekey="btnGuardar"></asp:Label>
                                    </button>
                                    <button type="button" id="btnCancelar" data-toggle="modal" class="btn letra SuKarne">
                                        <asp:Label ID="Label13" runat="server" meta:resourcekey="btnCancelar"></asp:Label>
                                    </button>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                    <asp:HiddenField ID="idUsuario" runat="server" />
                </div>
            </div>
        </div>
    </form>
    
    <div id="responsive" class="modal container hide fade" tabindex="-1" data-width="750" data-backdrop="static">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="caption">
                        <asp:Label ID="lblBusquedaFolio" runat="server" meta:resourcekey="lblTituloProgress"></asp:Label>
                    </div>
                </div>
                <div class="portlet-body">
                    <asp:Label ID="Label3" runat="server" meta:resourcekey="lblAvance"></asp:Label>

                    <div class="progress progress-striped active">
                      <div class="bar" style="width: 0%;"></div>
                    </div>
                    <div id="labelPorcentaje" align="center"></div>
                </div>
            </div>
        </div>

</body>
</html>
