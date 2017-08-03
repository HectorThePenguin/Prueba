<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RevisionImplantes.aspx.cs" Inherits="SIE.Web.Manejo.RevisionImplantes" %>
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
        <%: Scripts.Render("~/bundles/jscomunScript") %>
        <%: Scripts.Render("~/bundles/RevisionImplanteScript") %>
    </asp:PlaceHolder>

    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />

    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
    <style type="text/css">
        </style>
    <link rel="shortcut icon" href="../favicon.ico" />
    <style>
        .tdinvisible {
            display: none;
        }
         
        table tr.even.row_selected td {
            background-color: #B0BED9;
        }
 
        table tr.odd.row_selected td {
            background-color: #9FAFD1;
        }
    </style>
    <script type="text/javascript">
        var hArete = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderArete %>"/>';
        var btnAgregarText = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.btnAgregarText %>"/>';
        var btnGuardarText = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.btnGuardarText %>"/>';
        var btnCancelarText = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.btnCancelarText %>"/>';
        var btnLimpiarText = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.btnLimpiarText %>"/>';

        //MENSAJES
        var Aceptar = '<asp:Literal runat="server" Text="<%$ Resources:js.Aceptar %>"/>';
        var Si = '<asp:Literal runat="server" Text="<%$ Resources:js.Si %>"/>';
        var No = '<asp:Literal runat="server" Text="<%$ Resources:js.No %>"/>';
        var Ok = '<asp:Literal runat="server" Text="<%$ Resources:js.Ok %>"/>';
        var SalirSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.SalirSinGuardar %>"/>';
        var GuardadoExito = '<asp:Literal runat="server" Text="<%$ Resources:js.GuardadoExito %>"/>';
        var ErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorGuardar %>"/>';
        var ErrorLugarValidacion = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorObtenerLugarValidacion %>"/>';
        var ErrorCausas = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorCausas %>"/>';
        var NoExisteCorral = '<asp:Literal runat="server" Text="<%$ Resources:js.NoExisteCorral %>"/>';
        var msgUsuarioNoPermitido = '<asp:Literal runat="server" Text="<%$ Resources:msgUsuarioNoPermitido.Text %>"/>';
        var ErrorArete = '<asp:Literal runat="server" Text="<%$ Resources:msgArete.Text %>"/>';
        var ErrorObtenerCausas = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorCausas.Text %>"/>';
        var ErrorValidarCorral = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorValidarCorral.Text %>"/>';
        var ErrorLugares = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorLugares.Text %>"/>';

        var MensajeSinCorral = '<asp:Literal runat="server" Text="<%$ Resources:msgSinCorral.Text %>"/>';
        var MensajeSinLugar = '<asp:Literal runat="server" Text="<%$ Resources:msgSinLugar.Text %>"/>';
        var MensajeSinArete = '<asp:Literal runat="server" Text="<%$ Resources:msgSinArete.Text %>"/>';
        var MensajeSinCausa = '<asp:Literal runat="server" Text="<%$ Resources:msgSinCausa.Text %>"/>';
        var MensajeAreteExiste = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteExiste.Text %>"/>';
        var MensajeLimpiarPantalla = '<asp:Literal runat="server" Text="<%$ Resources:msgLimpiar.Text %>"/>';
        var MensajeCancelarPantalla = '<asp:Literal runat="server" Text="<%$ Resources:msgCancelar.Text %>"/>';

        var CabeceroCorral = '<asp:Literal runat="server" Text="<%$ Resources:Grid.Corral %>"/>';
        var CabeceroArete = '<asp:Literal runat="server" Text="<%$ Resources:Grid.Arete %>"/>';
        var CabeceroLugarValidacion = '<asp:Literal runat="server" Text="<%$ Resources:Grid.LugarValidacion %>"/>';
        var CabeceroCausa = '<asp:Literal runat="server" Text="<%$ Resources:Grid.Causa %>"/>';
        var CabeceroCorrecto = '<asp:Literal runat="server" Text="<%$ Resources:Grid.Correcto %>"/>';
        var CabeceroEliminar = '<asp:Literal runat="server" Text="<%$ Resources:Grid.Eliminar %>"/>';
        
    </script>
    <script type="text/javascript">
        function changeHashOnLoad() {
            window.location.href += "#";
            setTimeout("changeHashAgain()", "50");
        }

        function changeHashAgain() {
            window.location.href += "1";
        }

        var storedHash = window.location.hash;
        window.setInterval(function () {
            if (window.location.hash != storedHash) {
                window.location.hash = storedHash;
            }
        }, 50);

    </script>
