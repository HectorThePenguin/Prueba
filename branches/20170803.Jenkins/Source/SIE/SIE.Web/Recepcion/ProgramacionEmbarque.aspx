<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgramacionEmbarque.aspx.cs" Inherits="SIE.Web.Recepcion.ProgramacionEmbarque" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headEvaluacion" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-responsive.css" rel="stylesheet"/>

    <script src="../Scripts/ProgramacionEmbarque.js"></script>
    <link href="../assets/css/ProgramacionEmbarque.css" rel="stylesheet" />
    <link rel="shortcut icon" href="../favicon.ico" />

    <script type="text/javascript">
        javascript: window.history.forward(1);

        //ROLES
        var rolEmbarqueProgramacion = '<asp:Literal runat="server" Text="<%$ Resources:rolEmbarqueProgramacion.Text %>"/>';
        var rolEmbarqueTransporteDatos = '<asp:Literal runat="server" Text="<%$ Resources:rolEmbarqueTransporteDatos.Text %>"/>';

        //CABECEROS TABLA PROGRAMACIÓN EMBARQUE
        var cabeceroId = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroId %>"/>';
        var cabeceroFolioEmbarque = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroFolioEmbarque %>"/>';
        var cabeceroOrigen = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroOrigen %>"/>';
        var cabeceroDestino = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroDestino %>"/>';
        var cabeceroResponsableEmbarque = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroResponsableEmbarque  %>"/>';
        var cabeceroTipo = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroTipo %>"/>';
        var cabeceroCitaCarga = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroCitaCarga %>"/>';
        var cabeceroObservaciones = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroObservaciones %>"/>';

        //CABECEROS TABLA TRANSPORTE EMBARQUE
        var cabeceroId = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroId %>"/>';
        var cabeceroFolioEmbarque = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroFolioEmbarque %>"/>';
        var cabeceroTransportista = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroTransportista %>"/>';
        var cabeceroRuta = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroRuta %>"/>';
        var cabeceroKilometros = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroKilometros  %>"/>';
        var cabeceroCitaDescarga = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroCitaDescarga %>"/>';
        var cabeceroCitaCarga = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroCitaCarga %>"/>';
        var cabeceroFlete = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroFlete %>"/>';
        var cabeceroGastoFijo = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroGastoFijo  %>"/>';
        var cabeceroGastoVariable = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroGastoVariable %>"/>';
        var cabeceroDemora = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroDemora %>"/>';

        //CABECEROS TABLA DATOS EMBARQUE
        var cabeceroId = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroId %>"/>';
        var cabeceroFolioEmbarque = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroFolioEmbarque %>"/>';
        var cabeceroOperador = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroOperador %>"/>';
        var cabeceroPlacaTracto = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroPlacaTracto %>"/>';
        var cabeceroPlacaJaula = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroPlacaJaula  %>"/>';
        var cabeceroEcoTracto = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroEcoTracto %>"/>';
        var cabeceroEcoJaula = '<asp:Literal runat="server" Text="<%$ Resources:cabeceroEcoJaula %>"/>';

        //TEXTOS
        var descripcionEstatusRecibido = '<asp:Literal runat="server" Text="<%$ Resources:descripcionEstatusRecibido.Text%>"/>'; 
        var descripcionEstatusRecibido = '<asp:Literal runat="server" Text="<%$ Resources:descripcionEstatusRecibido.Text%>"/>';
        var tipoRuteo = '<asp:Literal runat="server" Text="<%$ Resources:tipoRuteo.Text%>"/>';

        //LABELS
        var lblAyudaGridIdentificador = '<asp:Literal runat="server" Text="<%$ Resources:lblAyudaGridIdentificador.Text%>"/>'; 
        var lblAyudaGrid = '<asp:Literal runat="server" Text="<%$ Resources:lblAyudaGrid.Text%>"/>'; 
        var lblHorasTransito = '<asp:Literal runat="server" Text="<%$ Resources:lblHorasTransito.Text%>"/>'; 
        var lblOrganizacion = '<asp:Literal runat="server" Text="<%$ Resources:lblOrganizacion.Text%>"/>'; 
        var lblBusquedaTransportista_Titulo = '<asp:Literal runat="server" Text="<%$ Resources:lblBusquedaTransportista_Titulo.Text%>"/>'; 
        var lblTransportista = '<asp:Literal runat="server" Text="<%$ Resources:lblTransportista.Text%>"/>'; 
        var lblCodigoSAP = '<asp:Literal runat="server" Text="<%$ Resources:lblCodigoSAP.Text%>"/>'; 
        var lblBusquedaRuta_Titulo = '<asp:Literal runat="server" Text="<%$ Resources:lblBusquedaRuta_Titulo.Text%>"/>'; 
        var lblRuta = '<asp:Literal runat="server" Text="<%$ Resources:lblRuta.Text%>"/>'; 
        var lblBusquedaOperador1 = '<asp:Literal runat="server" Text="<%$ Resources:lblBusquedaOperador1.Text%>"/>';
        var lblBusquedaOperador2 = '<asp:Literal runat="server" Text="<%$ Resources:lblBusquedaOperador2.Text%>"/>';
        var lblNombreOperador = '<asp:Literal runat="server" Text="<%$ Resources:lblNombreOperador.Text%>"/>';
        var lblNombre = '<asp:Literal runat="server" Text="<%$ Resources:lblNombre.Text%>"/>';
        var lblCamionID = '<asp:Literal runat="server" Text="<%$ Resources:lblCamionID.Text%>"/>'; 
        var lblBusquedaTracto_Titulo = '<asp:Literal runat="server" Text="<%$ Resources:lblBusquedaTracto_Titulo.Text%>"/>'; 
        var lblPlacaTracto = '<asp:Literal runat="server" Text="<%$ Resources:lblPlacaTracto.Text%>"/>'; 
        var lblApellidoPaterno = '<asp:Literal runat="server" Text="<%$ Resources:lblApellidoPaterno.Text%>"/>';
        var lblApellidoMaterno = '<asp:Literal runat="server" Text="<%$ Resources:lblApellidoMaterno.Text%>"/>';

        //DATATABLES
        var primeraPagina = '<asp:Literal runat="server" Text="<%$ Resources:datatable.primeraPagina %>"/>';
        var ultimaPagina = '<asp:Literal runat="server" Text="<%$ Resources:datatable.ultimaPagina %>"/>';
        var siguiente = '<asp:Literal runat="server" Text="<%$ Resources:datatable.siguiente %>"/>';
        var anterior = '<asp:Literal runat="server" Text="<%$ Resources:datatable.anterior %>"/>';
        var sinDatos = '<asp:Literal runat="server" Text="<%$ Resources:datatable.sinDatos %>"/>';
        var mostrando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.mostrando %>"/>';
        var sinInformacion = '<asp:Literal runat="server" Text="<%$ Resources:datatable.sinInformacion %>"/>';
        var filtrando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.filtrando %>"/>';
        var mostrar = '<asp:Literal runat="server" Text="<%$ Resources:datatable.mostrar %>"/>';
        var cargando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.cargando %>"/>';
        var procesando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.procesando %>"/>';
        var buscar = '<asp:Literal runat="server" Text="<%$ Resources:datatable.buscar %>"/>';
        var sinRegistros = '<asp:Literal runat="server" Text="<%$ Resources:datatable.sinRegistros %>"/>';

        //Mensajes
        var Aceptar = '<asp:Literal runat="server" Text="<%$ Resources:Aceptar.Text %>"/>';
        var msgSeleccionarOrganizacion = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionarOrganizacion.Text %>"/>';
        var msgSeleccionarOrganizacion = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionarOrganizacion.Text %>"/>';
        var msgSinTipoGanadera = '<asp:Literal runat="server" Text="<%$ Resources:msgSinTipoGanadera.Text %>"/>';
        var msgSinOrganizacionValida = '<asp:Literal runat="server" Text="<%$ Resources:msgSinOrganizacionValida.Text %>"/>';
        var msgSinOrganizacionOrigenValida = '<asp:Literal runat="server" Text="<%$ Resources:msgSinOrganizacionOrigenValida.Text %>"/>';
        var msgSinOrganizacionDestinoValida = '<asp:Literal runat="server" Text="<%$ Resources:msgSinOrganizacionDestinoValida.Text %>"/>';
        var msgErrorPrecondiciones = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorPrecondiciones.Text %>"/>';
        var msgDialogoSi = '<asp:Literal runat="server" Text="<%$ Resources:msgDialogoSi.Text %>"/>';
        var msgDialogoNo = '<asp:Literal runat="server" Text="<%$ Resources:msgDialogoNo.Text %>"/>';
        var msgErrorProgramaciones = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorProgramaciones.Text %>"/>';
        var msgErrorTiposEmbarque = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorTiposEmbarque.Text %>"/>';
        var msgErrorBusquedaRuteo = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorBusquedaRuteo.Text %>"/>';
        var msgSinRuteoConfigurado = '<asp:Literal runat="server" Text="<%$ Resources:msgSinRuteoConfigurado.Text %>"/>';
        var msgValidaRuteoSeleccionado = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaRuteoSeleccionado.Text %>"/>';
        var msgErrorConsultar = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorConsultar.Text %>"/>';
        var msgErrorValidarTiposEmbarque = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorValidarTiposEmbarque.Text %>"/>';
        var msgErrorValidarOrigen = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorValidarOrigen.Text %>"/>';
        var msgErrorValidarDestino = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorValidarDestino.Text %>"/>';
        var msgErrorValidarResponsableCarga = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorValidarResponsableCarga.Text %>"/>';
        var msgErrorValidarCitaCarga = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorValidarCitaCarga.Text %>"/>';
        var msgErrorValidarHorasTransito = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorValidarHorasTransito.Text %>"/>';
        var msgErrorObtenerJaulasProgramadas = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorObtenerJaulasProgramadas.Text %>"/>';
        var msgErrorCargarGridTransporte = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorCargarGridTransporte.Text %>"/>';
        var msgErrorEnviarCorreo = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorEnviarCorreo.Text %>"/>';
        var msgExitoCorreoEnviado = '<asp:Literal runat="server" Text="<%$ Resources:msgExitoCorreoEnviado.Text %>"/>';
        var msgExitoFolioEmbarque = '<asp:Literal runat="server" Text="<%$ Resources:msgExitoFolioEmbarque.Text %>"/>';
        var msgErrorBusquedaDetallesRuteo = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorBusquedaDetallesRuteo.Text %>"/>';
        var msgSinTractos = '<asp:Literal runat="server" Text="<%$ Resources:msgSinTractos.Text %>"/>';
        var msgNoRuteosActivos = '<asp:Literal runat="server" Text="<%$ Resources:msgNoRuteosActivos.Text %>"/>';
        var msgNoTieneTarifa = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTieneTarifa.Text %>"/>';
        var msgErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorGuardar.text %>"/>';
        var msgErrorEliminar = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorEliminar.text %>"/>';
        var msgExitoGuardar = '<asp:Literal runat="server" Text="<%$ Resources:msgExitoGuardar.text %>"/>';
        var msgCancelar = '<asp:Literal runat="server" Text="<%$ Resources:msgCancelar.Text %>"/>';
        var msgCancelarTransporte = '<asp:Literal runat="server" Text="<%$ Resources:msgDialogoCancelarTransporte.Text %>"/>';
        
        var msgTractoInvalido = '<asp:Literal runat="server" Text="<%$ Resources:msgTractoInvalido.Text %>"/>';
        var msgSinTractos = '<asp:Literal runat="server" Text="<%$ Resources:msgSinTractos.Text %>"/>';
        var msgTractoNoPerteneceProveedor = '<asp:Literal runat="server" Text="<%$ Resources:msgTractoNoPerteneceProveedor.Text %>"/>';
        var msgSeleccionarCamion = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionarCamion.Text %>"/>';
        var msgPeticionEliminar = '<asp:Literal runat="server" Text="<%$ Resources:msgPeticionEliminar.Text %>"/>';

        var msgJaulaInvalido = '<asp:Literal runat="server" Text="<%$ Resources:msgJaulaInvalido.Text %>"/>';
        var msgSinJaulas = '<asp:Literal runat="server" Text="<%$ Resources:msgSinJaulas.Text %>"/>';
        var msgSeleccionaJaula = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionaJaula.Text %>"/>';
        var msgJaulaNoPerteneceProveedor = '<asp:Literal runat="server" Text="<%$ Resources:msgJaulaNoPerteneceProveedor.Text %>"/>';
        var lblBusquedaJaula_Titulo = '<asp:Literal runat="server" Text="<%$ Resources:lblBusquedaJaula_Titulo.Text %>"/>';
        var lblPlacaJaula = '<asp:Literal runat="server" Text="<%$ Resources:lblPlacaJaula.Text %>"/>';
        var msgNoTieneTransportista = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTieneTransportista.Text %>"/>';
        var msgEstatusActualizar = '<asp:Literal runat="server" Text="<%$ Resources:msgEstatusActualizar.Text %>"/>';
        
        //Mensajes Transporte
        var msgSinTransporteValido = '<asp:Literal runat="server" Text="<%$ Resources:msgSinTransporteValido.Text %>"/>';
        var msgValidaCorreoProveedor = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaCorreoProveedor.Text %>"/>';
        var msgValidaChoferProveedor = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaChoferProveedor.Text %>"/>';
        var msgValidaJaulaTractoProveedor = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaJaulaTractoProveedor.Text %>"/>';
        var msgValidaConfiguracionOrigenDestinoProveedor = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaConfiguracionOrigenDestinoProveedor.Text %>"/>';        
        var msgValidaOrigenDestinoTieneVariasRutasProveedor = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaOrigenDestinoTieneVariasRutasProveedor.Text %>"/>';
        var msgSeleccionaTransportista = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionaTransportista.Text %>"/>';
        var msgSeleccionaRuta = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionaRuta.Text %>"/>';
        var msgSinRuta = '<asp:Literal runat="server" Text="<%$ Resources:msgSinRuta.Text %>"/>';
        var msgSinRutaProveedor = '<asp:Literal runat="server" Text="<%$ Resources:msgSinRutaProveedor.Text %>"/>'; 
        var msgProveedoresSinRuta = '<asp:Literal runat="server" Text="<%$ Resources:msgProveedoresSinRuta.Text %>"/>';
        var msgSeleccionaKms = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionaKms.Text %>"/>';
        var msgSeleccionaCitaDescarga = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionaCitaDescarga.Text %>"/>';
        var msgValidaFlete = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaFlete.Text %>"/>';
        var msgValidaGastoFijo = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaGastoFijo.Text %>"/>';
        var msgValidaProgramacionDatosCapturados = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaProgramacionDatosCapturados.Text %>"/>';
        var msgTransportistaNoValido = '<asp:Literal runat="server" Text="<%$ Resources:msgTransportistaNoValido.Text %>"/>';

        //Mensajes Datos
        var msgSinOperadorValido = '<asp:Literal runat="server" Text="<%$ Resources:msgSinOperadorValido.Text %>"/>';
        var msgSeleccionaOperador1 = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionaOperador1.Text %>"/>';
        var msgSeleccionaOperador2 = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionaOperador2.Text %>"/>';
        var msgSeleccionaTracto = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionaTracto.Text %>"/>';
        var msgValidaJaula = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaJaula.Text %>"/>';
        var msgValidaOperador1Igual = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaOperador1Igual.Text %>"/>';
        var msgValidaOperador2Igual = '<asp:Literal runat="server" Text="<%$ Resources:msgValidaOperador2Igual.Text %>"/>';
        var msgErrorCargarGridDatos = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorCargarGridDatos.Text %>"/>';
        var msgErrorGastosFijos = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorGastosFijos.Text %>"/>';
        var msgSeleccionaPlacaJaula = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionaPlacaJaula.Text %>"/>';

        var EmbarqueRecibido = '<asp:Literal runat="server" Text="<%$ Resources:EmbarqueRecibido.Text %>"/>';
        var EmbarqueCancelado = '<asp:Literal runat="server" Text="<%$ Resources:EmbarqueCancelado.Text %>"/>';

    </script>
    <style>
        .oculto {
            display: none !important;
        }
    </style>
