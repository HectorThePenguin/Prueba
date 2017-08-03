<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EstatusDeteccionGanado.aspx.cs" Inherits="SIE.Web.Sanidad.EstatusDeteccionGanado" MasterPageFile="" %>

<%@ Import Namespace="System.Web.Optimization" %>

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

        .tabladetectados {
            max-height: 240px;
            overflow: auto;
        }
    </style>
    
    <script src="../Scripts/EstatusDeteccionGanado.js"></script>
    
    <script type="text/javascript">
        var mensajeCorralNoValido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCorralNoValido %>"/>';
        var mensajeCorralNoPerteneceAEnfermeria = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCorralNoPerteneceAEnfermeria %>"/>';
        var mensajeCorralSinAretes = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCorralSinAretes %>"/>';
        var mensajeAreteInvalido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeAreteInvalido %>"/>';
        var mensajeGuardadoOk = '<asp:Literal runat="server" Text="<%$ Resources:mensajeGuardadoOk %>"/>';
        var mensajeAreteTestigoNoPertenece = '<asp:Literal runat="server" Text="<%$ Resources:mensajeAreteTestigoNoPertenece %>"/>';
        var mensajeExistenDatosEnBlanco = '<asp:Literal runat="server" Text="<%$ Resources:mensajeExistenDatosEnBlanco %>"/>';
        var mensajeAreteYaFueCapturado = '<asp:Literal runat="server" Text="<%$ Resources:mensajeAreteYaFueCapturado %>"/>';
        var mensajeErrorAlConsultarConceptos = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarConceptos %>"/>';
        var mensajeAreteYaDetectado = '<asp:Literal runat="server" Text="<%$ Resources:mensajeAreteYaDetectado %>"/>';
        var mensajeCancelacion = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCancelacion %>"/>';
        var lblSinOperador = '<asp:Literal runat="server" Text="<%$ Resources:lblSinOperador.Text %>"/>';
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="row-fluid caption">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <span>
                            <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTitulo"></asp:Label>
                        </span>
                    </div>
                </div>

                <div class="portlet-body form">
                    <div class="row-fluid">
                        <ul class="breadcrumb">
                            <li>
                                <i class="icon-home"></i>
                                <a href="../Principal.aspx">Home</a>
                                <i class="icon-angle-right"></i>
                            </li>
                            <li>
                                <a href="../Sanidad/EstatusDeteccionGanado.aspx">Estatus de la detección</a>
                            </li>
                        </ul>
                    </div>
                    
                    <div class="row-fluid">
                        <div class="span4"><asp:Label ID="LabelFormato" runat="server" meta:resourcekey="claveFormato" CssClass="etiquetaOrganizacion"></asp:Label></div>
                        <div class="span4"><asp:Label ID="Label1" runat="server" meta:resourcekey="revisionFormato"></asp:Label></div>
                        <div class="span4"><asp:Label ID="Label2" runat="server" meta:resourcekey="fechaFormato"></asp:Label></div>
                    </div>

                    <div class="row-fluid">
                        <div class="span4">
                            <div class="row-fluid">
                                <div class="span6"><asp:Label ID="Label8" runat="server" meta:resourcekey="lblHoraDeInicio"></asp:Label>: <asp:Label ID="lblHoraInicio" runat="server" Text=""></asp:Label></div>
                                <div class="span6">&nbsp;</div>
                            </div>
                            <div class="row-fluid">
                                <div class="span6"><asp:Label ID="Label9" runat="server" meta:resourcekey="lblHoraDeFinalizacion"></asp:Label>: </div>
                                <div class="span6"><asp:Label ID="lblHoraFinalizacion" runat="server" Text=""></asp:Label></div>
                            </div>
                        </div>
                        <div class="span8">
                            <div class="span4"><asp:Label ID="Label10" runat="server" meta:resourcekey="lblSupervisor"></asp:Label>: <asp:Label ID="lblSupervisor" runat="server" Text="Label"></asp:Label></div>
                            <div class="span4"><asp:Label ID="Label11" runat="server" meta:resourcekey="lblEnfemeria"></asp:Label>: <asp:Label ID="lblEnfermeria" runat="server" Text=""></asp:Label></div>
                            <div class="span4"><asp:Label ID="Label12" runat="server" meta:resourcekey="lblFecha"></asp:Label>: <asp:Label ID="lblFecha" runat="server" Text="Label"></asp:Label></div>
                        </div>
                    </div>

                    <div class="row-fluid">
                        <div class="span2">
                            <asp:Label ID="Label13" runat="server" meta:resourcekey="lblEnfemeria"></asp:Label>: 
                            <asp:DropDownList ID="cmbEnfermeria" runat="server" CssClass="control span12" focos="1"></asp:DropDownList>
                        </div>
                        <div class="span2">
                            <span class="requerido">*</span> <asp:Label ID="Label14" runat="server" meta:resourcekey="lblCorral"></asp:Label>: 
                            <asp:TextBox ID="txtCorral" runat="server" CssClass="control span12" focos="2"></asp:TextBox>
                            <input type="hidden" id="txtTipoCorralId"/>
                        </div>
                        <div class="span2">
                            <input type="checkbox" id="checkSinArete"/> Sin Arete
                            <button type="button" id="btnTomarFoto" data-toggle="modal" class="btn letra SuKarne span12">
                                <img src="../Images/camara.png" width="25" height="22"/>
                                <asp:Label ID="lblTomarFoto" runat="server" meta:resourcekey="lblEtiquetaTomarFoto"></asp:Label>
                            </button>
                            <input type="file" id="flFoto" name="fotoGanado[]" style="display: none;"/>
                        </div>
                    </div>

                    <div class="row-fluid">
                        <div class="caja span12">
                            <div class="span2">
                                <asp:Label ID="Label15" runat="server" meta:resourcekey="lblVaqueroDetector"></asp:Label>: 
                                <asp:TextBox ID="txtVaqueroDetector" runat="server" CssClass="control span12" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="span1">
                                <asp:Label ID="Label16" runat="server" meta:resourcekey="lblHora"></asp:Label>: 
                                <asp:TextBox ID="txtHora" runat="server" CssClass="control span12" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="span2">
                                <asp:Label ID="Label17" runat="server" meta:resourcekey="lblNoArete"></asp:Label>:
                                <input id="txtArete" class="control span12 text-right" type="tel" /> 
                                <%--<asp:TextBox ID="txtArete" runat="server" CssClass="control span12"></asp:TextBox>--%>
                                <div class="span12">
                                    <div class="" id="dvFoto"></div>
                                </div>
                            </div>
                            <div class="span2">
                                <asp:Label ID="Label21" runat="server" meta:resourcekey="lblNoAreteTestigo"></asp:Label>: 
                                <input id="txtAreteTestigo" class="control span12 text-right" type="tel" /> 
                                <%--<asp:TextBox ID="txtAreteTestigo" runat="server" CssClass="control span12"></asp:TextBox>--%>
                            </div>
                            <div class="span3">
                                <span class="requerido">*</span> <asp:Label ID="Label18" runat="server" meta:resourcekey="lblConcepto"></asp:Label>: 
								<div id="listaDeConceptos" class="checkbox-list caja"></div>
                            </div>
                            <div class="span2">
                                <span class="requerido">*</span> <asp:Label ID="Label19" runat="server" meta:resourcekey="lblAcuerdo"></asp:Label>: 
                                <textarea id="txtAcuerdo" maxlength="255" class="control span12"></textarea>
                            </div>
                            
                        </div>
                    </div>

                    <div class="row-fluid">
                        <div class="seccionbotones span12">
                            <div class="pull-right">
                                <button type="button" id="btnAgregar" data-toggle="modal" class="btn letra SuKarne">
                                    <asp:Label ID="Label3" runat="server" meta:resourcekey="btnAgregar"></asp:Label>
                                </button>
                                <button type="button" id="btnLimpiar" data-toggle="modal" class="btn letra SuKarne">
                                    <asp:Label ID="Label4" runat="server" meta:resourcekey="btnLimpiar"></asp:Label>
                                </button>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row-fluid">
                        <div class="span12 caja">
                            <table id="" class="table table-striped table-advance table-hover">
                                <thead>
                                    <tr>
                                        <th scope="col" class="span2">
                                            <asp:Label ID="Label20" runat="server" meta:resourcekey="lblVaqueroDetector"></asp:Label>
                                        </th>
                                        <th scope="col" class="span1">
                                            <asp:Label ID="lblHora" runat="server" meta:resourcekey="lblHora"></asp:Label>
                                        </th>
                                        <th scope="col" class="span2">
                                            <asp:Label ID="lblNoArete" runat="server" meta:resourcekey="lblNoArete"></asp:Label>
                                        </th>
                                        <th scope="col" class="span2">
                                            <asp:Label ID="lblNoAreteTestigo" runat="server" meta:resourcekey="lblNoAreteTestigo"></asp:Label>
                                        </th>
                                        <th scope="col" class="span2">
                                            &nbsp;
                                        </th>
                                        <th scope="col" class="span2">
                                            <asp:Label ID="lblConcepto" runat="server" meta:resourcekey="lblConcepto"></asp:Label>
                                        </th>
                                        <th scope="col" class="span2">
                                            <asp:Label ID="lblAcuerdo" runat="server" meta:resourcekey="lblAcuerdo"></asp:Label>
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                            <div class="tabladetectados">
                                <table id="detectados" class="table table-striped table-advance table-hover">
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div class="row-fluid">
                        <div class="seccionbotones span12">
                            <div class="pull-right">
                                <button type="button" id="btnGuardar" data-toggle="modal" class="btn letra SuKarne">
                                    <asp:Label ID="Label6" runat="server" meta:resourcekey="btnGuardar"></asp:Label>
                                </button>
                                <button type="button" id="btnCancelar" data-toggle="modal" class="btn letra SuKarne">
                                    <asp:Label ID="Label7" runat="server" meta:resourcekey="btnCancelar"></asp:Label>
                                </button>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
