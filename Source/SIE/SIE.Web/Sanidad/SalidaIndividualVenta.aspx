<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalidaIndividualVenta.aspx.cs" Inherits="SIE.Web.Sanidad.SalidaIndividualVenta" culture="auto" uiculture="auto" %>
<%@ Import Namespace="System.Web.Optimization" %>

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
    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/css/style-metro.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css"/>
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-modal.css" rel="stylesheet" />

    <script src="../assets/plugins/fancybox/source/jquery.fancybox.js"></script>
    <link rel="stylesheet" href="../assets/plugins/fancybox/source/jquery.fancybox.css" type="text/css" media="screen" />

    <script src="../Scripts/SalidaIndividualVenta.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    
    <script type="text/javascript">
        javascript: window.history.forward(1);

        var msgTicketInvalido = '<asp:Literal runat="server" Text="<%$ Resources:msgTicketInvalido.Text %>"/>';
        var msgCorralVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgCorralVacio.Text %>"/>';
        var msgCorralNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:msgCorralNoExiste.Text %>"/>';
        var msgNoVentaCronico = '<asp:Literal runat="server" Text="<%$ Resources:msgNoVentaCronico.Text %>"/>';
        var msgNoMaquilaIntensivo = '<asp:Literal runat="server" Text="<%$ Resources:msgNoMaquilaIntensivo.Text %>"/>';
        var msgNoTienePermiso = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTienePermiso.Text %>"/>';
        var msgSinFoto = '<asp:Literal runat="server" Text="<%$ Resources:msgSinFoto.Text %>"/>';
        var msgSinArete = '<asp:Literal runat="server" Text="<%$ Resources:msgSinArete.Text %>"/>';
        var msgAretesMaximo = '<asp:Literal runat="server" Text="<%$ Resources:msgAretesMaximo.Text %>"/>';
        var msgCapturarCabezas = '<asp:Literal runat="server" Text="<%$ Resources:msgCapturarCabezas.Text %>"/>';
        var msgAreteCapturado = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteCapturado.Text %>"/>';
        var msgAreteNoDelCorral = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteNoDelCorral.Text %>"/>';
        var msgDatosGuardados = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardados.Text %>"/>';
        var msgOcurrioErrorGrabar = '<asp:Literal runat="server" Text="<%$ Resources:msgOcurrioErrorGrabar.Text %>"/>';
        var msgDatosBlanco = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosBlanco.Text %>"/>';
        var msgAretesDifCabezas = '<asp:Literal runat="server" Text="<%$ Resources:msgAretesDifCabezas.Text %>"/>';
        var msgAreteSalida = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteSalida.Text %>"/>';
        var msgCabezasMayorLote = '<asp:Literal runat="server" Text="<%$ Resources:msgCabezasMayorLote.Text %>"/>';
    </script>
    <style type="text/css">
        input[type=radio] {
            margin-top: 0;
        }
        #txtCorral {
            text-transform: uppercase;
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
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" />
                                <span class="letra">
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1"></asp:Label></span>
                            </div>
                        </div>
                        <div class="portlet-body form">
							<ul class="breadcrumb">
				                <li>
					                <i class="icon-home"></i>
				                    <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="lblSalidaIndividualVenta_Home"/></a> 
					                <i class="icon-angle-right"></i>
				                </li>
                                <li>
					                <a href="SalidaIndividualPrincipal.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="SalidaIndividualVenta_Title"></asp:Label></a> 
				                </li>
			                </ul>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Seleccione</legend>
                                <div class="span12">
                                    <div class="span4">
                                        <div class="space10"></div>
                                        
                                        <div class="span12">
                                            <a href="SalidaIndividualRecuperacion.aspx" style="text-decoration: none;color:black;"><label><asp:RadioButton ID="rdSalidaRecuperacion" runat="server"  GroupName="radios" TextAlign="Right"/>Salida por recuperación</label></a>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="space10"></div>

                                        <div class="span12">
                                            <a href="SalidaIndividualSacrificio.aspx" style="text-decoration: none;color:black;"><label><asp:RadioButton ID="rdSalidaSacrificio" runat="server" GroupName="radios" TextAlign="Right"/>Salida por sacrificio</label></a>
                                        </div>
                                    </div>
                                    
                                    <div class="span4">
                                        <div class="space10"></div>

                                        <div class="span12">
                                            <label><asp:RadioButton ID="rdSalidaVenta" runat="server" Enabled="false"  Checked="true" GroupName="radios" TextAlign="Right"/>Salida por venta</label>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Salida por venta</legend>
                                <div class="span12">
                                     <div class="span5">
                                         <div class="space10"></div>
                                    
                                        <!--Tipo Venta-->
                                        <div class="span4">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblTipoVenta" runat="server" meta:resourcekey="lblTipoVenta"></asp:Label>
                                        </div>
                                        <div class="span7">
                                            <asp:DropDownList ID="ddlTipoVenta" runat="server" class="span12">
                                                <asp:ListItem Text="<%$ Resources:ddlTipoVentaPropio.Text %>" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="<%$ Resources:ddlTipoVentaExterno.Text %>" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                         <div class="space10"></div>

                                        <!--Folio Ticket-->
                                        <div class="span4">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblTicket" runat="server" meta:resourcekey="lblEtiquetaTicketResource1"></asp:Label>
                                        </div>
                                        <div class="span7">
                                            <asp:TextBox ID="txtTicket" runat="server" class="span12 textoDerecha" type="tel"></asp:TextBox>
                                        </div>

                                         <div class="space10"></div>
                                    

                                        <!--Corral-->
                                        <div class="span4">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblCorral" runat="server" meta:resourcekey="lblEtiquetaCorralResource1"></asp:Label>
                                        </div>
                                        <div class="span7">
                                            <asp:TextBox ID="txtCorral" runat="server" class="span12"></asp:TextBox>
                                        </div>
                                         
                                        <div class="space10"></div>
                                         
                                        <!--Causa-->
                                        <div class="span4">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblCausa" runat="server" meta:resourcekey="lblEtiquetaCausaResource1"></asp:Label>
                                        </div>
                                        <div class="span7">
                                            <asp:DropDownList ID="cmbCausa" runat="server" class="span12"></asp:DropDownList>
                                        </div>
                                         
                                        <div class="space10"></div>
                                         
                                        <!--Precio-->
                                        <div class="span4">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblPrecio" runat="server" meta:resourcekey="lblEtiquetaPrecioResource1"></asp:Label>
                                        </div>
                                        <div class="span7">
                                            <asp:DropDownList ID="cmbPrecio" runat="server" class="span12"></asp:DropDownList>
                                        </div>
                                         
                                        <div class="space10"></div>
                                    
                                        <!--Cabezas vendidas-->
                                        <div class="span4">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblCabezasVendidas" runat="server" meta:resourcekey="lblEtiquetaCabezasVendidasResource1"></asp:Label>
                                        </div>
                                        <div class="span7">
                                            <asp:TextBox ID="txtCabezasVendidas" runat="server" class="span12 textoDerecha" type="tel"></asp:TextBox>
                                        </div>
                                     </div>
                                    
                                    <div class="span6">
                                        <!--No. individual-->
                                        <div class="space10"></div>
                                        <div class="span3">
                                            <span class="requerido" id="lblReqArete">*</span>
                                            <asp:Label ID="lblNoIndividual" runat="server" meta:resourcekey="lblEtiquetaNoIndividualResource1"></asp:Label>
                                        </div>
                                        <div class="span4">
                                            <input id="txtNoIndividual" class="span12 textoDerecha" type="tel"/>
                                        </div>
                                        
                                        <div class="span2">
                                            <a id="btnFoto" class="btn SuKarne" data-toggle="modal"><img src="../Images/camara.png" width="20" height="20"/></a>
                                        </div>
                                        
                                        <div class="span2">
                                            <a id="btnAgregar" class="btn SuKarne" data-toggle="modal">Agregar</a>
                                        </div>
                                        <div class="space10"></div>
                                        <div class="span3">
                                            <span class="requerido" id="lblReqAreteRFID">*</span>
                                            <asp:Label ID="lblAreteRFID" runat="server" meta:resourcekey="lblEtiquetaAreteRFIDResource"></asp:Label>
                                        </div>
                                        <div class="span4">
                                           <input id="txtAreteRFID" class="span12 textoDerecha" type="tel"/>
                                        </div>

                                        <div class="control-group">
                                            <div class="controls">
                                                <input type="file" id="flFoto" name="fotoGanado[]" style="display: none;"/>
                                            </div>
                                        </div>
                                        
                                        

                                        
                                        <div class="space10"></div>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <table id="aretes" class="table table-striped table-advance table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th scope="col" class="span1 alineacionCentro">
                                                                <asp:Label ID="lblID" runat="server" meta:resourcekey="lblEtiquetaNoResource1"></asp:Label>
                                                            </th>
                                                            <th scope="col" class="alineacionCentro">
                                                                <asp:Label ID="lblNoIndividualGrid" runat="server" meta:resourcekey="lblEtiquetaNoIndividualResource2"></asp:Label>
                                                            </th>
                                                            <th scope="col" class="alineacionCentro">
                                                                <asp:Label ID="lblAreteRFIDGrid" runat="server" meta:resourcekey="lblEtiquetaAreteRFIDResource"></asp:Label>
                                                            </th>
                                                            <th scope="col" class="alineacionCentro">
                                                                <asp:Label ID="lblfoto" runat="server" meta:resourcekey="lblEtiquetaFotoResource1"></asp:Label>
                                                            </th>
                                                            <th scope="col" class="alineacionCentro">
                                                                <asp:Label ID="lblOpcion" runat="server" meta:resourcekey="lblEtiquetaOpcionResource1"></asp:Label>
                                                            </th>
                                                            <th scope="col" class="alineacionCentro" style="visibility: hidden;">
                                                                <asp:Label ID="CargaInicial" runat="server"></asp:Label>
                                                            </th>
                                                            <th scope="col" class="alineacionCentro" style="visibility: hidden;">
                                                                <asp:Label ID="lblCausaPrecio" runat="server"></asp:Label>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td style="visibility: hidden;">
                                                               &nbsp;
                                                            </td>
                                                            <td style="visibility: hidden;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td style="visibility: hidden;">&nbsp;</td>
                                                            <td style="visibility: hidden;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td style="visibility: hidden;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="visibility: hidden;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td style="visibility: hidden;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="visibility: hidden;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td style="visibility: hidden;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="visibility: hidden;">
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            
                            <div class="span12">
                                <div class="span4 pull-right">
                                    <div class="span4"></div>
                                    <table>
                                        <tr>
                                            <td>
                                                <a id="btnGuardar" href="#idConfirmacion" class="btn SuKarne" data-toggle="modal">Guardar</a>
                                            </td>
                                            <td>
                                               <a id="btnCancelar" class="btn SuKarne" >Cancelar</a>
                                                <a id="aCancelar" href="#dlgCancelar" data-toggle="modal"></a>
                                                <input id="hIr" type="hidden" value=""/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="space15"></div>
                            
                            <!--Dialogo de Cancelacion -->
                            <div id="dlgCancelar" class="modal hide fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
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
        </form>
    </div>
</body>
</html>