</head>
<body class="page-header-fixed" onload="changeHashOnLoad();" ondragstart="return false;" ondrop="return false;">
    <div id="Principal">
        <form id="idform" runat="server" class="form-inline">
            <div id="skm_LockPane" class="LockOff">
            </div>
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="span12">
                        <div class="portlet box SuKarne2">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" />
                                    <span>
                                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="RevisionImplante_Title"></asp:Label></span>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <!-- Breadcums -->
                                <div class="row-fluid">
                                    <div class="span12">
                                        <ul class="breadcrumb">
                                            <li>
                                                <i class="icon-home"></i>
                                                <a href="../Principal.aspx">
                                                    <asp:Label ID="LabelHome" runat="server" meta:resourcekey="RevisionImplante_Home" /></a>
                                                <i class="icon-angle-right"></i>
                                            </li>
                                            <li>
                                                <a href="RevisionImplantes.aspx">
                                                    <asp:Label ID="LabelMenu" runat="server" meta:resourcekey="RevisionImplante_Title"></asp:Label></a>
                                            </li>

                                        </ul>
                                    </div>
                                </div>

                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblCorral" runat="server" meta:resourcekey="lblCorral" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtCorral" CssClass="m-wrap medium span12" ></asp:TextBox>
                                                   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblLugarValidacion" runat="server" meta:resourcekey="lblLugarValidacion" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <span class="span6">
                                                        <select id="ddlLugarValidacion" class="span12">
                                                        </select>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                       <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtFecha" CssClass="m-wrap medium span12" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Arete y agregar -->
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblArete" runat="server" meta:resourcekey="lblArete" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <input class="m-wrap medium span12" oninput="maxLengthCheck(this)" maxlength="15" id="txtArete" type="tel" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblCausa" runat="server" meta:resourcekey="lblCausa" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <span class="span6">
                                                        <select id="ddlCausa" class="span12">
                                                        </select>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        
                                         <div class="span4">
                                             <div class="control-group">
                                                <button id="btnAgregar" class="btn SuKarne" data-toggle="modal"></button>
                                                <button id="btnLimpiar" class="btn SuKarne" data-toggle="modal"></button>
                                             </div>
                                         </div>
                                    </div>
                                    </div>
                                
                                <!-- Fin de Controles-->

                                <!-- Tabla de datos -->
                                <div class="row-fluid">
                                    <div id="scroll" class="span12">
                                        <div id="GridRevision" class="span12">
                                        </div>
                                    </div>
                                </div>
                                <br/>
                                <div class="row-fluid">
                                    <div class="span4">
                                        <div class="span4">
                                            <div class="control-group">
                                                <div class="controls">
                                                    <asp:Label ID="lblEtiquetaTotal" runat="server" meta:resourcekey="lblTotal" CssClass="control-label"></asp:Label>
                                                    <asp:Label ID="lblTotal" runat="server" CssClass="control-label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="span4">
                                        <div class="span4">
                                            <div class="control-group">
                                                <div class="controls">
                                                    <asp:Label ID="lblEtiquetaCorrecttos" runat="server" meta:resourcekey="lblCorrectos" CssClass="control-label"></asp:Label>
                                                    <asp:Label ID="lblCorrectos" runat="server" CssClass="control-label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="span4">
                                        <div class="span4">
                                            <div class="control-group">
                                                <div class="controls">
                                                    <asp:Label ID="lblEtiquetasIncorrectos" runat="server" meta:resourcekey="lblIncorrectos" CssClass="control-label"></asp:Label>
                                                    <asp:Label ID="lblIncorrectos" runat="server" CssClass="control-label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                     </div>
                                </div>
                            
                             <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span12">
                                            <div class="control-group">
                                                
                                                <div class="controls">
                                                    <asp:Label ID="lblEfectividad" runat="server" meta:resourcekey="lblEfectividad" CssClass="control-label"></asp:Label>
                                                    <input class="m-wrap medium span4" oninput="maxLengthCheck(this)" maxlength="15" id="txtEfectividad" type="tel" readonly="true"/>
                                                     <asp:Label ID="Label1" runat="server" meta:resourcekey="lblPorcentaje" CssClass="control-label"></asp:Label>
                                                </div>
                                               
                                            </div>
                                        </div>
                                     </div>
                             </div>

                     </div>
                            </div>
                                <!-- Footer controles Guardar Cancelar -->
                                <div class="row-fluid">
                                    <div class="modal-footer">
                                        <button class="btn SuKarne" id="btnGuardar" data-toggle="modal"></button>
                                        <button id="btnCancelar" class="btn SuKarne" data-toggle="modal"></button>
                                    </div>
                                </div>
                                <!-- Fin Footer controles -->

                            </div>
                            <div class="message-info">
                                <asp:Label ID="lblEstatus" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div></form>
    </div>
        

</body>
</html>