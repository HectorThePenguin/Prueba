<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalidaPorMuerteSeleccion.aspx.cs" Inherits="SIE.Web.Sanidad.SalidaPorMuerteSeleccion" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headEvaluacion" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
    </asp:PlaceHolder>

    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../assets/plugins/fancybox/source/jquery.fancybox.js"></script>
    <link rel="stylesheet" href="../assets/plugins/fancybox/source/jquery.fancybox.css" type="text/css" media="screen" />
    <script src="../Scripts/SalidaPorMuerteSeleccion.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
    
    <script type="text/javascript">
        var txtBtnGuardar = '<asp:Literal runat="server" Text="<%$ Resources:btnGuardar.Text %>"/>';
        var txtBtnCancelar = '<asp:Literal runat="server" Text="<%$ Resources:btnCancelar.Text %>"/>';
        var msgDlgCancelar = '<asp:Literal runat="server" Text="<%$ Resources:lblMensajeDialogo.Text %>"/>';
        var msgTitulo = '<asp:Literal runat="server" Text="<%$ Resources:SalidaPorMuerte_Title.Text %>"/>';
        var btnGuardarText = '<asp:Literal runat="server" Text="<%$ Resources:btnGuardar.Text %>"/>';
        var btnCancelarText = '<asp:Literal runat="server" Text="<%$ Resources:btnCancelar.Text %>"/>';
        var msgSinImagen = '<asp:Literal runat="server" Text="<%$ Resources:msgSinImagen.Text %>"/>';
        var msgGuardadoExito = '<asp:Literal runat="server" Text="<%$ Resources:msgGuardadoExito.Text %>"/>';
        var msgGuardadoFallido = '<asp:Literal runat="server" Text="<%$ Resources:msgGuardadoFallido.Text %>"/>';
        var msgNoHayProblemas = '<asp:Literal runat="server" Text="<%$ Resources:msgNoHayProblemas.Text %>"/>';
        var msgSeleccionarProblema = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionarProblema.Text %>"/>';
        var msgFalloAlCargarDatos = '<asp:Literal runat="server" Text="<%$ Resources:msgFalloAlCargarDatos.Text %>"/>';
        var msgObservacionesObligatorias = '<asp:Literal runat="server" Text="<%$ Resources:msgObservacionesObligatorias.Text %>"/>';
    </script>
