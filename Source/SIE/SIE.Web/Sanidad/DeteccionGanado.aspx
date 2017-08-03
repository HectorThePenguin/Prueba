<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeteccionGanado.aspx.cs" Inherits="SIE.Web.Sanidad.DeteccionGanado" Culture="auto" meta:resourcekey="DeteccionGanado" UICulture="auto" %>

<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headEvaluacion" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/css/DeteccionGanado.css" rel="stylesheet" />

    <script src="../Scripts/DeteccionGanado.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
        var hCorral = '<asp:Literal runat="server" Text="<%$ Resources:hCorral.Text %>"/>';
        var hConcepto = '<asp:Literal runat="server" Text="<%$ Resources:hConcepto.Text %>"/>';
        var hAcuerdo = '<asp:Literal runat="server" Text="<%$ Resources:hAcuerdo.Text %>"/>';
        var hNoArete = '<asp:Literal runat="server" Text="<%$ Resources:hNoArete.Text %>"/>';
        var hNoAreteTestigo = '<asp:Literal runat="server" Text="<%$ Resources:hNoAreteTestigo.Text %>"/>';
        var hFoto = '<asp:Literal runat="server" Text="<%$ Resources:HeaderFoto.Text %>"/>';
        var hIdProblema = '<asp:Literal runat="server" Text="<%$ Resources:hIdProblema.Text %>"/>';
        var hDescripcion = '<asp:Literal runat="server" Text="<%$ Resources:hProblema.Text %>"/>';
        var hSeleccione = '<asp:Literal runat="server" Text="<%$ Resources:hSeleccione.Text %>"/>';
        var msgNoTienePermiso = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTienePermiso.Text %>"/>';
        var msgIngresarCorral = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarCorral.Text %>"/>';
        var msgCorralNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:msgCorralNoExiste.Text %>"/>';
        var msgNoTieneLoteActivo = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTieneLoteActivo.Text %>"/>';
        var msgNoPerteneceCorral = '<asp:Literal runat="server" Text="<%$ Resources:msgNoPerteneceCorral.Text %>"/>';
        var msgNoHayProblemasSecundarios = '<asp:Literal runat="server" Text="<%$ Resources:msgNoHayProblemasSecundarios.Text %>"/>';
        var msgNoHayNotificaciones = '<asp:Literal runat="server" Text="<%$ Resources:msgNoHayNotificaciones.Text %>"/>';
        var msgAreteNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteNoExiste.Text %>"/>';
        var msgSeleccionarPartida = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionarPartida.Text %>"/>';
        var msgDatosGuardados = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardados.Text %>"/>';
        var msgTomarFoto = '<asp:Literal runat="server" Text="<%$ Resources:msgTomarFoto.Text %>"/>';
        var msgOcurrioErrorGrabar = '<asp:Literal runat="server" Text="<%$ Resources:msgOcurrioErrorGrabar.Text %>"/>';
        var msgDatosBlanco = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosBlanco.Text %>"/>';
        var lblMensajeDialogo = '<asp:Literal runat="server" Text="<%$ Resources:lblMensajeDialogo.Text %>"/>';
        var msgAreteDetectado = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteDetectado.Text %>"/>';
        var msgAreteMuerto = '<asp:Literal runat="server" Text="<%$ Resources:msgAreteMuerto.Text %>"/>';
        var msgCapturarArete = '<asp:Literal runat="server" Text="<%$ Resources:msgCapturarArete.Text %>"/>';
        var msgCorraletaNoSacrificio = '<asp:Literal runat="server" Text="<%$ Resources:msgCorraletaNoSacrificio.Text %>"/>';
        var msgGradoCorralRecepcion = '<asp:Literal runat="server" Text="<%$ Resources:msgGradoCorralRecepcion %>"/>';
        var msgSalirSinSeleccionar = '<asp:Literal runat="server" Text="<%$ Resources:msgSalirSinSeleccionar %>"/>';


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

        //LABELS

        var Si = '<asp:Literal runat="server" Text="<%$ Resources:Si %>"/>';
        var No = '<asp:Literal runat="server" Text="<%$ Resources:No %>"/>';

        //LABELS

    </script>
