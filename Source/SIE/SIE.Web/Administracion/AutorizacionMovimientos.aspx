<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutorizacionMovimientos.aspx.cs" Inherits="SIE.Web.Administracion.AutorizacionMovimientos" %>
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
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/AutorizacionMovimientos.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
        var lblFolio = '<asp:Literal runat="server" Text="<%$ Resources:lblFolio.Text %>"/>';
        var lblProducto = '<asp:Literal runat="server" Text="<%$ Resources:lblProducto.Text %>"/>';
        var lblAlmacen = '<asp:Literal runat="server" Text="<%$ Resources:lblAlmacen.Text %>"/>';
        var lblLote = '<asp:Literal runat="server" Text="<%$ Resources:lblLote.Text %>"/>';
        var lblCostoUnitario = '<asp:Literal runat="server" Text="<%$ Resources:lblCostoUnitario.Text %>"/>';
        var lblPrecioVenta = '<asp:Literal runat="server" Text="<%$ Resources:lblPrecioVenta.Text %>"/>';
        var lblLoteUtilizar = '<asp:Literal runat="server" Text="<%$ Resources:lblLoteUtilizar.Text %>"/>';
        var lblCantidadAjuste = '<asp:Literal runat="server" Text="<%$ Resources:lblCantidadAjuste.Text %>"/>';
        var lblPorcentajeAjuste = '<asp:Literal runat="server" Text="<%$ Resources:lblPorcentajeAjuste.Text %>"/>';
        var lblJustificacion = '<asp:Literal runat="server" Text="<%$ Resources:lblJustificacion.Text %>"/>'
        var lblAutRech = '<asp:Literal runat="server" Text="<%$ Resources:lblAutRech.Text %>"/>';
        var lblObservaciones = '<asp:Literal runat="server" Text="<%$ Resources:lblObservaciones.Text %>"/>';
        var msgSeleccionarMovimiento = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionarMovimiento.Text %>"/>';
        var msgSeleccionarOrganizacion = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionarOrganizacion.Text %>"/>';
        var msgSinTipoAutorizacion = '<asp:Literal runat="server" Text="<%$ Resources:msgSinTipoAutorizacion.Text %>"/>';
        var msgSinTipoGanadera = '<asp:Literal runat="server" Text="<%$ Resources:msgSinTipoGanadera.Text %>"/>';
        var msgSinMovimientosPendientes = '<asp:Literal runat="server" Text="<%$ Resources:msgSinMovimientosPendientes.Text %>"/>';
        var msgSinMovimientosPendientesSeleccionado = '<asp:Literal runat="server" Text="<%$ Resources:msgSinMovimientosPendientesSeleccionado.Text %>"/>';
        var msgSinOrganizacionValida = '<asp:Literal runat="server" Text="<%$ Resources:msgSinOrganizacionValida.Text %>"/>';
        var msgErrorMovimientos = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorMovimientos.Text %>"/>';
        var msgErrorPrecondiciones = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorPrecondiciones.Text %>"/>';
        var msgSinCambioEstatus = '<asp:Literal runat="server" Text="<%$ Resources:msgSinCambioEstatus.Text %>"/>';
        var msgSinObservacionesCapturadas = '<asp:Literal runat="server" Text="<%$ Resources:msgSinObservacionesCapturadas.Text %>"/>';
        var msgGuardadoConExito = '<asp:Literal runat="server" Text="<%$ Resources:msgGuardadoConExito.Text %>"/>';
        var msgGuardadoSinExito = '<asp:Literal runat="server" Text="<%$ Resources:msgGuardadoSinExito.Text %>"/>';
        var msgDialogoSi = '<asp:Literal runat="server" Text="<%$ Resources:msgDialogoSi.Text %>"/>';
        var msgDialogoNo = '<asp:Literal runat="server" Text="<%$ Resources:msgDialogoNo.Text %>"/>';       
        var msgCancelar = '<asp:Literal runat="server" Text="<%$ Resources:msgCancelar.Text %>"/>';
    </script>
    <style>
        .oculto {
            display: none !important;
        }
    </style>
