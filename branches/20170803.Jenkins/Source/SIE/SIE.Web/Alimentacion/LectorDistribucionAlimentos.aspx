<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LectorDistribucionAlimentos.aspx.cs" Inherits="SIE.Web.Alimentacion.LectorDistribucionAlimentos" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
    </asp:PlaceHolder>
    
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/LectorDistribucionAlimentos.js"></script>
    <style type="text/css">
        fieldset.scheduler-border
        {
            border: 1px groove #ddd !important;
            padding: 0 1.4em 1.4em 1.4em !important;
            margin: 0 0 1.5em 0 !important;
            -webkit-box-shadow: 0px 0px 0px 0px #000;
            box-shadow: 0px 0px 0px 0px #000;
        }

        legend.scheduler-border
        {
            width: auto; /* Or auto */
            padding: 0 10px; /* To give a bit of padding on the left and right */
            border-bottom: none;
            font-size: small;
            font-weight: bold;
        }
        tr, td {
            border: 1px solid #000000;
        }
    </style>
    
    <script type="text/javascript">
        var mensajeErrorSession = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorSession %>"/>';
        var mensajeCorralNoValido = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCorralNoValido %>"/>';
        var mensajeCorralNoEsDelLector = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCorralNoEsDelLector %>"/>';
        var mensajeNoHayMasCorrales = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoHayMasCorrales %>"/>';
        var mensajeNoHayEstadosDistribucion = '<asp:Literal runat="server" Text="<%$ Resources:mensajeNoHayEstadosDistribucion %>"/>';
        var mensajeGuardadoOk = '<asp:Literal runat="server" Text="<%$ Resources:mensajeGuardadoOk %>"/>';
        var mensajeErrorAlGuardar = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlGuardar %>"/>';
        var mensajeCorralVerificado = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCorralVerificado %>"/>';
        var mensajeCapturarCorral = '<asp:Literal runat="server" Text="<%$ Resources:mensajeCapturarCorral %>"/>';
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
                                    <a href="../Alimentacion/LectorDistribucionAlimentos.aspx"><asp:Label ID="Label1" runat="server" meta:resourcekey="lblTitulo"></asp:Label></a>
                                </li>
                            </ul>
                        </div>
                        
                        <div class="row-fluid">
                            <div class="span10">
                                <div class="span2"><asp:Label ID="Label8" CssClass="span12" runat="server" meta:resourcekey="lblOrganizacion"></asp:Label></div>
                                <div class="span4"><asp:TextBox ID="txtOrganizacion" Enabled="false" CssClass="span12" runat="server"></asp:TextBox></div>
                                <div class="span1"><asp:Label ID="Label2" CssClass="span12" runat="server" meta:resourcekey="lblFecha"></asp:Label></div>
                                <div class="span2"><asp:TextBox ID="txtFecha" Enabled="false" CssClass="span12" runat="server"></asp:TextBox></div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span10">
                                <div class="span2"><asp:Label ID="Label9" runat="server" meta:resourcekey="lblNombreLector"></asp:Label>: </div>
                                <div class="span4"><asp:TextBox ID="txtNombreLector" Enabled="false" CssClass="span12" runat="server"></asp:TextBox></div>
                                <div class="span1"><asp:Label ID="Label3" runat="server" meta:resourcekey="lblHora"></asp:Label></div>
                                <div class="span2"><asp:TextBox ID="txtHora" Enabled="false" CssClass="span12" runat="server"></asp:TextBox></div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span8">
                                <fieldset class="scheduler-border">
                                   <legend class="scheduler-border">
                                       <asp:Label ID="Label4" runat="server" meta:resourcekey="lblGroupBox"></asp:Label>
                                   </legend>
                                    <div class="row-fluid">
                                        <div class="span12">
                                            <div class="span2">
                                                <asp:Label ID="Label15" runat="server" meta:resourcekey="lblSeccion"></asp:Label>
                                                <asp:TextBox ID="txtSeccion" runat="server" CssClass="control span12" Enabled="False"></asp:TextBox>
                                            </div>
                                            <div class="span1"></div>
                                            <div class="span2">
                                                <asp:Label ID="Label16" runat="server" meta:resourcekey="lblCorral"></asp:Label>
                                                <asp:TextBox ID="txtCorral" runat="server" CssClass="control span12"></asp:TextBox>
                                            </div>
                                            <div class="span1"></div>
                                            <div class="span2">
                                                <asp:Label ID="Label5" runat="server" meta:resourcekey="lblOrden"></asp:Label>
                                                <asp:TextBox ID="txtOrden" runat="server" CssClass="control span12" Enabled="False"></asp:TextBox>
                                            </div>
                                            <div class="span1"></div>
                                            <div class="span3">
                                                <div class="space10"></div>
                                                <div class="span12">
                                                    <asp:Label ID="Label7" CssClass="span6" runat="server" meta:resourcekey="lblTotal"></asp:Label>
                                                    <asp:TextBox ID="txtTotal" runat="server" CssClass="control span6" Enabled="False"></asp:TextBox>
                                                </div>
                                                <div class="span12">
                                                    <asp:Label ID="Label6" CssClass="span6" runat="server" meta:resourcekey="lblLecturas"></asp:Label>
                                                    <asp:TextBox ID="txtLecturas" runat="server" CssClass="control span6" Enabled="False"></asp:TextBox>
                                                </div>
                                                <div class="span12">
                                                    <asp:Label ID="Label10" CssClass="span6" runat="server" meta:resourcekey="lblFaltantes"></asp:Label>
                                                    <asp:TextBox ID="txtFaltantes" runat="server" CssClass="control span6" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                         <div class="span3"></div>
                                         <div class="span6">
                                            <table id="tablaEstatus" class="table table-striped table-advance table-hover">
                                                <thead><tr><th colspan="1000" style="text-align: center; font-weight: bold;"><asp:Label ID="Label11" runat="server" meta:resourcekey="lblEncabezadoGrid"></asp:Label></th></tr></thead>
                                                <tbody></tbody>
                                            </table>
                                         </div>
                                         <div class="span3"></div>
                                    </div>
                                    <div class="row-fluid">&nbsp;</div>
                                     <div class="row-fluid">
                                         <div class="span4"></div>
                                         <div class="span4" style="text-align: center;">
                                             <button type="button" id="btnGuardar" data-toggle="modal" class="btn letra SuKarne">
                                                <asp:Label ID="Label12" runat="server" meta:resourcekey="btnGuardar"></asp:Label>
                                            </button>
                                         </div>
                                         <div class="span4"></div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        
                    </div>
                </div>
           </div>
       </div>
    </form>
</body>
</html>
