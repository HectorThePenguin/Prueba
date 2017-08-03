<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckListGanadoMuerto.aspx.cs" Inherits="SIE.Web.Sanidad.CheckListGanadoMuerto" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headEvaluacion" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Checklist de Detección de Ganado Muerto <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
    </asp:PlaceHolder>


    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/ChecklistGanadoMuerto.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
    <script type="text/javascript">
        var hArete = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderArete %>"/>';
        var hAreteTestigo = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderAreteTestigo %>"/>';
        var hObservaciones = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderObservaciones %>"/>';
        var hCorral = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderCorral %>"/>';
        var hFechaDeteccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderFechaDeteccion %>"/>';
        var hHoraDeteccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderHoraDeteccion %>"/>';
        var hHoraRecoleccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderHoraRecoleccion %>"/>';
        var hSeleccione = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderSeleccione %>"/>';
        var hDetector = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderDetector %>"/>';
       
        var btnGuardarText = '<asp:Literal runat="server" Text="<%$ Resources:btnGuardar.Text %>"/>';
        var btnCancelarText = '<asp:Literal runat="server" Text="<%$ Resources:btnCancelar.Text %>"/>';

        var msgTitulo = '<asp:Literal runat="server" Text="<%$ Resources:CheckListGanadoMuerto_Title.Text %>"/>';
        var msgNoHayMuertesRecoleccion = '<asp:Literal runat="server" Text="<%$ Resources:msgNoHayMuertesRecoleccion.Text %>"/>';
        var msgFalloCargarDatos = '<asp:Literal runat="server" Text="<%$ Resources:msgFalloCargarDatos.Text %>"/>';
        var msgDlgCancelar = '<asp:Literal runat="server" Text="<%$ Resources:msgDlgCancelar.Text %>"/>';
        var msgDatosGuardadosExito = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardadosExito.Text %>"/>';
        var msgAreteNoDetectado = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteNoDetectado.Text %>"/>';
        var msgFalloAlGuardar = '<asp:Literal runat="server" Text="<%$ Resources:msgFalloAlGuardar.Text %>"/>';
        var msgNoTienePermiso = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTienePermiso.Text %>"/>';
        var msgAreteNoDetectadoMetalico = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteNoDetectadoMetalico.Text %>"/>';
        var msgMostrarMensajeAreteMarcadoPrevio = '<asp:Literal runat="server" Text="<%$ Resources:msgMostrarMensajeAreteMarcadoPrevio.Text %>"/>';
        var msgMostrarMensajeAreteTestigoMarcadoPrevio = '<asp:Literal runat="server" Text="<%$ Resources:msgMostrarMensajeAreteMarcadoPrevio.Text %>"/>';
    </script>
    <style>
        .scrollTablaAretes {
            height: 270px;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>
</head>
<body class="page-header-fixed" ondragstart="return false" draggable="false"
        ondragenter="event.dataTransfer.dropEffect='none'; event.stopPropagation(); event.preventDefault();"  
        ondragover="event.dataTransfer.dropEffect='none';event.stopPropagation(); event.preventDefault();"  
        ondrop="event.dataTransfer.dropEffect='none';event.stopPropagation(); event.preventDefault();">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-inline">
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="span12">
                        <div class="portlet box SuKarne2">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" />
                                    <span>
                                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="CheckListGanadoMuerto_Title"></asp:Label></span>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <ul class="breadcrumb">
                                            <li>
                                                <i class="icon-home"></i>
                                                <a href="../Principal.aspx">
                                                    <asp:Label ID="LabelHome" runat="server" meta:resourcekey="RecepcionGanadoMuerto_Home" /></a>
                                                <i class="icon-angle-right"></i>
                                            </li>
                                            <li>
                                                <a href="CheckListGanadoMuerto.aspx">
                                                    <asp:Label ID="LabelMenu" runat="server" meta:resourcekey="CheckListGanadoMuerto_Title"></asp:Label></a>
                                            </li>

                                        </ul>
                                    </div>
                                </div>
                                 <div class="row-fluid">
                                    <div class="span12">
                                        <asp:Label ID="lblOrganizacion" runat="server" CssClass="etiquetaOrganizacion"></asp:Label>
                                    </div>
                                </div>
                                <!-- Seccion de controles-->
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span8">
                                            <div class="span5">
                                                <div class="control-group">
                                                    <asp:Label ID="lblArete" runat="server" meta:resourcekey="lblArete" CssClass="control-label"></asp:Label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtArete" CssClass="span12" Enabled="True" type="tel"></asp:TextBox>
                                                    </div>
                                                </div>
                                                
                                            </div>
                                            <div class="span5">
                                                 <div class="control-group">
                                                    <asp:Label ID="lblAreteTestigo" runat="server" meta:resourcekey="lblAreteTestigo" CssClass="control-label"></asp:Label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtAreteTestigo" CssClass="span12" Enabled="True" type="tel"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span2" style="margin: 5px;">
                                                <div class="space12"></div>
                                                <button type="button" id="btnSeleccionarArete" class="btn SuKarne">
                                                    <i class="icon-search"></i>
                                                </button>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblNombre" runat="server" meta:resourcekey="lblNombre" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="control-group">
                                                <div class="span12">
                                                     <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha" CssClass="control-label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Tabla de datos -->
                                <div class="row-fluid">
                                	<div class="span12">
                                    	<div class="portlet">
                                        	<div class="portlet-body">
                                            	<div id="scroll" class="span12">
                                                	<div id="TablaAretes"></div>
                                            	</div> 
                                       		 </div>
                                    	</div>
                               	 	</div>
                            	</div>
                                <div class="modal-footer">
                                    <button class="btn SuKarne" id="btnGuardar" data-toggle="modal"></button>
                                    <button id="btnCancelar" class="btn SuKarne" data-toggle="modal"></button>
                                </div>
                            </div>
                            <div class="message-info">
                                <asp:Label ID="lblEstatus" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>