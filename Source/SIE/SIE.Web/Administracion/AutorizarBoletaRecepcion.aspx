<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutorizarBoletaRecepcion.aspx.cs" Inherits="SIE.Web.MateriaPrima.AutorizarBoletaRecepcion" %>
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
    <script src="../Scripts/AutorizarBoletaRecepcion.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
        var msgTicketNoEncuentra = '<asp:Literal runat="server" Text="<%$ Resources:msgTicketNoEncuentra.Text %>"/>';
        var msgFolioInvalido = '<asp:Literal runat="server" Text="<%$ Resources:msgFolioInvalido.Text %>"/>';
        var msgProveedorSinChofer = '<asp:Literal runat="server" Text="<%$ Resources:msgProveedorSinChofer.Text %>"/>';
        var msgProveedorSinPlacas = '<asp:Literal runat="server" Text="<%$ Resources:msgProveedorSinPlacas.Text %>"/>';
        var msgProveedorSinProducto = '<asp:Literal runat="server" Text="<%$ Resources:msgProveedorSinProducto.Text %>"/>';
        var msgProveedorSinContrato = '<asp:Literal runat="server" Text="<%$ Resources:msgProveedorSinContrato.Text %>"/>';
        var msgContratoSinDetalle = '<asp:Literal runat="server" Text="<%$ Resources:msgContratoSinDetalle.Text %>"/>';
        var msgSeleccionarFolio = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionarFolio.Text %>"/>';
        var msgMayorPorcentaje = '<asp:Literal runat="server" Text="<%$ Resources:msgMayorPorcentaje.Text %>"/>';
        var msgDatosBlanco = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosBlanco.Text %>"/>';
        var msgSinProveedores = '<asp:Literal runat="server" Text="<%$ Resources:msgSinProveedores.Text %>"/>';
        var msgSolicitaAutorizacion = '<asp:Literal runat="server" Text="<%$ Resources:msgSolicitaAutorizacion.Text %>"/>';
        var msgDatosGuardados = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardados.Text %>"/>';
        var msgSinFolios = '<asp:Literal runat="server" Text="<%$ Resources:msgSinFolios.Text %>"/>';
        var msgEntradaProductoAprobado = '<asp:Literal runat="server" Text="<%$ Resources:msgEntradaProductoAprobado.Text %>"/>';
        var msgEntradaProductoPendiente = '<asp:Literal runat="server" Text="<%$ Resources:msgEntradaProductoPendiente.Text %>"/>';
        var msgEntradaProductoRechazado = '<asp:Literal runat="server" Text="<%$ Resources:msgEntradaProductoRechazado.Text %>"/>';
        var msgFolioRechazado = '<asp:Literal runat="server" Text="<%$ Resources:msgFolioRechazado.Text %>"/>';
        var msgFolioAprobado = '<asp:Literal runat="server" Text="<%$ Resources:msgFolioAprobado.Text %>"/>';
        var msgFolioAutorizado = '<asp:Literal runat="server" Text="<%$ Resources:msgFolioAutorizado.Text %>"/>';
        var msgErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorGuardar.Text %>"/>';
        var msgFolioRechazoRequerido = '<asp:Literal runat="server" Text="<%$ Resources:msgFolioRechazoRequerido.Text %>"/>'; 
        var msgErrorRechazar = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorRechazar.Text %>"/>'; 
        var msgOperadorRequerido = '<asp:Literal runat="server" Text="<%$ Resources:msgOperadorRequerido.Text %>"/>'; 
        var msgFolioAutorizacionRequerido = '<asp:Literal runat="server" Text="<%$ Resources:msgFolioAutorizacionRequerido.Text %>"/>';
    </script>
    <style>
        .oculto {
            display: none !important;
        }
        textarea {
            resize: none;
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
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloBoletaRecepcion"></asp:Label></span>
                            </div>
                        </div>
                        <div class="portlet-body form">
							<ul class="breadcrumb">
				                <li>
					                <i class="icon-home"></i>
				                    <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="BoletaRecepcion_Home"/></a> 
					                <i class="icon-angle-right"></i>
				                </li>
                                <li>
					                <a href="AutorizarBoletaRecepcion.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="BoletaRecepcion_Title"></asp:Label></a> 
				                </li>
			                </ul>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border span5">
                                    <legend class="scheduler-border"><asp:Label ID="Label1" runat="server" meta:resourcekey="lblDatosGenerales"></asp:Label></legend>
                                    
                                    <div class="span12">
                                        <!-- Folio -->
                                        <div class="space10"></div>
                                        <div class="span12">
                                            <div class="span3">
                                                <span class="requerido">*</span>
                                                <asp:Label ID="lblFolio" runat="server" meta:resourcekey="lblTicket"></asp:Label> 
                                            </div>
                                            <div class="span8">
                                                <asp:TextBox ID="txtFolio" CssClass="span10" runat="server"></asp:TextBox>
                                                <a id="lblBuscar" href="#dlgBusquedaFolioBoletaRecepcion" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                            </div>
                                        </div>
                                        
                                        <!-- Ticket -->
                                        <div class="space15"></div>
                                        <div class="span12">
                                            <div class="span3">
                                                <asp:Label ID="lblTicket"  runat="server" meta:resourcekey="lblFolio"></asp:Label>  
                                            </div>
                                            <div class="span8">
                                                <asp:TextBox ID="txtTicket" CssClass="span12" runat="server" Enabled="False"></asp:TextBox>
                                            </div>
                                        </div>

                                        <!-- Fecha -->
                                        <div class="space15"></div>
                                        <div class="span12">
                                            <div class="span3">
                                                <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha"></asp:Label>
                                            </div>
                                            <div class="span8">
                                                <asp:TextBox ID="txtFecha" CssClass="span12" runat="server" Enabled="False"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <!-- Producto -->
                                        <div class="space15"></div>
                                        <div class="span12">
                                            <div class="span3">
                                                <asp:Label ID="lblProducto" runat="server" meta:resourcekey="lblProducto"></asp:Label>
                                            </div>
                                            <div class="span8">
                                                <asp:DropDownList ID="cmbProducto" CssClass="span12" runat="server" Enabled="False"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        <!-- Destino -->
                                        <div class="space15"></div>
                                        <div class="span12">
                                            <div class="span3">
                                                <asp:Label ID="lblDestino" runat="server" meta:resourcekey="lblDestino"></asp:Label>
                                            </div>
                                            <div class="span8">
                                                <asp:DropDownList ID="cmbDestino" CssClass="span12" runat="server" Enabled="False"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <!-- Contrato -->
                                        <div class="space15"></div>
                                        <div class="span12">
                                            <div class="span3">
                                                <asp:Label ID="lblContrato" runat="server" meta:resourcekey="lblContrato"></asp:Label>
                                            </div>
                                            <div class="span8">
                                                <asp:DropDownList ID="cmbContrato" CssClass="span12" runat="server" Enabled="False"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        
                                        <!-- Proveedor -->
                                        <div class="space15"></div>
                                        <div class="span12">
                                            <div class="span3">
                                                <asp:Label ID="lblProveedor" runat="server" meta:resourcekey="lblProveedor"></asp:Label>
                                            </div>
                                            <div class="span8">
                                                <asp:DropDownList ID="cmbProveedor" CssClass="span12" runat="server" Enabled="False"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        <!-- Placas -->
                                        <div class="space15"></div>
                                        <div class="span12">
                                            <div class="span3">
                                                <asp:Label ID="lblPlacas" runat="server" meta:resourcekey="lblPlacas"></asp:Label>
                                            </div>
                                            <div class="span8">
                                                <asp:DropDownList ID="cmbPlacas" CssClass="span12" runat="server" Enabled="False"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        <!-- Chofer -->
                                        <div class="space15"></div>
                                        <div class="span12">
                                            <div class="span3">
                                                <asp:Label ID="lblChofer" runat="server" meta:resourcekey="lblChofer"></asp:Label>
                                            </div>
                                            <div class="span8">
                                                <asp:DropDownList ID="cmbChofer" CssClass="span12" runat="server" Enabled="False"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <asp:TextBox ID="txtAprobado" CssClass="oculto" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtRechazado" CssClass="oculto" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtAutorizado" CssClass="oculto"  runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtPagoEnGanadera" CssClass ="oculto" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtOperadorID" CssClass="oculto" runat="server"></asp:TextBox>
                                    </div>
                                </fieldset>
                                <div class="span1"></div>
                                <fieldset class="scheduler-border span5">
                                    <legend class="scheduler-border"><asp:Label ID="lblIndicadoresCalidad" runat="server" meta:resourcekey="lblIndicadoresCalidad"></asp:Label></legend>
                                    
                                    <div class="span12 no-left-margin">
                                        <table id="tbIndicadoresTitulos" class="table table-striped table-advance table-hover">
                                            <thead>
                                                <tr>
                                                    <th class="span5 alineacionCentro" scope="col"><asp:Label ID="lblIndicadores" runat="server" meta:resourcekey="HeaderIndicadores"></asp:Label></th>
                                                    <th style="width: 0;display: none;"><asp:Label ID="lblMinimo" runat="server" meta:resourcekey="HeaderIndicadores"></asp:Label></th>
                                                    <th style="width: 0;display: none;"><asp:Label ID="lblMaximo" runat="server" meta:resourcekey="HeaderIndicadores"></asp:Label></th>
                                                    <th class="span3 alineacionCentro" scope="col"><asp:Label ID="lblIndicadorRecepcion" runat="server" meta:resourcekey="HeaderIndicadorRecepcion"></asp:Label></th>
                                                    <th class="span3 alineacionCentro" scope="col" ><asp:Label ID="lblIndicadorContrato" runat="server" meta:resourcekey="HeaderIndicadorContrato"></asp:Label></th>
                                                </tr>
                                            </thead>
                                        </table>
                                        <div id="contenedorIndicadores" style="height: 350px; overflow: auto;">
                                            <table id="tbIndicadores" class="table table-striped table-advance table-hover no-left-margin">
                                                <tbody></tbody>
                                            </table>
                                        </div>
                                    </div>
                                </fieldset>
                                <div class="span5 no-left-margin" id="dvJustificacion" style="display: none;">
                                    <div class="span12 no-left-margin">
                                        
                                          <span class="requerido" style="float:left">*</span>
                                          <asp:Label ID="lblJustificacion" CssClass="span3" runat="server" meta:resourcekey="lblJustificacion"></asp:Label>
                                           
                                    </div>
                                    <div class="span12 no-left-margin">
                                        <textarea id="txtJustificacion" maxlength="255" class="span12"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="space12"></div>
                            <div class="row-fluid">
                                <div class="span4 pull-right">
                                    <div style="text-align:right;">
                                        <a id="btnAutorizar" class="btn SuKarne">Autorizar</a>
                                        <a id="btnRechazar" class="btn SuKarne" href="#dlgRechazar" data-toggle="modal">Rechazar</a>
                                        <a id="btnCancelar" class="btn SuKarne" data-toggle="modal">Cancelar</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Dialogo de Busqueda Folio Boleta Recepcion -->
                        <div id="dlgBusquedaFolioBoletaRecepcion" class="modal hide fade" style="margin-top: -150px;height: 400px;width:600px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
							<div class="portlet box SuKarne2">
							    <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                        <span class="letra">
                                            <asp:Label ID="Label2" runat="server" meta:resourcekey="lblTituloBoletaRecepcion"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="portlet-body form" style="height: 400px;">
                                    <div class="modal-body">
								        <fieldset class="scheduler-border span12">
                                            <legend class="scheduler-border"><asp:Label ID="lblFiltro" runat="server" meta:resourcekey="lblFiltro"></asp:Label></legend>
                                            <div class="span12 no-left-margin">
                                                <asp:Label ID="lblFolioBuscar" runat="server" meta:resourcekey="lblFolio"></asp:Label>
                                                <asp:TextBox ID="txtFolioBuscar" CssClass="span5" runat="server"></asp:TextBox>
                                                <a id="btnBuscar" class="btn SuKarne" style="margin-left: 10px;" meta:resourcekey="btnBuscar">Buscar</a>
                                                <a id="btnAgregarBuscar" class="btn SuKarne" meta:resourcekey="btnAgregar">Agregar</a>
                                                <a id="btnCancelarBuscar" class="btn SuKarne" meta:resourcekey="btnCancelar">Cancelar</a>
                                            </div>
                                        </fieldset>
                                        <div class="space10"></div>
                                        <div class="span12 no-left-margin">
                                            <table id="tbBusquedaEncabezado" class="table table-striped table-advance table-hover span12 no-left-margin">
                                                <thead>
                                                    <tr>
                                                        <th class="span1 alineacionCentro" scope="col"></th>
                                                        <th class="span1 alineacionCentro" scope="col"><asp:Label ID="lblIDGrid" runat="server" meta:resourcekey="HeaderId"></asp:Label></th>
                                                        <th class="span1 alineacionCentro" scope="col"><asp:Label ID="lblFolioGrid" runat="server" meta:resourcekey="HeaderFolio"></asp:Label></th>
                                                        <th class="span2 alineacionCentro" scope="col" ><asp:Label ID="lblContratoGrid" runat="server" meta:resourcekey="HeaderContrato"></asp:Label></th>
                                                        <th class="span3 alineacionCentro" scope="col" ><asp:Label ID="lblProductoGrid" runat="server" meta:resourcekey="HeaderProducto"></asp:Label></th>
                                                        <th class="span3 alineacionCentro" scope="col" ><asp:Label ID="lblProveedorGrid" runat="server" meta:resourcekey="HeaderProveedor"></asp:Label></th>
                                                        <th class="span1 alineacionCentro" scope="col" ><asp:Label ID="lblEstatusGrid" runat="server" meta:resourcekey="HeaderEstatus"></asp:Label></th>
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

                        <!-- Dialogo de Cancelacion -->
                        <div id="dlgCancelar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
							<div class="modal-body">
								<asp:Label ID="lblMensajeDialogo" runat="server" meta:resourcekey="msgCancelar"></asp:Label>
							</div>
							<div class="modal-footer">
								<asp:Button runat="server" ID="btnDialogoSi" CssClass="btn SuKarne" meta:resourcekey="btnDialogoSi" data-dismiss="modal"/>
                                <asp:Button runat="server" ID="btnDialogoNo" CssClass="btn SuKarne" meta:resourcekey="btnDialogoNo" data-dismiss="modal"/>
							</div>
						</div>

                         <!-- Dialogo de Rechazar -->
                        <div id="dlgRechazar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
							<div class="modal-body">
								<asp:Label ID="lblMensajeRechazar" runat="server" meta:resourcekey="msgRechazar"></asp:Label>
							</div>
							<div class="modal-footer">
								<asp:Button runat="server" ID="btnRechazarSi" CssClass="btn SuKarne" meta:resourcekey="btnDialogoSi" data-dismiss="modal"/>
                                <asp:Button runat="server" ID="btnRechazarNo" CssClass="btn SuKarne" meta:resourcekey="btnDialogoNo" data-dismiss="modal"/>
							</div>
						</div>
                        
                        <!-- Dialogo de Cancelacion Buscar -->
                        <div id="dlgCancelarBuscar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
							<div class="modal-body">
								<asp:Label ID="Label3" runat="server" meta:resourcekey="msgCancelarBuscar"></asp:Label>
							</div>
							<div class="modal-footer">
								<asp:Button runat="server" ID="btnSiBuscar" CssClass="btn SuKarne" meta:resourcekey="btnDialogoSi" data-dismiss="modal"/>
                                <asp:Button runat="server" ID="btnNoBuscar" CssClass="btn SuKarne" meta:resourcekey="btnDialogoNo" data-dismiss="modal"/>
							</div>
						</div>
                    </div>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="hdnFolioEntrada"/>
        </form>
    </div>
</body>
</html>

