<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecepcionGanadoMuerto.aspx.cs" Inherits="SIE.Web.Sanidad.RecepcionGanadoMuerto" %>

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
    <script src="../Scripts/RecepcionGanadoMuerto.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>

    <script type="text/javascript">
        var hArete = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderArete %>"/>';
        var hAreteTestigo = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderAreteTestigo %>"/>';
        var hFechaDeteccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderFechaDeteccion %>"/>';
        var hHoraDeteccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderHoraDeteccion %>"/>';
        var hFechaRecoleccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderFechaRecoleccion %>"/>';
        var hHoraRecoleccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderHoraRecoleccion %>"/>';
        var hRecibido = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderRecibido %>"/>';
        var msgTitulo = '<asp:Literal runat="server" Text="<%$ Resources:RecepcionGanadoMuerto_Title.Text %>"/>';
        var msgNoHayMuertesRecepcion = '<asp:Literal runat="server" Text="<%$ Resources:msgNoHayMuertesRecepcion.Text %>"/>';
        var msgFalloCargarDatos = '<asp:Literal runat="server" Text="<%$ Resources:msgFalloCargarDatos.Text %>"/>';
        var btnGuardarText = '<asp:Literal runat="server" Text="<%$ Resources:btnGuardar.Text %>"/>';
        var btnCancelarText = '<asp:Literal runat="server" Text="<%$ Resources:btnCancelar.Text %>"/>';
        var msgDlgCancelar = '<asp:Literal runat="server" Text="<%$ Resources:msgDlgCancelar.Text %>"/>';
        var msgDatosGuardadosExito = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardadosExito.Text %>"/>';
        var msgAreteNoDetectado = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteNoDetectado.Text %>"/>';
        var msgNoCuardraRecibidos = '<asp:Literal runat="server" Text="<%$ Resources:msgNoCuardraRecibidos.Text %>"/>';
        var msgNoHayAretesCapturados = '<asp:Literal runat="server" Text="<%$ Resources:msgNoHayAretesCapturados.Text %>"/>';
        var msgFalloAlGuardar = '<asp:Literal runat="server" Text="<%$ Resources:msgFalloAlGuardar.Text %>"/>';
        var msgNoTienePermiso = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTienePermiso.Text %>"/>';
        var msgAreteNoDetectadoTestigo = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteNoDetectadoTestigo.Text %>"/>';
    </script>

</head>
<body class="page-header-fixed">
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
                                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="RecepcionGanadoMuerto_Title"></asp:Label></span>
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
                                                <a href="RecepcionGanadoMuerto.aspx">
                                                    <asp:Label ID="LabelMenu" runat="server" meta:resourcekey="RecepcionGanadoMuerto_Title"></asp:Label></a>
                                            </li>

                                        </ul>
                                    </div>
                                </div>
                                 <div class="row-fluid">
                                    <div class="span12">
                                        <div class="pull-right">
                                            <asp:Label ID="lblOrganizacion" runat="server" CssClass="etiquetaOrganizacion"></asp:Label>
                                        </div>
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
                                                        <asp:TextBox runat="server" ID="txtArete" CssClass="m-wrap medium span12" Enabled="True" type="tel"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span5">
                                                <div class="control-group">
                                                    <asp:Label ID="lblAreteTestigo" runat="server" meta:resourcekey="lblAreteTestigo" CssClass="control-label"></asp:Label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtAreteTestigo" CssClass="m-wrap medium span12" Enabled="True" type="tel"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span2">
                                                <div class="control-group">
                                                    <label id="lblOculta" class="control-label" for="reload"></label>
                                                    <div class="controls">
                                                        <a href="#" id="reload">
                                                            <img src="../Images/buscar.ico" width="25" height="25"></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblNombre" runat="server" meta:resourcekey="lblNombre" CssClass="control-label"></asp:Label>
                                            </div>
                                            <div class="control-group">
                                                <div class="span12">
                                                    <div class="span6">
                                                        <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha" CssClass="control-label"></asp:Label>
                                                    </div>
                                                    <div class="span6">
                                                        <asp:Label ID="lblHora" runat="server" meta:resourcekey="lblHora" CssClass="control-label"></asp:Label>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Tabla de datos -->
                                <div class="row-fluid">
                                    <div id="scroll" class="span12">
                                        <div id="TablaAretesMuertos" class="span12">
                                        </div>
                                    </div>
                                </div>

                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span7">
                                            
                                            <div class="span5">
                                                <div class="control-group">
                                                    <asp:Label ID="lblTotalDetectados" runat="server" meta:resourcekey="lblTotalDetectados" CssClass="control-label"></asp:Label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtTotalDetectados" CssClass="m-wrap small span12" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            
                                            <div class="span5">
                                                <div class="control-group">
                                                    <asp:Label ID="lblTotalRecolectados" runat="server" meta:resourcekey="lblTotalRecolectados" CssClass="control-label"></asp:Label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtTotalRecolectados" CssClass="m-wrap small span12" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span5">
                                            <div class="pull-right">

                                                <div class="control-group">
                                                    <asp:Label ID="lblTotalRecibidos" runat="server" meta:resourcekey="lblTotalRecibidos" CssClass="control-label"></asp:Label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtTotalRecibidos" CssClass="m-wrap small span12" Enabled="False"></asp:TextBox>
                                                    </div>
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
