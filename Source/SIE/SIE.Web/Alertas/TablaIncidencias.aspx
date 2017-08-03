<%@ Page Language="C#" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="TablaIncidencias.aspx.cs" Inherits="SIE.Web.Alertas.TablaIncidencias" %>

<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Services.Description" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headAlertas" runat="server">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
            <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
            <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../assets/plugins/bootstrap-timepicker/css/bootstrap-responsive.css" />
    <script src="../Scripts/TablaIncidencias.js"></script>
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    <link rel="stylesheet" href="../assets/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css"/>
    <script type="text/javascript" src="../assets/plugins/bootstrap-timepicker/js/bootstrap-timepicker.js"></script>

    <script>
        var mensajeSeleccionarIncidencia = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarIncidencia %>"/>';
        var mensajeSeleccionarFecha = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarFecha %>"/>';
        var mensajeSeleccionarAccion = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSeleccionarAccion %>"/>';
        var btnRechazarText = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.btnRechazarText %>"/>';
        var btnAutorizarText = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.btnAutorizarText %>"/>';
        var msgSalirSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgSalirSinGuardar%>"/>';
        var tituloSalirSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.lblTituloAlertas%>"/>';
    </script>
    <style>
        .table-striped > tbody > tr:nth-child(2n+1) > td, .table-striped > tbody > tr:nth-child(2n+1) > th {
           background-color: #F1FFFB;
        }
        .table > thead > tr {
            background: #dbebf7; /* Old browsers */
            background: -moz-linear-gradient(top,  #dbebf7 49%, #cde5f7 51%); /* FF3.6-15 */
            background: -webkit-linear-gradient(top,  #dbebf7 49%,#cde5f7 51%); /* Chrome10-25,Safari5.1-6 */
            background: linear-gradient(to bottom,  #dbebf7 49%,#cde5f7 51%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#dbebf7', endColorstr='#cde5f7',GradientType=0 ); /* IE6-9 */
        }
        .table > thead > tr > th {
            font-family: "Arial Negrita", "Arial";
            font-weight: 700;
            font-style: normal;
            font-size: 11px;
            color: #2977A8;
            text-align: center;
        }
        .table > tbody > tr > td {
            text-align: center;
        }
    
        #CabeceroTabla {
            background: #dbebf7; /* Old browsers */
            background: -moz-linear-gradient(top,  #dbebf7 49%, #cde5f7 51%); /* FF3.6-15 */
            background: -webkit-linear-gradient(top,  #dbebf7 49%,#cde5f7 51%); /* Chrome10-25,Safari5.1-6 */
            background: linear-gradient(to bottom,  #dbebf7 49%,#cde5f7 51%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#dbebf7', endColorstr='#cde5f7',GradientType=0 ); /* IE6-9 */
            padding: 5px;
            -webkit-border-top-left-radius: 5px !important;
            -webkit-border-top-right-radius: 5px !important;
            -moz-border-top-right-radius: 5px !important;
            -moz-border-top-left-radius: 5px !important;
            border-top-right-radius: 5px !important;
            border-top-left-radius: 5px !important;
            border: 1px solid #ddd;
        }
        #TextoCabecero {
            padding-left: 5px;
            word-wrap: break-word;
            font-family: 'Arial Negrita', 'Arial';
            font-weight: 700;
            font-style: normal;
            font-size: 14px;
            vertical-align: middle;
            color: #0033FF;
        }
        .width100 {
            width: 100%;
        }

        .ventanaCargando {
            display:    none;
            position:   fixed;
            z-index:    1000;
            top:        0;
            left:       0;
            height:     100%;
            width:      100%;
            background: rgba( 255, 255, 255, .8 ) 
                        url('../assets/img/loadingme.gif') 
                        50% 50% 
                        no-repeat;
            background-size: 10% auto;
        }

        /* When the body has the loading class, we turn
           the scrollbar off with overflow:hidden */
        body.cargando {
            overflow: hidden;   
        }

        /* Anytime the body has the loading class, our
           modal element will be visible */
        body.cargando .ventanaCargando {
            display: block;
        }
        #TablaIncidenciasFiltrada {
            overflow-x: scroll;
            max-width: 40%;
            display: block;
            white-space: nowrap;
        }

        .requeridoAlerta {
            color: #e02222 !important;
            font-size: 12px;
            padding-left: 2px;
        }
        .incidenciasConSeguimiento {
            background-color: #4AB948 !important;
            color: white;
            font-weight: bold;
        }
        .incidenciasRechazadas {
            background-color: #e02222 !important;
            color: white;
            font-weight: bold;
        }

    </style>
</head>
<body class="page-header-fixed text">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="col-md-12">
                        <div class="portlet box SuKarne2">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                    <span class="letra">
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
                                            <a href="../Alertas/AlertasSIAP.aspx"><asp:Label ID="Label1" runat="server" meta:resourcekey="lblTitulo"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="row-fluid">
                                    <div id="dataTableControls" class="row-fluid">
                                        <div id="tableControlsLenght" class="span6"></div>
                                        <div id="tableControlsFilter" class="span6"></div>
                                    </div>
                                    <div id="CabeceroTabla"><img src="../Images/skLogo.png"/><asp:Label ID="TextoCabecero" runat="server"></asp:Label></div>
                                    <asp:Panel runat="server" ID="PanelNoRegistros" Visible="False">
                                        <asp:Label runat="server" meta:resourcekey="lblIncidenciasNoEncontradas"></asp:Label>
                                    </asp:Panel>
                                    <asp:Table ID="TablaIncidenciasFiltrada" 
                                        OnLoad="TablaIncidenciasFiltrada_OnLoad"
                                        CssClass="table table-hover table-striped table-bordered table-condensed"
                                        runat="server">
                                    </asp:Table>
                                    <asp:HiddenField ID="hiddenNivelAlertaID" runat="server"/>
                                    <asp:HiddenField ID="hiddenNivelAlertaUsuario" runat="server"/>
                                    <asp:HiddenField ID="hiddenEstatusAnteriorID" runat="server"/>
                                    <asp:HiddenField ID="hiddenModuloID" runat="server"/>
                                    <asp:HiddenField ID="hiddenAlertaID" runat="server"/>
                                    <asp:HiddenField ID="hiddenEsRechazado" runat="server"/>
                                    <asp:HiddenField ID="hiddenEsNuevo" runat="server"/>
                                    <asp:HiddenField ID="hiddenEsVencida" runat="server"/>
                                    <asp:HiddenField ID="hiddenEsRegistrado" runat="server"/>
                                    <asp:HiddenField ID="HiddenUsuarioID" runat="server"/>
                                    <asp:HiddenField ID="HiddenNivelAlertaConfigurado" runat="server"/>
                                    <asp:Panel runat="server" ID="PanelControlesAcciones" Visible="False">
                                    <div id="ControlesTabla">
                                        <fieldset id="Fieldset2" class="scheduler-border">
                                            <legend class="scheduler-border">
                                                <asp:Label ID="Label14" runat="server" meta:resourcekey="lblSeguimientoAlertas"></asp:Label>
                                            </legend>
                                            <div class="container-fluid">
                                                <div class="row">
                                                    <div class="span12">
                                                        <div class="span6">
                                                            <div class="space10"></div>
                                                            <div class="span12">
                                                                <div class="span2">
                                                                    <asp:Label ID="fechaRequerido" runat="server" Visible="False" CssClass="requeridoAlerta">*</asp:Label>
                                                                    <asp:Label ID="Label3" runat="server" meta:resourcekey="lblFecha">Fecha</asp:Label>
                                                                </div>
                                                                <div class="span9">
                                                                    <input type="text" id="datepicker" readonly="readonly" />
                                                                </div>
                                                            </div>
                                                             <div class="space10"></div>
                                                            <div class="span12">
                                                                <div class="span2">
                                                                    <asp:Label ID="horaRequerido" runat="server" Visible="False" CssClass="requeridoAlerta">*</asp:Label>
                                                                    <asp:Label ID="lblHora" runat="server" meta:resourcekey="lblHora"></asp:Label>
                                                                </div>
                                                                <div class="span9">
                                                                    <input id="timepicker1" type="text" class="form-control bootstrap-timepicker"/> 
                                                                </div>
                                                            </div>
                                                            <div class="space10"></div>
                                                            <div class="span12">
                                                                <div class="span2">
                                                                    <asp:Label ID="accionRequerido" runat="server" Visible="False" CssClass="requeridoAlerta">*</asp:Label>
                                                                    <asp:Label ID="Label4" runat="server" meta:resourcekey="lblAcciones">Acciones</asp:Label>
                                                                </div>
                                                                <div class="span9">
                                                                    <asp:DropDownList ID="DropDownAcciones" runat="server">
                                                                        <asp:ListItem Value="" Text="Seleccionar..."></asp:ListItem>
                                                                    </asp:DropDownList>    
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="span6">
                                                            <div class="space10"></div>
                                                            <asp:Panel runat="server" ID="panelComentarios">
                                                                <div class="span12">
                                                                    <div class="span2">
                                                                        <asp:Label ID="Label5" runat="server" meta:resourcekey="lblComentarios">Comentarios</asp:Label>
                                                                    </div>
                                                                    <div class="span9">
                                                                        <asp:TextBox runat="server" id="TextAreaComentarios" TextMode="MultiLine" Rows="6" CssClass="width100"></asp:TextBox>   
                                                                    </div>
                                                                </div>
                                                                <div class="space15"></div>
                                                             </asp:Panel>
                                                            <asp:Panel runat="server" ID="textHistorico" Visible="False">
                                                                <div class="span12">
                                                                    <div class="span2">
                                                                        <asp:Label ID="Label9" runat="server" meta:resourcekey="lblComentariosAnteriores"></asp:Label>
                                                                    </div>
                                                                    <div class="span9">
                                                                        <textarea id="txtComentariosAnteriores" readonly="readonly" style="width: 100%;" rows="6"></textarea>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="space15"></div>
                                                    
                                                </div>
                                            </div>
                                        </fieldset>
                                        </div>
                                    </asp:Panel>
                                    <div class="row">
                                        <div class="offset6 span6">
                                            <div class="pull-right">
                                                <asp:HyperLink runat="server" ID="btnGuardar" CssClass="btn btn-default"></asp:HyperLink>
                                                <asp:HyperLink runat="server" ID="btnCancelar" CssClass="btn btn-default"></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <div class="ventanaCargando"></div>
    </div>
</body>
</html>