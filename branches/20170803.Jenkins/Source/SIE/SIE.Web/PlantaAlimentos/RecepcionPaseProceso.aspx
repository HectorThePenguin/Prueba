<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecepcionPaseProceso.aspx.cs" Inherits="SIE.Web.PlantaAlimentos.RecepcionPaseProceso" %>

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
    </asp:PlaceHolder>

    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    
    <script src="../Scripts/RecepcionPaseProceso.js"></script>
    <link href="../assets/css/RecepcionMateriaPrimaPatio.css" rel="stylesheet" />


    <script type="text/javascript">
        var mensajeFolioNoValido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeFolioNoValido %>"/>';
        var mensajeFolioActivo = '<asp:Literal runat="server" Text="<%$ Resources:mensajeFolioActivo %>"/>';
        var mensajeGuardadoOK = '<asp:Literal runat="server" Text="<%$ Resources:mensajeGuardadoOK %>"/>';
        var mensajeNoExistenLotes = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoExistenLotes %>"/>';
        var headerFechaSurtida = '<asp:Literal runat="server" Text="<%$ Resources:lblFechaSurtido.Text %>"/>';
        var headerTicket = '<asp:Literal runat="server" Text="<%$ Resources:lblTicket.Text %>"/>';
        var headerProductos = '<asp:Literal runat="server" Text="<%$ Resources:lblProducto.Text %>"/>';
        var headerChofer = '<asp:Literal runat="server" Text="<%$ Resources:lblChofer.Text %>"/>';
        var headerProveedor = '<asp:Literal runat="server" Text="<%$ Resources:lblProveedor.Text %>"/>';
        var headerLoteDestino = '<asp:Literal runat="server" Text="<%$ Resources:lblLoteDestino.Text %>"/>';
        var headerCantidadSolicitada = '<asp:Literal runat="server" Text="<%$ Resources:lblCantidadSolicitada %>"/>';
        var headerCantidadSurtida = '<asp:Literal runat="server" Text="<%$ Resources:lblCantidadSurtida.Text %>"/>';
        var headerCantidadRecibida = '<asp:Literal runat="server" Text="<%$ Resources:lblCantidadRecibida.Text %>"/>';
        var headerCantidadPendiente = '<asp:Literal runat="server" Text="<%$ Resources:lblCantidadPendiente.Text %>"/>';
        var headerCantidadProgramada = '<asp:Literal runat="server" Text="<%$ Resources:lblCantidadProgramada.Text %>"/>';
        var headerCantidadEntregada = '<asp:Literal runat="server" Text="<%$ Resources:lblCantidadEntregada.Text %>"/>';
        var mensajeTienesQueCapturarUnFolioValido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCapturaNoValida %>"/>';
        var mensajeSeleccionarFolio = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCapturaNoValida %>"/>';
        var mensajeErrorAlConsultarPedidos = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorCosultarPedidos %>"/>';
        var mensajeSeguroDeCancelar = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeguroDeCancelar %>"/>';
        var mensajeErrorAlActualizarEstatus = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlActualizarEstatus %>"/>';
        var mensajeEstatusFinalizado = '<asp:Literal runat="server" Text="<%$ Resources:mensajeEstatusFinalizado %>"/>';
        
        //DATATABLES
        var PrimeraPagina = '<asp:Literal runat="server" Text="<%$ Resources:datatable.PrimeraPagina %>"/>';
        var UltimaPagina = '<asp:Literal runat="server" Text="<%$ Resources:datatable.UltimaPagina %>"/>';
        var Siguiente = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Siguiente %>"/>';
        var Anterior = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Anterior %>"/>';
        var SinDatos = '<asp:Literal runat="server" Text="<%$ Resources:datatable.SinDatos %>"/>';
        var Mostrando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Mostrando %>"/>';
        var SinInformacion = '<asp:Literal runat="server" Text="<%$ Resources:datatable.SinInformacion %>"/>';
        var Filtrando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Filtrando %>"/>';
        var Mostrar = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Mostrar %>"/>';
        var Cargando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Cargando %>"/>';
        var Procesando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Procesando %>"/>';
        var Buscar = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Buscar %>"/>';
        var SinRegistros = '<asp:Literal runat="server" Text="<%$ Resources:datatable.SinRegistros %>"/>';

        //Mensajes
        var mensajeSeleccioneRegistro = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccioneRegistro %>"/>';
        var mensajeGuardadoExito = '<asp:Literal runat="server" Text="<%$ Resources:mensajeGuardadoExito %>"/>';
        var mensajeSeleccioneFolio = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccioneFolio %>"/>';
        var mensajeErrorGuardado = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorGuardar %>"/>';
        var mensajeSinAlmacen = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSinAlmacen %>"/>';
        var mensajeEstatusIncorrecto = '<asp:Literal runat="server" Text="<%$ Resources:mensajeEstatusIncorrecto %>"/>';
        var mensajeCancelacion = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCancelacion %>"/>';
        var mensajeCancelacionAyuda = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCancelacionAyuda %>"/>';
        var mensajeSinSurtido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSinSurtido %>"/>';
        var labelOk = '<asp:Literal runat="server" Text="<%$ Resources:labelOk %>"/>';
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="row-fluid contenedorPaseProceso">
        <div class="span12">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="row-fluid caption">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png"  />
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
                                <a href="../PlantaAlimentos/RecepcionPaseProceso.aspx"><asp:Label ID="Label1" runat="server" meta:resourcekey="lblTitulo"></asp:Label></a>
                            </li>
                        </ul>
                    </div>
                        
                    <div class="row-fluid">
                        <fieldset id="FieldsetBusqueda" class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblGrupoBusqueda" runat="server" meta:resourcekey="lblGrupoBusqueda"></asp:Label>
                            </legend>
                            <div class="span10">
                                <div class="span6">
                                    <div class="span2"><span class="campoRequerido span4">*</span><asp:Label ID="Label2" runat="server" CssClass="control" meta:resourcekey="lblFolio"></asp:Label>:</div>
                                    <div class="span4"><asp:TextBox ID="txtFolio" runat="server" CssClass="control span12" type="tel"></asp:TextBox><asp:HiddenField ID="txtEntradaProductoId" runat="server" /></div>
                                    <div class="span1">
                                        <button type="button" id="btnBuscarFolio" class="btn letra SuKarne span12">
                                            <i class="icon-search"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>
                       </fieldset>
                    </div>
                   
                    <div class="row-fluid">
                        <fieldset id="Fieldset1" class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="lblDatosRecepcion"></asp:Label>
                            </legend>
                             <div class="row-fluid">
                                <div class="span12">
                                    <div id="scroll" class="span12">
                                        <div id="tablaFolios" class="span12">
                                      
                                        </div>
                                   </div>
                                 </div>
                             </div>
                            
                             <div class="row-fluid">
                                <div class="span12">
                                    <div class="span2">
                                        <asp:Label ID="lblObservaciones" runat="server" meta:resourcekey="lblObservaciones"></asp:Label>
                                    </div>
                                    <div class="span9">
                                        <asp:TextBox id="txtObservaciones" MaxLength="255" TextMode="multiline" Columns="50" Rows="5" runat="server" CssClass="span12" style="resize: none;" />
                                    </div>
                                </div>
                            </div>
                            
                            <div class="row-fluid">
                                <div class="span12">
                                    <div class="pull-right">
