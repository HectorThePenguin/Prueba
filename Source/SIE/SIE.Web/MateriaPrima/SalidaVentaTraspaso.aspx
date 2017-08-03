<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalidaVentaTraspaso.aspx.cs" Inherits="SIE.Web.MateriaPrima.SalidaVentaTraspaso" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headEvaluacion" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
     <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/SalidaVentaTraspaso.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
        var msgIngresarFolio = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarFolio.Text %>"/>';
        var msgIngresarAlmacen = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarAlmacen.Text %>"/>';
        var msgIngresarProducto = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarProducto.Text %>"/>';
        var msgIngresarLote = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarLote.Text %>"/>';
        var msgIngresarPiezas = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarPiezas.Text %>"/>';
        var msgFolioInvalido = '<asp:Literal runat="server" Text="<%$ Resources:msgFolioInvalido.Text %>"/>';
        var msgCantidadPiezas = '<asp:Literal runat="server" Text="<%$ Resources:msgCantidadPiezas.Text %>"/>';
        var msgCancelar = '<asp:Literal runat="server" Text="<%$ Resources:msgCancelar.Text %>"/>';
        var msgGuardarExito = '<asp:Literal runat="server" Text="<%$ Resources:msgGuardarExito.Text %>"/>';
        var msgSesionExpirada = '<asp:Literal runat="server" Text="<%$ Resources:msgSesionExpirada.Text %>"/>';
        var msgValidaOtroFolio = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaOtroFolio %>"/>';
        var msgSeleccionarFolio = '<asp:Literal runat="server" Text="<%$Resources:msgSeleccionarFolio.Text %>"/>';
        var msgSinFolios = '<asp:Literal runat="server" Text="<%$Resources:msgSinFolios.Text %>"/>';
        var msgSalidaSurtida = '<asp:Literal runat="server" Text="<%$Resources:msgSalidaSurtida.Text %>"/>';
        var msgProductoSinLote = '<asp:Literal runat="server" Text="<%$Resources:msgProductoSinLote.Text %>"/>';
        var msgAlmacenSinProducto = '<asp:Literal runat="server" Text="<%$Resources:msgAlmacenSinProducto.Text %>"/>';
        var msgSinAlmacenMateriaPrima = '<asp:Literal runat="server" Text="<%$Resources:msgSinAlmacenMateriaPrima.Text %>"/>';
        var msgSinAlmacenPlantaAlimento = '<asp:Literal runat="server" Text="<%$Resources:msgSinAlmacenPlantaAlimento.Text %>"/>';
        var msgSinAlmacenBodegaTerceros = '<asp:Literal runat="server" Text="<%$Resources:msgSinAlmacenBodegaTerceros.Text %>"/>';
        var msgSinAlmacenBodegaExterna = '<asp:Literal runat="server" Text="<%$Resources:msgSinAlmacenBodegaExterna.Text %>"/>';

    </script>
    <style>
        .campoRequerido {
             color: red;
             font-weight: bold;
             padding: 0px !important;
             margin: 0px 0px !important;
             position: relative;
         }
         .etiqueta{
             color: black;
             font-weight: normal;
         }
         .folios-resultado{
             padding-left: 0px !important;
         }
    </style>
