<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoletaRecepcionForraje.aspx.cs" Inherits="SIE.Web.Calidad.BoletaRecepcionForraje"%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml"> 
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
     <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Styles.Render("~/bundles/CalificacionGanadoEstilo") %>
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/BoletaRecepcionForraje.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        function ActualizarTicket(selectObj) {
            __doPostBack('<%= Page.ClientID %>', selectObj.name);
        }

        javascript: window.history.forward(1);
        var msgTicketNoExiste = '<asp:Literal runat="server" Text="<%$Resources:msgTicketNoExiste.Text%>"/>';
        var msgCancelar = '<asp:Literal runat="server" Text="<%$Resources:msgCancelar.Text %>"/>';
        var msgCancelarBuscar = '<asp:Literal runat="server" Text="<%$Resources:msgCancelarBuscar.Text %>"/>';
        var msgGuardarExito = '<asp:Literal runat="server" Text="<%$Resources:msgGuardarExito.Text %>"/>';
        var msgDatosObligatorios = '<asp:Literal runat="server" Text="<%$Resources:msgDatosObligatorios.Text %>"/>';
        var msgFolioInvalido = '<asp:Literal runat="server" Text="<%$Resources:msgFolioInvalido.Text %>"/>';
        var msgSinFolios = '<asp:Literal runat="server" Text="<%$Resources:msgSinFolios.Text %>"/>';
        var msgForrajeRechazado = '<asp:Literal runat="server" Text="<%$Resources:msgForrajeRechazado.Text %>"/>';
        var msgSeleccionarFolio = '<asp:Literal runat="server" Text="<%$Resources:msgSeleccionarFolio.Text %>"/>';
        var msgEntradaPendiente = '<asp:Literal runat="server" Text="<%$Resources:msgEntradaProductoPendiente.Text %>"/>';
        var msgEntradaAprobado = '<asp:Literal runat="server" Text="<%$Resources:msgEntradaProductoAprobado.Text %>"/>';
        var msgEntradaRechazado = '<asp:Literal runat="server" Text="<%$Resources:msgEntradaProductoRechazado.Text %>"/>';
        var msgErrorLimiteMuestras = '<asp:Literal runat="server" Text="<%$Resources:msgErrorLimiteMuestras.Text %>"/>';
        var msgErrorInterno = '<asp:Literal runat="server" Text="<%$Resources:msgErrorInterno.Text %>"/>';
        var msgSesionExpirada = '<asp:Literal runat="server" Text="<%$Resources:msgSesionExpirada.Text %>"/>';
        var msgProveedorSinChofer = '<asp:Literal runat="server" Text="<%$Resources:msgProveedorSinChofer.Text %>"/>';
        var msgProveedorSinContrato = '<asp:Literal runat="server" Text="<%$Resources:msgProveedorSinContrato.Text %>"/>';
        var msgProveedorSinPlacas = '<asp:Literal runat="server" Text="<%$Resources:msgProveedorSinPlacas.Text %>"/>';
        var msgProveedorSinProducto = '<asp:Literal runat="server" Text="<%$Resources:msgProveedorSinProducto.Text %>"/>';
        var msgNoValidoMuestras = '<asp:Literal runat="server" Text="<%$Resources:msgNoValidoMuestras.Text %>"/>';
        var msgForrajePendienteAutorizar = '<asp:Literal runat="server" Text="<%$Resources:msgForrajePendienteAutorizar.Text %>"/>';
        var msgNecesitaAutorizacion = '<asp:Literal runat="server" Text="<%$Resources:msgNecesitaAutorizacion.Text %>"/>';
        var msgMayorPorcentaje = '<asp:Literal runat="server" Text="<%$ Resources:msgMayorPorcentaje.Text %>"/>';
        var msgMensajeFolioYaRegistrado = '<asp:Literal runat="server" Text="<%$ Resources:msgMensajeFolioYaRegistrado.Text %>"/>';
        var msgForrajePorAutorizar = '<asp:Literal runat="server" Text="<%$ Resources:msgForrajePorAutorizar.Text %>"/>';
		var msgTicketAutorizado = '<asp:Literal runat="server" Text="<%$ Resources:msgTicketAutorizado.Text %>"/>';
        var msgTicketRechazado = '<asp:Literal runat="server" Text="<%$ Resources:msgTicketRechazado.Text %>"/>';
        var msgTicketAprobado = '<asp:Literal runat="server" Text="<%$ Resources:msgTicketAprobado.Text %>"/>';
        var msgTicketPendienteAutorizar = '<asp:Literal runat="server" Text="<%$ Resources:msgTicketPendienteAutorizar.Text %>"/>'; 
        var msgFechaFormatoIncorrecto = '<asp:Literal runat="server" Text="<%$ Resources:msgFechaFormatoIncorrecto.Text %>"/>';
        var msgProductoErrorRangos = '<asp:Literal runat="server" Text="<%$ Resources:msgProductoErrorRangos.Text %>"/>';
        var msgFolioFechaSalida = '<asp:Literal runat="server" Text="<%$ Resources:msgFolioFechaSalida.Text %>"/>';

        
    </script>
    <style>
        td {
            padding-left: 2px !important;
            vertical-align: middle !important;
        }
         .indicadores{
             min-height: 20px;
             padding-left: 20px;
         }
         .campoRequerido {
             color: red;
             font-weight: bold;
             padding: 0px !important;
             margin: 0px 0px !important;
             position: relative;
         }
         .etiqueta-forraje {
             color: black;
             font-weight: normal;
         }
         textarea {
            resize: none;
        }
        .muestra1 {
            text-align: right;
        }
        .muestra2 {
            text-align: right;
        }
        .oculto {
            display: none !important;
        }

    </style>
