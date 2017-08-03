<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlertasSIAP.aspx.cs" Inherits="SIE.Web.Alertas.AlertasSIAP" %>

<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Services.Description" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headAlertas" runat="server">
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/css/AlertasSIAP.css" rel="stylesheet" />    
    <script src="../Scripts/AlertasSIAP.js"></script>
    <style>
    </style>
</head>
<body class="page-header-fixed">
    <div id="pagewrap">
        <form id="idform" action="../Alertas/TablaIncidencias.aspx" runat="server" class="form-horizontal">
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
                                    <fieldset id="Fieldset2" class="scheduler-border">
                                        <legend class="scheduler-border">
                                            <asp:Label ID="Label14" runat="server" meta:resourcekey="lblSeguimientoAlertas" CssClass=""></asp:Label>
                                        </legend>
                                         <asp:Label id="supports" runat="server"/>
                                        <asp:Label runat="server" ID="lblNoTieneIncidencias"></asp:Label>
                                        <div id="TreeViewDinamico" runat="server">
                                            <ul id="tree1" runat="server">

                                            </ul>
                                        </div>
<%--                                        <asp:TreeView ID="AlertasTreeView" CssClass="treeView"
                                            ExpandImageUrl="~/Images/round_plus44.png" 
                                            CollapseImageUrl="~/Images/round_minus44.png"

                                            OnLoad="AlertasTreeView_OnLoad"
                                            runat="server">
                                            <NodeStyle Font-Names="Arial" ForeColor="Black" HorizontalPadding="5"/>
                                            <RootNodeStyle Font-Bold="True" Font-Size="12pt"/>
                                            <ParentNodeStyle Font-Bold="True" Font-Size="10pt"/>
                                            <LeafNodeStyle Font-Bold="True" Font-Size="8pt"/>
                                        </asp:TreeView>--%>
                                    </fieldset>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12" id="contenedorMensaje"></div>
                                    <asp:Label id="Message" runat="server"/>
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