<%--                                        <button type="button" id="btnFinalizarPedido" data-toggle="modal" class="btn letra SuKarne">
                                            <asp:Label ID="Label4" runat="server" meta:resourcekey="btnFinalizarPedido"></asp:Label>
                                        </button>--%>
                                        <button type="button" id="btnGuardar" data-toggle="modal" class="btn letra SuKarne">
                                            <asp:Label ID="Label16" runat="server" meta:resourcekey="btnGuardar"></asp:Label>
                                        </button>
                                        <button type="button" id="btnCancelar" data-toggle="modal" class="btn letra SuKarne">
                                            <asp:Label ID="Label17" runat="server" meta:resourcekey="btnCancelar"></asp:Label>
                                        </button>
                                    </div>
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
    <div id="dlgBusquedaFolio" class="modal hide fade" style="margin-top: -150px;height: 400px;width:600px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/skLogo.png" />
                    <span>
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="lblTituloAyudaFolio"></asp:Label>
                    </span>
                </div>
            </div>

            <div class="portlet-body form">
                <div class="row-fluid">
                    <fieldset class="scheduler-border span12">
                        <legend class="scheduler-border"><asp:Label ID="lblFiltro" runat="server" meta:resourcekey="lblFiltroAyuda"></asp:Label></legend>
                        <div class="span12 no-left-margin">
                            <asp:Label ID="lblFolioBuscar" runat="server" meta:resourcekey="lblFolio"></asp:Label>:
                            <input type="tel" id="txtFolioBuscar" style="width: 100px;"/>
                            <a id="btnBuscarAyudaFolio" class="btn SuKarne" style="margin-left: 10px;"><asp:Label ID="Label30" runat="server" meta:resourcekey="btnBuscar"></asp:Label></a>
                            <a id="btnAgregarAyudaFolio" class="btn SuKarne"><asp:Label ID="Label27" runat="server" meta:resourcekey="btnAgregar"></asp:Label></a>
                            <a id="btnCancelarAyudaFolio" class="btn SuKarne"><asp:Label ID="Label28" runat="server" meta:resourcekey="btnCancelar"></asp:Label></a>
                        </div>
                    </fieldset>
                </div>
                <div class="row-fluid">
        
                    <div id="Div2" style="height: 250px; overflow: auto;">
                        <table id="gridFoliosMateriaPrima" class="table table-striped table-advance table-hover no-left-margin">
                            <thead>
                                <tr>
                                    <th class="alineacionCentro tabs-right" scope="col"></th>
                                    <th class="alineacionCentro tabs-right" scope="col"><asp:Label ID="Label24" runat="server" meta:resourcekey="lblId"></asp:Label></th>
                                    <th class="alineacionCentro tabs-right" scope="col"><asp:Label ID="Label25" runat="server" meta:resourcekey="lblPedido"></asp:Label></th>
                                    <th class="alineacionCentro tabs-right" scope="col"><asp:Label ID="Label26" runat="server" meta:resourcekey="lblOrganizacion"></asp:Label></th>
                                    <th class="alineacionCentro tabs-right" scope="col"><asp:Label ID="Label29" runat="server" meta:resourcekey="lblEstatus"></asp:Label></th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
	</div>
</body>
</html>
