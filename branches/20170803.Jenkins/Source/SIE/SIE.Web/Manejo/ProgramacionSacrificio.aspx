<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgramacionSacrificio.aspx.cs" Inherits="SIE.Web.Manejo.ProgramacionSacrificio" %>

<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headEvaluacion" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>

    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/ProgramacionSacrificio.js"></script>

    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
    <style type="text/css">
        </style>
    <link rel="shortcut icon" href="../favicon.ico" />
    <style>
        .tdinvisible {
            display: none;
        }
         
        table tr.even.row_selected td {
            background-color: #B0BED9;
        }
 
        table tr.odd.row_selected td {
            background-color: #9FAFD1;
        }
    </style>
    <script type="text/javascript">
        var hArete = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderArete %>"/>';
        var hConsecutivo = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderConsecutivo %>"/>';
        var hSeleccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HeaderSeleccion %>"/>';
        var btnGuardarText = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.btnGuardarText %>"/>';
        var btnCancelarText = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.btnCancelarText %>"/>';
        var btnAgregarText = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.btnAgregarText %>"/>';
        var msgDlgCancelar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgDlgCancelar %>"/>';
        var msgGuardadoExito = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgGuardadoExito %>"/>';
        var msgIngresarCorral = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgIngresarCorral %>"/>';
        var msgAreteNoCorral = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgAreteNoCorral %>"/>';
        var msgCabezasMenorSacrificio = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgCabezasMenorSacrificio %>"/>';
        var msgCabezasIgualSacrificio = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgCabezasIgualSacrificio %>"/>';
        var msgErrorObtenerNoHayCorrales = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorObtenerNoHayCorrales.Text %>"/>';
        var msgErrorObtenerListaCorrales = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorObtenerListaCorrales.Text %>"/>';
        var msgUsuarioNoPermitido = '<asp:Literal runat="server" Text="<%$ Resources:msgUsuarioNoPermitido.Text %>"/>';
        var btnDlgSeleccionar = '<asp:Literal runat="server" Text="<%$ Resources:btnDlgSeleccionar.Text %>"/>';
        var btnDlgCerrar = '<asp:Literal runat="server" Text="<%$ Resources:btnDlgCerrar.Text %>"/>';
        var msgSeleccioneOrden = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccioneOrden.Text %>"/>';
        var msgIngreseNoIndividual = '<asp:Literal runat="server" Text="<%$ Resources:msgIngreseNoIndividual.Text %>"/>';
        var msgNoIndividualNoCorresponde = '<asp:Literal runat="server" Text="<%$ Resources:msgNoIndividualNoCorresponde.Text %>"/>';
        var msgAreteAgregado = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteAgregado.Text %>"/>';
        var msgDlgBuscar = '<asp:Literal runat="server" Text="<%$ Resources:msgDlgBuscar.Text %>"/>';
        var msgProgramadasNoCapacidad = '<asp:Literal runat="server" Text="<%$ Resources:msgProgramadasNoCapacidad.Text %>"/>';
        var msgCabezasMenorProgramadas = '<asp:Literal runat="server" Text="<%$ Resources:msgCabezasMenorProgramadas.Text %>"/>';
        var msgNoIngresoCorral = '<asp:Literal runat="server" Text="<%$ Resources:msgNoIngresoCorral.Text %>"/>';
        var msgNoExistenOrdenesSacrificio = '<asp:Literal runat="server" Text="<%$ Resources:msgNoExistenOrdenesSacrificio.Text %>"/>';
        var msgCorralSinAretes = '<asp:Literal runat="server" Text="<%$ Resources:msgCorralSinAretes.Text %>"/>';
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
                                <div class="caption">
                                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" />
                                    <span>
                                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="ProgramacionSacrificio_Title"></asp:Label></span>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <!-- Breadcums -->
                                <div class="row-fluid">
                                    <div class="span12">
                                        <ul class="breadcrumb">
                                            <li>
                                                <i class="icon-home"></i>
                                                <a href="../Principal.aspx">
                                                    <asp:Label ID="LabelHome" runat="server" meta:resourcekey="ProgramacionSacrificio_Home" /></a>
                                                <i class="icon-angle-right"></i>
                                            </li>
                                            <li>
                                                <a href="ProgramacionSacrificio.aspx">
                                                    <asp:Label ID="LabelMenu" runat="server" meta:resourcekey="ProgramacionSacrificio_Title"></asp:Label></a>
                                            </li>

                                        </ul>
                                    </div>
                                </div>
                                <!-- Fin de Breadcums -->
                                <!-- Controles -->
                                <!-- Busqueda del corral -->
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblCorral" runat="server" meta:resourcekey="lblCorral" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtCorral" CssClass="m-wrap medium span12" Enabled="false"></asp:TextBox>
                                                    <a data-toggle="modal" id="btnBuscar">
                                                        <img src="../Images/find.png" class="image" width="32" height="32"></a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtFecha" CssClass="m-wrap medium span12" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblHora" runat="server" meta:resourcekey="lblHora" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtHora" CssClass="m-wrap medium span12" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Arete y agregar -->
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblArete" runat="server" meta:resourcekey="lblArete" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <input class="m-wrap medium span12" disabled="disabled" oninput="maxLengthCheck(this)" maxlength="15" id="txtArete" type="tel" />
                                                    <%--<asp:TextBox runat="server" ID="txtArete"  CssClass="m-wrap medium span12" Enabled="false" MaxLength="15"></asp:TextBox>--%>
                                                    <button class="btn SuKarne" id="btnAgregar" data-toggle="modal"></button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblCabezasASacrificar" runat="server" meta:resourcekey="lblCabezasASacrificar" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtCabezasASacrificar" CssClass="m-wrap medium span12 text-right" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin de Controles-->

                                <!-- Tabla de datos -->
                                <div class="row-fluid">
                                    <div id="scroll" class="span12">
                                        <div id="TablaAretes" class="span12">
                                        </div>
                                    </div>
                                </div>

                                <!-- Footer controles Guardar Cancelar -->
                                <div class="row-fluid">
                                    <div class="modal-footer">
                                        <button class="btn SuKarne" id="btnGuardar" data-toggle="modal"></button>
                                        <button id="btnCancelar" class="btn SuKarne" data-toggle="modal"></button>
                                    </div>
                                </div>
                                <!-- Fin Footer controles -->

                                <!--Dialogo de OrdenesSacrificio-->
                                <div id="responsive" class="modal hide fade" tabindex="-1" style="margin-top: -150px; margin-left: -350px; display: block; width: 700px; " aria-hidden="false">
                                  <div class="portlet box SuKarne2">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <asp:Label ID="lblMensajeDialogoOrdenes" runat="server" meta:resourcekey="lblOrdenesSacrificioResource1"></asp:Label>
                                        </div>
                                    </div>
                                    <!--Cuerpo de dialogo-->
                                    <div class="portlet-body">
                                        <!-- <table class="table table-striped table-bordered table-hover">
                                            <tr>
                                                <td> -->
                                                    <asp:GridView ID="gvBusqueda" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True"
                                                        BorderColor="#CC9966" BorderStyle="None" CssClass="table table-striped"
                                                        BorderWidth="1px" CellPadding="10" OnRowDataBound="gvBusqueda_RowDataBound" meta:resourcekey="gvBusquedaResource1" OnPageIndexChanging="gvBusqueda_PageIndexChanging" PageSize="5" OnPreRender="gvBusqueda_PreRender">
                                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Codigo" HeaderText="Corral" HtmlEncode="false" meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                                                            <asp:BoundField DataField="Lote" HeaderText="Lote" HtmlEncode="false" meta:resourcekey="BoundFieldResource4"></asp:BoundField>
                                                            <asp:BoundField DataField="Cabezas" HeaderText="Cabezas" HtmlEncode="false" meta:resourcekey="BoundFieldResource5"></asp:BoundField>
                                                            <asp:BoundField DataField="CabezasASacrificar" HeaderText="Programadas a sacrificio" HtmlEncode="false" meta:resourcekey="BoundFieldResource6"></asp:BoundField>
                                                            <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" meta:resourcekey="BoundFieldResource7"></asp:BoundField>
                                                            <asp:BoundField DataField="Nombre" HeaderText="Jefe sanidad" HtmlEncode="false" meta:resourcekey="BoundFieldResource8"></asp:BoundField>
                                                            <asp:BoundField DataField="LoteID" HeaderText="" HtmlEncode="false" meta:resourcekey="BoundFieldResource9">
                                                            <HeaderStyle CssClass="tdinvisible" />
                                                            <ItemStyle CssClass="tdinvisible" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CorralID" HeaderText="" HtmlEncode="false" meta:resourcekey="BoundFieldResource10">
                                                            <HeaderStyle CssClass="tdinvisible" />
                                                            <ItemStyle CssClass="tdinvisible" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="OrdenSacrificioDetalleID" HeaderText="" HtmlEncode="false" meta:resourcekey="BoundFieldResource11">
                                                            <HeaderStyle CssClass="tdinvisible" />
                                                            <ItemStyle CssClass="tdinvisible" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <PagerSettings FirstPageText="Primero"  LastPageText="Ultimo" PageButtonCount="2" />
                                                        <PagerStyle CssClass="pagination" />
                                                    </asp:GridView>
                                               <!-- </td>
                                            </tr>
                                        </table> -->
                                    </div>
                                    <!--Fin cuerpo de dialogo-->
                                    <div class="modal-footer">
                                        <button id="btnCerrar" type="submit" class="btn SuKarne" data-dismiss="modal">
                                            <asp:Label ID="lblCerrarEtiqueta" runat="server" meta:resourcekey="lblCerrarEtiquetaResource1"></asp:Label>
                                        </button>
                                        <button type="button" id="btnSeleccion"  class="btn SuKarne">
                                            <asp:Label ID="lblSeleccionarEtiqueta" runat="server" meta:resourcekey="lblSeleccionarEtiquetaResource1"></asp:Label></button>
                                    </div>
                                  </div>
                                <asp:HiddenField runat="server" ID="paginado" Value="0" />
                                <asp:HiddenField runat="server" ID="cabezasASacrificar" Value="0" />
                                <asp:HiddenField runat="server" ID="loteID" Value="0" />
                                <asp:HiddenField runat="server" ID="corraletaID" Value="0" />
                                <asp:HiddenField runat="server" ID="ordenSacrificioDetalleID" Value="0" />
                                </div>  
                                <!--Fin Dialogo Ordenes de Sacrificio -->

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
</html>
