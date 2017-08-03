<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CierreCorral.aspx.cs" Inherits="SIE.Web.Manejo.CierreCorral" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/css/style-metro.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-modal.css" rel="stylesheet" />
    <script src="../assets/plugins/jquery-1.7.1.min.js"></script>
    <script src="../assets/plugins/bootstrap-bootbox/js/bootbox.min.js"></script>
    <link href="../assets/css/media-queries.css" rel="stylesheet" />
    <link href="../assets/plugins/data-tables/DT_bootstrap.css" rel="stylesheet" />
    <link href="../assets/css/CierreCorral.css" rel="stylesheet" />

    <script src="../assets/plugins/data-tables/jquery.dataTables.js"></script>

    <script src="../Scripts/CierreCorral.js"></script>
    <script src="../Scripts/json2.js"></script> 
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</head>
<body class="page-header-fixed">
    <div id="Principal">
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
                                <span>
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1"></asp:Label></span>
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
                                    <a href="CierreCorral.aspx">Cierre de Corral</a>
                                </li>
                            </ul>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="lblMonitor" runat="server" meta:resourcekey="lblMonitorResource1"></asp:Label></legend>
                                    <div class="control-group">
                                        <div id="GridCorrales"></div>


                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="responsive" class="modal container hide fade" data-backdrop="static" data-keyboard="false" tabindex="-1" data-width="1000">
                <div id="divModal" class="LockOff">
                    Por favor espere...
                </div>
                <div class="portlet box SuKarne2">
                    <div class="portlet-title">
                        <div class="caption">
                            <asp:Label ID="lblCheckList" runat="server" meta:resourcekey="lblCheckListResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="subTitulo">
                            <span class="alineacionIzquierda">
                                <asp:Label ID="lblFolioISO" runat="server" meta:resourcekey="lblFolioISOResource1"></asp:Label>
                            </span>
                            <span class="espacioIzquierda">
                                <asp:Label ID="lblRevision" runat="server" meta:resourcekey="lblRevisionResource1"></asp:Label>
                            </span>
                            <span class="alineacionDerecha">
                                <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFechaResource1"></asp:Label>
                            </span>
                        </div>
                        <div class="divCheckList alineacionCentro">
                            <asp:Label ID="lblCheck" CssClass="subTitulo" runat="server" meta:resourcekey="lblCheckResource1"></asp:Label>

                            <table class="table table-bordered table-striped pantallaCompleta ">
                                <tr>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblCorral" runat="server" meta:resourcekey="lblCorralResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCorral" class="span1 textBoxTablas textoDerecha" ReadOnly="True" runat="server" meta:resourcekey="txtCorralResource1"></asp:TextBox>
                                    </td>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblLote" runat="server" meta:resourcekey="lblLoteResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLote" class="span1 textBoxTablas textoDerecha" ReadOnly="True" runat="server" meta:resourcekey="txtLoteResource1"></asp:TextBox>
                                    </td>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblFechaCorral" runat="server" meta:resourcekey="lblFechaCorralResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaCorral" class="span2 textBoxTablas" ReadOnly="True" runat="server" meta:resourcekey="txtFechaCorralResource1"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblPesoCorte" runat="server" meta:resourcekey="lblPesoCorteResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPesoCorte" class="span2 textBoxTablas textoDerecha" ReadOnly="True" runat="server" meta:resourcekey="txtPesoCorteResource1"></asp:TextBox>
                                    </td>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblCabezasSistemas" runat="server" meta:resourcekey="lblCabezasSistemasResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCabezasSistemas" class="span1 textBoxTablas textoDerecha" ReadOnly="True" runat="server" meta:resourcekey="txtCabezasSistemasResource1"></asp:TextBox>
                                    </td>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblFechaSacrificio" runat="server" meta:resourcekey="lblFechaSacrificioResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaSacrificio" class="span2 textBoxTablas" ReadOnly="True" runat="server" meta:resourcekey="txtFechaSacrificioResource1"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblFechaAbierto" runat="server" meta:resourcekey="lblFechaAbiertoResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaAbierto" class="span2 textBoxTablas" ReadOnly="True" runat="server" meta:resourcekey="txtFechaAbiertoResource1"></asp:TextBox>
                                    </td>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblCabezasConteo" runat="server" meta:resourcekey="lblCabezasConteoResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <input class="span1 textBoxTablas soloNumeros textoDerecha" oninput="maxLengthCheck(this)" maxlength="3" id="txtCabezasConteo" type="number" />
                                    </td>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblFecha1Reimplante" runat="server" meta:resourcekey="lblFecha1ReimplanteResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFecha1Reimplante" class="span2 textBoxTablas" ReadOnly="True" runat="server" meta:resourcekey="txtFecha1ReimplanteResource1"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblFechaCerrado" runat="server" meta:resourcekey="lblFechaCerradoResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaCerrado" class="span2 textBoxTablas" ReadOnly="True" runat="server" meta:resourcekey="txtFechaCerradoResource1"></asp:TextBox>
                                    </td>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblOcupacion" runat="server" meta:resourcekey="lblOcupacionResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOcupacion" class="span1 textBoxTablas textoDerecha" ReadOnly="True" runat="server" meta:resourcekey="txtOcupacionResource1"></asp:TextBox>
                                    </td>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblFecha2Reimplante" runat="server" meta:resourcekey="lblFecha2ReimplanteResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFecha2Reimplante" class="span2 textBoxTablas" ReadOnly="True" runat="server" meta:resourcekey="txtFecha2ReimplanteResource1"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblDiasEngorda" runat="server" meta:resourcekey="lblDiasEngordaResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiasEngorda" class="span1 textBoxTablas" ReadOnly="True" runat="server" meta:resourcekey="txtDiasEngordaResource1"></asp:TextBox>
                                    </td>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblTipo" runat="server" meta:resourcekey="lblTipoResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTipo" class="span2 textBoxTablas" ReadOnly="True" runat="server" meta:resourcekey="txtTipoResource1"></asp:TextBox>
                                    </td>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblRaza" runat="server" meta:resourcekey="lblRazaResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRaza" class="span1 textBoxTablas" ReadOnly="True" runat="server" meta:resourcekey="txtRazaResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tabs-right">
                                        <asp:Label ID="lblRequiereRevision" runat="server" meta:resourcekey="lblRequiereRevision"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkRequiereRevision" runat="server" ></asp:CheckBox>
                                    </td>
                                    <td class="tabs-right">
                                        
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td class="tabs-right">
                                       
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div id="divGridConcepto">
                        </div>
                        <br />
                        <div id="divGridAcciones">
                        </div>

                    </div>
                    <div class="modal-footer">


                        <img id="imgImprimir" src="../Images/printer.png" />

                        <button type="button" id="btnGuardar" class="btn SuKarne">
                            <asp:Label ID="lblGuardar" runat="server" meta:resourcekey="lblGuardarResource1"></asp:Label></button>

                        <button id="btnCancelar" type="submit" class="btn SuKarne">

                            <asp:Label ID="lblCancelar" runat="server" meta:resourcekey="lblCancelarResource1"></asp:Label>
                        </button>

                    </div>
                </div>

            </div>
            
            <asp:HiddenField ID="hfOrganizacionID" runat="server" />
            <asp:HiddenField ID="hfErrorImprimir" runat="server" />
        </form>
    </div>
    <script type="text/javascript">

        //MENSAJES
        var ErrorAlConsultarCorrales = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorConsultarCorrales %>"/>';
        var ErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorGuardar %>"/>';
        var ErrorInformacionCorral = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorInformacionCorral %>"/>';
        var SalirSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:js.SalirSinGuardar %>"/>';
        var Si = '<asp:Literal runat="server" Text="<%$ Resources:js.Si %>"/>';
        var No = '<asp:Literal runat="server" Text="<%$ Resources:js.No %>"/>';
        var Imprimir = '<asp:Literal runat="server" Text="<%$ Resources:js.Imprimir %>"/>';
        var DatosObligatorios = '<asp:Literal runat="server" Text="<%$ Resources:js.DatosObligatorios %>"/>';
        var GuardoExito = '<asp:Literal runat="server" Text="<%$ Resources:js.GuardoExito %>"/>';
        var CheckListSinInformacion = '<asp:Literal runat="server" Text="<%$ Resources:js.CheckListSinInformacion %>"/>';
        var SinCorrales = '<asp:Literal runat="server" Text="<%$ Resources:js.SinCorrales %>"/>';
        var ErrorImprimir = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorImprimir %>"/>';
        var Impreso = '<asp:Literal runat="server" Text="<%$ Resources:js.Impreso %>"/>';
        var PartidaAbierta = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorPartidaAbierta %>"/>';
        var ConteoCero = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorConteoCero %>"/>';
        var PesoCompra = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorPesoCompra %>"/>';

        //MENSAJES

        //DESCRIPCIONES
        var Abierto = '<asp:Literal runat="server" Text="<%$ Resources:Abierto %>"/>';
        var Cerrado = '<asp:Literal runat="server" Text="<%$ Resources:Cerrado %>"/>';
        var Dias = '<asp:Literal runat="server" Text="<%$ Resources:Dias %>"/>';
        //DESCRIPCIONES

        //CABECEROS TABLA CORRALES
        var CabeceroCorral = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Corral %>"/>';
        var CabeceroLote = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Lote %>"/>';
        var CabeceroTotalCabezas = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.TotalCabezas %>"/>';
        var CabeceroCabezasActuales = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.CabezasActuales %>"/>';
        var CabeceroCabezasRestantes = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.CabezasRestantes %>"/>';
        var CabeceroFechaInicio = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.FechaInicio %>"/>';
        var CabeceroFechaFin = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.FechaFin %>"/>';
        var CabeceroEstatus = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Estatus %>"/>';
        var CabeceroOpcion = '<asp:Literal runat="server" Text="<%$ Resources:Corrales.Opcion %>"/>';
        //CABECEROS TABLA CORRALES

        //CABECEROS TABLA CONCEPTOS
        var CabeceroConcepto = '<asp:Literal runat="server" Text="<%$ Resources:Conceptos.Concepto %>"/>';
        var CabeceroBueno = '<asp:Literal runat="server" Text="<%$ Resources:Conceptos.Bueno %>"/>';
        var CabeceroMalo = '<asp:Literal runat="server" Text="<%$ Resources:Conceptos.Malo %>"/>';
        var CabeceroObservaciones = '<asp:Literal runat="server" Text="<%$ Resources:Conceptos.Observaciones %>"/>';
        //CABECEROS TABLA CONCEPTOS

        //CABECEROS TABLA ACCIONES
        var CabeceroEstructura = '<asp:Literal runat="server" Text="<%$ Resources:Acciones.Estructura %>"/>';
        var CabeceroAccionesObservaciones = '<asp:Literal runat="server" Text="<%$ Resources:Acciones.Observaciones %>"/>';
        var CabeceroAccionesTomadas = '<asp:Literal runat="server" Text="<%$ Resources:Acciones.AccionesTomadas %>"/>';
        //CABECEROS TABLA ACCIONES

        //DATATABLES
        var PrimeraPagina = '<asp:Literal runat="server" Text="<%$ Resources:datatable.PrimeraPagina %>"/>';
        var UltimaPagina = '<asp:Literal runat="server" Text="<%$ Resources:datatable.UltimaPagina %>"/>';
        var Siguiente = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Siguiente %>"/>';
        var Anterior = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Anterior %>"/>';
        var SinDatos = '<asp:Literal runat="server" Text="<%$ Resources:datatable.SinDatos %>"/>';
        var Mostrando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Mostrando %>"/>';
        var SinInformacion = '<asp:Literal runat="server" Text="<%$ Resources:datatable.SinInformacion %>"/>';
        var Filtrando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Filtrando %>"/>';
        var Mostrar = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Mostrar %>"/>';
        var Cargando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Cargando %>"/>';
        var Procesando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Procesando %>"/>';
        var Buscar = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Buscar %>"/>';
        var SinRegistros = '<asp:Literal runat="server" Text="<%$ Resources:datatable.SinRegistros %>"/>';

        //DATATABLES

    </script>
</body>
<script src="../assets/plugins/jquery-ui-1.10.1.custom.min.js"></script>
<script src="../assets/plugins/bootstrap-modal/js/bootstrap-modal.js"></script>
<script src="../assets/scripts/jquery-jtemplates.js"></script>
<script src="../assets/plugins/data-tables/DT_bootstrap.js"></script>
<script src="../assets/plugins/bootstrap-modal/js/bootstrap-modalmanager.js"></script>
<script src="../assets/scripts/ui-modals.js"></script>
<script src="../assets/scripts/app.js"></script>
<script src="../assets/plugins/spin.js"></script>
<script src="../assets/plugins/jquery.spin.js"></script>
<script src="../assets/plugins/numericInput/jquery-numericInput.min.js"></script>
<script src="../assets/plugins/jquery-linq/linq.js"></script>
<script src="../assets/plugins/jquery-alphanumeric/jquery-alphanumeric.js"></script>

</html>
