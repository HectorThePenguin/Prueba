<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckListReparto.aspx.cs" Inherits="SIE.Web.PlantaAlimentos.CheckListReparto" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
    <link href="../assets/plugins/datepicker/css/datepicker.css" rel="stylesheet" />
    <script src="../assets/plugins/datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/plugins/datepicker/js/locales/bootstrap-datepicker.es.js"></script>
    <script src="../Scripts/CheckListReparto.js"></script>
    <link href="../assets/css/CheckListReparto.css" rel="stylesheet" />
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
                            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                            <span>
                                <asp:Label ID="lblTitulo" runat="server" Text="CheckList de Reparto" meta:resourcekey="lblTituloResource1"></asp:Label></span>
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
                                <a href="../PlantaAlimentos/CheckListReparto.aspx">CheckList Reparto</a>
                            </li>
                        </ul>
                        <div class="row-fluid">
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">
                                    <asp:Label ID="lblDatosProduccion" runat="server" Text="Formato CheckList Reparto" meta:resourcekey="lblDatosProduccionResource1"></asp:Label></legend>
                                <div class="row-fluid">
                                    <span class="span6">
                                        <span class="labelRequerido span1"></span>
                                        <asp:Label ID="lblOperador" CssClass="span2" runat="server" Text="Operador:" meta:resourcekey="lblOperadorResource1"></asp:Label>
                                        <input class="span9 textBoxChico textBoxDerecha" disabled="disabled" id="txtOperador" type="text" />
                                    </span>

                                    <span class="span3">
                                        <span class="labelRequerido span1">*</span>
                                        <asp:Label ID="lblServicio" CssClass="span4" runat="server" Text="Servicio:" meta:resourcekey="lblServicioResource1"></asp:Label>
                                        <select id="ddlServicio" class="span7">
                                            <option value="0">Seleccione</option>
                                        </select>
                                    </span>

                                    <span class="span3">
                                        <span class="labelRequerido span1">*</span>
                                        <asp:Label ID="lblFecha" CssClass="span5" runat="server" Text="Fecha:" meta:resourcekey="lblFechaResource1"></asp:Label>
                                        <input class="span6 textBoxChico soloNumeros" disabled="disabled" oninput="maxLengthCheck(this)" maxlength="10" id="txtFecha" min="0" type="text" />
                                    </span>
                                </div>

                                <div class="row-fluid">
                                    <span class="span3">
                                        <span class="labelRequerido span1">*</span>
                                        <asp:Label ID="lblCamionReparto" CssClass="span4" runat="server" Text="Camión N°:" meta:resourcekey="lblCamionRepartoResource1"></asp:Label>
                                        <input class="span4 textBoxChico espacioLabelIzquierda" oninput="maxLengthCheck(this)" maxlength="7" id="txtCamionReparto" type="text" />
                                        <img src="../Images/find.png" id="btnAyudaCamionReparto" class="imagen" alt="" />
                                    </span>

                                    <span class="span3">
                                        <span class="labelRequerido span1">*</span>
                                        <asp:Label ID="lblHorometroInicial" CssClass="span5" runat="server" Text="Horómetro Inicial:" meta:resourcekey="lblHorometroInicialResource1"></asp:Label>
                                        <input class="span5 textBoxChico soloNumeros espacioLabelDerecha" oninput="maxLengthCheck(this)" maxlength="7" id="txtHorometroInicial" min="0" type="number" />
                                    </span>

                                    <span class="span3">
                                        <span class="labelRequerido span1">*</span>
                                        <asp:Label ID="lblOdometroInicial" CssClass="span4" runat="server" Text="Odómetro Inicial:" meta:resourcekey="lblOdometroInicialResource1"></asp:Label>
                                        <input class="span7 textBoxChico soloNumeros" oninput="maxLengthCheck(this)" maxlength="9" id="txtOdometroInicial" min="0" type="number" />
                                    </span>
                                </div>

                                <div class="row-fluid">
                                    <span class="span3">
                                        <span class="labelRequerido span1"></span>
                                        <asp:Label ID="lblLitrosDiesel" CssClass="span4 " runat="server" Text="Lts. Diesel:" meta:resourcekey="lblLitrosDieselResource1"></asp:Label>
                                        <input class="span4 textBoxChico soloNumeros espacioLabelIzquierda" oninput="maxLengthCheck(this)" maxlength="7" min="0" id="txtLitrosDiesel" type="number" />
                                    </span>

                                    <span class="span3">
                                        <span class="labelRequerido span1"></span>
                                        <asp:Label ID="lblHorometroFinal" CssClass="span5 " runat="server" Text="Horómetro Final:" meta:resourcekey="lblHorometroFinalResource1"></asp:Label>
                                        <input class="span5 textBoxChico soloNumeros espacioLabelDerecha" oninput="maxLengthCheck(this)" maxlength="7" id="txtHorometroFinal" min="0" type="number" />
                                    </span>

                                    <span class="span3">
                                        <span class="labelRequerido span1"></span>
                                        <asp:Label ID="lblOdometroFinal" CssClass="span4" runat="server" Text="Odómetro Final:" meta:resourcekey="lblOdometroFinalResource1"></asp:Label>
                                        <input class="span7 textBoxChico soloNumeros" oninput="maxLengthCheck(this)" maxlength="9" id="txtOdometroFinal" min="0" type="number" />
                                    </span>

                                    <span class="span3 textoDerecha">
                                        <button id="btnHoraMuertas" type="button" class="btn SuKarne">
                                            <asp:Label ID="lblTiempoMuerto" runat="server" Text="Horas Muertas" meta:resourcekey="lblTiempoMuertoResource1"></asp:Label>
                                        </button>
                                    </span>
                                </div>
                            </fieldset>
                        </div>
                        <div class="row-fluid">
                            <div id="divGridRepartos"></div>
                        </div>

                        <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones" meta:resourcekey="lblObservacionesResource1"></asp:Label>
                        <textarea id="txtObservacion" oninput="maxLengthCheck(this)" maxlength="255" class="span12"></textarea>
                        <div class="textoDerecha">
                            <span class="espacioCortoDerecha">
                                <button type="button" id="btnImprimir" class="btn SuKarne">
                                    <asp:Label ID="lblImprimir" runat="server" Text="Imprimir" meta:resourcekey="lblImprimirResource1"></asp:Label></button>
                            </span>

                            <span class="espacioCortoDerecha">
                                <button id="btnGuardar" type="button" class="btn SuKarne">
                                    <asp:Label ID="lblGuardar" runat="server" Text="Guardar" meta:resourcekey="lblGuardarResource1"></asp:Label>
                                </button>
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
        </div>
        <div>
        </div>
        <asp:HiddenField runat="server" ID="hfErrorImprimir" />
        <asp:HiddenField runat="server" ID="hfOperador" />
        <asp:HiddenField runat="server" ID="hfOperadorID" />
    </form>

    <div id="modalBusquedaCamionReparto" class="modal hide fade large" data-backdrop="static" data-keyboard="false" tabindex="-1">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="modal-header">
                    <button type="button" class="close cerrarBusquedaCamionReparto" aria-hidden="true">
                        <img src="../Images/close.png" />
                    </button>
                    <h3 class="caption">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <asp:Label ID="lblBusquedaCamionRepartos" runat="server" Text="Búsqueda de Camión Reparto" meta:resourcekey="lblBusquedaCamionRepartosResource1"></asp:Label>
                    </h3>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="modal-body">
                    <fieldset class="scheduler-border">
                        <legend class="scheduler-border">
                            <asp:Label ID="lblFiltrosCamionReparto" runat="server" Text="Filtros" meta:resourcekey="lblFiltrosCamionRepartoResource1"></asp:Label></legend>
                        <div class="row-fluid no-left-margin">

                            <span class="span6">
                                <asp:Label ID="lblCamionRepartoBusqueda" CssClass="span3" runat="server" Text="Número Economico:" meta:resourcekey="lblCamionRepartoBusquedaResource1"></asp:Label>
                                <input class="span8" oninput="maxLengthCheck(this)" maxlength="10" id="txtCamionRepartoBusqueda" type="text" />
                            </span>
                            <span class="span4 textoDerecha">
                                <button type="button" id="btnBuscarCamionReparto" class="btn SuKarne">
                                    <asp:Label ID="lblBuscarCamionReparto" runat="server" Text="Buscar" meta:resourcekey="lblBuscarCamionRepartoResource1"></asp:Label></button>
                                <button id="btnAgregarCamionReparto" type="button" class="btn SuKarne">
                                    <asp:Label ID="lblAgregarCamionReparto" runat="server" Text="Agregar" meta:resourcekey="lblAgregarCamionRepartoResource1"></asp:Label>
                                </button>
                                <button id="btnCancelarCamionReparto" type="button" class="btn SuKarne">
                                    <asp:Label ID="lblCancelarCamionReparto" runat="server" Text="Cancelar" meta:resourcekey="lblCancelarCamionRepartoResource1"></asp:Label>
                                </button>
                            </span>
                        </div>
                    </fieldset>
                    <div id="divGridCamionReparto">
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="modalHorasMuertas" class="modal hide fade large" data-backdrop="static" data-keyboard="false" tabindex="-1">
        <div class="portlet box SuKarne2">
            <div class="portlet-title">
                <div class="modal-header">
                    <button type="button" class="close cerrarHorasMuertas" aria-hidden="true">
                        <img src="../Images/close.png" />
                    </button>
                    <h3 class="caption">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <asp:Label ID="lblTituloHorasMuertas" runat="server" Text="Horas Muertas" meta:resourcekey="lblTituloHorasMuertasResource1"></asp:Label>
                    </h3>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="modal-body">

                    <fieldset class="scheduler-border">
                        <legend class="scheduler-border">
                            <asp:Label ID="lblDatosHorasMuertes" runat="server" Text="Horas Muertas" meta:resourcekey="lblDatosHorasMuertesResource1"></asp:Label></legend>
                        <div class="row-fluid">
                            <span class="span2">
                                <asp:Label ID="lblHoraInicial" runat="server" Text="Hora Inicial:" meta:resourcekey="lblHoraInicialResource1"></asp:Label>
                            </span>
                            <span class="span2">
                                <input class="span12 horas" id="txtHoraInicial" type="time" />
                            </span>
                            <span class="span2">
                                <asp:Label ID="lblHoraFinal" runat="server" Text="Hora Final:" meta:resourcekey="lblHoraFinalResource1"></asp:Label>
                            </span>
                            <span class="span2">
                                <input class="span12 horas" id="txtHoraFinal" type="time" />
                            </span>

                        </div>
                        <div class="row-fluid">
                            <span class="span2">
                                <asp:Label ID="lblCausa" runat="server" Text="Causa:" meta:resourcekey="lblCausaResource1"></asp:Label>
                            </span>
                            <span class="span6">
                                <select id="ddlCausa" class="span10">
                                    <option value="|">Seleccione
                                    </option>
                                </select>
                            </span>
                        </div>
                        <div class="row-fluid">
                            <span class="span12 textoDerecha">
                                <button type="button" id="btnAgregarMuertas" class="btn SuKarne">
                                    <asp:Label ID="lblAgregarMuertas" runat="server" Text="Agregar" meta:resourcekey="lblAgregarMuertasResource1"></asp:Label></button>
                                <button id="btnLimpiarMuertas" type="button" class="btn SuKarne">
                                    <asp:Label ID="lblLimpiarMuertas" runat="server" Text="Limpiar" meta:resourcekey="lblLimpiarMuertasResource1"></asp:Label>
                                </button>
                            </span>
                        </div>

                    </fieldset>
                    <fieldset class="scheduler-border">
                        <legend class="scheduler-border">
                            <asp:Label ID="lblCausasRegistradas" runat="server" Text="Causas Registradas" meta:resourcekey="lblCausasRegistradasResource1"></asp:Label></legend>
                        <div id="contenedorCausasRegistradas"></div>

                        <table id="GridCausasRegistradas" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th class="alineacionCentro" scope="col">Horas Muertas</th>
                                    <th class="alineacionCentro" scope="col">Causa</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>

                    </fieldset>
                </div>
            </div>

            <div class="modal-footer">
                <button id="btnCerrarHorasMuertas" type="button" class="btn SuKarne">
                    <asp:Label ID="lblCerrarHorasMuertas" runat="server" Text="Cerrar" meta:resourcekey="lblCerrarHorasMuertasResource1"></asp:Label>
                </button>

            </div>
        </div>

    </div>
    <script type="text/javascript">
        var ColumnaNumeroTolva = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaNumeroTolva %>"/>';
        var ColumnaServicio = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaServicio %>"/>';
        var ColumnaReparto = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaReparto %>"/>';
        var ColumnaRacion = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaRacion %>"/>';
        var ColumnaKilosEmbarcados = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaKilosEmbarcados %>"/>';
        var ColumnaKilosRepartidos = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaKilosRepartidos %>"/>';
        var ColumnaSobrante = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaSobrante %>"/>';
        var ColumnaCorralInicio = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaCorralInicio %>"/>';
        var ColumnaCorralFin = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaCorralFin %>"/>';
        var ColumnaHoraInicio = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaHoraInicio %>"/>';
        var ColumnaHoraFin = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaHoraFin %>"/>';
        var ColumnaTotalViaje = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaTotalViaje %>"/>';

        var ColumnaId = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaId %>"/>';
        var ColumnaNumeroEconomico = '<asp:Literal runat="server" Text="<%$ Resources:js.ColumnaNumeroEconomico %>"/>';


        var PiePaginaTotal = '<asp:Literal runat="server" Text="<%$ Resources:js.PiePaginaTotal %>"/>';
        var PiePaginaMerma = '<asp:Literal runat="server" Text="<%$ Resources:js.PiePaginaMerma %>"/>';

        var ErrorConsultarCamionReparto = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorConsultarCamionReparto %>"/>';
        var ErrorGenerarRepartos = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorGenerarRepartos %>"/>';
        var ErrorTipoServicio = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorTipoServicio %>"/>';


        var MensajeSeleccionarCamionReparto = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarCamionReparto %>"/>';
        var MensajeSalirSinSeleccionarCamionReparto = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSalirSinSeleccionarCamionReparto %>"/>';
        var MensajeCamionRepartoNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCamionRepartoNoExiste %>"/>';

        var MensajeSinOperador = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSinOperador %>"/>';
        var MensajeSeleccionarServicio = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarServicio %>"/>';
        var MensajeSeleccionarFecha = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarFecha %>"/>';
        var MensajeSeleccionarIniciales = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarIniciales %>"/>';
        var MensajeSinArchivo = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSinArchivo %>"/>';
        var MensajeSinRegistros = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSinRegistros %>"/>';
        var MensajeServicioMatutino = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeServicioMatutino %>"/>';
        var MensajeServicioVespertino = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeServicioVespertino %>"/>';
        var MensajeSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSinGuardar %>"/>';
        var MensajeSinImprimir = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSinImprimir %>"/>';
        var MensajeCancelar = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCancelar %>"/>';
        var MensajeGuardadoExitoso = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeGuardadoExitoso %>"/>';
        var MensajeSeleccionarCamion = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarCamion %>"/>';



        var MensajeSeleccionarOdometro = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarOdometro %>"/>';
        var MensajeSeleccionarHorometro = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeSeleccionarHorometro %>"/>';
        var MensajeOdometroFinalMenor = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeOdometroFinalMenor %>"/>';
        var MensajeHorometroFinalMenor = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeHorometroFinalMenor %>"/>';
        var MensajeHoraInicialRequerida = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeHoraInicialRequerida %>"/>';
        var MensajeHoraFinalRequerida = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeHoraFinalRequerida %>"/>';
        var MensajeFormatoHorasInicial = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeFormatoHoraInicial %>"/>';
        var MensajeFormatoHorasFinal = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeFormatoHoraFinal %>"/>';
        var MensajeHoraFinalMenor = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeHoraFinalMenor %>"/>';
        var MensajeCausaRequerida = '<asp:Literal runat="server" Text="<%$ Resources:js.MensajeCausaRequerida %>"/>';
        var ErrorCausasTiempoMuerto = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorCausasTiempoMuerto %>"/>';

        var Seleccione = '<asp:Literal runat="server" Text="<%$ Resources:js.Seleccione %>"/>';
        var Si = '<asp:Literal runat="server" Text="<%$ Resources:js.Si %>"/>';
        var No = '<asp:Literal runat="server" Text="<%$ Resources:js.No %>"/>';
        var Ok = '<asp:Literal runat="server" Text="<%$ Resources:js.OK %>"/>';
        
    </script>
</body>

</html>
