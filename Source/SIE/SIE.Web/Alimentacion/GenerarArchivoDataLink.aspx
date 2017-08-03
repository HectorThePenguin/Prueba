<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerarArchivoDataLink.aspx.cs" Inherits="SIE.Web.Alimentacion.GenerarArchivoDataLink" %>

<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
    </asp:PlaceHolder>

    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/GenerarArchivoDataLink.js"></script>
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />

    <style>
        .caja {
            border: 2px solid #c0c0c0;
            padding: 10px;
        }

        .seccionbotones {
            padding: 10px;
        }

        textarea {
	        resize: none;
        }
    </style>

    <script type="text/javascript">
        var msgIngreseFechaReparto = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgIngreseFechaReparto %>"/>';
        var msgGeneradoExito = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgGeneradoExito %>"/>';
        var msgOcurrioErrorGenerar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgOcurrioErrorGenerar %>"/>';
        var msgDlgCancelar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgDlgCancelar %>"/>';
        var msgErrorParametros = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgErrorParametros %>"/>';
        var msgErrorRolUsuario = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgErrorRolUsuario %>"/>';
        var msgOcurrioErrorProceso = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgOcurrioErrorProceso %>"/>';
        var msgSeleccioneServicio = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgSeleccioneServicio %>"/>';
        var msgSeleccioneFecha = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgSeleccioneFecha %>"/>';
        var msgNoExisteTurnos = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgNoExisteTurnos %>"/>';
        var msgRutaNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgRutaNoExiste %>"/>';
        var msgSeleccionesServicio = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgSeleccionesServicio %>"/>';
        var msgRutaNoConfigurada = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgRutaNoConfigurada %>"/>';
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
                    <div class="row-fluid caption">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <span>
                            <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="GenerarArchivoDataLink_Title"></asp:Label>
                        </span>
                    </div>
                </div>

                <div class="portlet-body form">
                    <div class="row-fluid">
                        <ul class="breadcrumb">
							<li>
							<i class="icon-home"></i>
							<a href="../Principal.aspx">
							<asp:label id="LabelHome" runat="server" meta:resourcekey="GenerarArchivoDataLink_Home"/></a>
							<i class="icon-angle-right"></i>
							</li>
							<li>
							<a href="GenerarArchivoDataLink.aspx">
							<asp:label id="LabelMenu" runat="server" meta:resourcekey="GenerarArchivoDataLink_Title"></asp:label></a>
							</li>
                        </ul>
                    </div>

                    <div class="row-fluid">
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblCorrales" runat="server"  meta:resourcekey="lblTituloGrupo"></asp:Label>
                            </legend>
                            
                            <div class="row-fluid">
                                <div class="span4"><label class=""><asp:Label ID="LblTipoServicio" runat="server" meta:resourcekey="lblTipoServicio"></asp:Label></label></div>
                            </div>
                             
                            <div class="span12">
                                
                                <div class="span3">
                                    <span class="requerido">*</span>
                                    <asp:Label ID="lblTurno" runat="server" meta:resourcekey="lblEtiquetaTurno"></asp:Label>
                                    <asp:DropDownList ID="cboServicios" CssClass="span6" runat="server" DataValueField="TipoServicioId" DataTextField="DescripcionCombo" />
                                </div>
                              
                                <div class="span5">
                                    <span class="requerido">*</span>
                                   <asp:Label ID="Label1" runat="server" meta:resourcekey="lblFecha"></asp:Label>
                                   <input type="text" id="datepicker" readonly="true"/>
                                </div>         
                            </div>

                            <div class="row-fluid">
                                <div class="seccionbotones span12">
                                    <div class="pull-right">
									    <button type="button" id="btnGenerarArchivo" data-toggle="modal" class="btn letra SuKarne">
									    <asp:label id="Label6" runat="server" meta:resourcekey="btnGenerarArchivo"></asp:label>
									    </button>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                       
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
