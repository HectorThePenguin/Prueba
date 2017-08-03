<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupervisionTecnicaDetectores.aspx.cs" Inherits="SIE.Web.Sanidad.SupervisionTecnicaDetectores" %>

<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headEvaluacion" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Supervisión de Técnica de Detectores <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
    </asp:PlaceHolder>


    <link href="../assets/css/style-metro.css" rel="stylesheet" type="text/css" />
    <link href="../assets/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="../assets/plugins/select2/select2_metro.css" />
    <link rel="stylesheet" href="../assets/plugins/data-tables/DT_bootstrap.css" />
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="shortcut icon" href="../favicon.ico" />
    <script src="../Scripts/SupervisionTecnicaDetectores.js"></script>
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>

    <script type="text/javascript">
        var btnGuardarText = '<asp:Literal runat="server" Text="<%$ Resources:btnGuardar.Text %>"/>';
        var btnCancelarText = '<asp:Literal runat="server" Text="<%$ Resources:btnCancelar.Text %>"/>';
        var msgTitulo = '<asp:Literal runat="server" Text="<%$ Resources:Supervision_Title.Text %>"/>';
        var msgNoCargaronPreguntas = '<asp:Literal runat="server" Text="<%$ Resources:msgNoCargaronPreguntas.Text %>"/>';
        var msgDetectorNoApto = '<asp:Literal runat="server" Text="<%$ Resources:msgDetectorNoApto.Text %>"/>';
        var msgInformacionIncompleta = '<asp:Literal runat="server" Text="<%$ Resources:msgInformacionIncompleta.Text %>"/>';
        var msgDatosGuardadosExito = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardadosExito.Text %>"/>';
        var msgDlgCancelar = '<asp:Literal runat="server" Text="<%$ Resources:msgCancelar.Text %>"/>';
        var msgNoTienePermiso = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTienePermiso.Text %>"/>';
        var msgNoHayDetectores = '<asp:Literal runat="server" Text="<%$ Resources:msgNoHayDetectores.Text %>"/>';
        var msgFalloCargarCriterios = '<asp:Literal runat="server" Text="<%$ Resources:msgFalloCargarCriterios.Text %>"/>';
        var msgNoExistenCriterios = '<asp:Literal runat="server" Text="<%$ Resources:msgNoExistenCriterios.Text %>"/>';
        var msgNoSeleccionoPregunta = '<asp:Literal runat="server" Text="<%$ Resources:msgNoSeleccionoPregunta.Text %>"/>';
        var msgFalloAlGuardar = '<asp:Literal runat="server" Text="<%$ Resources:msgFalloAlGuardar.Text %>"/>';
        var msgFalloCargarDatosSupervisionesAnteriores = '<asp:Literal runat="server" Text="<%$ Resources:msgFalloCargarDatosSupervisionesAnteriores.Text %>"/>';
        var msgMostrarNoHayConfiguracionPerido = '<asp:Literal runat="server" Text="<%$ Resources:msgMostrarNoHayConfiguracionPerido.Text %>"/>';
        var msgDetectorMasdeDosEvaluaciones = '<asp:Literal runat="server" Text="<%$ Resources:msgDetectorMasdeDosEvaluaciones.Text %>"/>';
    </script>

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
                                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="Supervision_Title"></asp:Label></span>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <ul class="breadcrumb">
                                            <li>
                                                <i class="icon-home"></i>
                                                <a href="../Principal.aspx">
                                                    <asp:Label ID="LabelHome" runat="server" meta:resourcekey="Supervision_Home" /></a>
                                                <i class="icon-angle-right"></i>
                                            </li>
                                            <li>
                                                <a href="SupervisionTecnicaDetectores.aspx">
                                                    <asp:Label ID="LabelMenu" runat="server" meta:resourcekey="Supervision_Title"></asp:Label></a>
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
                                            <div class="pull-right">
                                                <asp:Label ID="lblFormato" runat="server" meta:resourcekey="Supervision_IdFormato" CssClass="etiquetaOrganizacion"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Seccion de controles-->
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span3">
                                            <div class="control-group">
                                                <asp:Label ID="lblNombreDetector" runat="server" meta:resourcekey="lblNombreDetector" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList runat="server" ID="cmbDetectores" CssClass="m-wrap medium span12" />
                                                   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span6">
                                            <div class="control-group hidden" id="dvEvaluacionAnterior">
                                                <asp:Label ID="lblEvaluacionAnterior" runat="server" meta:resourcekey="lblEvaluacionAnterior" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <div class="span12">
                                                        <div class="span3">
                                                            <div><span id="evalSemaforo" class="span12"></span></div>
                                                        </div>
                                                        <div class="span6">
                                                            <div><span id="evalFecha" class="control-label"></span></div>
                                                        </div>
                                                        <div class="span3 pull-right">
                                                            <div><span id="evalResultado" class="control-label"></span></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="span3">
                                            <div class="pull-right">
                                                <div class="control-group">
                                                    <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha" CssClass="control-label"></asp:Label>
                                                    <div class="controls">
                                                        <asp:Label ID="lblFechaExpuesta" runat="server" CssClass="control-label"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Tabla de datos -->
                                <div class="row-fluid">
                                    <div id="scroll" class="span12">
                                        <div id="TablaPreguntas" class="span12">
                                        </div>
                                    </div>
                                </div>

                                <!--Control del semaforo-->
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span10">
                                            <div class="span2 pull-left">
                                                <div class="control-group">
                                                    <label id="lbls" runat="server" class="control-label"></label>
                                                    <div class="controls">
                                                        <span id="txtSemaforo" class="span12"></span>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="span7 pull-left">
                                                <div class="control-group">
                                                    <label id="lblMsg" runat="server" class="control-label"></label>
                                                    <div class="controls">
                                                        <span id="txtDescripcionCriterio" class="span12"></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span2">
                                            <div class="pull-right">
                                                <div class="control-group">
                                                    <asp:Label ID="lblTotal" runat="server" meta:resourcekey="lblTotal" CssClass="control-label"></asp:Label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtTotal" CssClass="m-wrap small span12" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Observaciones -->
                                <div class="portlet">
                                    <div class="portlet-title line">
                                        <asp:Label ID="lblObservaciones" runat="server" meta:resourcekey="lblObservaciones"></asp:Label>
                                    </div>
                                    <div class="portlet-body" id="divObservaciones">
                                        <div class="row-fluid">
                                            <div class="span12 line">
                                                <asp:TextBox runat="server" ID="txtObservaciones" MaxLength="255" TextMode="MultiLine" Rows="2" CssClass="span12" Style="-moz-resize: none; -ms-resize: none; -o-resize: none; resize: none"></asp:TextBox>
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
<script type="text/javascript" src="../assets/plugins/select2/select2.min.js"></script>
<script type="text/javascript" src="../assets/plugins/data-tables/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="../assets/plugins/data-tables/DT_bootstrap.js"></script>

</html>
