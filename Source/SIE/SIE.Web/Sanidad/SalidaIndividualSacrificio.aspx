<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalidaIndividualSacrificio.aspx.cs" Inherits="SIE.Web.Sanidad.SalidaIndividualSacrificio" %>
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
    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/css/style-metro.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css"/>
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-modal.css" rel="stylesheet" />
    
    <script src="../Scripts/SalidaIndividualSacrificio.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
        var msgNoExisteArete = '<asp:Literal runat="server" Text="<%$ Resources:msgNoExisteArete.Text %>"/>';
        var msgNoEnfermeria = '<asp:Literal runat="server" Text="<%$ Resources:msgNoEnfermeria.Text %>"/>';
        var msgCronicoVentaMuerte = '<asp:Literal runat="server" Text="<%$ Resources:msgCronicoVentaMuerte.Text %>"/>';
        var msgNoEncontroCorral = '<asp:Literal runat="server" Text="<%$ Resources:msgNoEncontroCorral.Text %>"/>';
        var msgNoCorraletaSacrificio = '<asp:Literal runat="server" Text="<%$ Resources:msgNoCorraletaSacrificio.Text %>"/>';
        var msgCorralNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:msgCorralNoExiste.Text %>"/>';
        var msgDatosBlanco = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosBlanco.Text %>"/>';
        var msgDatosGuardados = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardados.Text %>"/>';
        var msgOcurrioErrorGrabar = '<asp:Literal runat="server" Text="<%$ Resources:msgOcurrioErrorGrabar.Text %>"/>';
        var msgAreteVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteVacio.Text %>"/>';
        var msgCorraletaVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgCorraletaVacio.Text %>"/>';
        var msgPesoNoValido = '<asp:Literal runat="server" Text="<%$ Resources:msgPesoNoValido.Text %>"/>';
        var msgAreteSalida = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteSalida.Text %>"/>';
        var msg30Dias = '<asp:Literal runat="server" Text="<%$ Resources:msg30Dias.Text %>"/>';
        var msgNoTienePermiso = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTienePermiso.Text %>"/>';
    </script>
    <style type="text/css">
        input[type=radio] {
            margin-top: 0;
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
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1"></asp:Label></span>
                            </div>
                        </div>
                        <div class="portlet-body form">
							<ul class="breadcrumb">
				                <li>
					                <i class="icon-home"></i>
				                    <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="lblSalidaIndividualRecuperacion_Home"/></a> 
					                <i class="icon-angle-right"></i>
				                </li>
                                <li>
					                <a href="SalidaIndividualPrincipal.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="SalidaIndividualRecuperacion_Title"></asp:Label></a> 
				                </li>
			                </ul>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Seleccione</legend>
                                <div class="span12">
                                    <div class="span4">
                                        <div class="space10"></div>
                                        
                                        <div class="span12">
                                            <a href="SalidaIndividualRecuperacion.aspx" style="text-decoration: none;color:black;"><label><asp:RadioButton ID="rdSalidaRecuperacion" runat="server" GroupName="radios" TextAlign="Right"/>Salida por recuperación</label></a>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="space10"></div>

                                        <div class="span12">
                                            <label><asp:RadioButton ID="rdSalidaSacrificio" runat="server" Enabled="false" GroupName="radios" Checked="true" TextAlign="Right"/>Salida por sacrificio</label>
                                        </div>
                                    </div>
                                    
                                    <div class="span4">
                                        <div class="space10"></div>

                                        <div class="span12">
                                            <a href="SalidaIndividualVenta.aspx" style="text-decoration: none;color:black;"><label><asp:RadioButton ID="rdSalidaVenta" runat="server" GroupName="radios" TextAlign="Right"/>Salida por venta</label></a>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Salida individual ganado - por sacrificio</legend>
                            <div class="span12">
                                <div class="span4">
                                    <div class="space10"></div>

                                    <!--Salida-->
                                    <div class="span4">
                                        <asp:Label ID="lblSalida" runat="server" meta:resourcekey="lblEtiquetaSalidaResource1"></asp:Label>
                                    </div>
                                    <div class="span7">
                                        <asp:DropDownList ID="cmbSalida" runat="server" ReadOnly="true" class="span12"></asp:DropDownList>
                                    </div>
                                    
                                    <div class="space10"></div>

                                    <!--Causa-->
                                    <div class="span4">
                                        <asp:Label ID="lblCausa" runat="server" meta:resourcekey="lblEtiquetaCausaResource1"></asp:Label>
                                    </div>
                                    <div class="span7">
                                        <asp:DropDownList ID="cmbCausa" runat="server" ReadOnly="true" class="span12"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="span4">
                                    <div class="space10"></div>
                                    
                                    <!--Arete-->
                                    <div class="span4">
                                        <span class="requerido">*</span>
                                        <asp:Label ID="lblArete" runat="server" meta:resourcekey="lblEtiquetaAreteResource1"></asp:Label>
                                    </div>
                                    <div class="span7">
                                        <input id="txtArete" class="span12 text-right" type="tel"/>
                                        <%--<asp:TextBox ID="txtArete" runat="server" class="span12"></asp:TextBox>--%>
                                    </div>

                                    <div class="space10"></div>

                                    <!--Corral-->
                                    <div class="span4">
                                        <asp:Label ID="lblCorral" runat="server" meta:resourcekey="lblEtiquetaCorralResource1"></asp:Label>
                                    </div>
                                    <div class="span7">
                                        <asp:TextBox ID="txtCorral" runat="server" ReadOnly="True" class="span12"></asp:TextBox>
                                    </div>
                                    
                                    <div class="space10"></div>
                                    
                                    <div class="span12">
                                        <asp:Label ID="lblTituloSacrificio" CssClass="titulo" runat="server" meta:resourcekey="lblEtiquetaTituloSacrificioResource1"></asp:Label>
                                    </div>
                                </div>
                                
                                <div class="span4 no-left-margin">
                                    <div class="space10"></div>
                                    
                                    <!--Fecha-->
                                    <div class="span4">
                                        <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblEtiquetaFechaResource1"></asp:Label>
                                    </div>
                                    <div class="span7">
                                        <asp:TextBox ID="txtFecha" runat="server" ReadOnly="true" class="span12"></asp:TextBox>
                                    </div>
                                    
                                    <div class="space10"></div>
                                    
                                    <!--Corraleta-->
                                    <div class="span4">
                                        <span class="requerido">*</span>
                                        <asp:Label ID="lblCorraleta" runat="server" meta:resourcekey="lblEtiquetaCorraletaResource1"></asp:Label>
                                    </div>
                                    <div class="span7">
                                        <asp:DropDownList ID="cmbCorraletas" runat="server" ReadOnly="true" class="span12"></asp:DropDownList>
                                    </div>
                                    
                                    <div class="space10"></div>
                                    
                                    <!--Peso-->
                                    <div class="span4">
                                        <asp:Label ID="lblPesoProyectado" runat="server" meta:resourcekey="lblEtiquetaPesoProyectadoResource1"></asp:Label>
                                    </div>
                                    <div class="span7">
                                        <asp:TextBox ID="txtPesoProyectado" runat="server" ReadOnly="True" class="span12"></asp:TextBox>
                                        <input type="hidden" id="txtPeso" value="0"/>
                                    </div>
                                </div>
                            </div>
                            </fieldset>
                            <div class="space15"></div>

                            <div class="span12">
                                <div class="span4 pull-right">
                                    <div class="span4"></div>
                                    <table>
                                        <tr>
                                            <td>
                                                <a id="btnGuardar" tabindex="2" class="btn SuKarne" data-toggle="modal">Guardar</a>
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
                        </div>
                        <!--Dialogo de Cancelacion -->
                        <div id="dlgCancelar" class="modal hide fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
							<div class="modal-body">
								<asp:Label ID="lblMensajeDialogo" runat="server" meta:resourcekey="lblMensajeDialogo"></asp:Label>
							</div>
							<div class="modal-footer">
								<asp:Button runat="server" ID="btnDialogoSi" CssClass="btn SuKarne" meta:resourcekey="btnDialogoSi" data-dismiss="modal"/>
                                <asp:Button runat="server" ID="btnDialogoNo" CssClass="btn SuKarne" meta:resourcekey="btnDialogoNo" data-dismiss="modal"/>
							</div>
						</div>
                    </div>
                </div>
            </div>
        </form>
     </div>
</body>
</html>