<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecepcionMateriaPrimaEnPatio.aspx.cs" Inherits="SIE.Web.MateriaPrima.RecepcionMateriaPrimaEnPatio" %>

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
    
    <script src="../Scripts/RecepcionMateriaPrimaEnPatio.js"></script>
    <link href="../assets/css/RecepcionMateriaPrimaPatio.css" rel="stylesheet" />

    <script type="text/javascript">
        var mensajeFolioNoValido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeFolioNoValido %>"/>';
        var mensajeFolioActivo = '<asp:Literal runat="server" Text="<%$ Resources:mensajeFolioActivo %>"/>';
        var mensajeGuardadoOK = '<asp:Literal runat="server" Text="<%$ Resources:mensajeGuardadoOK %>"/>';
        var mensajeBoletaNoTienePesaje = '<asp:Literal runat="server" Text="<%$ Resources:mensajeBoletaNoTienePesaje %>"/>';
        var mensajeNoExistenLotes = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoExistenLotes %>"/>';
        var mensajeTienesQueCapturarUnFolioValido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeTienesQueCapturarUnFolioValido %>"/>';
        var mensajeErrorAlGenerarElNuevoLote = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlGenerarElNuevoLote %>"/>';
        var mensajeErrorAlConsultarLotes = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarLotes %>"/>';
        var mensajeCancelacion = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCancelacion %>"/>';
        var mensajeErrorAlGuardar = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlGuardar %>"/>';
        var mensajeCapturarLoteValido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCapturarLoteValido %>"/>';
        var mensajeElFolioTieneAsignadoLote = '<asp:Literal runat="server" Text="<%$ Resources:mensajeElFolioTieneAsignadoLote %>"/>';
        var mensajeSeleccionarFolio = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarFolio %>"/>';
        var btnHoraFinDescarga = '<asp:Literal runat="server" Text="<%$ Resources:btnHoraFinDescarga.Text %>"/>';
        var btnHoraInicioDescarga = '<asp:Literal runat="server" Text="<%$ Resources:btnHoraInicioDescarga.Text %>"/>';
        var mensajeFechaActualizada = '<asp:Literal runat="server" Text="<%$ Resources:mensajeFechaActualizada %>"/>';
        var mensajeErrorAlActualizarFecha = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlActualizarFecha %>"/>';
        var mensajeSeleccionarLote = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarLote %>"/>';
        var mensajeCancelarAyudaFolio = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCancelarAyudaFolio %>"/>';
        var mensajeCancelarAyudaLote = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCancelarAyudaLote %>"/>';
        var mensajeErrorAlConsultarFolios = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarFolios %>"/>';
        var mensajeErrorAlConsultarFolio = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarFolio %>"/>';
        var mensajeNoSeEncontraronFoliosPendientes = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoSeEncontraronFoliosPendientes %>"/>';
        var mensajeBoletaNoHaSidoFinalizada = '<asp:Literal runat="server" Text="<%$ Resources:mensajeBoletaNoHaSidoFinalizada %>"/>';
        var mensajePiezasMayorACero = '<asp:Literal runat="server" Text="<%$ Resources:mensajePiezasMayorACero %>"/>';
        var mensajeNoSeLeHaAsignadoLote = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoSeLeHaAsignadoLote %>"/>';
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="row-fluid contenido-patio">
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
                                <a href="../MateriaPrima/RecepcionMateriaPrimaEnPatio.aspx"><asp:Label ID="Label1" runat="server" meta:resourcekey="lblTitulo"></asp:Label></a>
                            </li>
                        </ul>
                    </div>
                        
                    <div class="row-fluid">
                        <div class="span10">
                            <div class="span6">
                                <div class="span2"><asp:HiddenField ID="txtAlmacenUsuario" value="0" runat="server"/><asp:Label ID="Label2" runat="server" CssClass="control" meta:resourcekey="lblFolio"></asp:Label>:</div>
                                <div class="span4"><asp:TextBox ID="txtFolio" runat="server" CssClass="control span12" type="tel"></asp:TextBox><asp:HiddenField ID="txtEntradaProductoId" runat="server" /></div>
                                <div class="span1">
                                    <button type="button" id="btnBuscarFolio" class="btn letra SuKarne span12">
                                        <i class="icon-search"></i>
                                    </button>
                                </div>
                            </div>
                            <div class="span6">
                                <div class="span2"><asp:Label ID="Label3" runat="server" meta:resourcekey="lblFecha"></asp:Label></div>
                                <div class="span4"><asp:TextBox ID="txtFecha" runat="server" CssClass="control span12" ></asp:TextBox></div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <fieldset id="groupCorral" class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="Label9" runat="server" meta:resourcekey="lblDatosRecepcion"></asp:Label>
                            </legend>
                            <div class="span12">
                                <div class="space10"></div>
                                <div class="span12">
                                    <div class="span1"><asp:Label ID="Label8" runat="server" meta:resourcekey="lblProducto"></asp:Label>:</div>
                                    <div class="span5"><asp:TextBox ID="txtProducto" runat="server" CssClass="control span12" Enabled="False"></asp:TextBox><asp:HiddenField ID="txtProductoId" runat="server" />
                                    </div>
                                </div>
                                <div class="span12">
                                    <div class="span1"><asp:Label ID="Label4" runat="server" meta:resourcekey="lblContrato"></asp:Label>:</div>
                                    <div class="span5"><asp:TextBox ID="txtContrato" runat="server" CssClass="control span12" Enabled="False"></asp:TextBox></div>
                                </div>
                                <div class="span12">
                                    <div class="span1"><asp:Label ID="Label5" runat="server" meta:resourcekey="lblProveedor"></asp:Label>:</div>
                                    <div class="span5"><asp:TextBox ID="txtProveedor" runat="server" CssClass="control span12" Enabled="False"></asp:TextBox></div>
                                </div>
                                <div class="span12">
                                    <div class="span1"><asp:Label ID="Label6" runat="server" meta:resourcekey="lblPlacas"></asp:Label></div>
                                    <div class="span5"><asp:TextBox ID="txtPlacas" runat="server" CssClass="control span12" Enabled="False"></asp:TextBox></div>
                                </div>
                                <div class="span12">
                                    <div class="span1"><asp:Label ID="Label7" runat="server" meta:resourcekey="lblChofer"></asp:Label></div>
                                    <div class="span5"><asp:TextBox ID="txtChofer" runat="server" CssClass="control span12" Enabled="False"></asp:TextBox></div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row-fluid">
                        <fieldset id="Fieldset1" class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="lblDatosPiso"></asp:Label>
                            </legend>
                            <div class="span12">
                                <div class="span6">
                                    <div class="space10"></div>
                                    <div class="span10">
                                        <div class="span5"><asp:RadioButton ID="rbLoteAlmacen" runat="server" GroupName="datosRecepcion" Checked="True" /> <asp:Label ID="Label11" runat="server" meta:resourcekey="lblLoteAlmacen"></asp:Label></div>
                                        <div class="span3"><asp:TextBox ID="txtLoteAlmacen" runat="server" CssClass="control span12" type="tel"></asp:TextBox><asp:HiddenField ID="txtLoteAlmacenLoteId" runat="server" /></div>
                                        <div class="span2">
                                        </div>
                                        <div class="span2">
                                        </div>
                                    </div>
                                    <div class="span10">
                                        <div class="span5"><asp:RadioButton ID="rbLoteProceso" runat="server"  GroupName="datosRecepcion"/> <asp:Label ID="Label12" runat="server" meta:resourcekey="lblLoteProceso"></asp:Label></div>
                                        <div class="span3"><asp:TextBox ID="txtLoteProceso" runat="server" CssClass="control span12" type="tel"></asp:TextBox><asp:HiddenField ID="txtLoteProcesoLoteId" runat="server" /></div>
                                        <div class="span2">
                                        </div>
                                        <div class="span2">
                                        </div>
                                    </div>
                                    <div class="span10">
                                        <div class="span5"><asp:RadioButton ID="rbBodegaExterna" runat="server"  GroupName="datosRecepcion"/> <asp:Label ID="Label13" runat="server" meta:resourcekey="lblBodegaExterna"></asp:Label></div>
                                        <div class="span3"><asp:TextBox ID="txtBodegaExterna" runat="server" CssClass="control span12" type="tel"></asp:TextBox><asp:HiddenField ID="txtBodegaExternaLoteId" runat="server" />
                                        </div>
                                        <div class="span2">
                                        </div>
                                        <div class="span2">
                                        </div>
                                    </div>
                                </div>
                                <div class="span6">
                                    <div class="space12"></div>
                                    <div class="span5">
                                        <button type="button" id="btnFechasDescarga" class="btn SuKarne span12">
                                            <i class="icon-time"></i>
                                            <asp:Label ID="lblFechaDescarga" runat="server" meta:resourcekey="btnHoraInicioDescarga"></asp:Label>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row-fluid">
                        <fieldset id="Fieldset2" class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="Label14" runat="server" meta:resourcekey="lblDescarga"></asp:Label>
                            </legend>
                            <div class="span11">
                                <div class="span2"><asp:Label ID="Label15" runat="server" meta:resourcekey="lblPiezas"></asp:Label></div>
                                <div class="span2">
                                    <input type="tel" id="txtPiezas" class="control span12" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row-fluid">
                        <div class="span8">
                            <div class="pull-right">
                                <button type="button" id="btnGuardar" data-toggle="modal" class="btn letra SuKarne">
                                    <asp:Label ID="Label16" runat="server" meta:resourcekey="btnGuardar"></asp:Label>
                                </button>
                                <button type="button" id="btnCancelar" data-toggle="modal" class="btn letra SuKarne">
                                    <asp:Label ID="Label17" runat="server" meta:resourcekey="btnCancelar"></asp:Label>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    
    <!-- Dialogo de Busqueda Ayuda de Lotes -->
    <div id="dlgBusquedaLotes" class="modal hide fade" style="margin-top: -150px;height: 400px;width:600px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblBusquedaFolio" runat="server" meta:resourcekey="lblTituloAyudaLote"></asp:Label>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="row-fluid">
                    <div>
                        <table id="Table1" class="table table-striped table-advance table-hover no-left-margin">
                            <thead>
                                <tr>
                                    <th style="width: 20px;" class=" alineacionCentro" scope="col"></th>
                                    <th style="width: 50px;" class=" alineacionCentro" scope="col"><asp:Label ID="Label20" runat="server" meta:resourcekey="lblGridLote"></asp:Label></th>
                                    <th style="width: 50px;" class=" alineacionCentro" scope="col"><asp:Label ID="Label21" runat="server" meta:resourcekey="lblGridAlmacen"></asp:Label></th>
                                    <th style="width: 50px;" class=" alineacionCentro" scope="col"><asp:Label ID="Label22" runat="server" meta:resourcekey="lblGridStock"></asp:Label></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div id="contenedorAyuda" style="height: 250px; overflow: auto;">
                        <table id="tbBusquedaLotes" class="table table-striped table-advance table-hover no-left-margin">
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="pull-right" style="margin: 15px;">
                        <a id="btnAyudaAgregar" class="btn SuKarne"><asp:Label ID="Label18" runat="server" meta:resourcekey="btnAgregar"></asp:Label></a>
                        <a id="btnAyudaCancelar" class="btn SuKarne"><asp:Label ID="Label19" runat="server" meta:resourcekey="btnCancelar"></asp:Label></a>
                    </div>
                </div>
            </div>
        </div>
	</div>
    
    <!-- Dialogo de Ayuda de Folios -->
    <div id="dlgBusquedaFolio" class="modal hide fade" style="margin-top: -200px;height: 300px;width:600px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="Label23" runat="server" meta:resourcekey="lblTituloAyudaFolio"></asp:Label>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="row-fluid">
                    <fieldset class="scheduler-border span12">
                        <legend class="scheduler-border"><asp:Label ID="lblFiltro" runat="server" meta:resourcekey="lblFiltro"></asp:Label></legend>
                        <div class="span12 no-left-margin">
                            <asp:Label ID="lblFolioBuscar" runat="server" meta:resourcekey="lblFolio"></asp:Label>:
                            <input type="text" id="txtFolioBuscar" style="width: 100px;"/>
                            <a id="btnBuscarAyudaFolio" class="btn SuKarne" style="margin-left: 10px;"><asp:Label ID="Label30" runat="server" meta:resourcekey="btnBuscar"></asp:Label></a>
                            <a id="btnAgregarAyudaFolio" class="btn SuKarne"><asp:Label ID="Label27" runat="server" meta:resourcekey="btnAgregar"></asp:Label></a>
                            <a id="btnCancelarAyudaFolio" class="btn SuKarne"><asp:Label ID="Label28" runat="server" meta:resourcekey="btnCancelar"></asp:Label></a>
                        </div>
                    </fieldset>
                </div>
                <div class="row-fluid">
                    <div>
                        <table id="Table2" class="table table-striped table-advance table-hover no-left-margin">
                            <thead>
                                <tr>
                                    <th style="width: 20px;" class=" alineacionCentro" scope="col"></th>
                                    <th style="width: 50px;" class=" alineacionCentro" scope="col"><asp:Label ID="Label24" runat="server" meta:resourcekey="lblFolio"></asp:Label></th>
                                    <th style="width: 100px;" class=" alineacionCentro" scope="col"><asp:Label ID="Label25" runat="server" meta:resourcekey="lblContrato"></asp:Label></th>
                                    <th style="width: 100px;" class=" alineacionCentro" scope="col"><asp:Label ID="Label26" runat="server" meta:resourcekey="lblProducto"></asp:Label></th>
                                    <th style="width: 100px;" class=" alineacionCentro" scope="col"><asp:Label ID="Label29" runat="server" meta:resourcekey="lblProveedor"></asp:Label></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div id="Div2" style="height: 250px; overflow: auto;">
                        <table id="gridFoliosProductos" class="table table-striped table-advance table-hover no-left-margin">
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
	</div>
</body>
</html>
