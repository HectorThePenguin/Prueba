<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConteoCalificacion.aspx.cs" Inherits="SIE.Web.Recepcion.ConteoCalificacion" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/ConteoCalificacion.js"></script>
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
        <div id="skm_LockPane" class="LockOff">
        </div>
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                    <span>
                        <asp:Label ID="lblTitulo" runat="server" Text="Conteo de Calificación" meta:resourcekey="lblTituloResource1"></asp:Label></span>
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
                        <a href="../Recepcion/ConteoCalificacion.aspx">Conteo de Calificación</a>
                    </li>
                </ul>

                <div class="row-fluid">



                    <div class="row-fluid">
                        <span class="span2">
                            <span class="requerido">*</span>
                            <asp:Label ID="lblEntrada" runat="server" Text="N° Entrada:" meta:resourcekey="lblEntradaResource1"></asp:Label>
                        </span>

                        <span class="span4">
                            <asp:Label ID="lblProveedor" runat="server" Text="Proveedor:" meta:resourcekey="lblProveedorResource1"></asp:Label>
                        </span>

                        <span class="span2">

                            <asp:Label ID="lblCorral" runat="server" Text="Corral:" meta:resourcekey="lblCorralResource1"></asp:Label>
                        </span>

                        <span class="span1">
                            <asp:Label ID="lblCabezas" runat="server" Text="Cabezas:" meta:resourcekey="lblCabezasResource1"></asp:Label>
                        </span>

                        <span class="span1">
                            <asp:Label ID="lblMuertas" runat="server" Text="Muertas:" meta:resourcekey="lblMuertasResource1"></asp:Label>
                        </span>

                        <span class="span2">
                            <asp:Label ID="llbFecha" runat="server" Text="Fecha:" meta:resourcekey="llbFechaResource1"></asp:Label>
                        </span>
                    </div>

                    <div class="row-fluid">
                        <span class="span2">
                            <input class="span10 textBoxChico soloNumeros" tabindex="1" oninput="maxLengthCheck(this)" maxlength="7" id="txtEntrada" min="0" type="number" />
                        </span>

                        <span class="span4">
                            <input class="span12 textBoxChico" disabled="disabled" id="txtProveedor" type="text" />
                        </span>

                        <span class="span2">
                            <input class="span12 textBoxChico" disabled="disabled" id="txtCorral" type="text" />

                        </span>

                        <span class="span1">
                            <input class="span12 textBoxChico textBoxDerecha" disabled="disabled" id="txtCabezas" type="text" />
                        </span>

                        <span class="span1">
                            <input class="span12 textBoxChico textBoxDerecha" disabled="disabled" id="txtMuertas" type="text" />
                        </span>

                        <span class="span2">
                            <input class="span12 textBoxChico" disabled="disabled" id="txtFecha" type="text" />
                        </span>
                    </div>

                    <div class="row-fluid">
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border"></legend>

                            <div class="row-fluid">
                                <span class="span2"></span>
                                <span class="span4">
                                    <asp:Label ID="lblMachos" runat="server" Text="Machos" meta:resourcekey="lblMachosResource1"></asp:Label>
                                </span>

                                <span class="span4">
                                    <asp:Label ID="lblHembras" runat="server" Text="Hembras" meta:resourcekey="lblHembrasResource1"></asp:Label>
                                </span>

                                <span class="span2">
                                    <asp:Label ID="lblGanadoMuerto" runat="server" Text="Ganado Muerto" meta:resourcekey="lblGanadoMuertoResource1"></asp:Label>
                                </span>
                            </div>

                            <div class="row-fluid">
                                <span class="span2"></span>
                                <span class="span4">
                                    <input class="span4 textBoxDerecha textBoxGrande" disabled="disabled" id="txtMachos" value="0" type="text" />
                                </span>

                                <span class="span4">
                                    <input class="span4 textBoxDerecha textBoxGrande" disabled="disabled" id="txtHembras" value="0" type="text" />
                                </span>

                                <span class="span2">
                                    <input class="span8 textBoxDerecha textBoxGrande soloNumeros"  disabled="disabled"  oninput="maxLengthCheck(this)" maxlength="2"  id="txtGanadoMuerto" min="0" type="number" />
                                </span>
                            </div>

                            <div id="ConteoCalidad" class="row-fluid">
                                <div class="row-fluid span12"></div>
                            </div>

                        </fieldset>

                        <div class="textoDerecha">
                            <span class="espacioCortoDerecha">
                                <button id="btnGuardar" type="button" class="btn SuKarne">
                                    <asp:Label ID="lblGuardar" runat="server" Text="Guardar" meta:resourcekey="lblGuardarResource1"></asp:Label>
                                </button>
                            </span>
                            <span class="espacioCortoDerecha">
                                <button id="btnCancelar" type="button" class="btn SuKarne">
                                    <asp:Label ID="lblCancelar" runat="server" Text="Cancelar" meta:resourcekey="lblCancelarResource1"></asp:Label>
                                </button>
                            </span>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfEntradaGanadoID" runat="server" />
    </form>
     <script type="text/javascript">

         //MENSAJES
        

         var EmbarqueNoRecibido = '<asp:Literal runat="server" Text="<%$ Resources:js.EmbarqueNoRecibido %>"/>';
         var EntradaCosteada = '<asp:Literal runat="server" Text="<%$ Resources:js.EntradaCosteada %>"/>';
         var EntradaConCalidad = '<asp:Literal runat="server" Text="<%$ Resources:js.EntradaConCalidad %>"/>';
         var EntradaSinCondicion = '<asp:Literal runat="server" Text="<%$ Resources:js.EntradaSinCondicion %>"/>';
        
         var FaltaDatos = '<asp:Literal runat="server" Text="<%$ Resources:js.FaltaDatos %>"/>';
         var CabezasMayor = '<asp:Literal runat="server" Text="<%$ Resources:js.CabezasMayor %>"/>';
         var Cancelar = '<asp:Literal runat="server" Text="<%$ Resources:js.Cancelar %>"/>';
         var Si = '<asp:Literal runat="server" Text="<%$ Resources:js.Si %>"/>';
         var No = '<asp:Literal runat="server" Text="<%$ Resources:js.No %>"/>';

         var GuardoExito = '<asp:Literal runat="server" Text="<%$ Resources:js.GuardoExito %>"/>';
         var ErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorGuardar %>"/>';
         var ErrorConsultaCalidad = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorConsultaCalidad %>"/>';
         var EntradaNoValida = '<asp:Literal runat="server" Text="<%$ Resources:js.EntradaNoValida %>"/>';
         var ErrorEntrada = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorEntrada %>"/>';
         
         //MENSAJES


    </script>
</body>
</html>