</head>
<body class="page-header-fixed" ondragstart="return false" draggable="false"
        ondragenter="event.dataTransfer.dropEffect='none'; event.stopPropagation(); event.preventDefault();"  
        ondragover="event.dataTransfer.dropEffect='none';event.stopPropagation(); event.preventDefault();"  
        ondrop="event.dataTransfer.dropEffect='none';event.stopPropagation(); event.preventDefault();">
    <div id="pagewrap" class="contenedor-salidapormuerte">
        <form id="idform" runat="server" class="form-horizontal">
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="span12">
                        <div class="portlet box SuKarne2">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" />
                                    <span>
                                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="SalidaPorMuerteSeleccion_Title"></asp:Label></span>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <ul class="breadcrumb">
                                            <li>
                                                <i class="icon-home"></i>
                                                <a href="../Principal.aspx">
                                                    <asp:Label ID="LabelHome" runat="server" meta:resourcekey="SalidaPorMuerteSeleccion_Home" /></a>
                                                <i class="icon-angle-right"></i>
                                            </li>
                                            <li>
                                                <a href="SalidaPorMuerte.aspx">
                                                    <asp:Label ID="LabelMenu" runat="server" meta:resourcekey="SalidaPorMuerte_Title"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span6">
                                            <asp:Label ID="lblOrganizacion" runat="server" CssClass="etiquetaOrganizacion"></asp:Label>
                                        </div>
                                        <div class="span6">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                   <asp:Label ID="lblFechaSalida" runat="server" meta:resourcekey="lblFechaSalida" CssClass="pull-right"></asp:Label>
                                                </div>
                                            </div>
                                             <div class="row-fluid">
                                                <div class="span12">
                                                   <asp:Label ID="lblHoraSalida" runat="server" meta:resourcekey="lblHoraSalida" CssClass="pull-right"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                 
                                <div class="portlet">
								    <div class="portlet-title line">
                                      
								    </div>
								    <div class="portlet-body from form-inline" id="frmSalidaPorMuerte">
								        <div class="row-fluid">
                                            <div class="span12">
								                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblCorral" runat="server" meta:resourcekey="lblCorral" CssClass="control-label"></asp:Label>
                                                        <div class="controls">
                                                            <asp:HiddenField runat="server" ID="hdAnimalId"/>
                                                            <asp:HiddenField runat="server" ID="hdMuerteId"/>
                                                            <asp:HiddenField runat="server" ID="hdLoteId"/>
                                                            <asp:HiddenField runat="server" ID="hdCorralId"/>
                                                            <asp:TextBox runat="server" ID="txtCorral" 
                                                                CssClass="medium span12" Enabled="False" />
                                                        </div>
                                                    </div>
                                                    <div class="control-group">
                                                        <asp:Label ID="lblPeso" runat="server" meta:resourcekey="lblPeso" CssClass="control-label"></asp:Label>                                                        
                                                        <div class="controls">
                                                            <asp:TextBox runat="server" ID="txtPeso" CssClass="medium span8" Enabled="False"></asp:TextBox>
                                                            <span class="help-inline">Kgs.</span>
                                                        </div>
                                                    </div> 
                                                    <div class="control-group">
                                                        <asp:Label ID="lblFotoDeteccion" runat="server" meta:resourcekey="lblFotoDeteccion" CssClass="control-label"></asp:Label>                                                        <div class="controls">
                                                            <div class="imagen-deteccion">
                                                            <output id="outputDeteccion">
                                                                <a id="imageDetect" href="#">
                                                                 <asp:Image runat="server" ID="imgFotoDeteccion" CssClass="fancybox-image"/>
                                                                </a>
                                                            </output>
                                                            </div>
                                                        </div>
                                                    </div>
								                </div>

                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblNumeroArete" runat="server" meta:resourcekey="lblNumeroArete" CssClass="control-label"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox runat="server" ID="txtNumeroArete" CssClass="medium span12" Enabled="False"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="control-group">
                                                        <asp:Label ID="lblProblema" runat="server" meta:resourcekey="lblProblema" CssClass="control-label"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList runat="server" ID="cmbProblemas" CssClass="medium span12"/>
                                                        </div>
                                                    </div>
                                                   <div class="control-group">
                                                        <div class="controls">
                                                            <div class="span12">
                                                                <a id="btnTomarFoto" class="btn SuKarne" data-togle="modal">
                                                                    <img src="../Images/camara.png" width="25" height="22"/>
                                                                    <asp:Label ID="lblTomarFoto" runat="server" meta:resourcekey="lblEtiquetaTomarFotoResource1"></asp:Label>
                                                                </a>
                                                            </div>
                                                             <div class="control-group">
                                                                <div class="controls">
                                                                    <input type="file" id="files" name="files[]" style="display: none;"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblAreteMetalico" runat="server" meta:resourcekey="lblAreteMetalico" CssClass="control-label"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox runat="server" ID="txtAreteMetalico" CssClass="medium span12" Enabled="False"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="control-group">
                                                       <asp:Label ID="lblNombreResposable" runat="server" meta:resourcekey="lblNombreResposable" CssClass="control-label"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox runat="server" ID="txtNombreResponsable" CssClass="medium span12" Enabled="False"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="control-group">
                                                       <asp:Label ID="lblFotoNecropsia" runat="server" meta:resourcekey="lblFotoNecropsia" CssClass="control-label"></asp:Label>
                                                       <div class="controls">
                                                        <div class="imagen-deteccion">
                                                        <output id="list"></output>
                                                       </div>
                                                      </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    <div class="portlet">
								        <div class="portlet-title line">
								            <span class="requerido">*</span>
                                            <asp:Label ID="lblObservaciones" runat="server" meta:resourcekey="lblObservaciones"></asp:Label>
								        </div>
								        <div class="portlet-body" id="divObservaciones">
                                             <div class="row-fluid">
                                                <div class="span12 line">
                                                    
                                                    <asp:TextBox runat="server" id="txtObservaciones" MaxLength="255" TextMode="MultiLine" Rows="2" CssClass="span12" style="-moz-resize:none; -ms-resize:none; -o-resize:none; resize:none"></asp:TextBox>
                                                </div>
                                            </div>
								        </div>
							    </div>
							<!-- END PORTLET-->
                                <div class="modal-footer">
                                    <button type="button" class=" btn SuKarne" id="btnGuardar" data-toggle="modal"></button>
                                    <button type="button" id="btnCancelar" class="btn SuKarne" data-toggle="modal"></button>
                                </div>
                                </div>
                                <div class="message-info">
                                    <asp:Label ID="lblEstatus" runat="server"></asp:Label>
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