</head>
<body class="page-header-fixed">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
        <div id="skm_LockPane" class="LockOff">
            Por favor espere...
        </div>
        <div class="container-fluid" />
            <div class="row-fluid">
                <div class="span12">
                    <div class="portlet box SuKarne2">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                <span class="letra">
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloBoletaRecepcionForraje"></asp:Label></span>
                            </div>
                        </div>
                        <div class="portlet-body form">
							<ul class="breadcrumb">
				                <li>
					                <i class="icon-home"></i>
				                    <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="BoletaRecepcionForraje_Home"/></a> 
					                <i class="icon-angle-right"></i>
				                </li>
                                <li>
					                <a href="BoletaRecepcionForraje.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="BoletaRecepcionForraje_Title"></asp:Label></a> 
				                </li>
			                </ul>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border span12">
                                    <legend class="scheduler-border"><asp:Label ID="Label1" runat="server" meta:resourcekey="lblDatosGenerales"></asp:Label></legend>
                                    
                                    <div class="span12">
                                        <div class="span4">
                                            <!-- Ticket -->
                                            <div class="space15"></div>
                                            <span class="campoRequerido span3">*<asp:Label ID="lblTicket" CssClass="etiqueta-forraje" runat="server" meta:resourcekey="lblFolio"></asp:Label></span>
                                            <asp:TextBox ID="txtTicket" CssClass="span7" runat="server"  AutoPostBack="True"  OnTextChanged="txtTicket_TextChanged"  type="tel"></asp:TextBox>

                                            <!-- Forraje -->
                                            <div class="space15"></div>
                                            <span class="campoRequerido span3">*<asp:Label ID="lblForraje" CssClass="etiqueta-forraje" runat="server" meta:resourcekey="lblForraje"></asp:Label></span>
                                            <asp:DropDownList ID="cmbForrajes" CssClass="span7"  runat="server" Enabled="False"></asp:DropDownList>
                                        
                                            <!-- Contrato -->
                                            <div class="space15"></div>
                                            <span class="campoRequerido span3">*<asp:Label ID="lblContrato" CssClass="etiqueta-forraje" runat="server" meta:resourcekey="lblContrato"></asp:Label></span>
                                            <asp:DropDownList ID="cmbContrato" CssClass="span7"  runat="server" Enabled="False"></asp:DropDownList>
                                        
                                        </div>
                                        <div class="span4">
                                                <!-- Proveedor -->
                                                <div class="space15"></div>
                                                <asp:Label ID="lblProveedor"  CssClass="span3" runat="server" meta:resourcekey="lblProveedor"></asp:Label>
                                                <asp:DropDownList ID="cmbProveedor" CssClass="span7" runat="server" Enabled="False"></asp:DropDownList>
                                                
                                                <!-- Chofer -->
                                                <div class="space15"></div>
                                                <asp:Label ID="lblChofer" CssClass="span3" runat="server" meta:resourcekey="lblChofer"></asp:Label>
                                                <asp:DropDownList ID="cmbChofer" CssClass="span7" runat="server" Enabled="False"></asp:DropDownList>
                                    
                                                <!-- Placas -->
                                                <div class="space15"></div>
                                                <asp:Label ID="lblPlacas" CssClass="span3" runat="server" meta:resourcekey="lblPlacasCamion"></asp:Label>
                                                <asp:DropDownList ID="cmbPlacas" CssClass="span7" runat="server" Enabled="False"></asp:DropDownList>
                                                
                                        </div>
                                        <div class="span4">
                                            <!-- Folio -->
                                            <div>
                                                <div class="space15"></div>
                                                <span class="campoRequerido span3"><asp:Label ID="lblFolio" CssClass="etiqueta-forraje" runat="server" meta:resourcekey="lblTicket"></asp:Label></span>
                                                <input id="txtFolio"  type="text" />
                                                <a href="javascript:void(0);" id="lblBuscar" ><img src="../Images/find.png" width="26" height="26" alt="Buscar" /></a>
                                            </div>
                                            

                                            <!-- Fecha -->
                                            <div>
                                                <div class="space15"></div>
                                                <span class="campoRequerido span3"><asp:Label ID="lblFecha" CssClass="etiqueta-forraje" runat="server" meta:resourcekey="lblFecha"></asp:Label></span>
                                                <asp:TextBox ID="txtFecha" CssClass="span7" runat="server" Enabled="False"></asp:TextBox>
                                            </div>
                                        
                                            <!-- Destino -->
                                            <div>
                                                <div class="space15"></div>
                                                <span class="campoRequerido span3">*<asp:Label ID="lblDestino" CssClass="etiqueta-forraje" runat="server" meta:resourcekey="lblDestino"></asp:Label></span>
                                                <asp:DropDownList ID="cmbDestino" CssClass="span7"  runat="server"></asp:DropDownList>
                                            </div>

                                        </div>
                                        <asp:HiddenField ID="txtFolioEntrada" Value="0" runat="server" />
                                        <asp:HiddenField ID="txtIndicador" Value="0" runat="server" />
                                        <asp:HiddenField ID="txtTipoContrato" Value="0" runat="server" />
                                        <asp:HiddenField ID="txtEntradaProductoId" Value="0" runat="server" />
                                        <asp:HiddenField ID="txtAutorizado" Value="0" runat="server" />
                                        <asp:HiddenField ID="txtRechazado" Value="0" runat="server" />
                                        <asp:HiddenField ID="txtAprobado" Value="0" runat="server" />
                                        <asp:HiddenField ID="txtPendienteAutorizar" Value="0" runat="server" />
                                        <asp:HiddenField ID="txtTotalMuestras" Value="0" runat="server" />
                                        
                                    </div>
                                </fieldset>
                                <div class="span1"></div>

                            </div>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border span12">
                                    <legend class="scheduler-border"><asp:Label ID="Label4" runat="server" meta:resourcekey="lblDatosOrigen"></asp:Label></legend>
                                    
                                    <div class="span12">
                                        <div class="span4">
                                            <!-- Folio origen -->
                                            <div class="space15"></div>
                                            <span class="campoRequerido span3">*<asp:Label ID="lblFolioOrigen" CssClass="etiqueta-forraje" runat="server" meta:resourcekey="lblFolioOrigen"></asp:Label></span>
                                            <asp:TextBox ID="txtFolioOrigen" CssClass="span7" runat="server" AutoPostBack="False"   type="tel"></asp:TextBox>
                                            
                                            
                                        </div>
                                        <div class="span4">
                                               <!-- Fecha origen -->
                                            
                                                <div class="space15"></div>
                                                <span class="campoRequerido span3">*<asp:Label ID="lblFechaOrigen" CssClass="etiqueta-forraje" runat="server" meta:resourcekey="lblFechaOrigen"></asp:Label></span>
                                                <asp:TextBox ID="txtFechaOrigen" CssClass="span7"  placeholder="dd/mm/aaaa"  AutoPostBack="False"  runat="server"></asp:TextBox>
                                        </div>
                                        <div class="span4">
                                            <!-- humedad origen -->
                                            <div class="space15"></div>
                                            <span class="campoRequerido span3">*<asp:Label ID="lblHumedadOrigen" CssClass="etiqueta-forraje" runat="server" meta:resourcekey="lblHumedadOrigen"></asp:Label></span>
                                            <asp:TextBox ID="txtHumedadOrigen" CssClass="span7" runat="server" AutoPostBack="False"   type="tel"></asp:TextBox>

                                        </div>
                                        <asp:TextBox ID="txtCalidadOrigenID" CssClass="oculto" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtPorcentajePermitido" CssClass="oculto" runat="server"></asp:TextBox>
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />  CssClass="oculto"
                                        <asp:HiddenField ID="HiddenField2" Value="0" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" Value="0" runat="server" />
                                        <asp:HiddenField ID="HiddenField4" Value="0" runat="server" />--%>
                                        
                                    </div>
                                </fieldset>
                                <div class="span1"></div>

                            </div>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border span12">
                                    <legend class="scheduler-border"><asp:Label ID="lblMuestraHumedades" runat="server" meta:resourcekey="lblMuestraHumedades"></asp:Label></legend>
                                    
                                    <div class="row-fluid"></div>
                                    <div class="span6 no-left-margin">
                                        <table id="tbMuestras" class="table table-striped table-advance table-hover span12">
                                            <thead>
                                                <tr>
                                                    <th class="span6 alineacionCentro" scope="col"><asp:Label ID="lblMuestra1" runat="server" meta:resourcekey="HeaderMuestra1"></asp:Label></th>
                                                    <th class="span6 alineacionCentro" scope="col" ><asp:Label ID="lblMuestra2" runat="server" meta:resourcekey="HeaderMuestra2"></asp:Label></th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>

                                    <div class="span6">
                                        <div>
                                            <div class="space15"></div>
                                            <asp:Label ID="lblPromedioHumedad" CssClass="span3 no-left-margin" runat="server" meta:resourcekey="lblPromedioHumedad"></asp:Label>
                                            <asp:TextBox ID="txtPromedioHumedad" CssClass="span6" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdPorcentajePermitidoMin" Value="0" runat="server" />
                                            <asp:HiddenField ID="hdPorcentajePermitidoMax" Value="0" runat="server" />

                                        </div>
                                        <div>
                                            <div class="space15"></div>
                                            <asp:Label ID="lblDescuento" CssClass="span3 no-left-margin" runat="server" meta:resourcekey="lblDescuento"></asp:Label>
                                            <asp:TextBox ID="txtDescuento" CssClass="span6" runat="server" Enabled="False"></asp:TextBox>
                                        </div>                                        
                                          <div>
                                              <div class="space15"></div>
                                                <span class="campoRequerido span3 no-left-margin">*<asp:Label ID="lblObservaciones" CssClass="etiqueta-forraje" runat="server" meta:resourcekey="lblObservaciones"></asp:Label></span>
                                                <asp:TextBox ID="txtObservaciones" TextMode="multiline"  Columns="70" Rows="5" CssClass="span6" runat="server"></asp:TextBox>
                                          </div>
                                          <div >
                                              <div class="space15"></div>
                                                <asp:Label ID="lblAnalista" CssClass="span3 no-left-margin" runat="server" meta:resourcekey="lblAnalista"></asp:Label>
                                                <asp:TextBox ID="txtAnalista"  CssClass="span6" Enabled="false" runat="server"></asp:TextBox>
                                          </div>
                                          <div >
                                              <div class="space15"></div>
                                                <asp:Label ID="lblAutorizo" CssClass="span3 no-left-margin" runat="server" meta:resourcekey="lblAutorizo"></asp:Label>
                                                <asp:TextBox ID="txtAoturizadoPor"  CssClass="span6" Enabled="false" runat="server"></asp:TextBox>
                                          </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="row-fluid">
                                <div class="span4 pull-right">
                                    <div style="text-align:right;">
                                        <a id="btnGuardar" class="btn SuKarne">Guardar</a>
                                        <a id="btnCancelar" class="btn SuKarne" href="#dlgCancelar" data-toggle="modal">Cancelar</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Dialogo de Busqueda Folio Boleta Muestas Capturadas -->
                        <div id="dlgBusquedaFolio" class="modal hide fade" style="margin-top: -150px;height: 400px;width:600px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
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
                                                <asp:Label ID="lblFolioBuscar" runat="server" meta:resourcekey="lblTicket"></asp:Label>
                                                <asp:TextBox ID="txtFolioBuscar" CssClass="span5" runat="server"  type="tel"></asp:TextBox>
                                                <a id="btnBuscar" class="btn SuKarne" style="margin-left: 10px;" meta:resourcekey="btnBuscar">Buscar</a>
                                                <a id="btnAgregarBuscar" class="btn SuKarne" meta:resourcekey="btnAgregar">Agregar</a>
                                                <a id="btnCancelarBuscar" class="btn SuKarne" meta:resourcekey="btnCancelar">Cancelar</a>
                                            </div>
                                        </fieldset>
                                        <div class="space10"></div>
                                
                                        <table class="table table-striped table-advance table-hover span12 no-left-margin">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th class="span2 alineacionCentro" scope="col"><asp:Label ID="lblFolioGrid" runat="server" meta:resourcekey="lblTicket"></asp:Label></th>
                                                    <th class="span7 alineacionCentro" scope="col" ><asp:Label ID="lblProveedorGrid" runat="server" meta:resourcekey="HeaderProveedor"></asp:Label></th>
                                                    <th class="span2 alineacionCentro" scope="col" ><asp:Label ID="lblEstatusGrid" runat="server" meta:resourcekey="HeaderFecha"></asp:Label></th>
                                                    <th class="span2 alineacionCentro" scope="col" ><asp:Label ID="HeaderEstatus" runat="server" meta:resourcekey="HeaderEstatus"></asp:Label></th>
                                                </tr>
                                            </thead>
                                        </table>
                                        <div class="space5"></div>
                                        <div style="overflow: auto; height: 250px;">
                                            <table id="tbBusqueda" class="table table-striped table-advance table-hover span12 no-left-margin">
                                                <tbody></tbody>
                                            </table>
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
        </form>
    </div>
</body>

</html>
