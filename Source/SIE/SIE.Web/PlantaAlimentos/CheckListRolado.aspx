<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckListRolado.aspx.cs" Inherits="SIE.Web.PlantaAlimentos.CheckListRolado" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <%--<link href="../assets/css/Comun.css" rel="stylesheet" />--%>
    <link href="../assets/plugins/datepicker/css/datepicker.css" rel="stylesheet" />
    <script src="../assets/plugins/datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/plugins/datepicker/js/locales/bootstrap-datepicker.es.js"></script>

    <link href="../assets/css/jquery.ui.tabs.css" rel="stylesheet" />
    <link href="../assets/css/jquery-ui-1.10.3.custom.css" rel="stylesheet" />

    <link href="../assets/css/ParametrosCheckListRolado.ashx" type="text/css" rel="stylesheet" />
    <link href="../assets/css/CheckListRolado.css" rel="stylesheet" />
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
<body onload="changeHashOnLoad();" ondragstart="return false;" ondrop="return false;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="skm_LockPane" class="LockOff">
        </div>
        <div class="container-fluid" />
        <div class="row-fluid">
            <div class="span12">
                <div class="portlet box SuKarne2">
                    <div class="portlet-title">
                        <div class="caption">
                            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                            <span class="letra">
                                <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1"></asp:Label></span>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <ul class="breadcrumb">
                            <li>
                                <i class="icon-home"></i>
                                <a href="../Principal.aspx">
                                    <asp:Label ID="LabelHome" Text="Home " runat="server" meta:resourcekey="LabelHomeResource1" /></a>
                                <i class="icon-angle-right"></i>
                            </li>
                            <li>
                                <a href="../PlantaAlimentos/CheckListRolado.aspx">
                                    <asp:Label ID="LabelMenu" runat="server" Text="Checklist de Rolado" meta:resourcekey="LabelMenuResource1"></asp:Label>
                                </a>
                            </li>
                        </ul>

                        <div id="tabs">
                            <ul>
                                <li><a href="#TabCheckListRolado"><span>Checklist de Rolado</span></a></li>
                                <li><a href="#TabParametrosRolado" id="A1"><span>Parámetros de Rolado</span></a></li>
                            </ul>
                            <div class="row-fluid">
                                <span class="alineacionDerecha">
                                    <img src="../Images/campanaNotificaciones.png" width="26" height="26" />
                                    <a href="#" id="lnkNotificaciones"><asp:Label runat="server" ID="lblNotificacionesTotales" CssClass="bold" ></asp:Label></a>                                    
                                </span>
                            </div>
                            <div id="TabCheckListRolado">
                                <div class="row-fluid">
                                    <span class="alineacionDerecha">
                                        <asp:Label runat="server" CssClass="bold" Text="Estatus:" meta:resourcekey="LabelResource2"></asp:Label>
                                        <asp:Label runat="server" ID="lblEstatus"></asp:Label>
                                        <img id="imgSupervisor" width="26" height="26" />
                                    </span>
                                </div>
                                <div class="row-fluid">
                                    <fieldset class="scheduler-border">
                                        <legend class="scheduler-border">
                                            <asp:Label ID="lblCheckListRolado" runat="server" Text="Datos Generales" meta:resourcekey="lblCheckListRoladoResource1"></asp:Label>
                                        </legend>
                                        <div class="span12">
                                            <span class="span4">
                                                <asp:Label ID="Label1" CssClass="span3" runat="server" Text="*Turno:" meta:resourcekey="Label1Resource1"></asp:Label>
                                                <select id="ddlTurno">
                                                </select>
                                            </span>
                                            <span class="span8">
                                                <asp:Label ID="Label2" runat="server" CssClass="span2" Text="Responsable:" meta:resourcekey="Label2Resource1"></asp:Label>
                                                <input type="text" class="span10" id="txtResponsable" disabled="disabled" />
                                            </span>
                                        </div>

                                        <div class="span12">
                                            <span class="span4">
                                                <asp:Label CssClass="span3" runat="server" Text="*Roladora:" meta:resourcekey="LabelResource4"></asp:Label>
                                                <select id="ddlRoladora">
                                                </select>
                                            </span>
                                            <span class="span8">
                                                <div class="span4">
                                                    <asp:Label runat="server" CssClass="span6" Text="Fecha:" meta:resourcekey="LabelResource5"></asp:Label>
                                                    <input id="txtFecha" class="span6 soloNumeros" type="text" disabled="disabled" />
                                                </div>
                                                <div class="span4">
                                                    <asp:Label runat="server" CssClass="span6" Text="Hora Inicio:" meta:resourcekey="LabelResource6"></asp:Label>
                                                    <input type="text" class="span6 soloNumeros" id="txtHoraInicio" disabled="disabled" />
                                                </div>
                                            </span>
                                        </div>
                                    </fieldset>
                                </div>
                                <!-- SECCION PARAMETROS -->
                                <div id="divCocedor" class="row-fluid">
                                </div>
                                <div id="divAmperaje" class="row-fluid">
                                </div>
                                <div id="divCalidadRolado" class="row-fluid">
                                </div>
                                <!-- SECCION PARAMETROS -->
                                <div class="textoDerecha">
                                    <span class="espacioCortoDerecha">
                                        <button type="button" id="btnSupervisor" class="btn SuKarne">
                                            <asp:Label ID="Label5" runat="server" Text="Supervisor"></asp:Label></button>
                                    </span>
                                    <span class="espacioCortoDerecha">
                                        <button type="button" id="btnGuardar" class="btn SuKarne">
                                            <asp:Label ID="Label6" runat="server" Text="Guardar"></asp:Label></button>
                                    </span>
                                    <span>
                                        <button id="btnCancelar" type="button" class="btn SuKarne">
                                            <asp:Label ID="Label7" runat="server" Text="Cancelar"></asp:Label>
                                        </button>
                                    </span>
                                </div>
                            </div>
                            <!-- SEGUNDO TAB -->
                            <div id="TabParametrosRolado">
                                <div class="span12 row-fluid">
                                    <div class="span5 row-fluid">
                                        <div id="divHorometro"></div>
                                        <br />
                                        <div id="divHumedad"></div>
                                        <br />
                                        <div id="divSurfantante"></div>
                                    </div>
                                    <div class="span1 row-fluid">
                                    </div>
                                    <div class="span5 row-fluid">
                                        <div id="divAguas"></div>
                                        <br />
                                        <div id="divGrano"></div>
                                        <br />
                                        <div id="divDiesel"></div>
                                    </div>
                                </div>
                                <div class="textoDerecha">
                                    <span class="espacioCortoDerecha">
                                        <button type="button" id="btnGuardarParametros" class="btn SuKarne">
                                            <asp:Label ID="Label9" runat="server" Text="Guardar"></asp:Label></button>
                                    </span>
                                    <span>
                                        <button id="btnCancelarParametros" type="button" class="btn SuKarne">
                                            <asp:Label ID="Label10" runat="server" Text="Cancelar"></asp:Label>
                                        </button>
                                    </span>
                                </div>
                            </div>
                            <!-- SEGUNDO TAB -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- MODALES -->
        <div id="modalSupervisor" class="modal hide fade" data-backdrop="static" data-keyboard="false" tabindex="-1">
            <div id="divModal" class="LockOff">
            </div>
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button type="button" class="close cerrarSupervisor" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/skLogo.png" />
                            <asp:Label ID="lblSupervisorTitulo" runat="server" Text="Supervisión"></asp:Label>
                        </h3>
                    </div>

                </div>
                <div class="portlet-body form">
                    <div class="modal-body" style="border: solid 1px #ddd">
                        <div class="row-fluid sinMargen">
                            <div class="span12">
                                <div class="span8">
                                    <span class="margenLabel span3">*Usuario:</span>
                                    <input class="span8 supervisor" oninput="maxLengthCheck(this)" maxlength="50" id="txtUsuario" type="text" />
                                </div>
                                <div class="span4">
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid sinMargen">
                            <div class="span12">
                                <div class="span8">
                                    <span class="margenLabel span3">*Contraseña:</span>
                                    <input class="span8 supervisor" oninput="maxLengthCheck(this)" maxlength="50" id="txtContrasenia" type="password" />
                                </div>
                                <div class="span4">
                                    <button type="button" id="btnOKSupervisor" class="btn SuKarne">
                                        <asp:Label ID="Label4" runat="server" Text="OK">
                                        </asp:Label>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid sinMargen">
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="Label3" runat="server" Text="Observaciones"></asp:Label>
                            </legend>
                            <div class="row-fluid">
                                <textarea id="txtObservaciones" class="span12" rows="3"></textarea>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" id="btnGuardarSupervisor" class="btn SuKarne">
                    <asp:Label ID="lblAceptar" runat="server" Text="Guardar"></asp:Label></button>
                <button id="btnCancelarSupervisor" type="button" class="btn SuKarne">
                    <asp:Label ID="lblCancelarSupervisor" runat="server" Text="Cancelar"></asp:Label>
                </button>
            </div>
        </div>

        <div id="modalNotificaciones" class="modal hide fade" data-backdrop="static" data-keyboard="false" tabindex="-1">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="modal-header">
                        <button type="button" class="close cerrarNotificacion" aria-hidden="true">
                            <img src="../Images/close.png" />
                        </button>
                        <h3 class="caption">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/skLogo.png" />
                            <asp:Label ID="lblNotificacionTitulo" runat="server" Text="Checklist de Rolado"></asp:Label>
                        </h3>
                    </div>

                </div>
                <div class="portlet-body form">
                    <div class="modal-body" style="border: solid 1px #ddd">
                        <div id="divGridNotificaciones">
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button id="btnCerrarNotificacion" type="button" class="btn SuKarne">
                    <asp:Label ID="lblCerrarNotificacion" runat="server" Text="Cerrar"></asp:Label>
                </button>
            </div>
        </div>
        <!-- MODALES -->
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script src="../Scripts/CheckListRolado.js"></script>
        <!-- END PAGE LEVEL SCRIPTS -->
        <!-- END JAVASCRIPTS -->
        <input type="hidden" id="hdnXmlParametros" />
        <input type="hidden" id="hdnUsuarioSupervisorID" />
        <input type="hidden" id="hdnUsuarioID" />
    </form>
</body>
</html>
