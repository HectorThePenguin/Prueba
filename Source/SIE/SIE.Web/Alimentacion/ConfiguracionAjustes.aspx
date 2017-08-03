<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfiguracionAjustes.aspx.cs" Inherits="SIE.Web.Alimentacion.ConfiguracionAjustes" %>
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
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>

    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/ConfiguracionAjustes.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
        var msgIngresarCorral = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarCorral.Text %>"/>';
        var msgCorralNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:msgCorralNoExiste.Text %>"/>';
        var msgNoTieneLoteActivo = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTieneLoteActivo.Text %>"/>';
        var msgNoTieneOrdenReparto = '<asp:Literal runat="server" Text="<%$ Resources:msgNoTieneOrdenReparto.Text %>"/>';
        var msgIngresarCabezas = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarCabezas.Text %>"/>';
        var msgIngresarKilogramos = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarKilogramos.Text %>"/>';
        var msgIngresarObservaciones = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarObservaciones.Text %>"/>';
        var msgIngresarFormulaManiana = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarFormulaManiana.Text %>"/>';
        var msgIngresarFormulaTarde = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarFormulaTarde.Text %>"/>';
        var msgIngresarComedero = '<asp:Literal runat="server" Text="<%$ Resources:msgIngresarComedero.Text %>"/>';
        var msgCorralCapturado = '<asp:Literal runat="server" Text="<%$ Resources:msgCorralCapturado.Text %>"/>';
        var msgEliminarRegistro = '<asp:Literal runat="server" Text="<%$ Resources:msgEliminarRegistro.Text %>"/>';
        var msgDatosGuardados = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardados.Text %>"/>';
        var msgDatosBlanco = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosBlanco.Text %>"/>';
        var msgOcurrioErrorGrabar = '<asp:Literal runat="server" Text="<%$ Resources:msgOcurrioErrorGrabar.Text %>"/>';
        var MensajeCantidadMenorAProgramada = '<asp:Literal runat="server" Text="<%$ Resources:MensajeCantidadMenorAProgramada.Text %>"/>';
        var MensajeCorralNoTieneProgramacion = '<asp:Literal runat="server" Text="<%$ Resources:MensajeCorralNoTieneProgramacion.Text %>"/>';
        var msgKilosAjusteMenorMatutino = '<asp:Literal runat="server" Text="<%$ Resources:msgKilosAjusteMenorMatutino %>"/>';
        
    </script>
    <style>
        textarea {
            resize:none;
        }
        /*#tbRegistros thead {
            position: static;
        }*/
        /*#tbRegistros tbody {
           display: block; 
           height: 100px;
           overflow: auto;
           width: 100%;
        }*/
        .th1 {width:80px;word-wrap: break-word; }
        .th2 {width:80px;word-wrap: break-word; }
        .th3 {width:80px;word-wrap: break-word; }

        .td1 {width:80px;word-wrap: break-word; }
        .td2 {width:80px;word-wrap: break-word; }
        .td3 {width:80px;word-wrap: break-word; }
        input {
            text-transform: uppercase;
        }
        textarea {
            text-transform: uppercase;
        }
        select {
            text-transform: uppercase;
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
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1"></asp:Label></span>
                            </div>
                        </div>
                        <div class="portlet-body form">
							<ul class="breadcrumb">
				                <li>
					                <i class="icon-home"></i>
				                    <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="Configuracion_Home"/></a> 
					                <i class="icon-angle-right"></i>
				                </li>
                                <li>
					                <a href="ConfiguracionAjustes.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="Configuracion_Title"></asp:Label></a> 
				                </li>
			                </ul>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border"><asp:Label ID="Label3" runat="server" meta:resourcekey="lblGroupConfiguracion"></asp:Label></legend>
                                    <div class="row-fluid">
                                        <div class="span12">
                                            <input id="checkSoloFormula" type="checkbox" name="rdSoloFormula" class="span1"/>
                                            <asp:Label ID="Label2" runat="server" meta:resourcekey="lblActualizarFormula"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="span12">
                                            <div class="span10">
                                                <div class="space10"></div>
                                                
                                                 <div class="span2">
                                                    <span class="requerido">*</span>
                                                    <asp:Label ID="lblFechaReparto" runat="server" meta:resourcekey="lblFechaReparto"></asp:Label>
                                                    <input type="text" class="span12" id="datepicker" readonly="readonly" style="margin-bottom: 0px; width: 110px"/>
                                                </div>

                                                <div class="span2">
                                                    <span class="requerido">*</span>
                                                    <asp:Label ID="lblEtiquetaCorral" runat="server" meta:resourcekey="lblEtiquetaCorralResource1"></asp:Label>
                                                    <asp:TextBox ID="txtCorral" runat="server" AutoPostBack="False" class="span12" MaxLength="10"></asp:TextBox>
                                                </div>
                                                <div class="span2">
                                                    <asp:Label ID="lblLote" runat="server" meta:resourcekey="lblEtiquetaLoteResource1"></asp:Label>
                                                    <asp:TextBox ID="txtLote" runat="server" AutoPostBack="False" class="span12" MaxLength="10"></asp:TextBox>
                                                </div>
                                                <div class="span2">
                                                    <span class="requerido">*</span>
                                                    <asp:Label ID="lblCabezas" runat="server" meta:resourcekey="lblEtiquetaCabezasResource1"></asp:Label>
                                                    <asp:TextBox ID="txtCabezas" runat="server" AutoPostBack="False" class="span12" MaxLength="10"></asp:TextBox>
                                                </div>
                                                <div class="span2">
                                                    <asp:Label ID="lblTipoProceso" runat="server" meta:resourcekey="lblEtiquetaTipoProcesoResource1"></asp:Label>
                                                    <asp:TextBox ID="txtTipoProceso" runat="server" AutoPostBack="False" class="span12" MaxLength="10"></asp:TextBox>
                                                </div>
                                               

                                                <div class="span2">
                                                    <asp:Label ID="Label1" runat="server" meta:resourcekey="lblEtiquetaFormulaManianaResource1"></asp:Label>
                                                    <asp:DropDownList ID="cmbFormulaManiana" CssClass="span12" runat="server"/>
                                                    <input id="txtRepartoDetalleIdManiana" type="hidden" />
                                                    <input id="txtServidoManiana" type="hidden" />
                                                    <input id="txtProgramadoManiana" type="hidden" />
                                                    <input id="txtCantidadServidaManiana" type="hidden" />
                                                </div>

                                                <div class="span2">
                                                    <span class="requerido">*</span>
                                                    <asp:Label ID="lblFormulaTarde" runat="server" meta:resourcekey="lblEtiquetaFormulaTardeResource1"></asp:Label>
                                                    <asp:DropDownList ID="cmbFormulaTarde" CssClass="span12" runat="server"/>
                                                    <input id="txtRepartoDetalleIdTarde" type="hidden" />
                                                    <input id="txtServidoTarde" type="hidden" />
                                                </div>

                                                <div class="span2">
                                                    <span class="requerido">*</span>
                                                    <asp:Label ID="lblEstadoComedero" runat="server" meta:resourcekey="lblEtiquetaEstadoComederoResource1"></asp:Label>
                                                    <asp:DropDownList ID="cmbEstadoComedero" CssClass="span12" runat="server"/>
                                                </div>

                                                <div class="span3">
                                                    <span class="requerido">*</span>
                                                    <input id="txtKilogramosOculto" type="hidden" />
                                                    <input id="txtRepartoID" type="hidden" />
                                                    <asp:Label ID="lblKilogramos" runat="server" meta:resourcekey="lblEtiquetaKilogramosResource1"></asp:Label>
                                                    <input type="tel" id="txtKilogramos" class="span8 textoDerecha" oninput="maxLengthCheck(this)" maxlength="10"  />
                                                    <%--<asp:TextBox ID="txtKilogramos" runat="server" AutoPostBack="False" CssClass="span12" MaxLength="10"></asp:TextBox>--%>
                                                </div>
                                            
                                                <div class="space10"></div>

                                                <div class="span6">
                                                    <asp:Label ID="lblObservaciones" runat="server" meta:resourcekey="lblEtiquetaObservacionesResource1"></asp:Label>
                                                    <textarea id="txtObservaciones" class="span12" rows="2" maxlength="255"></textarea>
                                                </div>
                                            </div>
                                            <div class="span2 no-left-margin">
                                                <div class="space20"></div>
                                                <a id="btnLimpiar" class="btn SuKarne span8">Limpiar</a>

                                                <div class="space10"></div>
                                                <a id="btnAgregar" class="btn SuKarne span8">Agregar</a>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <!--<div class="span12 no-left-margin">-->
                                    <div >
                                        <table class="table table-striped table-advance table-hover">
                                            <thead>
                                                <tr>
                                                    <th class="alineacionCentro span1" scope="col"><asp:Label ID="lblHCorral" runat="server" meta:resourcekey="HeaderCorral"></asp:Label></th>
                                                    <th class="alineacionCentro span1" scope="col"><asp:Label ID="lblHLote" runat="server" meta:resourcekey="HeaderLote"></asp:Label></th>
                                                    <th class="alineacionCentro span1" scope="col"><asp:Label ID="lblHCabezas" runat="server" meta:resourcekey="HeaderCabezas"></asp:Label></th>
                                                    <th class="alineacionCentro span2" scope="col"><asp:Label ID="lblHTipoProceso" runat="server" meta:resourcekey="HeaderTipoProceso"></asp:Label></th>
                                                    <th style="display:none;" class="alineacionCentro" scope="col"><asp:Label ID="lblFormulaManianaOculto" runat="server" meta:resourcekey="HeaderFormulaManiana"></asp:Label></th>
                                                    <th class="alineacionCentro span2" scope="col"><asp:Label ID="lblHFormulaManiana" runat="server" meta:resourcekey="HeaderFormulaManiana"></asp:Label></th>
                                                    <th style="display:none;" class="alineacionCentro" scope="col"><asp:Label ID="lblFormulaTardeOculto" runat="server" meta:resourcekey="HeaderFormulaTarde"></asp:Label></th>
                                                    <th class="alineacionCentro span2" scope="col"><asp:Label ID="lblHFormulaTarde" runat="server" meta:resourcekey="HeaderFormulaTarde"></asp:Label></th>
                                                    <th style="display:none;" class="alineacionCentro" scope="col"><asp:Label ID="lblEstadoComederoOculto" runat="server" meta:resourcekey="HeaderEstadoComedero"></asp:Label></th>
                                                    <th class="alineacionCentro span2" scope="col"><asp:Label ID="lblHEstadoComedero" runat="server" meta:resourcekey="HeaderEstadoComedero"></asp:Label></th>
                                                    <th  style="display:none;" class="alineacionCentro" scope="col"><asp:Label ID="lblHKilogramosOculto" runat="server" meta:resourcekey="HeaderKilogramos"></asp:Label></th>
                                                    <th class="alineacionCentro span1" scope="col"><asp:Label ID="lblHKilogramos" runat="server" meta:resourcekey="HeaderKilogramos"></asp:Label></th>
                                                    <th class="alineacionCentro span2" scope="col"><asp:Label ID="lblHObservaciones" runat="server" meta:resourcekey="HeaderObservaciones"></asp:Label></th>
                                                    <th class="alineacionCentro span1" scope="col"><asp:Label ID="lblHOpcion" runat="server" meta:resourcekey="HeaderOpcion"></asp:Label></th>
                                                </tr>
                                            </thead>
                                        </table>
                                    <!--</div>-->
                                    <div style="height:170px;overflow-y:auto;overflow-x:auto;">
                                        <table id="tbRegistros" class="table table-striped table-advance table-hover">
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="space10"></div>
                                <div class="span12 no-left-margin">
                                    <div class="span4 pull-right">
                                        <div style="text-align:right;">
                                            <a id="btnGuardar" class="btn SuKarne">Guardar</a>
                                            <a id="btnCancelar" href="#dlgCancelar" class="btn SuKarne" data-toggle="modal">Cancelar</a>
                                        </div>
                                    </div>
                                </div>

                                <!--Dialogo de Cancelacion -->
                                <div id="dlgCancelar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
								    <div class="modal-body">
									    <asp:Label ID="lblMensajeDialogo" runat="server" meta:resourcekey="msgCancelar"></asp:Label>
								    </div>
								    <div class="modal-footer">
									    <asp:Button runat="server" ID="btnDialogoSi" CssClass="btn SuKarne" meta:resourcekey="btnDialogoSi" data-dismiss="modal"/>
                                        <asp:Button runat="server" ID="btnDialogoNo" CssClass="btn SuKarne" meta:resourcekey="btnDialogoNo" data-dismiss="modal"/>
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
