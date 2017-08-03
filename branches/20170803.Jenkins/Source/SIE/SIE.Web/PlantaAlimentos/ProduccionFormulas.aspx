<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProduccionFormulas.aspx.cs" Inherits="SIE.Web.PlantaAlimentos.ProduccionFormulas" %>
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
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />
    <script src="../Scripts/ProduccionFormulas.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
        var msgKilogramosMenor = '<asp:Literal runat="server" Text="<%$ Resources:msgKilogramosMenor.Text %>"/>';
        var msgKilogramosMayor = '<asp:Literal runat="server" Text="<%$ Resources:msgKilogramosMayor.Text %>"/>';
        var msgSinIngredientes = '<asp:Literal runat="server" Text="<%$ Resources:msgSinIngredientes.Text %>"/>';
        var msgDatosGuardados = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardados.Text %>"/>';
        var msgSinAlmacen = '<asp:Literal runat="server" Text="<%$ Resources:msgSinAlmacen.Text %>"/>';
        var msgSinInventarioProductos = '<asp:Literal runat="server" Text="<%$ Resources:msgSinInventarioProductos.Text %>"/>';
        var msgSinInventarioProductosVerificar = '<asp:Literal runat="server" Text="<%$ Resources:msgSinInventarioProductosVerificar.Text %>"/>';
        var msgErrorGrabar = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorGrabar.Text %>"/>';
        var msgSinKilogramosProducidos = '<asp:Literal runat="server" Text="<%$ Resources:msgSinKilogramosProducidos.Text %>"/>';
        var msgFechaInvalida = '<asp:Literal runat="server" Text="<%$ Resources:msgFechaInvalida.Text %>"/>';
        var lblMensajeCapturarKilogramos = '<asp:Literal runat="server" Text="<%$ Resources:lblMensajeCapturarKilogramos.Text %>"/>';
    </script>
    <style>
        #lblMensajeCapturarKilogramos {
            color: brown;
        }
    </style>
