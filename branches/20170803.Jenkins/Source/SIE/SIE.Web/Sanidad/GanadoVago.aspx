<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GanadoVago.aspx.cs" Inherits="SIE.Web.Sanidad.GanadoVago" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <head>    
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
            <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
            <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        </asp:PlaceHolder>
        <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    
        <script src="../Scripts/GanadoVago.js"></script>
        <style>
            #contenedorMensaje {
                font-size: 14px;
                text-align: center;
            }
            .numeroGrande {
                font-size: 16px;
            }
        </style>

         <script type="text/javascript">
             var mensajeErrorAlConsultarAretes = '<asp:Literal runat="server" Text="<%$ Resources:mensajeErrorAlConsultarAretes %>"/>';
             var mensajeAreteNoEncontrado = '<asp:Literal runat="server" Text="<%$ Resources:mensajeAreteNoEncontrado %>"/>';
             var mensajeAreteTestigoNoEncontrado = '<asp:Literal runat="server" Text="<%$ Resources:mensajeAreteTestigoNoEncontrado %>"/>';
             var mensajeAmbosAretesNoEncontrado = '<asp:Literal runat="server" Text="<%$ Resources:mensajeAmbosAretesNoEncontrado %>"/>';
             var mensajeDebeIngresarArete = '<asp:Literal runat="server" Text="<%$ Resources:mensajeDebeIngresarArete %>"/>';
             var mensajePrimerSeccionArete = '<asp:Literal runat="server" Text="<%$ Resources:mensajePrimerSeccionArete %>"/>';
             var mensajeSegundaSeccion = '<asp:Literal runat="server" Text="<%$ Resources:mensajeSegundaSeccion %>"/>';
             var mensajePrimerSeccionAreteTestigo = '<asp:Literal runat="server" Text="<%$ Resources:mensajePrimerSeccionAreteTestigo %>"/>';
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
                                    <a href="../Sanidad/GanadoVago.aspx"><asp:Label ID="Label1" runat="server" meta:resourcekey="lblTitulo"></asp:Label></a>
                                </li>
                            </ul>
                        </div>
                        
                        <div class="row-fluid">
                             <fieldset id="Fieldset2" class="scheduler-border">
                                <legend class="scheduler-border">
                                    <asp:Label ID="Label14" runat="server" meta:resourcekey="lblFiltroBusqueda"></asp:Label>
                                </legend>
                                 <div class="span4">
                                    <div class="span3"><asp:Label ID="lblArete" runat="server" meta:resourcekey="lblArete"></asp:Label>:</div>
                                    <div class="span5"><asp:TextBox ID="txtArete" runat="server" CssClass="control span12" type="number"></asp:TextBox></div>
                                 </div>
                                 <div class="span4">
                                    <div class="span3"><asp:Label ID="lblAreteTestigo" runat="server" meta:resourcekey="lblAreteTestigo"></asp:Label>:</div>
                                    <div class="span5"><asp:TextBox ID="txtAreteTestigo" runat="server" CssClass="control span12" type="number"></asp:TextBox></div>
                                 </div>
                                 <div class="span3 pull-right">
                                    <button type="button" id="btnBuscar" class="btn SuKarne"><asp:Label ID="label4" runat="server" meta:resourcekey="btnBuscar"></asp:Label></button>
                                    <button type="button" id="btnLimpiar" class="btn SuKarne"><asp:Label ID="label2" runat="server" meta:resourcekey="btnLimpiar"></asp:Label></button>
                                </div>
                            </fieldset>
                        </div>
                        <div class="row-fluid">
                            <div class="span12" id="contenedorMensaje"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
