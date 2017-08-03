<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalidaIndividualPrincipal.aspx.cs" Inherits="SIE.Web.Sanidad.SalidaIndividualPrincipal" %>
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
    
    <script src="../Scripts/SalidaIndividualPrincipal.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
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
				                    <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="lblSalidaIndividualPrincipal_Home"/></a> 
					                <i class="icon-angle-right"></i>
				                </li>
                                <li>
					                <a href="SalidaIndividualPrincipal.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="SalidaIndividualPrincipal_Title"></asp:Label></a> 
				                </li>
			                </ul>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Seleccione</legend>
                                <div class="span12">
                                    <div class="span4">
                                        <div class="space10"></div>
                                        
                                        <div class="span12">
                                            <a href="SalidaIndividualRecuperacion.aspx" style="text-decoration: none;color:black;"><label><asp:RadioButton ID="rdSalidaRecuperacion" runat="server" GroupName="radios" TextAlign="Right"/>Salida por recuperación</label>
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
                                            <a href="SalidaIndividualVenta.aspx" style="text-decoration: none;color:black;"><label><asp:RadioButton ID="rdSalidaVenta" runat="server" GroupName="radios" TextAlign="Right"/>Salida por venta</label></a>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
