<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalidaPorMuerte.aspx.cs" Inherits="SIE.Web.Sanidad.SalidaPorMuerte" %>

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
    <script src="../Scripts/SalidaPorMuerte.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
    <script type="text/javascript">
        var hCorral = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderCorral %>"/>';
        var hArete = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderArete %>"/>';
        var hAreteMetalico = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderAreteMetalico %>"/>';
        var hFecha = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderFecha %>"/>';
        var hOpcion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderOpcion %>"/>';
        var msgNoHayMuertes = '<asp:Literal runat="server" Text="<%$ Resources:msgNoHayMuertes.Text %>"/>';
        var msgFalloCargarDatos = '<asp:Literal runat="server" Text="<%$ Resources:msgFalloCargarDatos.Text %>"/>';
        var msgSalidaPorMuerteTitulo = '<asp:Literal runat="server" Text="<%$ Resources:SalidaPorMuerte_Title.Text %>"/>';
        var msgNoTienePermiso = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTienePermiso.Text %>"/>';
    </script>
</head>
<body class="page-header-fixed">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="span12">
                        <div class="portlet box SuKarne2">

                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" />
                                    <span>
                                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="SalidaPorMuerte_Title"></asp:Label></span>
                                </div>
                            </div>

                            <div class="portlet-body form">

                                <div class="row-fluid">
                                    <div class="span12">
                                        <ul class="breadcrumb">
                                            <li>
                                                <i class="icon-home"></i>
                                                <a href="../Principal.aspx">
                                                    <asp:Label ID="LabelHome" runat="server" meta:resourcekey="SalidaPorMuerte_Home" /></a>
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
                                            <div class="pull-left">
                                                <asp:Label ID="lblOrganizacion" runat="server" CssClass="etiquetaOrganizacion"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="span6">
                                            <div class="pull-right">
                                                <asp:Label ID="lblNombre" runat="server" CssClass="etiquetaOrganizacion"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row-fluid">
                                    <div id="scroll" class="span12">
                                        <div id="TablaAretesMuertos" class="span12">
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
