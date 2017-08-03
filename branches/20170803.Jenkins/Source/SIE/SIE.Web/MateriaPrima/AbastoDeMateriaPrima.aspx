<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AbastoDeMateriaPrima.aspx.cs" Inherits="SIE.Web.MateriaPrima.AbastoDeMateriaPrima" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    
    <script src="../Scripts/AbastoDeMateriaPrima.js"></script>
    <link href="../assets/css/AbastoDeMateriaPrima.css" rel="stylesheet" />

     <script type="text/javascript">
         var mensajePiezasMayorALote = '<asp:Literal runat="server" Text="<%$ Resources:mensajePiezasMayorALote %>"/>';
         var mensajeCantidadEntregadaCero = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCantidadEntregadaCero %>"/>';
         var mensajeCantidadEntregadaMayorAProgramada = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCantidadEntregadaMayorAProgramada %>"/>';
         var mensajeExistenDatosEnBlanco = '<asp:Literal runat="server" Text="<%$ Resources:mensajeExistenDatosEnBlanco %>"/>';
         var MensajeDatosGuardadosConExito = '<asp:Literal runat="server" Text="<%$ Resources:MensajeDatosGuardadosConExito %>"/>';
         var mensjaeErrorAlGuardar = '<asp:Literal runat="server" Text="<%$ Resources:mensjaeErrorAlGuardar %>"/>';
         var MensajeCancelar = '<asp:Literal runat="server" Text="<%$ Resources:MensajeCancelar %>"/>';
         var MensajeCancelarAyudaFolio = '<asp:Literal runat="server" Text="<%$ Resources:MensajeCancelarAyudaFolio %>"/>';
         var MensajeLimpiar = '<asp:Literal runat="server" Text="<%$ Resources:MensajeLimpiar %>"/>';
         var mensajeTienesQueCapturarUnFolioValido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeTienesQueCapturarUnFolioValido %>"/>';
         var mensajeSeleccionarFolio = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarFolio %>"/>';
         var mensajeFolioInvalido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeFolioInvalido %>"/>';
         var mensajeErrorAlConsultarFolio = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarFolio %>"/>';
         var mensajeErrorAlConsultarProgramacion = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarProgramacion %>"/>';
         var mensajeNoTieneProgramacion = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoTieneProgramacion %>"/>';
         var mensajeNoHayTransportistas = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoHayTransportistas %>"/>';
         var mensajeErrorAlConsultarTransportistas = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarTransportistas %>"/>';
         var mensajeSeleccionarTransportista = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarTransportista %>"/>';
         var mensajeCancelarAyudaTransportista = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCancelarAyudaTransportista %>"/>';
         var mensajeNoSeEncontroElTransportista = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoSeEncontroElTransportista %>"/>';
         var mensajeFavorCapturaTransportista = '<asp:Literal runat="server" Text="<%$ Resources:mensajeFavorCapturaTransportista %>"/>';
         var mensajeNoSeEncontraronFolios = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoSeEncontraronFolios %>"/>';
         
         var mensajeErrorAlConsultarChoferes = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarChoferes %>"/>';
         var mensajeNoSeEncontroElChofer = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoSeEncontroElChofer %>"/>';
         var mensajeFavorCapturaChofer = '<asp:Literal runat="server" Text="<%$ Resources:mensajeFavorCapturaChofer %>"/>';
         var mensajeNoHayChoferesAsignados = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoHayChoferesAsignados %>"/>';
         var mensajeCancelarAyudaChofer = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCancelarAyudaChofer %>"/>';
         var mensajeSeleccionarChofer = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarChofer %>"/>';
         var mensajeNoSeEncontroElChoferAyuda = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoSeEncontroElChoferAyuda %>"/>';
         
         var mensajeErrorAlConsultarPlacas = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarPlacas %>"/>';
         var mensajeNoSeEncontroLaPlaca = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoSeEncontroLaPlaca %>"/>';
         var mensajeFavorCapturaPlaca = '<asp:Literal runat="server" Text="<%$ Resources:mensajeFavorCapturaPlaca %>"/>';
         var mensajeNoHayPlacasAsignadas = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoHayPlacasAsignadas %>"/>';
         var mensajeCancelarAyudaPlacas = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCancelarAyudaPlacas %>"/>';
         var mensajeSeleccionarPlaca = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarPlaca %>"/>';
         
         var mensajeTienesQueCapturarUnTicketValido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeTienesQueCapturarUnTicketValido %>"/>';
         var mensajeTicketInvalido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeTicketInvalido %>"/>';
         var mensajeErrorAlConsultarTicket = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarTicket %>"/>';
         var mensajeTicketInvalido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeTicketInvalido %>"/>';
         var mensajeErrorAlConsultarLosFolios = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarLosFolios %>"/>';

     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row-fluid contenedor-abasto">
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
                                    <a href="../MateriaPrima/AbastoDeMateriaPrima.aspx"><asp:Label ID="Label1" runat="server" meta:resourcekey="lblTitulo"></asp:Label></a>
                                </li>
                            </ul>
                        </div>
                        
                        <div class="row-fluid">
                            <fieldset id="Fieldset2" class="scheduler-border">
                                <legend class="scheduler-border">
                                    <asp:Label ID="Label14" runat="server" meta:resourcekey="lblFiltroBusqueda"></asp:Label>
                                </legend>
                                <div class="span7">
                                    <div class="span1 labelfolioticket"><asp:Label ID="lblFolio" runat="server" meta:resourcekey="lblFolio"></asp:Label>:</div>
                                    <div class="span3 cajaFolioTicketText"><asp:TextBox ID="txtFolio" runat="server" CssClass="control span12" type="tel"></asp:TextBox></div>
                                    <div class="span1 cajaboton">
                                        <button type="button" id="btnBuscarFolio" class="btn SuKarne">
                                            <i class="icon-search"></i>
                                        </button>
                                    </div>
                                    
                                    <div class="span1 labelfolioticket"><asp:Label ID="lblTicket" runat="server" meta:resourcekey="lblTicket"></asp:Label>:</div>
                                    <div class="span3 cajaFolioTicketText"><asp:TextBox ID="txtTicket" runat="server" CssClass="control span12" type="tel"></asp:TextBox></div>
                                </div>
                                <div class="span3">
                                    <button type="button" id="btnBuscarFolioTicket" class="btn SuKarne"><asp:Label ID="label15" runat="server" meta:resourcekey="btnBuscar"></asp:Label></button>
                                    <button type="button" id="btnLimpiar" class="btn SuKarne"><asp:Label ID="label4" runat="server" meta:resourcekey="btnLimpiar"></asp:Label></button>
                                </div>
                            </fieldset>
                        </div>
                        
                        <div class="row-fluid">
                            <fieldset id="Fieldset1" class="scheduler-border">
                                <legend class="scheduler-border">
                                    <asp:Label ID="Label5" runat="server" meta:resourcekey="lblDatosDeKilosPendientes"></asp:Label>
                                </legend>
                                <div class="caja">
                                    <div>
                                        <table id="encabezadoDetallesProductos" class="table-striped table-advance table-hover no-left-margin">
                                            <thead>
                                                <tr>
                                                    <th class="colProducto alineacionCentro" scope="col"><asp:Label ID="Label20" runat="server" meta:resourcekey="lblProducto"></asp:Label></th>
                                                    <th class="colCantidades alineacionCentro" scope="col"><asp:Label ID="Label21" runat="server" meta:resourcekey="lblCantidadSolicitada"></asp:Label></th>
                                                    <th class="colCantidades alineacionCentro" scope="col"><asp:Label ID="Label22" runat="server" meta:resourcekey="lblCantidadEntregada"></asp:Label></th>
                                                    <th class="colCantidades alineacionCentro" scope="col"><asp:Label ID="Label2" runat="server" meta:resourcekey="lblCantidadPendiente"></asp:Label></th>
                                                    <th class="colLoteProceso alineacionCentro" scope="col"><asp:Label ID="Label3" runat="server" meta:resourcekey="lblLoteProceso"></asp:Label></th>
                                                    <th class="colOpcionEditar alineacionCentro" scope="col"></th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                    <div id="contenedorAyuda" style="height: 250px; overflow: auto;">
                                        <table id="tbDetallesProductos" class="table-striped table-advance table-hover no-left-margin">
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
    
    
    <!-- Dialogo de Ayuda de Folios -->
    <div id="dlgBusquedaFolio" class="modal hide fade" style="margin-top: -200px;height: 250px;width:500px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="Label23" runat="server" meta:resourcekey="lblBusquedaDeFolios"></asp:Label>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="row-fluid">
                    <fieldset class="scheduler-border span12">
                        <legend class="scheduler-border"><asp:Label ID="lblFiltro" runat="server" meta:resourcekey="lblFiltro"></asp:Label></legend>
                        <div class="span12 no-left-margin" style="margin-top: -30px;">
                            <asp:Label ID="lblFolioBuscar" runat="server" meta:resourcekey="lblFolio"></asp:Label>:
                            <input id="txtFolioBuscar" style="width: 100px; margin-top: 10px;" type="tel"/>
                            <a id="btnBuscarAyudaFolio" class="btn SuKarne" style="margin-left: 10px;"><asp:Label ID="Label30" runat="server" meta:resourcekey="btnBuscar"></asp:Label></a>
                            <a id="btnAgregarAyudaFolio" class="btn SuKarne"><asp:Label ID="Label27" runat="server" meta:resourcekey="btnAgregar"></asp:Label></a>
                            <a id="btnCancelarAyudaFolio" class="btn SuKarne"><asp:Label ID="Label28" runat="server" meta:resourcekey="btnCancelar"></asp:Label></a>
                        </div>
                    </fieldset>
                </div>
                <div class="row-fluid">
                    <div>
                        <table id="encabezadoGridFoliosProductos" class="table-striped table-advance table-hover no-left-margin">
                            <thead>
                                <tr>
                                    <th class="colCheckBox alineacionCentro" scope="col"></th>
                                    <th class="colClave alineacionCentro" scope="col"><asp:Label ID="Label25" runat="server" meta:resourcekey="lblFolio"></asp:Label></th>
                                    <th class="colDescripcion alineacionCentro" scope="col"><asp:Label ID="Label26" runat="server" meta:resourcekey="lblOrganizacion"></asp:Label></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div id="Div2" class="scroller" style="height: 200px;" data-always-visible="1" data-rail-visible="0">
                        <table id="gridFoliosProductos" class="table-striped table-advance table-hover no-left-margin">
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
	</div>
    
    
    <!-- Dialogo de Solicitud de Materia Prima -->
    <div id="dlgSolicitudMateriaPrima" class="modal hide fade" style="margin-top: -200px;height: 250px;width:600px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="Label6" runat="server" meta:resourcekey="lblTituloSolicitud"></asp:Label>
                </div>
            </div>
            <div class="portlet-body form">
                
                
                <div class="tabbable"> <!-- Only required for left/right tabs -->
                  <ul class="nav nav-tabs">
                        <li class="active">
                            <a id="tabProducto" href="#tab1" data-toggle="tab">
                                <asp:Label ID="Label32" runat="server" meta:resourcekey="lblProducto"></asp:Label>
                            </a>
                        </li>
                        <li>
                            <a id="tabSurtir" href="#tab2" data-toggle="tab">
                                <asp:Label ID="Label33" runat="server" meta:resourcekey="lblSurtirMateriaPrima"></asp:Label>
                            </a>
                        </li>
                        <li class="pull-right">
                            <button type="button" id="btnGuardarProgramacion" data-toggle="modal" class="btn letra SuKarne">
                                <asp:Label ID="Label8" runat="server" meta:resourcekey="btnGuardar"></asp:Label>
                            </button>
                            <button type="button" id="btnCancelarProgramacion" data-toggle="modal" class="btn letra SuKarne">
                                <asp:Label ID="Label9" runat="server" meta:resourcekey="btnCancelar"></asp:Label>
                            </button>
                        </li>
                  </ul>

                  <div class="tab-content">
                        <div class="tab-pane active" id="tab1">
                          <div class="row-fluid">
                              <div class="span12">
                                <div class="span3"><asp:Label ID="Label34" runat="server" meta:resourcekey="lblSubFamilia"></asp:Label>:</div>
                                <div class="span6">
                                    <input type="text" id="txtSubFamilia" class="control span12" disabled="disabled"/>
                                    <input type="hidden" id="txtPesajeMateriaPrimaId"/><input type="hidden" id="txtPedidoDetalleId"/>
                                </div>
                              </div>
                          </div>
                          <div class="row-fluid">
                              <div class="span12">
                                <div class="span3"><asp:Label ID="Label10" runat="server" meta:resourcekey="lblProducto"></asp:Label>:</div>
                                <div class="span6"><input type="hidden" id="txtProductoId"/><input type="text" id="txtProducto" class="control span12" disabled="disabled"/></div>
                              </div>
                          </div>
                          <div class="row-fluid">
                              <div class="span12">
                                <div class="span3"><asp:Label ID="Label11" runat="server" meta:resourcekey="lblCantidadSolicitada"></asp:Label>:</div>
                                <div class="span6"><input type="text" id="txtCantidadSolicitada" class="control span12" disabled="disabled" style="text-align:right;"/></div>
                              </div>
                          </div>          
                          <div class="row-fluid">
                              <div class="span12">
                                <div class="span3"><i class="requerido">*</i>&nbsp;<asp:Label ID="Label29" runat="server" meta:resourcekey="lblTransportista"></asp:Label>:</div>
                                <div class="span2 cajaidentificador"><input type="hidden" id="txtProveedorId"/><input type="text" id="txtProveedorCodigoSap" class="control span12"/></div>
                                <div class="span6 cajadescripcion"><input type="text" id="txtProveedor" class="control span12" disabled="disabled"/></div>
                                <div class="span1 cajaboton">
                                    <button type="button" id="btnAyudaProveedores" class="btn letra SuKarne">
                                        <i class="icon-search"></i>
                                    </button>
                                </div>
                              </div>
                          </div>              
                          <div class="row-fluid">
                              <div class="span12">
                                <div class="span3"><i class="requerido">*</i>&nbsp;<asp:Label ID="Label31" runat="server" meta:resourcekey="lblChofer"></asp:Label>:</div>
                                <div class="span2 cajaidentificador"><input type="hidden" id="txtChoferValido"/><input id="txtChoferId" class="control span12" type="tel"/></div>
                                <div class="span6 cajadescripcion"><input type="text" id="txtChofer" class="control span12" disabled="disabled"/></div>
                                <div class="span1 cajaboton">
                                    <button type="button" id="btnAyudaChoferes" class="btn letra SuKarne">
                                        <i class="icon-search"></i>
                                    </button>
                                </div>
                              </div>
                          </div>                  
                          <div class="row-fluid">
                              <div class="span12">
                                <div class="span3"><i class="requerido">*</i>&nbsp;<asp:Label ID="Label35" runat="server" meta:resourcekey="lblPlaca"></asp:Label>:</div>
                                <div class="span2 cajaidentificador"><input type="hidden" id="txtPlacaValida"/><input id="txtCamionId" class="control span12" type="tel"/></div>
                                <div class="span6 cajadescripcion"><input type="text" id="txtPlaca" class="control span12" disabled="disabled"/></div>
                                  <div class="span1 cajaboton">
                                  <button type="button" id="btnAyudaPlacas" class="btn letra SuKarne">
                                        <i class="icon-search"></i>
                                    </button>
                                </div>
                              </div>
                          </div>
                        </div>
                        <div class="tab-pane" id="tab2">
                            <div>
                                <table id="encabezadoTablaProgramacion" class="table table-striped table-advance table-hover no-left-margin">
                                    <thead>
                                        <tr>
                                            <th style="width: 5%;" class="alineacionCentro"scope="col"></th>
                                            <th style="width: 20%;" class=" alineacionCentro" scope="col"><asp:Label ID="Label12" runat="server" meta:resourcekey="lblCantidadProgramada"></asp:Label></th>
                                            <th style="width: 20%;" class=" alineacionCentro" scope="col"><asp:Label ID="Label13" runat="server" meta:resourcekey="lblLoteMateriaPrima"></asp:Label></th>
                                            <th style="width: 20%;" class=" alineacionCentro" scope="col"><asp:Label ID="Label18" runat="server" meta:resourcekey="lblCantidadEntregada"></asp:Label></th>
                                            <th style="width: 20%;" class=" alineacionCentro" scope="col"><asp:Label ID="Label19" runat="server" meta:resourcekey="lblJustificacion"></asp:Label></th>
                                            <th style="width: 15%;" class=" alineacionCentro" scope="col"><asp:Label ID="Label7" runat="server" meta:resourcekey="lblPiezas"></asp:Label></th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <div id="Div3" class="scroller" style="height: 200px;" data-always-visible="1" data-rail-visible="0">
                                <table id="tablaProgramacion" class="table table-striped table-advance table-hover no-left-margin">
                                    <tbody></tbody>
                                </table>
                            </div>
                        </div>
                  </div>
                </div>
                
            </div>
        </div>
	</div>
    
    <!-- Dialogo de Ayuda de Proveedores -->
    <div id="dlgAyudaProveedores" class="modal hide fade" style="margin-top: -200px;height: 250px;width:600px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="Label36" runat="server" meta:resourcekey="lblBusquedaDeTransportista"></asp:Label>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="row-fluid">
                    <fieldset class="scheduler-border span12">
                        <legend class="scheduler-border"><asp:Label ID="Label37" runat="server" meta:resourcekey="lblFiltro"></asp:Label></legend>
                        <div class="span12 no-left-margin" style="margin-top: -30px;">
                            <asp:Label ID="Label38" runat="server" meta:resourcekey="lblTransportista"></asp:Label>:
                            <input type="text" id="txtDescripcionProveedorAyuda" style="margin-top: 10px;"/>
                            <d id="divBotones">
                                <a id="btnBuscarAyudaProveedor" class="btn SuKarne" style="margin-left: 10px;"><asp:Label ID="Label39" runat="server" meta:resourcekey="btnBuscar"></asp:Label></a>
                                <a id="btnAgregarAyudaProveedor" class="btn SuKarne"><asp:Label ID="Label40" runat="server" meta:resourcekey="btnAgregar"></asp:Label></a>
                                <a id="btnCancelarAyudaProveedor" class="btn SuKarne"><asp:Label ID="Label41" runat="server" meta:resourcekey="btnCancelar"></asp:Label></a>
                            </d>
                        </div>
                    </fieldset>
                </div>
                <div class="row-fluid">
                    <div>
                        <table id="encabezadoTablaProveedores" class="table-striped table-advance table-hover no-left-margin">
                            <thead>
                                <tr>
                                    <th class="colCheckBox alineacionCentro"></th>
                                    <th class="colClave alineacionCentro"><asp:Label ID="Label42" runat="server" meta:resourcekey="lblClave"></asp:Label></th>
                                    <th  class="colDescripcion alineacionCentro"><asp:Label ID="Label43" runat="server" meta:resourcekey="lblDescripcion"></asp:Label></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div id="divx" class="scroller" style="height: 200px;" data-always-visible="1" data-rail-visible="0">
                        <table id="tablaProveedores" class="table-striped table-advance table-hover no-left-margin">
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
	</div>
    
    <!-- Dialogo de Ayuda de Choferes -->
    <div id="dlgAyudaChoferes" class="modal hide fade" style="margin-top: -200px;height: 250px;width:600px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="Label44" runat="server" meta:resourcekey="lblBusquedaDeChofer"></asp:Label>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="row-fluid">
                    <fieldset class="scheduler-border span12">
                        <legend class="scheduler-border"><asp:Label ID="Label45" runat="server" meta:resourcekey="lblFiltro"></asp:Label></legend>
                        <div class="span12 no-left-margin" style="margin-top: -30px;">
                            <asp:Label ID="Label46" runat="server" meta:resourcekey="lblChofer"></asp:Label>:
                            <input type="text" id="txtDescripcionChoferAyuda" style="margin-top: 10px;"/>
                            <d id="divBotonesChofer">
                                <a id="btnBuscarAyudaChofer" class="btn SuKarne" style="margin-left: 10px;"><asp:Label ID="Label47" runat="server" meta:resourcekey="btnBuscar"></asp:Label></a>
                                <a id="btnAgregarAyudaChofer" class="btn SuKarne"><asp:Label ID="Label48" runat="server" meta:resourcekey="btnAgregar"></asp:Label></a>
                                <a id="btnCancelarAyudaChofer" class="btn SuKarne"><asp:Label ID="Label49" runat="server" meta:resourcekey="btnCancelar"></asp:Label></a>
                            </d>
                        </div>
                    </fieldset>
                </div>
                <div class="row-fluid">
                    <div>
                        <table id="encabezadoTablaChoferes" class="table-striped table-advance table-hover no-left-margin">
                            <thead>
                                <tr>
                                    <th class="colCheckBox alineacionCentro"></th>
                                    <th class="colClave alineacionCentro"><asp:Label ID="Label50" runat="server" meta:resourcekey="lblClave"></asp:Label></th>
                                    <th  class="colDescripcion alineacionCentro"><asp:Label ID="Label51" runat="server" meta:resourcekey="lblDescripcion"></asp:Label></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div id="div4" class="scroller" style="height: 200px;" data-always-visible="1" data-rail-visible="0">
                        <table id="tablaChoferes" class="table-striped table-advance table-hover no-left-margin">
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
	</div>
    
    <!-- Dialogo de Ayuda de Placas -->
    <div id="dlgAyudaPlacas" class="modal hide fade" style="margin-top: -200px;height: 250px;width:450px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="Label52" runat="server" meta:resourcekey="lblBusquedaDePlacas"></asp:Label>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="row-fluid">
                    <fieldset class="scheduler-border span12">
                        <legend class="scheduler-border"><asp:Label ID="Label53" runat="server" meta:resourcekey="lblFiltro"></asp:Label></legend>
                        <div class="span12 no-left-margin" style="margin-top: -30px;">
                            <asp:Label ID="Label54" runat="server" meta:resourcekey="lblPlaca"></asp:Label>:
                            <input type="text" id="txtDescripcionPlacaAyuda" style="width: 100px; margin-top: 10px;"/>
                            <a id="btnBuscarAyudaPlaca" class="btn SuKarne" style="margin-left: 10px;"><asp:Label ID="Label55" runat="server" meta:resourcekey="btnBuscar"></asp:Label></a>
                            <a id="btnAgregarAyudaPlaca" class="btn SuKarne"><asp:Label ID="Label56" runat="server" meta:resourcekey="btnAgregar"></asp:Label></a>
                            <a id="btnCancelarAyudaPlaca" class="btn SuKarne"><asp:Label ID="Label57" runat="server" meta:resourcekey="btnCancelar"></asp:Label></a>
                        </div>
                    </fieldset>
                </div>
                <div class="row-fluid">
                    <div>
                        <table id="encabezadoTablaPlacas" class="table-striped table-advance table-hover no-left-margin">
                            <thead>
                                <tr>
                                    <th class="colCheckBox alineacionCentro"></th>
                                    <th class="colClave alineacionCentro"><asp:Label ID="Label58" runat="server" meta:resourcekey="lblClave"></asp:Label></th>
                                    <th  class="colDescripcion alineacionCentro"><asp:Label ID="Label59" runat="server" meta:resourcekey="lblDescripcion"></asp:Label></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div id="" class="scroller" style="height: 200px;" data-always-visible="1" data-rail-visible="0">
                        <table id="tablaPlacas" class="table-striped table-advance table-hover no-left-margin">
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
	</div>


</body>
</html>
