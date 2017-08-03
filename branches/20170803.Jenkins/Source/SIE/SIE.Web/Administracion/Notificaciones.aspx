<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="SIE.Web.Administracion.Notificaciones" %>
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
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="shortcut icon" href="../favicon.ico" />
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
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="skm_LockPane" class="LockOff">
        </div>
        <div class="container-fluid" />
        <div id="divContenedor" class="row-fluid">
            <div class="span12">
                <div class="portlet box SuKarne2">
                    <div class="portlet-title">
                        <div class="caption">
                            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" />
                            <span>
                                <asp:Label ID="lblTitulo" runat="server" Text="Notificaciones" ></asp:Label></span>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <ul class="breadcrumb">
                            <li>
                                <i class="icon-home"></i>
                                <a href="../Principal.aspx">Home</a>
                                <i class="icon-angle-right"></i>
                            </li>
                            <li>
                                <a href="../Administracion/Notificaciones.aspx">Notificaciones</a>
                            </li>
                        </ul>
                        <div class="row-fluid">
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">
                                    <asp:Label ID="lblDatosProduccion" runat="server" Text="Boletas de Recepción Autorizadas"></asp:Label></legend>
                                <div class="row-fluid">
                                    <div id="divGridNotificacionesAutorizadas">
                                    </div>
                                </div>                                
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="../Scripts/Notificaciones.js"></script>
    </form>
</body>
</html>
