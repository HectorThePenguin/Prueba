<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Disponibilidad.aspx.cs" Inherits="SIE.Web.Manejo.Disponibilidad" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
        <%: Styles.Render("~/bundles/DisponibilidadEstilo") %>
    </asp:PlaceHolder>
    <script src="../Scripts/Disponibilidad.js"></script>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <%--<link href="../assets/css/Disponibilidad.css" rel="stylesheet" />--%>
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
        <div>
            <div id="skm_LockPane" class="LockOff">
            </div>
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="caption">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <span>
                            <asp:Label ID="lblTitulo" runat="server" Text="Disponibilidad" meta:resourcekey="lblTituloResource1"></asp:Label></span>
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
                            <a href="../Manejo/Disponibilidad.aspx">Disponibilidad</a>
                        </li>
                    </ul>

                    <div class="row-fluid">
                        <div class="span5">
                            <span class="span2">
                                <asp:Label ID="lblSemanas" runat="server" Text="Semanas" meta:resourcekey="lblSemanasResource1"></asp:Label>
                            </span>
                            <span class="span3">
                                <select id="ddlSemanas" class="span12">
                                </select>
                            </span>
                        </div>
                    </div>
                    <br />
                    <div id="divCorrales" style='overflow-x:auto;overflow-y:hidden;'>

                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                            <asp:Label ID="lblCorrales" runat="server" Text="Corrales" meta:resourcekey="lblCorralesResource1"></asp:Label></legend>
                        
                            <div id="GridCorrales">
                            </div>
                        </fieldset>
                     </div>
                    <div class="textoDerecha">
                        <span class="espacioCortoDerecha">
                            <button type="button" id="btnGuardar" class="btn SuKarne">
                                <asp:Label ID="lblGuardar" runat="server" Text="Guardar" meta:resourcekey="lblGuardarResource1"></asp:Label></button>
                        </span>

                        <span>
                            <button id="btnCancelar" type="button" class="btn SuKarne">
                                <asp:Label ID="lblCancelar" runat="server" Text="Cancelar" meta:resourcekey="lblCancelarResource1"></asp:Label>
                            </button>
                        </span>
                    </div>
                </div>
            </div>

        </div>
    </form>
    <script type="text/javascript">

        //MENSAJES
        var Aceptar = '<asp:Literal runat="server" Text="<%$ Resources:js.Aceptar %>"/>';
        var Si = '<asp:Literal runat="server" Text="<%$ Resources:js.Si %>"/>';
        var No = '<asp:Literal runat="server" Text="<%$ Resources:js.No %>"/>';
        var CambiarSemana = '<asp:Literal runat="server" Text="<%$ Resources:js.CambiarSemana %>"/>';
        var SalirSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.SalirSinGuardar %>"/>';
        var GuardarSinCambios = '<asp:Literal runat="server" Text="<%$ Resources:js.GuardarSinCambios %>"/>';
        var GuardadoExito = '<asp:Literal runat="server" Text="<%$ Resources:js.GuardadoExito %>"/>';
        var ErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorGuardar %>"/>';
        var SinSemanas = '<asp:Literal runat="server" Text="<%$ Resources:js.SinSemanas %>"/>';
        var ErrorSemanas = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorSemanas %>"/>';
        var Zilmax = '<asp:Literal runat="server" Text="<%$ Resources:js.Zilmax %>"/>';
        var SinCorrales = '<asp:Literal runat="server" Text="<%$ Resources:js.SinCorrales %>"/>';
        var ErrorCorrales = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorCorrales %>"/>';
        //MENSAJES


        //CABECEROS TABLA CORRALES
        var CabeceroCorral = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Corral %>"/>';
        var CabeceroLote = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Lote %>"/>';
        var CabeceroTipo = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Tipo %>"/>';
        var CabeceroClasificacion = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Clasificacion %>"/>';
        var CabeceroCabezas = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Cabezas %>"/>';
        var CabeceroPesoOrigen = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.PesoOrigen %>"/>';
        var CabeceroMerma = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Merma %>"/>';
        var CabeceroPesoProyectado = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.PesoProyectado %>"/>';
        var CabeceroGananciaDiaria = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.GananciaDiaria %>"/>';
        var CabeceroDiasEngorda = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.DiasEngorda %>"/>';
        var CabeceroDiasProyectados = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.DiasProyectados %>"/>';
        var Cabecero1FechaReimplante = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Fecha1Reimplante %>"/>';
        var Cabecero1Peso = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Peso1Reimplante %>"/>';
        var Cabecero1GananciaDiaria = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Ganancia1Reimplante %>"/>';
        var Cabecero2FechaReimplante = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Fecha2Reimplante %>"/>';
        var Cabecero2Peso = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Peso2Reimplante %>"/>';
        var Cabecero2GananciaDiaria = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Ganancia2Reimplante %>"/>';
        var Cabecero3FechaReimplante = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Fecha3Reimplante %>"/>';
        var Cabecero3Peso = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Peso3Reimplante %>"/>';
        var Cabecero3GananciaDiaria = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Ganancia3Reimplante %>"/>';
        var CabeceroDiasF4 = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.DiasF4 %>"/>';
        var CabeceroDiasZilmax = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.DiasZilmax %>"/>';
        var CabeceroFechaDisponibilidad = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.FechaDisponibilidad %>"/>';
        var CabeceroFechaAsignada = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.FechaAsignada %>"/>';
        var CabeceroSemanas = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Semanas %>"/>';
        var CabeceroDias = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Dias %>"/>';
        //CABECEROS TABLA CORRALES

    </script>
    <%--<script src="../Scripts/jscomun.js"></script>--%>
    <%--<script src="../Scripts/Disponibilidad.js"></script>--%>
</body>
</html>