</head>
<body class="page-header-fixed">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
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
                                        <asp:Label ID="LabelHome" runat="server" meta:resourcekey="DeteccionGanado_Home" /></a>
                                    <i class="icon-angle-right"></i>
                                </li>
                                <li>
                                    <a href="DeteccionGanado.aspx">
                                        <asp:Label ID="LabelMenu" runat="server" meta:resourcekey="DeteccionGanado_Title"></asp:Label></a>
                                </li>
                            </ul>
                            <div class="row-fluid">
                                <!--Espacio-->
                                <div class="space10"></div>
                                <div class="span12">
                                    <div class="span4">
                                        <span class="letra">
                                            <asp:Label ID="lblIso" CssClass="etiquetaOrganizacion" runat="server" meta:resourcekey="lblEtiquetaIsoResource1"></asp:Label></span>
                                    </div>
                                    <div class="span4">
                                        <span class="letra">
                                            <asp:Label ID="lblRevision" runat="server" meta:resourcekey="lblEtiquetaRevisionResource1"></asp:Label></span>
                                    </div>
                                    <div class="span4">
                                        <div class="span10">
                                            <asp:Label ID="lblFechaISO" runat="server" meta:resourcekey="lblEtiquetaFechaISOResource1"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <!--Espacio-->
                                <div class="space10"></div>

                                <div class="span12">
                                    <div class="span6">
                                        <div class="span5">
                                            <a id="btnNotificaciones" href="#dlgNotificaciones" data-toggle="modal">
                                                <img src="../Images/notificaciones.png" width="22" height="22" />
                                                <asp:Label ID="lblNotificaciones" runat="server" meta:resourcekey="lblEtiquetaNotificacionesResource1"></asp:Label>
                                            </a>
                                        </div>
                                    </div>

                                    <div class="span6">
                                        <div class="span6 pull-right">
                                            <span>
                                                <a id="btnGanadoMuerto" href="#dlgGanadoMuertoDialogo" data-toggle="modal"></a>
                                                <asp:CheckBox ID="chkGanadoMuerto" runat="server"></asp:CheckBox>
                                                <asp:Label ID="lblGanadoMuerto" runat="server" meta:resourcekey="chkEtiquetaGanadoMuertoResource1"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="span1">
                                        </div>
                                    </div>
                                </div>

                                <!--Espacio-->
                                <div class="space10"></div>

                                <div class="span12">
                                    <div class="span4">
                                        <!--Nombre Vaquero-->
                                        <div class="span4">
                                            <asp:Label ID="lblNombreVaquero" runat="server" meta:resourcekey="lblEtiquetaNombreVaqueroResource1"></asp:Label>
                                        </div>
                                        <div class="span8">
                                            <asp:TextBox ID="txtNombreVaquero" runat="server" ReadOnly="true" class="span12" meta:resourcekey="txtNombreVaqueroResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <!--Fecha-->
                                        <div class="span3">
                                            <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblEtiquetaFechaResource1"></asp:Label>
                                        </div>
                                        <div class="span6">
                                            <asp:TextBox ID="txtFecha" runat="server" ReadOnly="true" class="span12" meta:resourcekey="txtFechaResource1"></asp:TextBox>
                                        </div>
                                        <div class="span1">
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <!--Hora-->
                                        <div class="span3">
                                            <asp:Label ID="lblHora" runat="server" meta:resourcekey="lblEtiquetaHoraResource1"></asp:Label>
                                        </div>
                                        <div class="span7">
                                            <asp:TextBox ID="txtHora" runat="server" ReadOnly="true" class="span12" meta:resourcekey="txtHoraResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <!--Espacio-->
                                <div class="space10"></div>

                                <div class="span12">
                                    <div class="span4">
                                        <!--Corral-->
                                        <div class="span4">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblEtiquetaCorral" runat="server" meta:resourcekey="lblEtiquetaCorralResource1"></asp:Label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtCorral" runat="server" AutoPostBack="False" class="span12" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <!--No. de partida-->
                                        <div class="span3">
                                            <asp:Label ID="lblNoPartida" runat="server" meta:resourcekey="lblEtiquetaNoPartidaResource1"></asp:Label>
                                        </div>
                                        <div class="span8">
                                            <asp:TextBox ID="txtNoPartida" runat="server" ReadOnly="true" class="span12"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <!--No. de Fierro-->
                                        <div class="span3">
                                            <asp:Label ID="lblNofierro" runat="server" meta:resourcekey="lblEtiquetaNofierroResource1"></asp:Label>
                                        </div>
                                        <div class="span7">
                                            <asp:TextBox ID="txtNoFierro" runat="server" AutoPostBack="False" class="span12 bloquearMuerte"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <!--Espacio-->
                                <div class="space10"></div>

                                <!--Partida con arete individual-->
                                <div class="span2">
                                    <asp:Label ID="lblPartidaAreteIndivitual" runat="server" meta:resourcekey="lblPartidaAreteIndivitualResource1"></asp:Label>
                                </div>
                                <div class="span1">
                                    <span>
                                        <asp:CheckBox ID="chkSi" CssClass="clasePartida" runat="server"></asp:CheckBox>
                                        <asp:Label ID="lblSi" runat="server" meta:resourcekey="chkSiResource1"></asp:Label>
                                    </span>
                                </div>
                                <div class="span1">
                                    <span>
                                        <asp:CheckBox ID="chkNo" CssClass="clasePartida bloquearMuerte" runat="server"></asp:CheckBox>
                                        <asp:Label ID="lblNo" runat="server">No</asp:Label>
                                    </span>
                                </div>

                                <!--Espacio-->
                                <div class="space10"></div>

                                <div class="span12">
                                    <div class="span7">
                                        <!--No. Arete-->
                                        <div class="span2">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblNoArete" runat="server" meta:resourcekey="lblEtiquetaNoAreteResource1"></asp:Label>
                                        </div>
                                        <div class="span4 no-left-margin">
                                            <asp:TextBox ID="txtNoArete" runat="server" class="span11 bloquearMuerte" meta:resourcekey="txtCorralResource1" type="number"></asp:TextBox>
                                        </div>

                                        <!--Arete Testigo-->
                                        <div class="span2 no-left-margin">
                                            <asp:Label ID="lblAreteTestigo" runat="server" meta:resourcekey="lblEtiquetaAreteTestigoResource1"></asp:Label>
                                        </div>
                                        <div class="span4 no-left-margin">
                                            <asp:TextBox ID="txtAreteTestigo" runat="server" class="span11 bloquearMuerte" meta:resourcekey="txtCorralResource1" type="number"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="span3">
                                        <div class="span5">
                                            <div class="span12">
                                                <a id="btnTomarFoto" class="btn SuKarne" data-togle="modal">
                                                    <img src="../Images/camara.png" width="25" height="22" />
                                                    <asp:Label ID="lblTomarFoto" runat="server" meta:resourcekey="lblEtiquetaTomarFotoResource1"></asp:Label>
                                                </a>
                                            </div>
                                            <div class="control-group">
                                                <div class="controls">
                                                    <input type="file" id="flFoto" name="fotoGanado[]" style="display: none;" />
                                                </div>
                                            </div>
                                            <!--Espacio-->
                                            <div class="space20"></div>
                                        </div>

                                        <div class="span4" id="dvFoto">
                                        </div>
                                        <div class="control-group">
                                            <asp:Label ID="lblFotoDeteccion" runat="server" meta:resourcekey="lblFotoDeteccion" CssClass="control-label"></asp:Label>
                                            <div class="controls">
                                                <div class="imagen-deteccion">
                                                    <output id="outputDeteccion">
                                                        <a id="imageDetect" href="#" style="display: none;">
                                                            <asp:Image runat="server" ID="imgFotoDeteccion" CssClass="fancybox-image" />
                                                        </a>
                                                    </output>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="span2">
                                        <div class="span4">
                                            <input type="button" id="btnAretes" class="btn SuKarne" value="Aretes" />
                                        </div>

                                    </div>

                                </div>

                                <!--Espacio-->
                                <div class="space20"></div>
                                <table class="tablaProblemasSintomas table no-left-margin span12">
                                    <tr>
                                        <td style="border-color: black; border-style: solid; border-width: 1px;" class="span4">
                                            <div id="dvSintomasCRB" class="no-left-margin">
                                            </div>
                                        </td>
                                        <td style="border-color: black; border-style: solid; border-width: 1px;" class="span3">
                                            <div id="dvSintomasPD" class="no-left-margin">
                                            </div>
                                        </td>
                                        <td style="border-color: black; border-style: solid; border-width: 1px;" class="span4">
                                            <div id="dvGolpeado" class="no-left-margin">
                                                <!--Espacio-->
                                                <div class="space10"></div>

                                                <!--Inflamacion del abdomen-->
                                                <div class="span9">
                                                    <asp:Label ID="lblInflamacionAbdomen" runat="server" meta:resourcekey="lblEtiquetaInformacionAbdomenResource1"></asp:Label>
                                                </div>
                                                <div class="span1 sintoma">
                                                    <input type="checkbox" class="checkSolos" id="chkInflamacionAbdomen" value="7" />
                                                </div>

                                                <!--Golpeado-->
                                                <div class="span9">
                                                    <asp:Label ID="lblGolpeado" runat="server" meta:resourcekey="lblEtiquetaGolpeadoResource1"></asp:Label>
                                                </div>
                                                <div class="span1">
                                                    <asp:CheckBox ID="chkGolpeado" CssClass="checkSolos" runat="server" />
                                                </div>

                                                <div class="span11 alineacionCentro">
                                                    <img src="../Images/vaca.png" width="120" />
                                                </div>

                                                <!--Espacio-->
                                                <div class="space10"></div>
                                                <div class="span11 alineacionCentro">
                                                    <asp:DropDownList ID="cmbParteGolpeada" runat="server" />
                                                </div>

                                                <!--Espacio-->
                                                <div class="space15"></div>
                                                <!--Espacio-->
                                                <div class="space20"></div>
                                                <!--Espacio-->
                                                <div class="space10"></div>
                                            </div>
                                        </td>
                                        <td style="border-color: black; border-style: solid; border-width: 1px;" class="span3">
                                            <div id="dvSintomasProblemaGenerico" class="no-left-margin">
                                            </div>
                                        </td>
                                        <td style="border-color: black; border-style: solid; border-width: 1px;" class="span4">
                                            <div id="dvProblemas" class="no-left-margin">
                                                <div class="span12 row-fluid">
                                                    <!--Espacio-->
                                                    <div class="space10"></div>

                                                    <!--CRB-->
                                                    <div class="span8">
                                                        <asp:Label ID="lblCRB" runat="server" meta:resourcekey="lblEtiquetaCRBResource1"></asp:Label>
                                                    </div>
                                                    <div class="span1">
                                                        <span class="problema">
                                                            <input type="checkbox" id="chkCRB" class="checkSolos" value="1" disabled />
                                                        </span>
                                                    </div>

                                                    <!--PD-->
                                                    <div class="span8">
                                                        <asp:Label ID="lblPD" runat="server" meta:resourcekey="lblEtiquetaPDResource1"></asp:Label>
                                                    </div>
                                                    <div class="span1">
                                                        <span class="problema">
                                                            <input type="checkbox" id="chkPD" class="checkSolos" value="4" disabled />
                                                        </span>
                                                    </div>

                                                    <!--Otros-->
                                                    <div class="span8">
                                                        <asp:Label ID="lblOtros" runat="server" meta:resourcekey="lblEtiquetaOtrosResource1"></asp:Label>
                                                    </div>
                                                    <div class="span1">
                                                        <a id="dvVer" data-toggle="modal" href="#dlgVer"></a>
                                                        <a id="btnVer" class="btn SuKarne bloquearMuerte">Ver</a>
                                                    </div>
                                                </div>

                                                <div class="span12 row-fluid">
                                                    <!--Leve-->
                                                    <div class="span6 alineacionCentro">
                                                        <asp:Label ID="lblLeve" runat="server" meta:resourcekey="lblEtiquetaLeveResource1"></asp:Label>
                                                    </div>

                                                    <!--Grave-->
                                                    <div class="span5 alineacionCentro">
                                                        <asp:Label ID="lblGrave" runat="server" meta:resourcekey="lblEtiquetaGraveResource1"></asp:Label>
                                                    </div>

                                                    <!--Grado-->
                                                    <div class="span12 alineacionCentro">
                                                        <asp:Label ID="lblGrado" runat="server" meta:resourcekey="lblEtiquetaGradoResource1"></asp:Label>
                                                    </div>

                                                    <div id="dvRadios">
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                                <div class="space15"></div>
                                <div class="span12 border no-left-margin " style="border-color: black;">
                                    <div class="space10"></div>
                                    <div class="span2">
                                        <asp:Label ID="lblObservaciones" runat="server" meta:resourcekey="lblEtiquetaObservacionesResource1"></asp:Label>
                                    </div>
                                    <div class="span9  no-left-margin">
                                        <asp:TextBox ID="txtObservaciones" CssClass="span12 bloquearMuerte" MaxLength="255" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="space10"></div>
                                </div>
                                <div class="span12 border no-left-margin " style="border-color: black;">
                                    <div class="space10"></div>
                                    <div class="span2">
                                        <span class="requerido">*</span>
                                        <asp:Label ID="lblDescripcionGanado" runat="server" meta:resourcekey="lblEtiquetaDescripcionGanadoResource1"></asp:Label>
                                    </div>

                                    <div class="span9 no-left-margin">
                                        <asp:DropDownList runat="server" ID="ddlDescripcionGanado" CssClass="bloquearMuerte" DataTextField="Descripcion" DataValueField="DescripcionGanadoID" />
                                        <%--<asp:TextBox ID="txtDescripcion" CssClass="span12 bloquearMuerte" MaxLength="255" runat="server"></asp:TextBox>--%>
                                    </div>
                                    <div class="space10"></div>
                                </div>
                            </div>

                            <!--Espacio-->
                            <div class="space15"></div>

                            <div class="form-actions">
                                <div>
                                    <table class="pull-right">
                                        <tr>
                                            <td>
                                                <a id="lblGuardarEtiquetaBoton" href="#idConfirmacion" class="btn SuKarne" data-toggle="modal">Guardar</a>
                                            </td>
                                            <td>
                                                <a id="btnCancelar" class="btn SuKarne" data-toggle="modal" href="#dlgCancelar">Cancelar</a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>


                            <!--Dialogo de Notificaciones-->
                            <div id="dlgNotificaciones" class="modal hide fade" style="margin-top: -150px; display: block;" tabindex="-1" data-backdrop="static" data-keyboard="false">
                                <div class="modal-body">
                                    <asp:Label ID="lblTituloGanadoSinDeteccion" CssClass="titulo" runat="server" meta:resourcekey="lblMensajeDialogoNotificaciones"></asp:Label>
                                    <div id="TablaNotificaciones" class="span12 no-left-margin">
                                    </div>

                                    <div class="space10"></div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" ID="btnCerrar" CssClass="btn SuKarne" meta:resourcekey="btnDialogoCerrar" data-dismiss="modal" />
                                </div>
                            </div>

                            <!--Dialogo de Notificaciones-->
                            <div id="dvNoHayNotificaciones" style="margin-top: -150px; display: block;" class="modal hide fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
                                <div class="modal-body">
                                    <asp:Label ID="lblNoHayNotificaciones" runat="server" meta:resourcekey="msgNoHayNotificaciones"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" ID="btnOk" CssClass="btn SuKarne" Text="Ok" data-dismiss="modal" />
                                </div>
                            </div>

                            <!--Dialogo de Cancelacion -->
                            <div id="dlgCancelar" class="modal hide fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
                                <div class="modal-body">
                                    <asp:Label ID="lblMensajeDialogo" runat="server" meta:resourcekey="lblMensajeDialogo"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" ID="btnDialogoSi" CssClass="btn SuKarne" meta:resourcekey="btnDialogoSi" data-dismiss="modal" />
                                    <asp:Button runat="server" ID="btnDialogoNo" CssClass="btn SuKarne" meta:resourcekey="btnDialogoNo" data-dismiss="modal" />
                                </div>
                            </div>

                            <!--Dialogo de ver-->
                            <div id="dlgVer" class="modal hide fade" style="margin-top: -150px; display: block;" tabindex="-1" data-backdrop="static" data-keyboard="false">
                                <div class="modal-body">
                                    <div id="TablaOtros" class="span12 no-left-margin">
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" ID="btnAceptarVer" CssClass="btn SuKarne" meta:resourcekey="btnDialogoAceptar" data-dismiss="modal" />
                                    <asp:Button runat="server" ID="btnCancelarVer" CssClass="btn SuKarne" meta:resourcekey="btnDialogoCancelar" />
                                </div>
                            </div>



                            <!--Dialogo Ganado Muerto-->
                            <div id="dlgGanadoMuertoDialogo" style="margin-top: -150px; display: block;" class="modal hide fade" data-backdrop="static" data-keyboard="false">
                                <div class="modal-body">
                                    <asp:Label ID="lblTituloGanadoMuerto" CssClass="titulo" runat="server" meta:resourcekey="lblGanadoMuertoTitulo"></asp:Label>
                                    <div class="space10"></div>
                                    <div class="span12">
                                        <div class="span4">
                                            <!--No. Arete-->
                                            <div class="span5">
                                                <span class="requerido">*</span>
                                                <asp:Label ID="lblAreteGanadoMuerto" runat="server" meta:resourcekey="lblEtiquetaNoAreteResource1"></asp:Label>
                                            </div>
                                            <div class="span7">
                                                <asp:TextBox ID="txtAreteGanadoMuerto" CssClass="span12" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="span5">
                                            <!--Arete Testigo-->
                                            <div class="span5">
                                                <asp:Label ID="lblAreteTestigoGanadoMuerto" runat="server" meta:resourcekey="lblEtiquetaAreteTestigoResource1"></asp:Label>
                                            </div>
                                            <div class="span6">
                                                <asp:TextBox ID="txtAreteTestigoGanadoMuerto" CssClass="span12" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="span2">
                                            <!--No. Arete-->
                                            <div class="span12">
                                                <span>
                                                    <asp:CheckBox ID="chkSinArete" runat="server" />
                                                    <asp:Label ID="lblSinArete" runat="server">Sin arete</asp:Label>
                                                </span>
                                            </div>
                                        </div>
                                        <!--Espacio-->
                                        <div class="space10"></div>

                                        <div class="span12">
                                            <!--Boton tomar foto-->
                                            <div class="span4">
                                            </div>
                                            <div class="span4 alineacionCentro">
                                                <a id="btnTomarFotoGanadoMuerto" class="btn SuKarne alineacionCentro" data-togle="modal">
                                                    <img src="../Images/camara.png" width="25" height="22" />
                                                    <asp:Label ID="lblEtiquetaTomarFoto" runat="server" meta:resourcekey="lblEtiquetaTomarFotoResource1"></asp:Label>
                                                </a>
                                                <div class="control-group">
                                                    <div class="controls">
                                                        <input type="file" id="flFotoGanadoMuerto" name="fotoGanadoMuerto[]" style="display: none;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span4">
                                            </div>
                                            <div class="control-group">
                                                <asp:Label ID="lblFotoMuerte" runat="server" meta:resourcekey="lblFotoDeteccion" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <div class="imagen-deteccion">
                                                        <output id="outputMuerte">
                                                            <a id="imagenMuerte" href="#" style="display: none;">
                                                                <asp:Image runat="server" ID="imgFotoMuerte" CssClass="fancybox-image" />
                                                            </a>
                                                        </output>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--Espacio-->
                                        <div class="space10"></div>
                                        <div class="span12">
                                            <!--Espacio-->
                                            <div class="span5">
                                            </div>
                                            <div class="span2 alineacionCentro" id="dvFotoGanadoMuerto">
                                            </div>
                                            <!--Espacio-->
                                            <div class="span5">
                                            </div>
                                        </div>
                                        <!--Espacio-->
                                        <div class="space10"></div>

                                        <!--Observaciones-->
                                        <div class="span3 no-left-margin">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="Label1" runat="server" meta:resourcekey="lblEtiquetaObservacionesResource1"></asp:Label>
                                        </div>
                                        <div class="span9 no-left-margin">
                                            <asp:TextBox ID="txtObservacionesGanadoMuerto" runat="server" class="span11" MaxLength="255" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                        </div>

                                    </div>

                                    <!--Espacio-->
                                    <div class="space10"></div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" ID="btnAceptarGanadoMuerto" CssClass="btn SuKarne" meta:resourcekey="lblGuardarEtiquetaBotonResource1" TabIndex="99" />
                                    <asp:Button runat="server" ID="btnCancelarGanadoMuerto" CssClass="btn SuKarne" meta:resourcekey="btnDialogoCancelar" TabIndex="100" />
                                </div>
                            </div>

                            <div id="modalAretes" style="margin-top: -150px; display: block;" class="modal hide fade extraLarge" data-backdrop="static" data-keyboard="false">
                                <div class="portlet box SuKarne2">
                                    <div class="portlet-title">
                                        <div class="modal-header">
                                            <button type="button" class="close cerrarModalAretes" aria-hidden="true">
                                                <img src="../Images/close.png" />
                                            </button>
                                            <h3 class="caption">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                                <asp:Label ID="lblAretesTitulo" runat="server" Text="Aretes En Corral" meta:resourcekey="lblAretesTitulo"></asp:Label>
                                            </h3>
                                        </div>
                                    </div>

                                    <div class="portlet-body form">
                                        <div class="modal-body">

                                            <div id="divAretes"></div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" id="btnAgregar" class="btn SuKarne">
                                                <asp:Label ID="lblBotonAgregar" runat="server" Text="Guardar" meta:resourcekey="lblBotonAgregar"></asp:Label></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