</head>
<body class="page-header-fixed">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
            <div class="container-fluid" />
                <div class="row-fluid">
                    <div class="span12">
                        <div class="portlet box SuKarne2">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                    <span class="letra">
                                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloProgramacionEmbarque"></asp:Label>
                                    </span>
                                </div>
                            </div>
                            <div class="portlet-body form">
						        <ul class="breadcrumb">
				                    <li>
					                    <i class="icon-home"></i>
				                        <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="ProgramacionEmbarque_Home"/></a> 
					                    <i class="icon-angle-right"></i>
				                    </li>
                                    <li>
					                    <a href="ProgramacionEmbarque.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="ProgramacionEmbarque_Title"></asp:Label></a> 
				                    </li>
			                    </ul>
                                <div class="row-fluid">
                                    <fieldset id="dlgFiltrosBusqueda"  class="scheduler-border">
                                        <legend class="scheduler-border">
                                            Filtros de Búsqueda
                                        </legend>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <!-- Organizacion -->
                                                <div class="span2 textoDerecha">
                                                    <span class="requerido">*</span>
                                                    <asp:Label ID="lblOrganizacion" runat="server" meta:resourcekey="lblOrganizacion"></asp:Label> 
                                                </div>
                                                <div class="span1">
                                                    <asp:TextBox ID="txtNumOrganizacion" CssClass="span12" runat="server" TabIndex="1"></asp:TextBox>
                                                </div>
                                                <div class="span4">
                                                    <asp:TextBox ID="txtOrganizacion" CssClass="span10" runat="server"></asp:TextBox>
                                                    <a id="btnAyudaOrganizacion" class="focus-ayuda" href="#dlgBusquedaOrganizacion" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                                </div>
                                                <div class="span4" >
                                                    <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha"></asp:Label>
                                                    <asp:TextBox ID="txtFecha" runat="server" CssClass="span4" Enabled="false" MaxLength="10" TabIndex="1"/>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- GRID DE SEMANAS -->
                                        <div id="Div1" class="span11 margenTopdiv alineacionDerecha">
                                            <span class="span2 textoDerecha">
                                                <asp:Label ID="Label11" runat="server" class="span12 titulosJaulas" meta:resourcekey="lblJaulasSolicitadas"></asp:Label>
                                                <asp:Label ID="Label14" runat="server" class="span12" meta:resourcekey="lblJaulasProgramadas"></asp:Label>
                                            </span>
                                            <span class="span10">
                                                <table class="table table-hover table-striped table-bordered span11">
                                                        <thead>
                                                            <tr>
                                                                <th id="anioEtiqueta" scope="col" class="alineacionCentro" colspan="8"></th>           
                                                            </tr>
                                                            <tr>
                                                                <th id="lunesEtiqueta" scope="col" class="alineacionCentro span12">-</th>        
                                                                <th id="martesEtiqueta" class="alineacionCentro" scope="col">-</th>        
                                                                <th id="miercolesEtiqueta" class="alineacionCentro" scope="col">-</th>        
                                                                <th id="juevesEtiqueta" scope="col" class="alineacionCentro">-</th>        
                                                                <th id="viernesEtiqueta" class="alineacionCentro" scope="col">-</th>        
                                                                <th id="sabadoEtiqueta" class="alineacionCentro" scope="col">-</th>        
                                                                <th id="domingoEtiqueta" class="alineacionCentro" scope="col">-</th>     
                                                            </tr>
                                                            <tr>
                                                                <th scope="col" class="alineacionCentro columnaGridTitulo">LUNES</th>        
                                                                <th class="alineacionCentro columnaGridTitulo" scope="col">MARTES</th>        
                                                                <th class="alineacionCentro columnaGridTitulo" scope="col">MIÉRCOLES</th>        
                                                                <th scope="col" class="alineacionCentro columnaGridTitulo">JUEVES</th>        
                                                                <th class="alineacionCentro columnaGridTitulo" scope="col">VIERNES</th>        
                                                                <th class="alineacionCentro columnaGridTitulo" scope="col">SÁBADO</th>        
                                                                <th class="alineacionCentro columnaGridTitulo" scope="col">DOMINGO</th>       
                                                            </tr>
                                                        </thead>    
                                                    <tbody style ="overflow-y:scroll">
                                                            <tr class="CeldaSemanal">
                                                                <td id="txtJaulasLunes" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulasMartes" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulasMiercoles" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulasJueves" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulasViernes" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulasSabado" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulasDomingo" class="span9 alineacionCentro">-</td>        
                                                            </tr>
                                                            <tr class="CeldaSemanal alineacionCentro">
                                                                <td id="txtJaulaProgramadasLunes" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulaProgramadasMartes" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulaProgramadasMiercoles" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulaProgramadasJueves" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulaProgramadasViernes" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulaProgramadasSabado" class="span9 alineacionCentro">-</td> 
                                                                <td id="txtJaulaProgramadasDomingo" class="span9 alineacionCentro">-</td>           
                                                            </tr>
                                                    </tbody>
                                                </table>
                                            </span>
                                        </div>
                                        <!--GRID DE SEMANAS -->
                                    </fieldset>
                                    
                                    <fieldset id="Fieldset2"  class="scheduler-border">
                                        <legend class="scheduler-border">
                                            Captura de Datos
                                        </legend>

                                        <!-- CAPTURA DATOS PESTAÑA PROGRAMACION -->

                                        <div id="divCapturaDatos">
                                            <div class="span12">
                                                <div class="span6">
                                                    <!-- Id -->
                                                    <span class="textoDerecha span4">
                                                        <asp:Label ID="lblId" runat="server" meta:resourcekey="lblId"></asp:Label>
                                                    </span>
                                                    <asp:TextBox ID="txtId" CssClass="span2 margenIzquierdaCapturaDatos" runat="server"></asp:TextBox>
                                                    <!-- Tipo Embarque -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="Label10" runat="server" meta:resourcekey="lblTipoEmbarque"></asp:Label>
                                                        </span>
                                                        <asp:DropDownList ID="ddlTipoEmbarque" CssClass="span3 margenIzquierdaCapturaDatos" runat="server" ViewStateMode="Enabled" TabIndex="2" />
                                                    </div>
                                                    <!-- Origen -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblOrigen" runat="server" meta:resourcekey="lblOrigen"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtNumOrigen" class="span1 margenIzquierdaCapturaDatos" runat="server" TabIndex="3"></asp:TextBox>
                                                        <asp:TextBox ID="txtOrigen" class="span6" runat="server"></asp:TextBox>
                                                        <a id="btnBusquedaOrigen" class="btn-disabled focus-ayuda" TabIndex="4" href="#dlgBusquedaOrganizacion" data-toggle="modal"><img id="idPrueba" src="../Images/find.png" width="26" height="26"/></a>
                                                    </div>
                                                    <!-- Destino -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblDestino" runat="server" meta:resourcekey="lblDestino"></asp:Label> 
                                                        </span>
                                                        <asp:TextBox ID="txtNumDestino" class="span1 margenIzquierdaCapturaDatos" runat="server" TabIndex="5"></asp:TextBox>
                                                        <asp:TextBox ID="txtDestino" class="span6" runat="server"></asp:TextBox>
                                                        <a id="btnBusquedaDestino" class="btn-disabled focus-ayuda" href="#dlgBusquedaOrganizacion" TabIndex="6" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                                    </div>
                                                    <!-- Responsable de Embarque -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblResponsableEmbarque" runat="server" meta:resourcekey="lblResponsableEmbarque"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtResponsableEmbarque" class="span7 margenIzquierdaCapturaDatos" runat="server" TabIndex="7"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="span6">
                                                    <!-- Cita Carga -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span3">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblCitaCarga" runat="server" meta:resourcekey="lblCitaCarga"></asp:Label>
                                                        </span>
                                                        <!-- Hora -->
                                                        <asp:TextBox ID="txtCitaCarga" class="span2 textBoxTablas" runat="server" Enabled="false" TabIndex="8" MaxLength="10"/>
                                                        <span class="requerido">*</span>
                                                        <asp:TextBox ID="txtHoraCarga" class="span2 textBoxTablas" runat="server" MaxLength="2" TabIndex="9"/>
                                                    </div>
                                                    <!-- Horas Transito -->
                                                    <div class="margenTopdiv" id="divCambiante" >
                                                    </div>
                                                    <!-- Cita Descarga -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span3">
                                                            <asp:Label ID="lblCitaDescarga" runat="server" meta:resourcekey="lblCitaDescarga"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtDescarga" class="span3 textBoxTablas margenIzquierdaCapturaDatos" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span12 no-left-margin">
                                                <!-- Observaciones -->
                                                <div class="margenTopdiv">
                                                    <span class="textoDerecha span2 margenIzquierdaCapturaDatos margenArribaObservaciones">
                                                        <asp:Label ID="lblObservaciones" runat="server" meta:resourcekey="lblObservaciones"></asp:Label>
                                                    </span>
                                                    <asp:TextBox ID="txtObservaciones" MaxLength="255" TextMode="multiline" class="span9 margenIzquierdaCapturaDatos alturaObservacion" runat="server"></asp:TextBox>
                                                </div>
                                                <!-- Observacion Captura -->
                                                <div class="margenTopdiv">
                                                    <span class="textoDerecha span2 margenIzquierdaCapturaDatos margenAbajoCapturaDatos"></span>
                                                    <%--<asp:TextBox ID="txtObservacionCaptura" maxlength="255" TextMode="multiline" class="span9 margenIzquierdaCapturaDatos margenAbajoCapturaDatos" runat="server"></asp:TextBox>--%>
                                                    <textarea id="txtObservacionCaptura" maxlength="255" textmode="multiline" class="span9 margenIzquierdaCapturaDatos margenAbajoCapturaDatos" tabindex="11"></textarea>
                                                </div>
                                                <!-- Botones Guardar - Cancelar -->
                                                <div class="margenTopdiv">
                                                    <span class="textoDerecha span2 margenIzquierdaCapturaDatos">
                                                    </span>
                                                    <span class="span9">
                                                        <input type="button" id="btnCancelar" value="Cancelar" class="btn SuKarne alineacionDerecha margenIzquierdaCapturaDatos" tabindex="13" />
                                                        <input type="button" id="btnGuardar" value="Agregar" class="btn SuKarne alineacionDerecha" tabindex="12" />
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <!-- CAPTURA DATOS PESTAÑA TRANSPORTE -->

                                        <div id="divCapturaDatosEmbarqueTransporte">
                                            <div class="span12">
                                                <div class="span6">
                                                    <!-- Id -->
                                                    <span class="textoDerecha span4">
                                                        <asp:Label ID="lblIdTransporte" runat="server" meta:resourcekey="lblId"></asp:Label>
                                                    </span>
                                                    <asp:TextBox ID="txtIdTransporte" Enabled="false" CssClass="span2 margenIzquierdaCapturaDatos margenCampoIdDerecha" runat="server"></asp:TextBox>
                                                    <asp:CheckBox ID="cbxDobleTransportista"  TabIndex="1" Enabled="false" runat="server" />
                                                    <asp:Label ID="lblDobleTransportista" runat="server" meta:resourcekey="lblDobleTransportista"></asp:Label>
                                                    <!-- Ruta -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblRuta" runat="server" meta:resourcekey="lblRuta"></asp:Label> 
                                                        </span>
                                                        <asp:TextBox ID="txtNumRuta" TabIndex="2" disabled="disabled" CssClass="span2 margenIzquierdaCapturaDatos" Enabled="false" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtRuta" CssClass="span5" Enabled="False" runat="server"></asp:TextBox>
                                                        <a id="btnRuta" class="btn-disabled focus-ayuda" tabindex="3" href="#dlgBusquedaOrganizacion" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                                    </div>
                                                    <!-- Transportista -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblTransportista" runat="server" meta:resourcekey="lblTransportista"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtNumTransportista" TabIndex="4" disabled="disabled" CssClass="span2 margenIzquierdaCapturaDatos" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtTransportista" Enabled="false" CssClass="span5" runat="server"></asp:TextBox>
                                                        <a id="btnTransportista" tabindex="5" class="btn-disabled focus-ayuda" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                                    </div>
                                                    <!-- Kilometros -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblKms" runat="server" meta:resourcekey="lblKms"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtKms" CssClass="span3 margenIzquierdaCapturaDatos" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                    <!-- Cita Descarga -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblCitaDescargaTransporte" runat="server" meta:resourcekey="lblCitaDescarga"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtCitaDescargaTransporte" CssClass="span3 textBoxTablas margenIzquierdaCapturaDatos" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="span6">
                                                    <!-- Flete -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span3">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblFlete" runat="server" meta:resourcekey="lblFlete"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtFlete" CssClass="span3 textBoxTablas margenIzquierdaCapturaDatos" Enabled="false" runat="server" MaxLength="10"/>
                                                    </div>
                                                    <!-- Gasto Fijo -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span3">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblGastoFijo" runat="server" meta:resourcekey="lblGastoFijo"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtGastoFijo" CssClass="span3 textBoxTablas margenIzquierdaCapturaDatos" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                    <!-- Gasto Variable -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span3">
                                                            <asp:Label ID="lblGastoVariable" runat="server" meta:resourcekey="lblGastoVariable"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtGastoVariable" TabIndex="6" CssClass="span3 textBoxTablas margenIzquierdaCapturaDatos" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                    <!-- Demora -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span3">
                                                            <asp:Label ID="lblDemora" runat="server" meta:resourcekey="lblDemora"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtDemora" TabIndex="7" CssClass="span3 textBoxTablas margenIzquierdaCapturaDatos" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span12 no-left-margin">
                                                <!-- Observaciones -->
                                                <div class="margenTopdiv">
                                                    <span class="textoDerecha span2 margenIzquierdaCapturaDatos">
                                                        <asp:Label ID="lblObservacionesTransporte" runat="server" meta:resourcekey="lblObservaciones"></asp:Label>
                                                    </span>
                                                    <textarea id="txtObservacionesTransporte" class="span9 margenIzquierdaCapturaDatos" disabled="disabled"></textarea>
                                                </div>
                                                <!-- Observacion Captura -->
                                                <div class="margenTopdiv">
                                                    <span class="textoDerecha span2 margenIzquierdaCapturaDatos margenAbajoCapturaDatos"></span>
                                                    <asp:TextBox ID="txtObservacionesTransporteCaptura" TabIndex="8" cssClass="span9 margenIzquierdaCapturaDatos margenAbajoCapturaDatos" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                                <!-- Botones Guardar - Cancelar -->
                                                <div class="margenTopdiv">
                                                <span class="textoDerecha span2 margenIzquierdaCapturaDatos margenArribaObservaciones">
                                                </span>
                                                <span class="span9">
                                                    <input type="button" id="btnCancelarTransporte" tabindex="10" disabled="disabled" value="Cancelar" class="btn SuKarne alineacionDerecha margenIzquierdaCapturaDatos" />
                                                    <input type="button" id="btnAgregarTransporte" tabindex="9" disabled="disabled" value="Agregar" class="btn SuKarne alineacionDerecha" />
                                                </span>
                                            </div>
                                            </div>
                                        </div>
                                        
                                        <!-- CAPTURA DATOS PESTAÑA DATOS -->

                                        <div id="divCapturaDatosEmbarqueDatos">
                                            <div class="span12">
                                                <div class="span6">
                                                    <!-- Id -->
                                                    <span class="textoDerecha span4">
                                                        <asp:Label ID="lblIdDatos" runat="server" meta:resourcekey="lblId"></asp:Label>
                                                    </span>
                                                    <asp:TextBox ID="txtIdDatos" Enabled="false" CssClass="span2 margenIzquierdaCapturaDatos" runat="server"></asp:TextBox>
                                                    <!-- Operador 1 -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblOperador1" runat="server" meta:resourcekey="lblOperador1"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtNumOperador1" TabIndex="1" CssClass="span2 margenIzquierdaCapturaDatos" Enabled="false" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtOperador1" Enabled="false" CssClass="span5" runat="server"></asp:TextBox>
                                                        <a id="btnOperador1" tabindex="2" class="btn-disabled focus-ayuda" tabindex="2" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                                    </div>
                                                    <!-- Operador 2 -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span id="requeridoOperador2" class="requerido">*</span>
                                                            <asp:Label ID="lblOperador2" runat="server" meta:resourcekey="lblOperador2"></asp:Label> 
                                                        </span>
                                                        <asp:TextBox ID="txtNumOperador2" TabIndex="3" CssClass="span2 margenIzquierdaCapturaDatos" Enabled="false" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtOperador2" CssClass="span5" Enabled="False" runat="server"></asp:TextBox>
                                                        <a id="btnOperador2" tabindex="4" class="btn-disabled focus-ayuda" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                                    </div>
                                                    <!-- Placas Tracto -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblPlacasTracto" runat="server" meta:resourcekey="lblPlacasTracto"></asp:Label> 
                                                        </span>
                                                        <asp:TextBox ID="txtPlacasTracto" TabIndex="5" CssClass="span6 margenIzquierdaCapturaDatos" Enabled="False" runat="server"></asp:TextBox>
                                                        <a id="btnPlacasTracto" tabindex="6" class="btn-disabled focus-ayuda" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                                    </div>
                                                    <!-- Placas Jaula -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span4">
                                                            <span class="requerido">*</span>
                                                            <asp:Label ID="lblPlacasJaula" runat="server" meta:resourcekey="lblPlacasJaula"></asp:Label> 
                                                        </span>
                                                        <asp:TextBox ID="txtPlacasJaula" TabIndex="7" CssClass="span6 margenIzquierdaCapturaDatos" Enabled="False" runat="server"></asp:TextBox>
                                                        <a id="btnPlacasJaula" tabindex="8" class="btn-disabled focus-ayuda" href="#dlgBusquedaOrganizacion" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                                    </div>
                                                </div>
                                                <div class="span6">
                                                    <!-- # Eco Tracto -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span3">
                                                            <asp:Label ID="lblEcoTracto" runat="server" meta:resourcekey="lblEcoTracto"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtEcoTracto" CssClass="span3 textBoxTablas margenIzquierdaCapturaDatos" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                    <!-- # Eco Jaula -->
                                                    <div class="margenTopdiv">
                                                        <span class="textoDerecha span3">
                                                            <asp:Label ID="lblEcoJaula" runat="server" meta:resourcekey="lblEcoJaula"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtEcoJaula" CssClass="span3 textBoxTablas margenIzquierdaCapturaDatos " Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="span12 no-left-margin">
                                                <!-- Observaciones -->
                                                <div class="margenTopdiv">
                                                    <span class="textoDerecha span2 margenIzquierdaCapturaDatos">
                                                        <asp:Label ID="lblObservacionesDatos" runat="server" meta:resourcekey="lblObservaciones"></asp:Label>
                                                    </span>
                                                    <textarea id="txtObservacionesDatos" class="span9 margenIzquierdaCapturaDatos" disabled="disabled"></textarea>
                                                </div>
                                                <!-- Observacion Captura -->
                                                <div class="margenTopdiv">
                                                    <span class="textoDerecha span2 margenIzquierdaCapturaDatos margenAbajoCapturaDatos"></span>
                                                    <asp:TextBox ID="txtObservacionesDatosCaptura" TabIndex="9" cssClass="span9 margenIzquierdaCapturaDatos margenAbajoCapturaDatos" Enabled="False" runat="server"></asp:TextBox>
                                                </div>
                                                <!-- Botones Guardar - Cancelar -->
                                                <div class="margenTopdiv">
                                                <span class="textoDerecha span2 margenIzquierdaCapturaDatos">
                                                </span>
                                                <span class="span9">
                                                    <input type="button" id="btnCancelarDatos" tabindex="11" disabled="disabled" value="Cancelar" class="btn SuKarne alineacionDerecha margenIzquierdaCapturaDatos" />
                                                    <input type="button" id="btnGuardarDatos" tabindex="10" disabled="disabled" value="Agregar" class="btn SuKarne alineacionDerecha" />
                                                </span>
                                            </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <ul id="tab" class="nav nav-tabs">
                                        <li class="active"><a href="#TapGridProgramacion" data-target="#TapGridProgramacion" data-toggle="tab">Programación</a></li>
                                        <li><a href="#TapGridTransporte" data-toggle="tab">Transporte</a></li>
                                        <li><a href="#TapGridDatos" data-toggle="tab">Datos</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <!-- Grid Programación Embarque -->
                                        <div id="TapGridProgramacion" class="tab-pane margenTopdiv active">
                                            <div class="row-fluid">
                                                <div class="control-group">
                                                    <div id="GridProgramacionEmbarque"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Grid Transporte Embarque -->
                                        <div id="TapGridTransporte" class="tab-pane margenTopdiv">
                                            <div class="row-fluid">
                                                <!-- Label Pendientes -->
                                                <div style="text-align: center;margin-bottom: -25px;font-size: 14px;font-weight: normal;line-height: 20px;">
                                                    <asp:Label ID="lblPendientes" runat="server" meta:resourcekey="lblPendientes"></asp:Label>
                                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                </div>
                                                <div class="control-group">
                                                    <div id="GridTransporteEmbarque"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Grid Datos Embarque -->
                                        <div id="TapGridDatos" class="tab-pane margenTopdiv">
                                            <div class="row-fluid">
                                                <!-- Label Pendientes -->
                                                <div style="text-align: center;margin-bottom: -25px;font-size: 14px;font-weight: normal;line-height: 20px;">
                                                    <asp:Label runat="server" meta:resourcekey="lblPendientes"></asp:Label>
                                                    <asp:Label ID="lblTotalDatos" runat="server"></asp:Label>
                                                </div>
                                                <div class="control-group">
                                                    <div id="GridDatosEmbarque"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Dialogo de Cancelacion Buscar -->
                                <div id="dlgCancelarBuscar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
			                        <div class="modal-body">
				                        <asp:Label ID="Label3" runat="server" meta:resourcekey="msgCancelarBuscar"></asp:Label>
			                        </div>
			                        <div class="modal-footer">
				                        <asp:Button runat="server" ID="btnSiBuscar" CssClass="btn SuKarne" meta:resourcekey="msgDialogoSi" data-dismiss="modal"/>
                                        <asp:Button runat="server" ID="btnNoBuscar" CssClass="btn SuKarne" meta:resourcekey="msgDialogoNo" data-dismiss="modal"/>
			                        </div>
		                        </div>
                            </div>
                        </div>  
                    </div>
                </div>
        </form>
        
        <!-- Dialogo de Busqueda -->
        <div id="dlgBusquedaOrganizacion" class="modal hide fade" style="margin-top: -150px;height: 400px; width:650px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
			<div class="portlet box SuKarne2">
				<div class="portlet-title">
                    <div class="caption">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <span class="letra">
                            <asp:Label ID="lblTituloDialogo" runat="server" meta:resourcekey="BusquedaGanadera_Title"></asp:Label>
                        </span>
                    </div>
                </div>
                <div class="portlet-body form" style="height: 400px; margin-right: 0px;">
                    <div class="modal-body">
						<fieldset class="scheduler-border span6">
                            <legend class="scheduler-border"><asp:Label ID="lblFiltro" runat="server" meta:resourcekey="lblFiltro"></asp:Label></legend>
                            <div id="OpcionesAyuda" class="no-left-margin">
                                <asp:Label ID="lblOrganizacionBusqueda"  runat='server' meta:resourcekey="lblOrganizacion"></asp:Label>
                                <input type="text" id="txtOrganizacionBuscar" style="width: 230px;"/>
                                <a id="btnAyudaBuscarOrganizacion" class="btn SuKarne" style="margin-left: 10px;">Buscar</a>
                                <a id="btnAyudaAgregarBuscar" class="btn SuKarne" meta:resourcekey="btnAgregar">Agregar</a>
                                <a id="btnAyudaCancelarBuscar" class="btn SuKarne" meta:resourcekey="btnCancelar">Cancelar</a>
                            </div>
                        </fieldset>
                        <div class="alineacionCentro">
                            <table id="tbBusquedaEncabezado" class="table table-striped table-advance table-hover">
                                <thead>
                                    <tr>
                                        <th style="width: 20px;" class=" alineacionCentro" scope="col"></th>
                                        <th style="width: 100px;" class=" alineacionCentro" scope="col"><asp:Label ID="Label4" runat="server" meta:resourcekey="lblAyudaGridIdentificador"></asp:Label></th>
                                        <th style="width: auto;" class=" alineacionCentro" scope="col"><asp:Label ID="Label5" runat="server" meta:resourcekey="lblAyudaGrid"></asp:Label></th>
                                    </tr>
                                </thead>
                            </table>
                            <div class="space5"></div>
                            <div id="dvBusqueda" style="height: 200px; overflow: auto;">
                                <table id="tbBusqueda" class="table table-striped table-advance table-hover no-left-margin">
                                    <tbody></tbody>
                                </table>
                            </div>
                        </div>
					</div>
                </div>
            </div>
		</div>
        

        
        <div id="dvSeleccionaRuta" class="modal hide fade" style="margin-top: -150px;height: 300px; width:400px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
			<div class="portlet box SuKarne2">
				<div class="portlet-title">
                    <div class="caption">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <span class="letra">
                            <asp:Label ID="lblTituloRuteo" runat="server" meta:resourcekey="SeleccionarRuta_Titulo"></asp:Label>
                        </span>
                    </div>
                </div>
                <div class="portlet-body form" style="height: 300px; margin-right: 0px;">
                    <div class="modal-body">
                        <asp:Label ID="Label1"  runat='server' meta:resourcekey="SeleccionarRuta_Subtitulo"></asp:Label>
                        <div class="alineacionCentro">
                            <div id="dvTbRutas" style="height: 200px; overflow: auto;">
                                <table id="tbRutas" class="table table-striped table-bordered table-advance table-hover no-left-margin">
                                   
                                        <thead>
                                            <tr>
                                            <td class="columnaGridTitulo alineacionCentro" style="width: 50px;"></td>
                                            <td class='columnaGridTitulo alineacionCentro' style='width: 150px;'>Ruta</td>
                                                </tr>
                                             
                                        </thead>
                                    <tbody id="tbRutasBody">
                                       
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <input type="button" class="btn SuKarne" id="btnSeleccionaRuta" value="Seleccionar"/>
                            <input type="button" class="btn SuKarne" id="btnCancelaRuta" value="Cancelar"/>
                        </div>
					</div>
                </div>
            </div>
		</div>

        <div id="skm_LockPane" class="LockOff" style="position:fixed;top:1;">
        Enviando Correo, por favor espere...
        </div>
    </div>
</body>
</html>