</head>
<body class="page-header-fixed">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
        <div class="container-fluid" />
            <div class="row-fluid">
                <div class="span12">
                    <div class="portlet box SuKarne2">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                <span class="letra">
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloAutorizacionMovimientos"></asp:Label>
                                </span>
                            </div>
                        </div>
                        <div class="portlet-body form">
						    <ul class="breadcrumb">
				                <li>
					                <i class="icon-home"></i>
				                    <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="AutorizacionMovimientos_Home"/></a> 
					                <i class="icon-angle-right"></i>
				                </li>
                                <li>
					                <a href="AutorizacionMovimientos.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="AutorizacionMovimientos_Title"></asp:Label></a> 
				                </li>
			                </ul>
                            <div class="row-fluid">
                                <fieldset id="dlgAutorizacionMovimientos"  class="scheduler-border" disabled="disabled">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="Label1" runat="server" meta:resourcekey="lblDatosGenerales"></asp:Label>
                                    </legend>
                                    <div class="row-fluid">
                                        <div class="span7">
                                            <!-- Organizacion -->
                                            <div class="span2">
                                                <span class="requerido">*</span>
                                                <asp:Label ID="lblOrganizacion" runat="server" meta:resourcekey="lblOrganizacion"></asp:Label> 
                                            </div>
                                            <div class="span1 no-left-margin">
                                                <asp:TextBox ID="txtNumOrganizacion" CssClass="span12" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="span9">
                                                <asp:TextBox ID="txtOrganizacion" CssClass="span9" runat="server"></asp:TextBox>
                                                <a id="btnAyudaOrganizacion" href="#dlgBusquedaOrganizacion" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                            </div>

                                            <div class="space15"></div>
                                            <!-- Movimiento -->
                                            <div class="span2 no-left-margin">
                                                <span class="requerido">*</span>
                                                <asp:Label ID="lblMovimiento" runat="server" meta:resourcekey="lblMovimiento"></asp:Label>    
                                            </div>
                                            <asp:DropDownList ID="cmbMovimiento" runat="server"/>

                                            <asp:TextBox ID="txtPrecioVenta" CssClass="oculto" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtUsoLote" CssClass="oculto" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtAjusteInventario" CssClass="oculto"  runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtAutorizado" CssClass="oculto" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtRechazado" CssClass="oculto"  runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="textoDerecha">
                                            <button id="btnBuscar" type="button" class="btn SuKarne">
                                                <asp:Label ID="lblBuscar" runat="server" meta:resourcekey="lblBuscar"></asp:Label>
                                            </button>

                                            <button id="btnLimpiar" type="button" class="btn SuKarne">
                                                <asp:Label ID="lblLimpiar" runat="server"  meta:resourcekey="lblLimpiar"></asp:Label>
                                            </button>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <!-- Grid de Movimientos con solicitudes pendientes -->
                            <div id="GridSolicitudes" class="portlet-body form" style="display: none; height: 300px; margin-right: 0px;">
                                <div class="alineacionCentro">
                                    <table id="GridEncabezado" class="table table-striped table-advance table-hover">
                                        <thead></thead>
                                    </table>
                                    <div id="dvContenido" style="height: 300px; overflow: auto;">
                                        <table id="GridContenido" class="table table-striped table-advance table-hover no-left-margin">
                                            <tbody></tbody>
                                        </table>
                                    </div>
					            </div>
                            </div>
                            <div id="BotonesPie" class="row-fluid" style="display: none;">
                                <div class="textoDerecha">
                                    <button id="btnGuardar" type="button" class="btn SuKarne">
                                        <asp:Label ID="lblGuardar" runat="server" meta:resourcekey="lblGuardar"></asp:Label>
                                    </button>

                                    <button id="btnCancelar" type="button" class="btn SuKarne">
                                        <asp:Label ID="lblCancelar" runat="server"  meta:resourcekey="lblCancelar"></asp:Label>
                                    </button>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Dialogo de Cancelacion Buscar -->
                        <div id="dlgCancelarBuscar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
			                <div class="modal-body">
				                <asp:Label ID="Label3" runat="server" meta:resourcekey="msgCancelarBuscar"></asp:Label>
			                </div>
			                <div class="modal-footer">
				                <asp:Button runat="server" ID="btnSiBuscar" CssClass="btn SuKarne" meta:resourcekey="msgDialogoSi" data-dismiss="modal"/>
                                <asp:Button runat="server" ID="btnNoBuscar" CssClass="btn SuKarne" meta:resourcekey="msgDialogoNo" data-dismiss="modal"/>
			                </div>
		                </div>
                    </div>
                </div>  
            </div>
        </form>

        <!-- Dialogo de Busqueda Organización -->
        <div id="dlgBusquedaOrganizacion" class="modal hide fade" style="margin-top: -150px;height: 400px; width:650px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
			<div class="portlet box SuKarne2">
				<div class="portlet-title">
                    <div class="caption">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <span class="letra">
                            <asp:Label ID="Label2" runat="server" meta:resourcekey="BusquedaGanadera_Title"></asp:Label>
                        </span>
                    </div>
                </div>
                <div class="portlet-body form" style="height: 400px; margin-right: 0px;">
                    <div class="modal-body">
						<fieldset class="scheduler-border span6">
                            <legend class="scheduler-border"><asp:Label ID="lblFiltro" runat="server" meta:resourcekey="lblFiltro"></asp:Label></legend>
                            <div class="span6 no-left-margin">
                                <asp:Label ID="lblOrganizacionBusqueda" runat="server" meta:resourcekey="lblOrganizacion"></asp:Label>
                                <input type="text" id="txtOrganizacionBuscar" style="width: 230px;"/>
                                <a id="btnAyudaBuscarOrganizacion" class="btn SuKarne" style="margin-left: 10px;" meta:resourcekey="btnBuscar">Buscar</a>
                                <a id="btnAyudaAgregarBuscar" class="btn SuKarne" meta:resourcekey="btnAgregar">Agregar</a>
                                <a id="btnAyudaCancelarBuscar" class="btn SuKarne" meta:resourcekey="btnCancelar">Cancelar</a>
                            </div>
                        </fieldset>
                        <div class="alineacionCentro">
                            <table id="tbBusquedaEncabezado" class="table table-striped table-advance table-hover">
                                <thead>
                                    <tr>
                                        <th style="width: 20px;" class=" alineacionCentro" scope="col"></th>
                                        <th style="width: 100px;" class=" alineacionCentro" scope="col"><asp:Label ID="Label4" runat="server" meta:resourcekey="lblAyudaGridIdentificador"></asp:Label></th>
                                        <th style="width: auto;" class=" alineacionCentro" scope="col"><asp:Label ID="Label5" runat="server" meta:resourcekey="lblAyudaGridGanadera"></asp:Label></th>
                                    </tr>
                                </thead>
                            </table>
                            <div class="space5"></div>
                            <div id="dvBusqueda" style="height: 200px; overflow: auto;">
                                <table id="tbBusqueda" class="table table-striped table-advance table-hover no-left-margin">
                                    <tbody></tbody>
                                </table>
                            </div>
                        </div>
					</div>
                </div>
            </div>
		</div>
    </div>
</body>
</html>