</head>
<body class="page-header-fixed">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
         <asp:HiddenField ID="txtSubFamiliaForraje" runat="server"/>
         <asp:HiddenField ID="txtClaveSalida" runat="server" Value="0"/>
         <asp:HiddenField ID="txtTipoMovimiento" runat="server"/>
            <asp:HiddenField ID="txtAlmacenMateriaPrima" runat="server"/>
            <asp:HiddenField ID="txtAlmacenPlantaAlimento" runat="server"/>
            <asp:HiddenField ID="txtAlmacenBodegaTercero" runat="server"/>
            <asp:HiddenField ID="txtAlmacenBodegaExterno" runat="server"/>
        <div class="container-fluid" />
            <div class="row-fluid">
                
                <div class="span12">
                    <div class="portlet box SuKarne2">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                <span class="letra">
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloSalidaVentaTraspaso"></asp:Label></span>
                            </div>
                        </div>
                        <div class="portlet-body form">
							<ul class="breadcrumb">
				                <li>
					                <i class="icon-home"></i>
				                    <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="SalidaVentaTraspaso_Home"/></a> 
					                <i class="icon-angle-right"></i>
				                </li>
                                <li>
					                <a href="SalidaVentaTraspaso.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="SalidaVentaTraspaso_Title"></asp:Label></a> 
				                </li>
			                </ul>
                            <div class="row-fluid">
                                <asp:Label ID="txtOrganizacion" runat="server" ></asp:Label>
                                <fieldset class="scheduler-border span12">
                                    <legend class="scheduler-border"><asp:Label ID="lblBusquedaFolio" runat="server" meta:resourcekey="lblBusquedaFolioSalida"></asp:Label></legend>
                                    <div class="space10"></div>
                                    <span class="campoRequerido">*<asp:Label ID="lblFolio" runat="server" CssClass="etiqueta" meta:resourcekey="lblFolio"></asp:Label></span>
                                    <input type="number" id="txtFolio" class="textoDerecha"/>
                                    <%--<asp:TextBox ID="txtFolio" runat="server"></asp:TextBox>--%>
                                    <a href="javascript:void(0);" id="lblBuscar" ><img src="../Images/find.png" width="26" height="26" alt="Buscar" /></a>
                                </fieldset>
                                <fieldset id="DatosSalida" class="scheduler-border span12">
                                    <legend class="scheduler-border"><asp:Label ID="lblDatosSalida" runat="server" meta:resourcekey="lblDatosFolioSalida"></asp:Label></legend>
                                    <div class="space10"></div>
                                    
                                    <div class="container-fluid">
                                        <div class="row-fluid">
                                            <div class="span4">
                                                <asp:Label ID="lblSalida" CssClass="span4" runat="server" meta:resourcekey="lblSalida"></asp:Label>
                                                <asp:TextBox ID="txtSalida" CssClass="span8" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        
                                            <div class="span4">
                                                <asp:Label ID="lblFecha" CssClass="span4" runat="server" meta:resourcekey="lblFecha"></asp:Label>
                                                <asp:TextBox ID="txtFecha" CssClass="span8" runat="server" Enabled="False"></asp:TextBox>
                                            </div>
                                        
                                            <div class="span4">
                                                <span class="campoRequerido span4">*<asp:Label ID="lblAlmacen" CssClass="etiqueta" runat="server" meta:resourcekey="lblAlmacen"></asp:Label></span>
                                                <asp:DropDownList ID="cmbAlmacen" CssClass="span8" runat="server"/>
                                            </div>
                                        </div>
                                        <div class="space10 span12"></div>
                                        <div class="row-fluid">
                                            <div class="span4">
                                                <span class="campoRequerido span4">*<asp:Label ID="lblProducto"  CssClass="etiqueta" runat="server"  meta:resourcekey="lblProducto"></asp:Label></span>
                                                <asp:DropDownList ID="cmbProducto" CssClass="span8" Enabled="false" runat="server" ></asp:DropDownList>
                                            </div>
                                        
                                            <div class="span4">
                                                <span class="campoRequerido span4">*<asp:Label ID="lblLote" CssClass="etiqueta" runat="server" meta:resourcekey="lblLote"></asp:Label></span>
                                                <asp:DropDownList ID="cmbLote" CssClass="span8 text-left" Enabled="false" runat="server"></asp:DropDownList>
                                            </div>
                                        
                                            <div class="span4">
                                                <asp:Label ID="lblKilogramos" CssClass="span4" runat="server" meta:resourcekey="lblKilogramos"></asp:Label>
                                                <asp:TextBox ID="txtKilogramos"  CssClass="span8 text-right" runat="server" Enabled="false"/>
                                            </div>
                                        </div>
                                        <div class="space10 span12"></div>
                                        <div class="row-fluid">
                                            <div class="span4">
                                                <asp:Label ID="lblPiezas" CssClass="span4" runat="server" meta:resourcekey="lblPiezas"></asp:Label>
                                                <input id="txtPiezas" type="number" class="span8 text-right" disabled="disabled" oninput="maxLengthCheck(this)" maxlength="9"/>
                                                <%--<asp:TextBox ID="txtPiezas" CssClass="span8 text-right" runat="server"  Enabled="False" MaxLength="9"></asp:TextBox>--%>
                                            </div>
                                        
                                            <div class="span8">
                                                <asp:Label ID="lblObservaciones" CssClass="span2" runat="server" meta:resourcekey="lblObservaciones"></asp:Label>
                                                <asp:TextBox ID="txtObservaciones" CssClass="span10" runat="server" TextMode="MultiLine" Rows="3" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="space15 span12"></div>
                                            <div class="span12">
                                                <br/>
                                                <div style="text-align:right;padding-right:20px;">
                                                    <a id="btnGuardar" class="btn SuKarne" ><asp:Literal runat="server" Text="<%$Resources:btnGuardar.Text%>"/></a>
                                                    <a id="btnCancelar"  class="btn SuKarne"> <asp:Literal runat="server" Text="<%$Resources:btnCancelar.Text%>"/></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <!-- Dialogo de Busqueda Folio  -->
                        <div id="dlgBusquedaFolio" class="modal hide fade" style="margin-top: -150px;height: 400px;width:600px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
							<div class="portlet box SuKarne2">
							    <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                        <span class="letra">
                                            <asp:Label ID="Label2" runat="server" meta:resourcekey="lblTituloBusquedaFolio"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="portlet-body form" style="height: 400px;">
                                    <div class="modal-body">
								        <fieldset class="scheduler-border span12">
                                            <legend class="scheduler-border"><asp:Label ID="lblFiltro" runat="server" meta:resourcekey="lblFiltro"></asp:Label></legend>
                                            <div class="span12 no-left-margin">
                                                <asp:Label ID="lblFolioBuscar" runat="server" meta:resourcekey="lblFolio"></asp:Label>
                                               <input type="number" id="txtFolioBuscar" class="span5"/>
                                                <%--<asp:TextBox ID="txtFolioBuscar" CssClass="span5" runat="server"></asp:TextBox>--%>
                                                <a id="btnBuscarFolio" class="btn SuKarne" style="margin-left: 10px;" ><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:btnBuscar.Text %>"/></a>
                                                <a id="btnAgregarBuscar" class="btn SuKarne" ><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:btnAgregar.Text %>"/></a>
                                                <a id="btnCancelarBuscar" class="btn SuKarne" ><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:btnCancelar.Text %>"/></a>
                                            </div>
                                        </fieldset>
                                        <div class="space10"></div>
                                        <div class="row-fluid">
                                            <div>
                                                <table id="encabezadoBusqueda" class="table table-striped table-advance table-hover no-left-margin">
                                                    <thead>
                                                        <tr>
                                                            <th class="span2 alineacionCentro" scope="col"><asp:Label ID="lblFolioGrid" runat="server" meta:resourcekey="HeaderFolio"></asp:Label></th>
                                                            <th class="span4 alineacionCentro" scope="col" ><asp:Label ID="lblProveedorGrid" runat="server" meta:resourcekey="HeaderSalida"></asp:Label></th>
                                                            <th class="span5 alineacionCentro" scope="col" ><asp:Label ID="lblEstatusGrid" runat="server" meta:resourcekey="HeaderDivision"></asp:Label></th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </div>
                                            <div id="Div2" style="height: 200px;overflow:auto;margin-top:-5px;" data-always-visible="1" data-rail-visible="0">
                                                <table id="tbBusqueda" class="table table-striped table-advance table-hover">
                                                    <tbody></tbody>
                                                </table>
                                            </div>
                                        </div>
                                        
                                        
							        </div>
                                </div>
                            </div>
						</div>
                    <!-- Dialogo de Cancelacion -->
                        <div id="dlgCancelar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
							<div class="modal-body">
								<asp:Label ID="lblMensajeDialogo" runat="server" meta:resourcekey="msgCancelar"></asp:Label>
							</div>
							<div class="modal-footer">
							    <a id="btnDialogoSi" meta:resourcekey="btnDialogoSi" class="btn SuKarne" data-dismiss="modal" ><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:btnDialogoSi.Text%>"/></a>
                                <a id="btnDialogoNo"  meta:resourcekey="btnDialogoNo" class="btn SuKarne" data-dismiss="modal"><asp:Literal ID="Literal2" runat="server" Text="<%$Resources:btnDialogoNo.Text%>"/></a>
							</div>
						</div>
                    
                        <!-- Dialogo de Cancelacion Buscar -->
                        <div id="dlgCancelarBuscar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
							<div class="modal-body">
								<asp:Label ID="Label3" runat="server" meta:resourcekey="msgCancelarBuscar"></asp:Label>
							</div>
							<div class="modal-footer">
								<a id="btnDialogoCancelarSi" class="btn SuKarne" data-dismiss="modal" ><asp:Literal ID="Literal6" runat="server" Text="<%$Resources:btnDialogoSi.Text%>"/></a>
                                <a id="btnDialogoCancelarNo" class="btn SuKarne" data-dismiss="modal"><asp:Literal ID="Literal7" runat="server" Text="<%$Resources:btnDialogoNo.Text%>"/></a>
							</div>
						</div>
                </div>
            </div>
        </form>
    </div>
    <script>
        $("#DatosSalida").hide();
    </script>
</body>
</html>