</head>
<body class="page-header-fixed">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
            <div class="container-fluid" >
                <div class="row-fluid">
                    <div class="span12">
                        <div class="portlet box SuKarne2">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png"/>
                                    <span class="letra">
                                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloProduccionFormulas"></asp:Label></span>
                                </div>
                            </div>
                            <div class="portlet-body form">
							    <ul class="breadcrumb">
				                    <li>
					                    <i class="icon-home"></i>
				                        <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="ProduccionFormulas_Home"/></a> 
					                    <i class="icon-angle-right"></i>
				                    </li>
                                    <li>
					                    <a href="ProduccionFormulas.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="ProduccionFormulas_Title"></asp:Label></a> 
				                    </li>
			                    </ul>

                                <div class="row-fluid">
                                    <fieldset class="scheduler-border span12">
                                        <legend class="scheduler-border"><asp:Label ID="Label1" runat="server" meta:resourcekey="lblFormulaProducida"></asp:Label></legend>
                                        <div class="space10"></div>
                                        <div class="span12 no-left-margin">
                                            <div class="span3">
                                                <div class="space10"></div>
                                                <span class="requerido">*</span>
                                                <asp:Label ID="lblRotomix" runat="server" meta:resourcekey="lblRotoMix"></asp:Label>
                                                <asp:DropDownList ID="cmbRotoMix" runat="server"/>
                                            </div>
                                            <div class="span2" style="text-align: right">
                                                <div class="space10"></div>
                                                <span class="requerido">*</span>
                                                <asp:Label ID="lblBatch" runat="server" meta:resourcekey="lblBatch"></asp:Label>
                                            </div>
                                            <div class="span4">
                                                <div class="space10"></div>                                                
                                                <asp:TextBox ID="txtBatch" runat="server" MaxLength="10" type="tel"  Enabled="False"/>
                                            </div>
                                        </div>
                                        <div class="span12 no-left-margin">
                                            <div class="span3">
                                                <div class="space10"></div>
                                                <span class="requerido">*</span>
                                                <asp:Label ID="lblFormula" runat="server" meta:resourcekey="lblFormula"></asp:Label>
                                                <asp:DropDownList ID="cmbFormula" runat="server"/>
                                            </div>
                                            <div class="span2" style="text-align: right">
                                                <div class="space10"></div>
                                                <span class="requerido">*</span>
                                                <asp:Label ID="lblKilogramosProducidos" runat="server" meta:resourcekey="lblKilogramosProducidos"></asp:Label>
                                            </div>
                                            <div class="span4">
                                                <div class="space10"></div>                                                
                                                <asp:TextBox ID="txtKilogramosProducidos" runat="server" MaxLength="10" type="tel"/>
                                            </div>
                                            <div class="span3">
                                                <div class="space10"></div>
                                                <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha"></asp:Label>
                                                <asp:TextBox ID="txtFecha" runat="server" Enabled="false" MaxLength="10"/>
                                            </div>
                                        </div>
                                        <div class="space10"></div>
                                        <div class="space15"></div>
                                    </fieldset>
                                    
                                    <div class="space10"></div>
                                    <fieldset class="scheduler-border span12">
                                        <legend class="scheduler-border"><asp:Label ID="lblIngredientes" runat="server" meta:resourcekey="lblIngredientes"></asp:Label></legend>
                                        <asp:Label ID="lblMensajeCapturarKilogramos" runat="server" meta:resourcekey="lblMensajeCapturarKilogramos" CssClass="negritas"></asp:Label>
                                        <div class="space10"></div>
                                        <div class="span12 no-left-margin">
                                            <div class="span12 no-left-margin">
                                                <table id="tbIngredientes" class="table table-striped table-advance table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th class="span6 alineacionCentro" style="display: none;" scope="col">IngredienteId</th>
                                                            <th class="span6 alineacionCentro" style="display: none;" scope="col">ProductoId</th>
                                                            <th class="span6 alineacionCentro" scope="col"><asp:Label ID="lblIngrediente" runat="server" meta:resourcekey="HeaderIngrediente"></asp:Label></th>
                                                            <th class="span6 alineacionCentro" scope="col"><asp:Label ID="lblKilogramosIngrediente" runat="server" meta:resourcekey="HeaderKilogramosIngrediente"></asp:Label></th>
                                                            <th class="span6 alineacionCentro" style="display: none;" scope="col"><asp:Label ID="Label2" runat="server" meta:resourcekey="HeaderPorcentajeIngredienteProducido"></asp:Label></th>
                                                            <th class="span6 alineacionCentro" style="display: none;" scope="col"><asp:Label ID="Label3" runat="server" meta:resourcekey="HeaderPorcentajeIngredienteRequerido"></asp:Label></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
                                            </div>
                                            <div class="space20"></div>
                                            <div class="span12 no-left-margin">
                                                <div class="pull-right">
                                                    <asp:Label ID="lblTotalKilogramosIngrediente" runat="server" meta:resourcekey="lblTotalKilogramosIngrediente"></asp:Label>
                                                    <asp:TextBox ID="txtTotalKilogramosIngrediente" runat="server" Enabled="False"/>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    
                                   
                                    <div id="dlgDiferencias" class="modal hide fade" style="margin-top: -200px;"  tabindex="-1" data-backdrop="static" data-keyboard="false">
                                        <div class="portlet box SuKarne2">
							                <div class="portlet-title">
                                                <div class="caption">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                                    <span class="letra">
                                                        <asp:Label ID="lblTituloDialogoDiferencias" runat="server" meta:resourcekey="lblTituloDialogoDiferencias"></asp:Label></span>
                                                </div>
                                            </div>
                                            <div class="portlet-body form">
							                    <div class="modal-body">
							                         <fieldset class="scheduler-border span12">
                                                        <legend class="scheduler-border"><asp:Label ID="Label4" runat="server" meta:resourcekey="lblIngredientes"></asp:Label></legend>
							                            <div class="span12 no-left-margin">
                                                            <table id="tbIngredientesDiferencias" class="table table-striped table-advance table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="span6 alineacionCentro" style="display: none;" scope="col">IngredienteId</th>
                                                                        <th class="span6 alineacionCentro" style="display: none;" scope="col">ProductoId</th>
                                                                        <th class="span6 alineacionCentro" scope="col"><asp:Label ID="Label5" runat="server" meta:resourcekey="HeaderIngrediente"></asp:Label></th>
                                                                        <th class="span6 alineacionCentro" scope="col"><asp:Label ID="Label6" runat="server" meta:resourcekey="HeaderKilogramosIngrediente"></asp:Label></th>
                                                                        <th class="span6 alineacionCentro" scope="col"><asp:Label ID="Label7" runat="server" meta:resourcekey="HeaderPorcentajeIngredienteProducido"></asp:Label></th>
                                                                        <th class="span6 alineacionCentro" scope="col"><asp:Label ID="Label8" runat="server" meta:resourcekey="HeaderPorcentajeIngredienteRequerido"></asp:Label></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody></tbody>
                                                            </table>
                                                        </div>
                                                    </fieldset>
								                    <asp:Button runat="server" ID="btnAceptar" CssClass="btn SuKarne" meta:resourcekey="btnAceptar" data-dismiss="modal"/>
							                    </div>
                                            </div>
                                        </div>
						            </div>
                                    
                                    
                                    <div id="dlgResumenProduccionFormulas" class="modal hide fade" style="margin-top: 1px;"  tabindex="-1" data-backdrop="static" data-keyboard="false">
                                        <div class="portlet box SuKarne2">
							                <div class="portlet-title">
                                                <div class="caption">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                                    <span class="letra">
                                                        <asp:Label ID="Label9" runat="server" meta:resourcekey="lblTituloDialogoResumen"></asp:Label></span>
                                                </div>
                                            </div>
                                            <div class="portlet-body form">
							                    <div class="modal-body" style="text-align: right">
							                         <fieldset class="scheduler-border span12">
							                            <div class="span12 no-left-margin">
                                                            <table id="tbResumenFormulas" class="table table-striped table-advance table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="span4 alineacionCentro" scope="col"><asp:Label ID="lblResumenDescripcion" runat="server" meta:resourcekey="HeaderResumenDescripcion"></asp:Label></th>
                                                                        <th class="span4 alineacionCentro" scope="col"><asp:Label ID="lblResumenCantidad" runat="server" meta:resourcekey="HeaderResumenCantidad"></asp:Label></th>
                                                                        <th class="span4 alineacionCentro" scope="col"><asp:Label ID="lblResumenCantidadReparto" runat="server" meta:resourcekey="HeaderResumenCantidadReparto"></asp:Label></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody></tbody>
                                                            </table>
                                                        </div>
                                                    </fieldset>
								                         <asp:Button runat="server" ID="btnAceptarResumen" CssClass="btn SuKarne" meta:resourcekey="btnAceptarResumen" data-dismiss="modal"/>
							                    </div>
                                            </div>
                                        </div>
						            </div>
                                    
                                    <!-- Boton aceptar y boton cancelar -->
                                    <div class="row-fluid">
                                        <div class="span4 pull-right">
                                            <div style="text-align:right;">
                                                <a id="btnGuardar" class="btn SuKarne">Guardar</a>
                                                <a id="btnCancelar" class="btn SuKarne" href="#dlgCancelar" data-toggle="modal">Cancelar</a>
                                            </div>
                                        </div>
                                    </div>

                                    <!--Dialogo Cancelar-->
                                    <div id="dlgCancelar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
							            <div class="modal-body">
								            <asp:Label ID="lblMensajeDialogo" runat="server" meta:resourcekey="msgCancelar"></asp:Label>
							            </div>
							            <div class="modal-footer">
								            <asp:Button runat="server" ID="btnDialogoSi" CssClass="btn SuKarne" meta:resourcekey="btnDialogoSi" data-dismiss="modal"/>
                                            <asp:Button runat="server" ID="btnDialogoNo" CssClass="btn SuKarne" meta:resourcekey="btnDialogoNo" data-dismiss="modal"/>
							            </div>
						            </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div> 
</body>
</html>
